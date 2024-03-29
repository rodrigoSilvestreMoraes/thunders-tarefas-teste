using Tarefas.Core.Domain.Entities.Tarefas;

namespace Tarefas.Core.Domain.ServiceBusiness.Tarefas;
public interface ITarefaService
{
	Task<ITarefaDefinition> Criar(ITarefaDefinition tarefaDefinition);
	Task<bool> Alterar(ITarefaDefinition tarefaDefinition);
	Task<bool> AlterarStatus(string tarefaId, int status);
	Task<bool> Apagar(string tarefaId);
	Task<ITarefaDefinition> Pegar(string tarefaId);
	Task<List<ITarefaDefinition>> Consultar(string usuario);
	Task<List<ITarefaDefinition>> Consultar(DateTime dataInicio, DateTime dataFinal);
	Task<List<ITarefaDefinition>> Consultar(string usuario, DateTime dataInicio, DateTime dataFinal);	
}
