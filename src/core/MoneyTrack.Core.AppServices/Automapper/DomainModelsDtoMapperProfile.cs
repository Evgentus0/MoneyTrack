using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models;

namespace MoneyTrack.Core.AppServices.Automapper
{
    public class DomainModelsDtoMapperProfile: Profile
    {
        public DomainModelsDtoMapperProfile()
        {
            CreateMap<TransactionDto, Transaction>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<AccountDto, Account>().ReverseMap();
        }
    }
}
