namespace KutuphaneTakip.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();// Kategoriye ait birden fazla kitap olabilir
    }
}