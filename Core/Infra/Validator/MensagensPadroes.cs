using System.Diagnostics.CodeAnalysis;
using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Core.Infra.Validator
{
	[ExcludeFromCodeCoverage]
	public static class MensagensPadroes
	{
		public static string CodeErrorBusiness = "422";
		public static string CampoObrigatorio(string campo) => $"Campo {campo} precisa ser preenchido.";		
		public static RestClientVndErrors ErrorInternoServidor()
		{
			var error = new RestClientVndErrors();
			error.VndErros.Errors.Add(new ErrorDetail { ErrorCode = "999", Message = "Ocorreu um erro intenro, nos desculpe pelo transtorno.Por favor tente novamente." });
			return error;
		}
	}
}
