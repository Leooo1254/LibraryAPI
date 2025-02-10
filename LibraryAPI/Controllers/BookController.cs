using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Models;
using RestFull.Repositories.Interfaces;

namespace RestFull.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface bookInterface;

        public BookController(IBookInterface bookInterface)
        {
            this.bookInterface = bookInterface;
        }

        [HttpGet("Get")]
        public async Task<ActionResult> GetAllBooks()
        {
            var books = await bookInterface.GetAllBooks();

            if (books != null)
            {
                return Ok(books);
            }
            return BadRequest();
        }

        [HttpPost("Post")]
        public async Task<ActionResult<string>> AddNewBook(string id, Book book)
        {
            var result = await bookInterface.AddNewBook(id, book);
            if (result.Contains("sikeres"))
            {
                return Ok(result);
            } 
            return BadRequest();
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var result = await bookInterface.Delete(id);
            if (result.Contains("sikeres"))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
