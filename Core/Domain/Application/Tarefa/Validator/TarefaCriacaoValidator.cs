using FluentValidation;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Infra.Validator;

namespace Tarefas.Core.Domain.Application.Tarefa.Validator
{
	public class TarefaCriacaoValidator : AbstractValidator<TarefaRegistro>
	{
		public TarefaCriacaoValidator() 
		{
			RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("Nome"));
			RuleFor(x => x.Detalhe).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("Detalhe"));
			RuleFor(x => x.DataInicio).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("DataInicio"));
			RuleFor(x => x.DataFinal).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("DataFinal"));
			RuleFor(x => x.Prioridade).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("Prioridade"));
			RuleFor(x => x.Usuario).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("Usuario"));
			RuleFor(x => x.CategoriaId).NotNull().NotEmpty().WithMessage(MensagensPadroes.CampoObrigatorio("CategoriaId"));

			RuleFor(x => x.DataInicio).Must((x, DataInicio) => CustomValidators.ValidarRangeData(DataInicio, x.DataFinal));			
		}
	}
}
