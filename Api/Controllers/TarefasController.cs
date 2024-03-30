using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tarefas.Core.Domain.Application.Tarefa;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Infra.CustomLogger;
using Tarefas.Core.Infra.Rest.Error;
using Tarefas.Core.Infra.Validator;

namespace Tarefas.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TarefasController : ControllerBase
	{
		readonly ICustomLog _customLog;
		readonly ITarefaApp _tarefaApp;

		const string _tag = "Tarefas";
		public TarefasController(ITarefaApp tarefaApp, ICustomLog customLog)
		{
			_tarefaApp = tarefaApp;
			_customLog = customLog;
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Cria uma tarefa.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status201Created, "Solicitação válida.", typeof(TarefaConsulta))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> Criar([FromBody] TarefaRegistro tarefaRegistro)
		{
			try
			{
				var result = await _tarefaApp.InserirTarefa(tarefaRegistro);
				if (!result.Invalid) return StatusCode(StatusCodes.Status201Created, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpPut]
		[SwaggerOperation(Summary = "Atualiza uma tarefa.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> Atualizar([FromBody] TarefaAlteracao tarefaAlteracao)
		{
			try
			{
				var result = await _tarefaApp.AlterarTarefa(tarefaAlteracao);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpPut("{tarefaId}/status/{statusId}")]
		[SwaggerOperation(Summary = "Atualiza o status de uma tarefa.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> AtualizarStatus([FromRoute][SwaggerParameter("Id da tarefa")] string tarefaId, 
													     [FromRoute][SwaggerParameter("Os valores possíveis são: Pendente = 0, EmProgrego = 1, Pausa = 2, Cancelada = 3, Finalizada = 4,")] int statusId)
		{
			try
			{
				var result = await _tarefaApp.AlterarStatus(tarefaId, statusId);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpDelete("{tarefaId}")]
		[SwaggerOperation(Summary = "Apaga uma tarefa.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(bool))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> Apagar([FromRoute] string tarefaId)
		{
			try
			{
				var result = await _tarefaApp.ApagarTarefa(tarefaId);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpGet("{tarefaId}")]
		[SwaggerOperation(Summary = "Pega uma tarefa.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(TarefaConsulta))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> Pegar([FromRoute] string tarefaId)
		{
			try
			{
				var result = await _tarefaApp.PegarTarefa(tarefaId);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpGet("listar/{usuario}")]
		[SwaggerOperation(Summary = "Listar tarefas por usuário.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(List<TarefaConsulta>))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> ListarPorUsuario([FromRoute] string usuario)
		{
			try
			{
				var result = await _tarefaApp.ConsultarTarefas(usuario);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpGet("listar/periodo")]
		[SwaggerOperation(Summary = "Listar tarefas por um período.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(List<TarefaConsulta>))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> ListarPorPeriodo([FromQuery][SwaggerParameter("Formato da data: yyyy-mm-dd")] string dataInicial,
																			[SwaggerParameter("Formato da data: yyyy-mm-dd")] string dataFinal)
		{
			try
			{
				var result = await _tarefaApp.ConsultarTarefas(dataInicial, dataFinal);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}

		[HttpGet("listar/usuario/{usuario}/periodo")]
		[SwaggerOperation(Summary = "Listar tarefas por usuário e um período.", Tags = new[] { _tag })]
		[SwaggerResponse(StatusCodes.Status200OK, "Solicitação inválida.", typeof(List<TarefaConsulta>))]
		[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(RestClientVndErrors))]
		public async Task<IActionResult> ListarPorUsuarioPeriodo([FromRoute] string usuario, 
																 [FromQuery][SwaggerParameter("Formato da data: yyyy-mm-dd")] string dataInicial, 
																			[SwaggerParameter("Formato da data: yyyy-mm-dd")] string dataFinal)
		{
			try
			{
				var result = await _tarefaApp.ConsultarTarefas(usuario, dataInicial, dataFinal);
				if (!result.Invalid) return StatusCode(StatusCodes.Status200OK, result.Response);

				return BadRequest(result.Validation);
			}
			catch (Exception ex)
			{
				_customLog.GravarLog(CustomLogRequest.Create(Startup._title, ControllerContext?.ActionDescriptor?.ControllerName, ex));
				return StatusCode(StatusCodes.Status500InternalServerError, MensagensPadroes.ErrorInternoServidor());
			}
		}
	}
}