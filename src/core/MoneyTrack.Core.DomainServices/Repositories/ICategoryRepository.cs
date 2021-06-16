﻿using MoneyTrack.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Core.DomainServices.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}
