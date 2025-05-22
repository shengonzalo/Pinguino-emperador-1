using FluentValidation;
using OGA.AAS.BackEnd.Application.Features.Token.Commands;
using OGA.Core.BackEnd.Application.Messages;

namespace OGA.AAS.BackEnd.Application.Features.Token
{
    public class TokenValidator : AbstractValidator<AddTokenCommand>
    {
        public TokenValidator()
        {
            RuleFor(x => x.TokenModel)
                .NotNull().WithMessage(ValidatorMessages.NullModel);

            RuleFor(x => x.TokenModel.Email)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Email}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Email}"))
                .MaximumLength(256).WithMessage(string.Format(ValidatorMessages.MaxLengthProperty, "{Email}", "256"))
                .EmailAddress().WithMessage(string.Format(ValidatorMessages.FormatProperty, "{Email}"));

            RuleFor(x => x.TokenModel.AccessToken)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{AccessToken}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{AccessToken}"));

            RuleFor(x => x.TokenModel.ExpiredDate)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{ExpiredDate}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{ExpiredDate}"));
        }
    }
}
