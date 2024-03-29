using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Domain.Repositorys.Tarefa;

namespace Tarefas.Core.Domain.ServiceBusiness.Tarefas;
public class TarefaService : ITarefaService
{
	readonly ITarefaRepo _tarefaRepo;
	public TarefaService(ITarefaRepo tarefaRepo) 
	{
		_tarefaRepo = tarefaRepo;
	}

	public async Task<ITarefaDefinition> Criar(ITarefaDefinition tarefaEntitie)
	{
		var existeTarefa = await _tarefaRepo.ExisteTarefaPorNome(tarefaEntitie.Nome);

		if(existeTarefa != null)
			return existeTarefa.MappingResponse(new TarefaConsulta());

		var result = await _tarefaRepo.Criar(TarefaEntitie.BuilderForInsert(tarefaEntitie));
		return result.MappingResponse(new TarefaConsulta());
	}
	public async Task<bool> Alterar(ITarefaDefinition tarefaEntitie)
	{
		var existeTarefa = await _tarefaRepo.ExisteTarefaPorNome(tarefaEntitie.Nome);
		if (existeTarefa != null && existeTarefa.Id != tarefaEntitie.Id)
			return false;

		return await _tarefaRepo.Alterar(TarefaEntitie.BuilderForUpdate(tarefaEntitie));
	}
	public async Task<bool> AlterarStatus(string tarefaId, int status)
	{
		//TODO: Coloquei para demonstrar onde ficaria regras de negócio específicas do dominio
		var tarefaExiste = await Pegar(tarefaId);
		if (tarefaExiste == null)
			return false;

		var listStatus = new List<EnumStatusTarefa> {
			EnumStatusTarefa.Pendente,
			EnumStatusTarefa.Pausa,
			EnumStatusTarefa.Cancelada,
			EnumStatusTarefa.EmProgrego,
			EnumStatusTarefa.Finalizada
		};

		if(listStatus.Contains((EnumStatusTarefa)status))
			return await _tarefaRepo.AlterarStatus(tarefaId, (EnumStatusTarefa)status);

		return false;
	}
	public async Task<bool> Apagar(string tarefaId) => await _tarefaRepo.Apagar(tarefaId);		
	public async Task<List<ITarefaDefinition>> Consultar(string usuario)
		=> Mapping(await _tarefaRepo.Consultar(usuario));	
	public async Task<List<ITarefaDefinition>> Consultar(DateTime dataInicio, DateTime dataFinal)
		=> Mapping(await _tarefaRepo.Consultar(dataInicio, dataFinal));
	public async Task<List<ITarefaDefinition>> Consultar(string usuario, DateTime dataInicio, DateTime dataFinal)
		=> Mapping(await _tarefaRepo.Consultar(usuario, dataInicio, dataFinal));
	
	public async Task<ITarefaDefinition> Pegar(string tarefaId)
	{
		var result = await _tarefaRepo.Pegar(tarefaId);
		return result.MappingResponse(new TarefaConsulta());
	}

	List<ITarefaDefinition> Mapping(List<TarefaEntitie> entities)
	{
		if (entities.Any())
			return entities.Select(x => x.MappingResponse(new TarefaConsulta())).ToList();

		return new List<ITarefaDefinition>();
	}
}