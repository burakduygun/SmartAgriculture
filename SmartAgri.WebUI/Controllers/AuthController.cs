﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using SmartAgri.Business.Abstract;
using SmartAgri.Entities.Concrete;
using SmartAgri.Entities.Enums;
using SmartAgri.ServiceAPI.Abstract;
using SmartAgri.WebUI.DTOs;
using SmartAgri.WebUI.JwtFeatures;
using SmartAgri.WebUI.Mailing;
using SmartAgri.WebUI.Mailing.Messages;
using System.IdentityModel.Tokens.Jwt;

namespace SmartAgri.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly JwtHandler _jwtHandler;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IAgriCoinApi _agricoinApi;

        public AuthController(JwtHandler jwtHandler, IUserService userService, IEmailSender emailSender, IAgriCoinApi agricoinApi)
        {
            _jwtHandler = jwtHandler;
            _userService = userService;
            _emailSender = emailSender;
            _agricoinApi = agricoinApi;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = _userService.GetUserByEmail(userForAuthentication.Email);
            if (user == null || !_userService.CheckPassword(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, RoleId = user.RoleId});
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserForRegistrationDto userForRegistration)
        {
            //geçici olarak eklendi
            if (userForRegistration.Email == null || userForRegistration.Name == null || userForRegistration.Surname == null || userForRegistration.Password == null)
            {
                return BadRequest(new RegistrationResponseDto { IsSuccessfulRegistration = false });
            }

            var user = new User
            {
                Name = userForRegistration.Name,
                Surname = userForRegistration.Surname,
                Email = userForRegistration.Email,
                Password = userForRegistration.Password,
                RoleId = (int)UserRoleEnum.User,
                CoinAccountId = Guid.NewGuid(),
                LockedBalance = 0,
            };

            user.CoinAddress = _agricoinApi.CreateAccount(user.CoinAccountId);

            if (_userService.CreateUser(user))
            {
                return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true });
            }

            return BadRequest(new RegistrationResponseDto { IsSuccessfulRegistration = false });
        }

        [HttpPost]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userService.GetUserByEmail(forgotPasswordDto.Email!);
            if (user == null)
                return BadRequest();

            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI!, param);

            var message = new ForgotPasswordMessage(user.Name, new List<string> { user.Email }, callback);
            _emailSender.SendEmail(message);
            return Ok();
        }

        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userService.CheckUser(resetPasswordDto.Email!);
            if (!user)
                return BadRequest();
            if (!_userService.ChangePassword(resetPasswordDto.Email, resetPasswordDto.Password))
                return BadRequest();

            return Ok();
        }
    }
}
