using FluentValidation.Results;
using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Core.Infra.Validator
{
	public static class CustomValidators
	{
		public static bool ValidarRangeData(string dataInicial, string dataFinal)
		{
			DateTime dataInicialEnviada = DateTime.MinValue;
			var resultIni = DateTime.TryParse(dataInicial, out dataInicialEnviada);

			DateTime dataFinalEnviada = DateTime.MinValue;
			var resultFim = DateTime.TryParse(dataFinal, out dataFinalEnviada);

			if (resultIni && resultFim)
				return (dataFinalEnviada.Date - dataInicialEnviada.Date).TotalDays > 0;

			return false;
		}		

		public static List<ErrorDetail> ListarErrorValidacoes(List<ValidationFailure> validationResult)
			=> validationResult.Select(x => new ErrorDetail { ErrorCode = x.PropertyName, Message = x.ErrorMessage }).ToList();
	}
}
