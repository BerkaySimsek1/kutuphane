using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllLoans();
        Task<Loan> GetLoanById(int id);
        Task<Loan> AddLoan(Loan loan);
        Task<bool> UpdateLoan(int id, Loan loan);
        Task<bool> DeleteLoan(int id);
    }
}