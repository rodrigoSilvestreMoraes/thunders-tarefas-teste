using Newtonsoft.Json;

namespace Tarefas.Core.Domain.Models.Tarefas
{
	public record TarefaAlteracao : TarefaBase
	{
		[JsonIgnore]
		public new virtual int Status { get; private set; }
	}
}
