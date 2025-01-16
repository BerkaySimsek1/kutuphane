namespace KutuphaneTakip.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime LoanDate { get; set; } = DateTime.UtcNow; // Ödünç verme tarihi
        public DateTime? ReturnDate { get; set; } // İade tarihi (nullable)
    }
}