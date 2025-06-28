using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Create;
using Library.Infraestructure.Persistence.DTOs.Auth.Roles.Update;
using Library.Infraestructure.Persistence.DTOs.General.Bank.Create;
using Library.Infraestructure.Persistence.DTOs.General.Bank.Read;
using Library.Infraestructure.Persistence.DTOs.General.Bank.Update;
using Library.Infraestructure.Persistence.DTOs.General.City.Create;
using Library.Infraestructure.Persistence.DTOs.General.City.Read;
using Library.Infraestructure.Persistence.DTOs.General.City.Update;
using Library.Infraestructure.Persistence.DTOs.General.Country.Create;
using Library.Infraestructure.Persistence.DTOs.General.Country.Read;
using Library.Infraestructure.Persistence.DTOs.General.Country.Update;
using Library.Infraestructure.Persistence.DTOs.General.Region.Create;
using Library.Infraestructure.Persistence.DTOs.General.Region.Read;
using Library.Infraestructure.Persistence.DTOs.General.Region.Update;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Configuration.Automapper.General
{
    public class GeneralProfiles : AutoMapper.Profile
    {
        public GeneralProfiles() 
        {

            #region Bank

            //CreateMap<Bank, BankReadDto>();
            //CreateMap<Bank, BankReadFirstDto>()
            //    .ForMember(dest => dest.CreatedByNavigation, opt => opt.MapFrom(src => src.CreatedByNavigation))
            //    .ForMember(dest => dest.ModifiedByNavigation, opt => opt.MapFrom(src => src.ModifiedByNavigation));

            //CreateMap<BankCreateDto, Bank>()
            //    .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
            //    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            //CreateMap<BankUpdateDto, Bank>()
            //    .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => DateTime.Now));

            #endregion

            #region Country

            CreateMap<GeneralCountry, CountryReadDto>();
            CreateMap<GeneralCountry, CountryReadFirstDto>()
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.MapFrom(src => src.CreatedByNavigation))
                .ForMember(dest => dest.ModifiedByNavigation, opt => opt.MapFrom(src => src.ModifiedByNavigation));

            CreateMap<CountryCreateDto, GeneralCountry>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<CountryUpdateDto, GeneralCountry>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => DateTime.Now));

            #endregion

            #region Region


            CreateMap<GeneralRegion, RegionReadDto>();
            CreateMap<GeneralRegion, RegionReadFirstDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.MapFrom(src => src.CreatedByNavigation))
                .ForMember(dest => dest.ModifiedByNavigation, opt => opt.MapFrom(src => src.ModifiedByNavigation));

            CreateMap<RegionCreateDto, GeneralRegion>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<RegionUpdateDto, GeneralRegion>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => DateTime.Now));

            #endregion

            #region City


            CreateMap<GeneralCity, CityReadDto>();
            CreateMap<GeneralCity, CityReadFirstDto>()
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForPath(dest => dest.Region.Country, opt => opt.MapFrom(src => src.Region.Country))
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.MapFrom(src => src.CreatedByNavigation))
                .ForMember(dest => dest.ModifiedByNavigation, opt => opt.MapFrom(src => src.ModifiedByNavigation));

            CreateMap<CityCreateDto, GeneralCity>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<CityUpdateDto, GeneralCity>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(_ => DateTime.Now));

            #endregion

        }

    }
}
