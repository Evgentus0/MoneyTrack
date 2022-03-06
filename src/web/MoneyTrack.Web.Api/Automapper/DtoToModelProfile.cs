using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Web.Api.Models.Entities;

namespace MoneyTrack.Web.Api.Automapper
{
    public class DtoToModelProfile: Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<AccountModel, AccountDto>().ReverseMap();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
            CreateMap<TransactionModel, TransactionDto>().ReverseMap();
            CreateMap<UserModel, UserDto>().ReverseMap();
        }
    }
}
