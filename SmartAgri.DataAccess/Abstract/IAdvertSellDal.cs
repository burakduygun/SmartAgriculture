﻿using SmartAgri.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAgri.DataAccess.Abstract
{
    public interface IAdvertSellDal : IEntityRepository<AdvertSell>
    {
        AdvertSell GetWithUser(Expression<Func<AdvertSell, bool>>? filter);
    }
}
