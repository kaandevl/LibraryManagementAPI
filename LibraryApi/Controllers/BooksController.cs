using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        // Artık DbContext yerine IBookRepository kullanıyoruz
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        // 1. Tüm Kitapları Getir (GET) - SAYFALAMA KONTROLLERİ EKLENDİ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var books = await _repository.GetAllBooksAsync(pageNumber, pageSize);
            return Ok(books);
        }

        // 2. ID'ye Göre Tek Bir Kitap Getir (GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);

            if (book == null)
                return NotFound("Kitap bulunamadı.");

            return Ok(book);
        }

        // 3. Yazar Adına Göre Arama (GET)
        [HttpGet("search/{authorName}")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByAuthor(string authorName)
        {
            var books = await _repository.SearchByAuthorAsync(authorName);

            if (!books.Any())
                return NotFound("Bu yazara ait kitap bulunamadı.");

            return Ok(books);
        }

        // 4. Yeni Kitap Ekle (POST) - KİLİTLİ
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var createdBook = await _repository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        // 5. Kitap Güncelle (PUT) - KİLİTLİ
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
                return BadRequest("ID uyuşmazlığı.");

            var exists = await _repository.BookExistsAsync(id);
            if (!exists)
                return NotFound("Kitap bulunamadı.");

            await _repository.UpdateBookAsync(updatedBook);

            return NoContent();
        }

        // 6. Kitap Sil (DELETE) - KİLİTLİ
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var exists = await _repository.BookExistsAsync(id);
            if (!exists)
                return NotFound("Silinecek kitap bulunamadı.");

            await _repository.DeleteBookAsync(id);

            return NoContent();
        }
    }
}