using System;
using AutoMapper;
using tower_api.Models.Requests;
using tower_api.Repositories.Models;

namespace credit_work_app.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Card, tower_api.Business.Models.Card>();
            CreateMap<ApiCardCreateRequest, Card>();
        }
    }
}

