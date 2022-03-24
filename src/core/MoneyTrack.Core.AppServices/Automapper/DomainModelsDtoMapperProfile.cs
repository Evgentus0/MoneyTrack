using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models;

namespace MoneyTrack.Core.AppServices.Automapper
{
    public class DomainModelsDtoMapperProfile: Profile
    {
        public DomainModelsDtoMapperProfile()
        {
            CreateMap<TransactionDto, Transaction>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.Category.Id))
                .ForMember(x => x.AccountId, opt => opt.MapFrom(x => x.Account.Id))
                .ForMember(x => x.Category, opt => opt.Ignore())
                .ForMember(x => x.Account, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<AccountDto, Account>().ReverseMap();

            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
