﻿using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.User;

namespace WebWallet.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public IQueryable<User> GetAll()
        {
            return this._userRepository.GetAll();
        }

        public async Task<User> GetById(string id)
        {
            return await this._userRepository.GetById(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await this._userRepository.GetByUserName(username);
        }

        public async Task<string> GetEmailConfirmationToken(User user)
        {
            return await this._userRepository.GenerateEmailConfirmationToken(user);
        }

        public async Task Login(string username, string password, bool percist)
        {
            await this._userRepository.PasswordSignIn(username, password, percist);
        }

        public async Task Logout()
        {
            await this._userRepository.SignOut();
        }

        public async Task<User> Register(RegistrationVM registrationVM)
        {
            var user = this._mapper.Map<User>(registrationVM);
            return await this._userRepository.Create(user);
        }

        public async Task<bool> ConfirmEmail(User user, string token)
        {
            return await this._userRepository.ConfirmEmail(user, token);
        }
    }
}