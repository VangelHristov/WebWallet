using AutoMapper;
using System.Collections.Generic;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.Account;
using WebWallet.ViewModels.Budget;
using WebWallet.ViewModels.Transaction;
using WebWallet.ViewModels.User;

namespace WebWallet.Services.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationVM, User>()
                .ReverseMap();

            CreateMap<AccountVM, Account>()
                .ReverseMap();

            CreateMap<TransactionVM, Transaction>()
                .ReverseMap();

            CreateMap<Budget, BudgetVM>()
                .ForMember(dest => dest.Available, opt => opt.MapFrom(dest => dest.GetAvailable()));

            CreateMap<BudgetVM, Budget>();
        }
    }
}