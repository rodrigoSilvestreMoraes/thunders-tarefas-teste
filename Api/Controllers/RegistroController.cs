using Microsoft.AspNetCore.Mvc;

namespace Tarefas.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegistroController : ControllerBase
	{
		//readonly IRegistroFluxoService _registroFluxoService;
		//readonly IDominioService _dominioService;
		//public RegistroController(IRegistroFluxoService registroFluxoService, IDominioService dominioService) 
		//{ 
		//	_registroFluxoService = registroFluxoService;
		//	_dominioService = dominioService;
		//}

		//[HttpPost("despesa")]
		//[SwaggerOperation(
		//Summary = "Permite o registro de uma determinada despesa.",
		//Description = "Permite o registro de uma determinada despesa.",
		//OperationId = "Transações", Tags = new[] { "Despesas" })]
		//[SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitação inválida.", typeof(List<ErroPadrao>))]
		//[SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Informações inválidas no request.", typeof(ErroPadrao))]
		//public async Task<IActionResult> CriarDespesa([FromBody] DespesaRequest despesaRequest)
		//{
		//	var resultOperation = await _registroFluxoService.RegistrarDespesa(despesaRequest.Mapping(_dominioService));
		//	if (!resultOperation.Erro)
		//		return StatusCode(StatusCodes.Status201Created, resultOperation.Data);

		//	var erro = new ErroPadrao { Code = "StatusCodes.Status422UnprocessableEntity", Message = resultOperation.Erros };
		//	return StatusCode(StatusCodes.Status422UnprocessableEntity, erro);
		//}

		
	}
}
