using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.RecurringPayment;

namespace WebWallet.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<bool> Create(RecurringPaymentVM paymentVM, string username);

        Task<RecurringPaymentVM> GetById(string paymentId);

        Task<IEnumerable<RecurringPaymentVM>> GetAll(string username);

        Task<bool> Update(RecurringPaymentVM paymentVM);

        Task<bool> Delete(string paymentId);
    }
}