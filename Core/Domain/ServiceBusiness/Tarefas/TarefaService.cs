using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Repositorys.Tarefa;

namespace Tarefas.Core.Domain.ServiceBusiness.Tarefas
{
	public class TarefaService : ITarefaService
	{
		readonly ITarefaRepo _tarefaRepo;
		public TarefaService(ITarefaRepo tarefaRepo) 
		{
			_tarefaRepo = tarefaRepo;
		}

		public async Task<TarefaEntitie> Criar(TarefaEntitie tarefaEntitie) => await _tarefaRepo.Criar(tarefaEntitie);
		public async Task<bool> Alterar(TarefaEntitie tarefaEntitie) => await _tarefaRepo.Alterar(tarefaEntitie);
		public async Task<bool> AlterarStatus(string tarefaId, EnumStatusTarefa status) => await _tarefaRepo.AlterarStatus(tarefaId, status);
		public async Task<bool> Apagar(string tarefaId) => await _tarefaRepo.Apagar(tarefaId);		
		public async Task<List<TarefaEntitie>> Consultar(string usuario) => await _tarefaRepo.Consultar(usuario);
		public async Task<List<TarefaEntitie>> Consultar(DateTime dataInicio, DateTime dataFinal) => await _tarefaRepo.Consultar(dataInicio, dataFinal);
		public async Task<List<TarefaEntitie>> Consultar(string usuario, DateTime dataInicio, DateTime dataFinal)
		 => await _tarefaRepo.Consultar(usuario, dataInicio, dataFinal);
		public async Task<TarefaEntitie> Pegar(string tarefaId)
			=> await _tarefaRepo.Pegar(tarefaId);		
	}
}
