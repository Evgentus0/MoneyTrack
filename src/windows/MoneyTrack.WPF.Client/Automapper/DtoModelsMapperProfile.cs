using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.WPF.Client.Models;

namespace MoneyTrack.WPF.Client.Automapper
{
    public class DtoModelsMapperProfile: Profile
    {
        public DtoModelsMapperProfile()
        {
            CreateMap<TransactionDto, TransactionModel>().ReverseMap();

            CreateMap<CategoryDto, CategoryModel>().ReverseMap();

            CreateMap<AccountDto, AccountModel>().ReverseMap();
        }
    }
}
