using FluentValidation;
using Hexus.Daemon.Contracts.Requests;

namespace Hexus.Daemon.Validators;

public class SendInputValidator : AbstractValidator<SendInputRequest>
{
    public SendInputValidator()
    {
        RuleFor(r => r.Text).NotEmpty();
    }
}
