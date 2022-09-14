using AutoMapper;
using DutchTreat_th.Data.Entities;
using DutchTreat_th.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.Data
{
    public class DuctchMappingProfile : Profile
    {
        public DuctchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
              .ForMember(o => o.Id, ex => ex.MapFrom(o => o.Id))
               .ReverseMap()
               .ForMember(o => o.Product, ex => ex.Ignore());
        }
    }
}
