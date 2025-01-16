using Microsoft.AspNetCore.Mvc;
using KutuphaneTakip.Services.Interfaces;
using KutuphaneTakip.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KutuphaneTakip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author)
        {
            var newAuthor = await _authorService.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            var result = await _authorService.UpdateAuthor(id, author);
            if (!result)
            {
                return NotFound("Author not found.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthor(id);
            if (!result)
            {
                return NotFound("Author not found.");
            }
            return NoContent();
        }
    }
}