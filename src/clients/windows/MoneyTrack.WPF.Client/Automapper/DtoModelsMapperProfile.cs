using AutoMapper;
using MoneyTrack.Core.AppServices.DTOs;
using MoneyTrack.Core.Models.Operational;
using MoneyTrack.WPF.Client.Models;

namespace MoneyTrack.WPF.Client.Automapper
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
