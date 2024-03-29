using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Core.Infra.Validator
{
	public static class MensagensPadroes
	{
		public static string CodeErrorBusiness = "422";
		public static string ListaVazia(string campo) => $"É obrigatório adicionar {campo} ao conjunto.";
		public static string CampoObrigatorio(string campo) => $"Campo {campo} precisa ser preenchido.";
		public static string ValorInvalido(string campo) => $"Campo {campo} possuí um valor inválido.";
		public static string CampoFormatoInvalido(string campo, string formato) => $"Campo {campo} se encontra com formato errado. Formato correto é {formato}";
		
		public static RestClientVndErrors ErrorInternoServidor()
		{
			var error = new RestClientVndErrors();
			error.VndErros.Errors.Add(new ErrorDetail { ErrorCode = "999", Message = "Ocorreu um erro intenro, nos desculpe pelo transtorno.Por favor tente novamente." });
			return error;
		}
	}
}
