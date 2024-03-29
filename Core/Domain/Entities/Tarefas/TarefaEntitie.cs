using Tarefas.Core.Infra.Extension;

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
	public void SetId(string id)
	{
		this.Id = id;
	}
	public static TarefaEntitie BuilderForInsert(ITarefaDefinition tarefaDefinition) => Builder(tarefaDefinition, EnumStatusTarefa.Pendente);
	public static TarefaEntitie BuilderForUpdate(ITarefaDefinition tarefaDefinition) => Builder(tarefaDefinition, (EnumStatusTarefa)tarefaDefinition.Status);
	static TarefaEntitie Builder(ITarefaDefinition tarefaDefinition, EnumStatusTarefa status)
	{
		return new TarefaEntitie
		{
			CategoriaId = tarefaDefinition.CategoriaId,
			Nome = tarefaDefinition.Nome.Trim().ToLower(),
			Prioridade = (EnumPrioridadeTarefa)tarefaDefinition.Prioridade,
			Status = status,
			SemPrazo = tarefaDefinition.SemPrazo,
			Usuario = tarefaDefinition.Usuario,
			Detalhe = tarefaDefinition.Detalhe,
			DataInicio = DateTimeExtension.DateTimeParse(tarefaDefinition.DataInicio),
			DataFinal = DateTimeExtension.DateTimeParse(tarefaDefinition.DataFinal),
		};
	}

	public ITarefaDefinition MappingResponse(ITarefaDefinition response)
	{
		response.Id = this.Id;
		response.Nome = this.Nome.FirstCharToUpper();
		response.Status = (int)this.Status;
		response.Prioridade = (int)this.Prioridade;
		response.SemPrazo = this.SemPrazo;
		response.Detalhe = this.Detalhe;
		response.DataInicio = this.DataInicio.ToShortDateString();
		response.DataFinal = this.DataFinal.ToShortDateString();
		response.CategoriaId = this.CategoriaId;
		response.Usuario = this.Usuario;

		return response;
	}
}

