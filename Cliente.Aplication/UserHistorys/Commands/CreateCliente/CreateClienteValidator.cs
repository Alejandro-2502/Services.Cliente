using Cliente.Aplication.Request;
using Cliente.Application.Helpers;
using Cliente.Application.Messages;
using FluentValidation;

namespace Cliente.Application.UserHistory.Comands.CreateCliente;

public class CreateClienteValidator : AbstractValidator<ClienteRequest>
{
    public CreateClienteValidator()
    {
        RuleFor(emp => emp.Nombre).NotNull().NotEmpty().WithMessage(MessagesValidationsCliente.ValidationsClienteNombreNotNull)
            .MaximumLength(15).WithMessage(MessagesValidationsCliente.ValidationsClienteNombreMax15Caracteres)
            .Must(CadenasHelper.ExisteCaracteresEspeciales).WithMessage(MessagesValidationsCliente.ValidationsCalienteCaracteresEspeciales);
        RuleFor(emp => emp.Apellido).NotNull().NotEmpty().WithMessage(MessagesValidationsCliente.ValidationsClienteApellidoNotNull)
            .MaximumLength(15).WithMessage(MessagesValidationsCliente.ValidationsClienteApellidoMax15Caracteres)
            .Must(CadenasHelper.ExisteCaracteresEspeciales).WithMessage(MessagesValidationsCliente.ValidationsCalienteCaracteresEspeciales);
        RuleFor(emp => emp.EMail).NotNull().NotEmpty().WithMessage(MessagesValidationsCliente.ValidationsClienteEMailNotNull)
            .MaximumLength(25).WithMessage(MessagesValidationsCliente.ValidationsClienteEMailMax25Caracteres)
            .EmailAddress().WithMessage(MessagesValidationsCliente.ValidationsClienteEMailFormatoIncorrecto);
        RuleFor(emp => emp.Nombre).NotEqual(cliente => cliente.Apellido)
            .WithMessage(MessagesValidationsCliente.ValidationsClienteNombreIgualApellido);
        RuleFor(emp => emp.Edad).LessThan(100).WithMessage(MessagesValidationsCliente.ValidationsClienteEdadMenor100);
    }
}