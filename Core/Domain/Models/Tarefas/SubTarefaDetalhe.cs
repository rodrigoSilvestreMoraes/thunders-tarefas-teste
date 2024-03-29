namespace Tarefas.Core.Domain.Models.Tarefas
{
	public record SubTarefaDetalhe
	{
		public string TarefaId { get; private set; }
		public string Id { get; private set; }
		public string Nome { get; private set; }
		public int Ordem { get; private set; }
		public int Status { get; private set; }
	}
}
