using KutuphaneTakip.Repositories.Interfaces;
using KutuphaneTakip.Models;
using KutuphaneTakip.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Loan>> GetAllLoans()
        {
            return await _unitOfWork.Loans.GetAll();
        }

        public async Task<Loan> GetLoanById(int id)
        {
            return await _unitOfWork.Loans.GetById(id);
        }

        public async Task<Loan> AddLoan(Loan loan)
        {
            await _unitOfWork.Loans.Add(loan);
            await _unitOfWork.Complete();
            return loan;
        }

        public async Task<bool> UpdateLoan(int id, Loan loan)
        {
            var existingLoan = await _unitOfWork.Loans.GetById(id);
            if (existingLoan == null)
            {
                return false;
            }

            existingLoan.MemberId = loan.MemberId;
            existingLoan.BookId = loan.BookId;
            existingLoan.LoanDate = loan.LoanDate;
            existingLoan.ReturnDate = loan.ReturnDate;

            await _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> DeleteLoan(int id)
        {
            var loan = await _unitOfWork.Loans.GetById(id);
            if (loan == null)
            {
                return false;
            }

            await _unitOfWork.Loans.Delete(loan);
            await _unitOfWork.Complete();
            return true;
        }
    }
}