using LibraryApi.Models;

namespace LibraryApi.Repositories
{
    public interface IBookRepository
    {
        // Sayfalama (Pagination) parametrelerini buraya ekliyoruz
        Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber, int pageSize);
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> SearchByAuthorAsync(string authorName);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<bool> BookExistsAsync(int id);
    }
}