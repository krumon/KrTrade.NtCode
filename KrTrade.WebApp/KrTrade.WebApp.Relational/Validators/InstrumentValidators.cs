using FluentValidation;
using KrTrade.WebApp.Core.DTOs;

namespace KrTrade.WebApp.Relational.Validators
{
    public class InstrumentValidator : AbstractValidator<InstrumentDto>
    {
        public InstrumentValidator()
        {
            RuleFor(instrument => instrument.Description)
                .NotNull()
                .WithMessage("La descripcion no puede ser nula");

            RuleFor(instrument => instrument.Description)
                    .Length(10, 500)
                    .WithMessage("La longitud del la descripcion debe estar entre 10 y 500 caracteres");

            RuleFor(instrument => instrument.Name)
                .NotNull()
                .WithMessage("El nombre no puede ser nulo");
        }
    }
}
