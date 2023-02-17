using BLL.Dtos;
using FluentValidation;

namespace BLL.Validators
{
    public class ReviewDtoValidator : AbstractValidator<ReviewDto>
    {
        public ReviewDtoValidator()
        {
            RuleFor(b => b.Message).NotEmpty().WithMessage("Review message cannot be blank");
            RuleFor(b => b.Reviewer).NotEmpty().WithMessage("Review cannot be anonymous");
        }
    }
}
