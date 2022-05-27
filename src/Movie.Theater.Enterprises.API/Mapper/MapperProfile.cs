using AutoMapper;
using Movie.Theater.Enterprises.Models.DTOs;
using Movie.Theater.Enterprises.Models.Entities;

namespace Movie.Theater.Enterprises.API.Mapper
{
    /// <summary>
    /// Mapper profile to create maps between models
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
