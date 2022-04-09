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
            CreateMap<Account, Core.Models.Account>()
                .ForPath(x => x.User.Id, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Category, Core.Models.Category>()
                .ForPath(x => x.User.Id, opt => opt.MapFrom(x => x.UserId))
                .ReverseMap()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Transaction, Core.Models.Transaction>()
                .ForPath(x => x.Account.Id, opt => opt.MapFrom(x => x.AccountId))
                .ForPath(x => x.Category.Id, opt => opt.MapFrom(x => x.CategoryId))
                .ReverseMap()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
                .ForMember(x => x.AccountId, opt => opt.MapFrom(x => x.Account.Id))
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.Account, opt => opt.Ignore());

            CreateMap<ApplicationUser, Core.Models.User>().ReverseMap();
        }
    }
}
