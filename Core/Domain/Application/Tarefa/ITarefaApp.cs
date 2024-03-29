using Tarefas.Core.Domain.Models.Tarefas;

namespace Tarefas.Core.Domain.Application.Tarefa;
public interface ITarefaApp
{
	Task<TarefaConsulta> InserirTarefa(TarefaRegistro tarefaRegistro);
	Task<bool> AlterarTarefa(TarefaAlteracao tarefaAlteracao);
}
