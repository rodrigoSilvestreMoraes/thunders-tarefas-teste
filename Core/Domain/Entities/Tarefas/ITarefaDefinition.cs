namespace Tarefas.Core.Domain.Entities.Tarefas
{
	public interface ITarefaDefinition
	{
		public string Id { get; set; }
		public string Nome { get; set; }
		public string Detalhe { get; set; }
		public string DataInicio { get; set; }
		public string DataFinal { get; set; }
		public int Prioridade { get; set; }
		public int Status { get; set; }
		public bool SemPrazo { get; set; }
		public string Usuario { get; set; }
		public string CategoriaId { get;  set; }
	}
}
