using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Core.Domain.Models.Tarefas
{
	[ExcludeFromCodeCoverage]
	public record TarefaAlteracao : TarefaBase
	{
		[JsonIgnore]
		public new virtual int Status { get; private set; }
	}
}
