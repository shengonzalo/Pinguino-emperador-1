using FluentValidation;
using OGA.AAS.BackEnd.Application.Features.User.Commands;
using OGA.Core.BackEnd.Application.Messages;

namespace OGA.AAS.BackEnd.Application.Features.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Model)
                .NotNull().WithMessage(ValidatorMessages.NullModel);

            RuleFor(x => x.Model.IdentifierTypeId)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{IdentifierType}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{IdentifierType}"));
            RuleFor(x => x.Model.Identifier)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Identifier}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Identifier}"));
            RuleFor(x => x.Model.Name)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Name}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Name}"));
            RuleFor(x => x.Model.PasswordHash)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{PasswordHash}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{PasswordHash}"));
            RuleFor(x => x.Model.Email)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Email}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Email}"))
                .MaximumLength(256).WithMessage(string.Format(ValidatorMessages.MaxLengthProperty, "{Email}", "256"))
                .EmailAddress().WithMessage(string.Format(ValidatorMessages.FormatProperty, "{Email}"));
        }
    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Model)
                .NotNull().WithMessage(ValidatorMessages.NullModel);

            RuleFor(x => x.Model.IdentifierTypeId)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{IdentifierType}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{IdentifierType}"));
            RuleFor(x => x.Model.Identifier)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Identifier}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Identifier}"));
            RuleFor(x => x.Model.Name)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Name}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Name}"));
            RuleFor(x => x.Model.Email)
                .NotEmpty().WithMessage(string.Format(ValidatorMessages.EmptyProperty, "{Email}"))
                .NotNull().WithMessage(string.Format(ValidatorMessages.NullProperty, "{Email}"))
                .MaximumLength(256).WithMessage(string.Format(ValidatorMessages.MaxLengthProperty, "{Email}", "256"))
                .EmailAddress().WithMessage(string.Format(ValidatorMessages.FormatProperty, "{Email}"));
        }
    }
}
