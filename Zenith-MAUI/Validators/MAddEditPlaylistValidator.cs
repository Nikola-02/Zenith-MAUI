using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.Pages;

namespace Zenith_MAUI.Validators
{
    public class MAddEditPlaylistValidator : AbstractValidator<AddPlaylistPage>
    {
        public MAddEditPlaylistValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name.Value)
                .NotEmpty()
                .WithMessage("Naziv je obavezan.");
        }
    }
}
