using AutoMapper;
using DataModel = DotNetApi.Core.Data.Models;
using DotNetApi.Domain.DTOs;

namespace DotNetApi.Core.Data.Mappers;

public class CustomerContactMapper : Profile
{
    public CustomerContactMapper()
    {
        this.CreateMap<DataModel.Contact, Contact>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId , opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ReverseMap();
    }
}
