using Microsoft.AspNetCore.Mvc;
using KutuphaneTakip.Services.Interfaces;
using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _loanService.GetAllLoans();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _loanService.GetLoanById(id);
            if (loan == null)
            {
                return NotFound("Loan not found.");
            }
            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> AddLoan(Loan loan)
        {
            var newLoan = await _loanService.AddLoan(loan);
            return CreatedAtAction(nameof(GetLoan), new { id = newLoan.Id }, newLoan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, Loan loan)
        {
            var result = await _loanService.UpdateLoan(id, loan);
            if (!result)
            {
                return NotFound("Loan not found.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var result = await _loanService.DeleteLoan(id);
            if (!result)
            {
                return NotFound("Loan not found.");
            }
            return NoContent();
        }
    }
}