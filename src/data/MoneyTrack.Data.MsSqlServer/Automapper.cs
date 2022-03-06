using AutoMapper;
using MoneyTrack.Data.MsSqlServer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer
{
    public class DbToDomainProfile: Profile
    {
        public DbToDomainProfile()
        {
            CreateMap<Account, Core.Models.Account>().ReverseMap();
            CreateMap<Category, Core.Models.Category>().ReverseMap();
            CreateMap<Transaction, Core.Models.Transaction>().ReverseMap();
            CreateMap<ApplicationUser, Core.Models.User>().ReverseMap();
        }
    }
}
