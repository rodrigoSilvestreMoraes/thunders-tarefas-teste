using Tarefas.Core.Domain.Entities.Tarefas;

namespace Tarefas.Core.Domain.Repositorys.Tarefa
{
	public interface ITarefaRepo
	{
		Task<TarefaEntitie> Criar(TarefaEntitie tarefaEntitie);
		Task<bool> Alterar(TarefaEntitie tarefaEntitie);
		Task<bool> AlterarStatus(string tarefaId, EnumStatusTarefa status);
		Task<bool> Apagar(string tarefaId);
		Task<TarefaEntitie> Pegar(string tarefaId);
		Task<List<TarefaEntitie>> Consultar(string usuario);
		Task<List<TarefaEntitie>> ExisteTarefaPorNome(string nome);
		Task<List<TarefaEntitie>> Consultar(DateTime dataInicio, DateTime dataFinal);
		Task<List<TarefaEntitie>> Consultar(string usuario, DateTime dataInicio, DateTime dataFinal);
	}
}
