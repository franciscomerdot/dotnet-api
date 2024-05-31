using AutoMapper;
using DataModel = DotNetApi.Core.Data.Models;
using DotNetApi.Domain.DTOs;

namespace DotNetApi.Core.Data.Mappers;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        this.CreateMap<DataModel.Order, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.CompletedAt, opt => opt.MapFrom(src => src.CompletedAt))
            .ReverseMap();
    }
}
