using AutoMapper;
using DataModel = DotNetApi.Core.Data.Models;
using DotNetApi.Domain.DTOs;

namespace DotNetApi.Core.Data.Mappers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        this.CreateMap<DataModel.Customer, Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ReverseMap();
    }
}


