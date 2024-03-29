using Swashbuckle.AspNetCore.Annotations;
using Tarefas.Core.Domain.Entities.Tarefas;

namespace Tarefas.Core.Domain.Models.Tarefas
{
	public abstract record TarefaBase : ITarefaDefinition
	{
		public string Id { get; set; }
		public string Nome { get; set; }
		public string Detalhe { get; set; }
		
		[SwaggerSchema("A data deve estar no formato yyyy-mm-dd")]
		public string DataInicio { get; set; }

		[SwaggerSchema("A data deve estar no formato yyyy-mm-dd")]
		public string DataFinal { get; set; }

		[SwaggerSchema("Os valores possíveis são: Baixa = 0, Media = 1, Alta = 2")]
		public int Prioridade { get; set; }

		[SwaggerSchema("Os valores possíveis são: Pendente = 0, EmProgrego = 1, Pausa = 2, Cancelada = 3, Finalizada = 4,")]
		public int Status { get; set; }
		public bool SemPrazo { get; set; }
		public string Usuario { get; set; }
		public string CategoriaId { get; set; }
	}
}
