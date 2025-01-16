using Microsoft.AspNetCore.Mvc;
using KutuphaneTakip.Services.Interfaces;
using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using KutuphaneTakip.Data;
using KutuphaneTakip.DTOs;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneTakip.Controllers
{
    
    
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        
        private readonly IBookService _bookService;

         public BooksController(LibraryDbContext context, IBookService bookService)
    {
        _context = context;
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                AuthorId = b.AuthorId,
                AuthorName = b.Author != null ? b.Author.Name : null,
                CategoryId = b.CategoryId,
                CategoryName = b.Category != null ? b.Category.Name : null
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound("Book not found.");
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // AuthorId ve CategoryId kontrol ediliyor
        if (book.AuthorId == 0 || book.CategoryId == 0)
        {
            return BadRequest("AuthorId and CategoryId are required and must be valid.");
        }

        // Metadata alanını kontrol ediyoruz, boşsa varsayılan bir değer atanıyor
        if (string.IsNullOrWhiteSpace(book.Metadata))
        {
            book.Metadata = "{}"; // Varsayılan olarak boş bir JSON atanıyor
        }

        // Loans koleksiyonu kontrol ediliyor, null ise boş bir liste atanıyor
        book.Loans = book.Loans ?? new List<Loan>();

        // Yeni kitap ekleniyor
        var newBook = await _bookService.AddBook(book);
        return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        var result = await _bookService.UpdateBook(id, book);
        if (!result)
        {
            return NotFound("Book not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBook(id);
        if (!result)
        {
            return NotFound("Book not found.");
        }

        return NoContent();
    }
}
}