using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Infra.Rest;

namespace Tarefas.Core.Domain.Application.Tarefa;
public interface ITarefaApp
{
	Task<AppResponse<ITarefaDefinition>> InserirTarefa(TarefaRegistro tarefaRegistro);
	Task<AppResponse<bool>> AlterarTarefa(TarefaAlteracao tarefaAlteracao);
	Task<AppResponse<bool>> AlterarStatus(string tarefaId, int status);
	Task<AppResponse<bool>> ApagarTarefa(string tarefaId);

	Task<AppResponse<ITarefaDefinition>> PegarTarefa(string tarefaId);
	Task<AppResponse<List<ITarefaDefinition>>> ConsultarTarefas(string usuario);
	Task<AppResponse<List<ITarefaDefinition>>> ConsultarTarefas(string dataInicial, string dataFinal);
	Task<AppResponse<List<ITarefaDefinition>>> ConsultarTarefas(string usuario, string dataInicial, string dataFinal);
}
