namespace KutuphaneTakip.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }
        public Author? Author { get; set; } 

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string Metadata { get; set; } = "{}"; 

        public ICollection<Loan>? Loans { get; set; } = new List<Loan>(); 
    }

}