using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.DTOs.Book
{
    public class BookPostDto
    {
        public string Name { get; set; }
        public short page { get; set; }
        public int CategoryId { get; set; }
    }
    public class BookPostDtoValidation : AbstractValidator<BookPostDto>
    {
        public BookPostDtoValidation()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("please enter value").MaximumLength(30).WithMessage("max length 30");
            RuleFor(p => p.page).NotNull().WithMessage("please enter value").GreaterThanOrEqualTo((short)10).LessThanOrEqualTo((short)1000);
            RuleFor(p => p.CategoryId).NotEmpty();

        }
    }
}
