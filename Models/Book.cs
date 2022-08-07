using BookApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Models
{
    public class Book: BaseEntity
    {
        public string Name { get; set; }
        public short page { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
