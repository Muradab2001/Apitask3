using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.DTOs.Book
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short page { get; set; }
        public int CategoryId { get; set; }
        public CategoryInGetDto Category { get; set; }
    }
    public class CategoryInGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
    }
}
