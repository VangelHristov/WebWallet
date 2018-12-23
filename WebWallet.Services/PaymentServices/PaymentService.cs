using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.RecurringPayment;

namespace WebWallet.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<RecurringPayment> _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PaymentService(IRepository<RecurringPayment> paymentRepository, IMapper mapper, IUserService userService)
        {
            this._paymentRepository = paymentRepository;
            this._mapper = mapper;
            this._userService = userService;
        }

        public async Task<bool> Create(RecurringPaymentVM paymentVM, string username)
        {
            var user = await _userService.GetByUsername(username);
            paymentVM.UserId = user.Id;
            var payment = _mapper.Map<RecurringPayment>(paymentVM);
            return await _paymentRepository.Create(payment);
        }

        public Task<bool> Delete(string paymentId)
        {
            // TODO: delete transactions
            return _paymentRepository.Delete(paymentId);
        }

        public async Task<IEnumerable<RecurringPaymentVM>> GetAll(string username)
        {
            var user = await _userService.GetByUsername(username);
            return _paymentRepository
                .GetAll()
                .Where(x => x.UserId == user.Id)
                .Select(x => _mapper.Map<RecurringPaymentVM>(x))
                .AsEnumerable();
        }

        public async Task<RecurringPaymentVM> GetById(string paymentId)
        {
            var payment = await _paymentRepository.GetById(paymentId);
            var paymentVM = _mapper.Map<RecurringPaymentVM>(payment);
            return paymentVM;
        }

        public Task<bool> Update(RecurringPaymentVM paymentVM)
        {
            var payment = _mapper.Map<RecurringPayment>(paymentVM);
            return _paymentRepository.Update(payment);
        }
    }
}