using BitcoinLib.Services.Coins.Base;
using BitcoinLib.Services.Coins.Cryptocoin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartAgri.Business.Abstract;
using SmartAgri.Business.Concrete;
using SmartAgri.DataAccess.Abstract;
using SmartAgri.DataAccess.Concrete.EntityFramework;
using SmartAgri.Entities.Enums;
using SmartAgri.ServiceAPI.Abstract;
using SmartAgri.ServiceAPI.Concrete;
using SmartAgri.ServiceAPI.Concrete.PricePredictionService;
using SmartAgri.WebUI.JwtFeatures;
using SmartAgri.WebUI.Mailing;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var constr = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContextFactory<SmartAgriContext>(option =>
{
    option.UseNpgsql(constr);
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
});

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => 
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});


builder.Services.AddSingleton<ITopicDal, EfTopicDal>();
builder.Services.AddSingleton<IReplyDal, EfReplyDal>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();
builder.Services.AddSingleton<IProductDal, EfProductDal>();
builder.Services.AddSingleton<IAdvertBuyDal, EfAdvertBuyDal>();
builder.Services.AddSingleton<IAdvertSellDal, EfAdvertSellDal>();
builder.Services.AddSingleton<ITransactionDal, EfTransactionDal>();
builder.Services.AddSingleton<IGuestMessageDal, EfGuestMessageDal>();

builder.Services.AddSingleton<IFormService, FormService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IBazaarService, BazaarSevice>();
builder.Services.AddSingleton<IUserManagementService, UserManagementService>();

builder.Services.AddSingleton<IPredictPrice, PredictPrice>();
builder.Services.AddSingleton<IAgriCoinApi, AgriCoinApi>();


var AgriCoinSettings = builder.Configuration.GetSection("AgriCoinSettings");

builder.Services.AddSingleton<ICoinService>(x =>
{
    return new CryptocoinService(
        AgriCoinSettings["daemonUrl"], 
        AgriCoinSettings["rpcUsername"], 
        AgriCoinSettings["rpcPassword"], 
        AgriCoinSettings["walletPassword"],
        15);
});

builder.Services.AddScoped<JwtHandler>();

var emailConfig = builder.Configuration
	.GetSection("EmailConfiguration")
	.Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value!))
    };
});

builder.Services.AddAuthorization(x => x.AddPolicy("AdminPolicy", policy =>
{
    //policy.RequireClaim(UserClaimEnum.role_id.ToString(), ((int)RoleEnum.Admin).ToString());
    policy.RequireClaim(UserClaimEnum.role_id.ToString(), "1");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SmartAgriContext>();
    context.Database.Migrate();
}

app.Run();
