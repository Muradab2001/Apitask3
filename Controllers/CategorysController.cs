using AutoMapper;
using BookApi.DAL;
using BookApi.DTOs;
using BookApi.DTOs.Book;
using BookApi.DTOs.Category;
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
    public class CategorysController : ControllerBase
    {
        private readonly APIDbContext _context;
        private IMapper _mapper;

        public CategorysController(APIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest();
            Category category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category is null) return NotFound();
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
            return Ok(dto);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CategoryPostDto categoryPostDto)
        {
            if (categoryPostDto is null) return NotFound();
            if (_context.Categories.Any(c => c.Name == categoryPostDto.Name)) return BadRequest();
            Category category = new Category
            {
       Name=categoryPostDto.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(201, new { id = category.Id, processor = categoryPostDto });
        }
        [HttpPut("updata/{id}")]
        public async  Task<IActionResult> Updata(int id, CategoryPostDto categoryPostDto)
        {
            if (id == 0) return BadRequest();
            if (_context.Categories.Any(c => c.Name == categoryPostDto.Name)) BadRequest();
            Category existed = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
             _context.Entry(existed).CurrentValues.SetValues(categoryPostDto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(int page = 1, string serarch = null)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(serarch))
            {
                query = query.Where(p => p.Name.Contains(serarch));
            }
            ListDto<CategoryListItemDto> dto = new ListDto<CategoryListItemDto>
            {
                ListDtos = _mapper.Map<List<CategoryListItemDto>>(query),
                TotalCount = query.Count()
                
            };
            
            return Ok(dto);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Category existed = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (existed is null) return NotFound();
            _context.Remove(existed);
            _context.SaveChanges();
            return NoContent();
        }
    }
 
}
