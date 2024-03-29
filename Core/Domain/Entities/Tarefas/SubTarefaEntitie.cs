namespace Tarefas.Core.Domain.Entities.Tarefas;

public class SubTarefaEntitie
{
	public string TarefaId { get; private set; }
	public string Id { get; private set; }
	public string Nome {  get; private set; }
	public int Ordem {  get; private set; }
	public EnumStatusTarefa Status { get; private set; }
}
