using FluentValidation.Results;
using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Core.Infra.Validator
{
	public static class CustomValidators
	{
		/// <summary>
		/// Garantir o formato PT/BR
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static bool ValidarFormatoData(string data)
		{
			if (string.IsNullOrEmpty(data)) return false;

			DateTime dataEnviada = DateTime.MinValue;
			var result = DateTime.TryParse(data, out dataEnviada);

			return result;
		}

		public static bool ValidarValorMin(decimal? valorMin) => ((!valorMin.HasValue ? true : valorMin.Value > 0));
		public static bool ValidarDataPassado(string data)
		{
			if (!ValidarFormatoData(data)) return false;

			var dataInput = DateTime.Parse(data);
			return (dataInput.Date - DateTime.Now.Date).TotalDays > 0;
		}

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
