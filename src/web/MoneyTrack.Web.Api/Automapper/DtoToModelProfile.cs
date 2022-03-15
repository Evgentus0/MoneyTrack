using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;
using MoneyTrack.Web.Api.Models.Entities;
using MoneyTrack.Web.Api.Models.Entities.Operational;

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

            CreateMap<FilterModel, Filter>().ReverseMap();
            CreateMap<PagingModel, Paging>().ReverseMap();
            CreateMap<SortingModel, Sorting>().ReverseMap();
            CreateMap<DbQueryModel, DbQueryRequest>().ReverseMap();
        }
    }
}
