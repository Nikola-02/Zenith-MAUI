using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.ViewModels;

namespace Zenith_MAUI.Validators
{
    public class MRegisterViewModelValidator : AbstractValidator<MRegisterViewModel>
    {
        public MRegisterViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email.Value).NotEmpty()
                                       .WithMessage("Email je obavezan.")
                                       .EmailAddress()
                                       .WithMessage("Email nije ispravnog formata.");

            RuleFor(x => x.Email.Value).NotEmpty()
                                       .WithMessage("Email je obavezan.")
                                       .EmailAddress()
                                       .WithMessage("Email nije ispravnog formata.");

            RuleFor(x => x.Password.Value).NotEmpty()
                                          .WithMessage("Lozinka je obavezna.");
        }
    }
}
