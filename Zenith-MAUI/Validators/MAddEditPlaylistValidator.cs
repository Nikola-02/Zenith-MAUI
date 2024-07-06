using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.Pages;

namespace Zenith_MAUI.Validators
{
    public class MAddPlaylistValidator : AbstractValidator<AddPlaylistPage>
    {
        public MAddPlaylistValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name.Value)
                .NotEmpty()
                .WithMessage("Naziv je obavezan.");
        }
    }

    public class MEditPlaylistValidator : AbstractValidator<EditPlaylistPage>
    {
        public MEditPlaylistValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name.Value)
                .NotEmpty()
                .WithMessage("Naziv je obavezan.");
        }
    }
}
