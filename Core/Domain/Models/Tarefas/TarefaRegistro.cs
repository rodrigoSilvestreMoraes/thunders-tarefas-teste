using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Tarefas.Core.Domain.Models.Tarefas
{
	[ExcludeFromCodeCoverage]
	public record TarefaRegistro : TarefaBase
	{
		[JsonIgnore]
		public new virtual string Id { get; private set; }

		[JsonIgnore]
		public new virtual int Status {  get; private set; }
	}
}
