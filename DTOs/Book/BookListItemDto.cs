using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.DTOs.Book
{
    public class BookListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short page { get; set; }
        public CategoryInGetDto Category { get; set; }
    }
}
