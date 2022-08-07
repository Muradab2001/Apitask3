using AutoMapper;
using BookApi.DAL;
using BookApi.DTOs;
using BookApi.DTOs.Book;
using BookApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly APIDbContext _context;
        private IMapper _mapper;

        public BooksController(APIDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest();
            Book book = _context.Books.Include(b => b.Category).ThenInclude(c => c.Books).FirstOrDefault(b => b.Id == id);
            if (book is null) return NotFound();

            BookGetDto dto = _mapper.Map<BookGetDto>(book);
            if (dto is null) return NotFound();
            return Ok(dto);
        }
        [HttpPut("updata/{id}")]
        public IActionResult Updata(int id, BookPostDto bookPostDto)
        {
            if (id == 0) return BadRequest();
            Book existed = _context.Books.FirstOrDefault(p => p.Id == id);
            if (existed is null) return NotFound();
            _context.Entry(existed).CurrentValues.SetValues(bookPostDto);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(int page = 1, string serarch = null)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(serarch))
            {
                query = query.Where(p => p.Name.Contains(serarch));
            }
            ListDto<BookListItemDto> dto = new ListDto<BookListItemDto>
            {
                ListDtos = _mapper.Map<List<BookListItemDto>>(query),
                TotalCount = query.Count()
            };
            return Ok(dto);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(BookPostDto bookPostDto)
        {
            if (bookPostDto is null) return NotFound();
            Book book = new Book
            {
              Name=bookPostDto.Name,
              page=bookPostDto.page,
              CategoryId=bookPostDto.CategoryId
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(201, new { id= book.Id,Book= bookPostDto });
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Book existed = _context.Books.FirstOrDefault(p => p.Id == id);
            if (existed is null) return NotFound();
            _context.Remove(existed);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
