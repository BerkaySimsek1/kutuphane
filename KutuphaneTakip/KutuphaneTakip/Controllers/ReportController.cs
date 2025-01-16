using KutuphaneTakip.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // En çok ödünç alınan kitapları getirme
        [HttpGet("most-loaned-books")]
        public async Task<IActionResult> GetMostLoanedBooks(int topCount = 5)
        {
            var books = await _reportService.GetMostLoanedBooks(topCount);
            return Ok(books);
        }

        // Gecikmiş iade işlemlerini getirme
        [HttpGet("overdue-loans")]
        public async Task<IActionResult> GetOverdueLoans()
        {
            var overdueLoans = await _reportService.GetOverdueLoans();
            return Ok(overdueLoans);
        }
    }
}