namespace KutuphaneTakip.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}
