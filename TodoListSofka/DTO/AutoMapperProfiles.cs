﻿using AutoMapper;
using TodoListSofka.Model;

namespace TodoListSofka.DTO
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            CreateMap<Calendar, CalendarDTO>().ReverseMap();
        }
    }
}