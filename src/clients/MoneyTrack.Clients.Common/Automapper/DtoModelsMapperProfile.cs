using AutoMapper;
using MoneyTrack.Clients.Common.Models;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;

namespace MoneyTrack.Clients.Common.Automapper
{
    public class DtoModelsMapperProfile: Profile
    {
        public DtoModelsMapperProfile()
        {
            CreateMap<TransactionDto, TransactionModel>()
                .ForMember(dest => dest.AddedDttm, opt => opt.MapFrom(src => src.AddedDttm))
                .ReverseMap();

            CreateMap<CategoryDto, CategoryModel>().ReverseMap();

            CreateMap<AccountDto, AccountModel>().ReverseMap();

            CreateMap<PagingModel, Paging>().ReverseMap();

            CreateMap<FilterModel, Filter>().ReverseMap();
        }
    }
}
