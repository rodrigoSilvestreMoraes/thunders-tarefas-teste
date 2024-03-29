using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Core.Infra.Rest
{
	public class AppResponse<T>
	{
		public T Response { get; set; }
		public RestClientVndErrors Validation { get; set; } = new RestClientVndErrors();
		public bool Result { get; set; }

		public bool Invalid
		{
			get
			{
				if (Validation.VndErros.Errors.Any()) return true;
				return false;
			}
		}
	}
}
