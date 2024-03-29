using System.Text.Json.Serialization;

namespace Tarefas.Core.Domain.Models.Tarefas
{
	public record TarefaRegistro : TarefaBase
	{
		[JsonIgnore]
		public new virtual int Status {  get; private set; }
	}
}
