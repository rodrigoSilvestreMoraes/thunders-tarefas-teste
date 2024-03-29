namespace Tarefas.Core.Domain.Models.Tarefas
{
	public abstract record TarefaBase
	{
		public string Id { get; private set; }
		public string Nome { get; private set; }
		public string Detalhe { get; private set; }
		public string DataInicio { get; private set; }
		public string DataFinal { get; private set; }
		public int Prioridade { get; private set; }
		public int Status { get; private set; }
		public bool SemPrazo { get; private set; }
		public string Usuario { get; private set; }
		public string CategoriaId { get; private set; }
		public List<SubTarefaDetalhe> SubTarefas { get; private set; } = new List<SubTarefaDetalhe>();
	}
}
