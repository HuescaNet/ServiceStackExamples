using ServiceStack;
using ServiceStack.FluentValidation;

namespace Validacion
{
    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            // Comprueba que el nombre no esté vacío
            RuleFor(r => r.Nombre).NotEmpty();

            // Comprueba a través de servicio que el nombre no esté duplicado
            RuleFor(x => x.Nombre).Must((persona, nombre) => (bool)HostContext.ServiceController.Execute(new CompruebaNombreRequest { Nombre = persona.Nombre }))
                .WithMessage("No se debe repetir el nombre");
        }
    }
}