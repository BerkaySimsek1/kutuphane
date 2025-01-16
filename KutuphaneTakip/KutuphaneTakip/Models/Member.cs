namespace KutuphaneTakip.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;// Üyelik başlangıç tarihi

        public ICollection<Loan> Loans { get; set; } = new List<Loan>(); // Üyenin birden fazla ödünç işlemi olabilir
    }
}