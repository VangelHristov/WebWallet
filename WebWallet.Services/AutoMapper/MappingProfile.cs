﻿using AutoMapper;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.User;

namespace WebWallet.Services.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationVM, User>()
                .ReverseMap();
        }
    }
}