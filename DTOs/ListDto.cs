using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.DTOs
{
    public class ListDto<T>
    {
        public List<T> ListDtos { get; set; }
        public int TotalCount { get; set; }
    }
}
