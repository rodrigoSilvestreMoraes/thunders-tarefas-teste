using FluentValidation;
using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Domain.ServiceBusiness.Tarefas;
using Tarefas.Core.Infra.Extension;
using Tarefas.Core.Infra.Rest;
using Tarefas.Core.Infra.Validator;

namespace Tarefas.Core.Domain.Application.Tarefa
{
	/// <summary>
	/// Apresentation
	/// </summary>
	public class TarefaApp : ITarefaApp
	{
		readonly ITarefaService _tarefaService;
		readonly IValidator<TarefaRegistro> _validatorInsercaoTarefa;
		readonly IValidator<TarefaAlteracao> _validatorAlteracaoTarefa;
		public TarefaApp(
			ITarefaService tarefaService,
			IValidator<TarefaRegistro> validatorInsercaoTarefa,
			IValidator<TarefaAlteracao> validatorAlteracaoTarefa)
		{
			_tarefaService = tarefaService;
			_validatorInsercaoTarefa = validatorInsercaoTarefa;
			_validatorAlteracaoTarefa = validatorAlteracaoTarefa;
		}

		public async Task<AppResponse<ITarefaDefinition>> InserirTarefa(TarefaRegistro tarefaRegistro)
		{
			var response = new AppResponse<ITarefaDefinition>();

			var resultValidator = _validatorInsercaoTarefa.Validate(tarefaRegistro);
			if (!resultValidator.IsValid)
			{
				response.Validation.VndErros.Errors.AddRange(CustomValidators.ListarErrorValidacoes(resultValidator.Errors));
				return response;
			}

			var insertResult = await _tarefaService.Criar(tarefaRegistro);
			if (insertResult != null)
			{
				response.Result = true;
				response.Response = insertResult;
			}

			return response;
		}
		public async Task<AppResponse<bool>> AlterarTarefa(TarefaAlteracao tarefaAlteracao)
		{
			var response = new AppResponse<bool>();

			var resultValidator = _validatorAlteracaoTarefa.Validate(tarefaAlteracao);
			if (!resultValidator.IsValid)
			{
				response.Validation.VndErros.Errors.AddRange(CustomValidators.ListarErrorValidacoes(resultValidator.Errors));
				return response;
			}

			response.Response = await _tarefaService.Alterar(tarefaAlteracao);
			response.Result = response.Response;
			return response;
		}
		public async Task<AppResponse<bool>> AlterarStatus(string tarefaId, int status)
		{
			var response = new AppResponse<bool>();
			response.Response = await _tarefaService.AlterarStatus(tarefaId, status);
			response.Result = response.Response;
			return response;
		}
		public async Task<AppResponse<bool>> ApagarTarefa(string tarefaId)
		{
			var response = new AppResponse<bool>();
			response.Response = await _tarefaService.Apagar(tarefaId);
			response.Result = response.Response;
			return response;
		}
		public async Task<AppResponse<List<ITarefaDefinition>>> ConsultarTarefas(string usuario)
		{
			var response = new AppResponse<List<ITarefaDefinition>>();
			response.Response = await _tarefaService.Consultar(usuario);
			response.Result = response.Response.Count > 0;
			return response;
		}
		public async Task<AppResponse<List<ITarefaDefinition>>> ConsultarTarefas(string dataInicial, string dataFinal)
		{
			var response = new AppResponse<List<ITarefaDefinition>>();
			response.Response = await _tarefaService.Consultar(DateTimeExtension.DateTimeParse(dataInicial), DateTimeExtension.DateTimeParse(dataFinal));
			response.Result = response.Response.Count > 0;
			return response;
		}
		public async Task<AppResponse<List<ITarefaDefinition>>> ConsultarTarefas(string usuario, string dataInicial, string dataFinal)
		{
			var response = new AppResponse<List<ITarefaDefinition>>();
			response.Response = await _tarefaService.Consultar(usuario, 
				DateTimeExtension.DateTimeParse(dataInicial), 
				DateTimeExtension.DateTimeParse(dataFinal));

			response.Result = response.Response.Count > 0;
			return response;
		}		
		public async Task<AppResponse<ITarefaDefinition>> PegarTarefa(string tarefaId)
		{
			var response = new AppResponse<ITarefaDefinition>();
			response.Response = await _tarefaService.Pegar(tarefaId);
			response.Result = response != null;
			return response;
		}
	}
}