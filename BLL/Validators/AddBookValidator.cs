using BLL.Dtos;
using FluentValidation;

namespace BLL.Validators
{
    public class AddBookValidator : AbstractValidator<AddBook>
    {
        public AddBookValidator()
        {
            RuleFor(b => b.Author).NotEmpty().WithMessage("Book must have author");
            RuleFor(b => b.Title).NotEmpty().WithMessage("Book must have title");
            RuleFor(b => b.Content).NotEmpty().WithMessage("Book must have content");
            RuleFor(b => b.Cover).NotEmpty().WithMessage("Book must have cover");
            RuleFor(b => b.Genre).NotEmpty().WithMessage("Book must have genre");
        }
    }
}
