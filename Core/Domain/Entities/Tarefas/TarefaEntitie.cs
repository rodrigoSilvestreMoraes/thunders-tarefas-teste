namespace Tarefas.Core.Domain.Entities.Tarefas;

public class TarefaEntitie
{
	public string Id { get; private set; }
	public string Nome { get; private set; }
	public string Detalhe {  get; private set; }
	public DateTime DataInicio { get; private set; }
	public DateTime DataFinal { get; private set; }
	public EnumPrioridadeTarefa Prioridade { get; private set; }
	public EnumStatusTarefa Status { get; private set; }
	public bool SemPrazo {  get; private set; }
	public string Usuario { get; private set; }
	public string CategoriaId { get; private set; }
	public List<SubTarefaEntitie> SubTarefas { get; private set; } = new List<SubTarefaEntitie>();

	public void SetId(string id)
	{
		this.Id = id;
	}
}

