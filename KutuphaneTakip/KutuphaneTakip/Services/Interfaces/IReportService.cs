using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Book>> GetMostLoanedBooks(int topCount);
        Task<IEnumerable<Loan>> GetOverdueLoans();
    }
}