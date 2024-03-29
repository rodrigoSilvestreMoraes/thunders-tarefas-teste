using Tarefas.Core.Domain.Models.Cliente;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Api.Controllers;

/// <summary>
/// TODO: A idéia dessa controller é prover um meio para listar os dados de domínio existente e que podem ser utilizados por outras operações.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DomainController : ControllerBase
{
	readonly IDominioService _dominioService;
	const string _tagSwagger = "Domínio Geral";
	public DomainController(IDominioService dominioService)
	{
		_dominioService = dominioService;
	}

	[HttpGet("categorias")]
	[SwaggerOperation(
	Summary = "Lista todas as categorias de tarefa.",
	Description = "Permite a consulta de todas as categorias disponíveis.",
	OperationId = "Listar Categorias", Tags = new[] { _tagSwagger })]
	[SwaggerResponse(StatusCodes.Status200OK, "Solicitação realizada.", typeof(List<CategoriaView>))]
	[SwaggerResponse(StatusCodes.Status404NotFound, "Sem resultados.", typeof(RestClientVndErrors))]
	public async Task<IActionResult> ListarCaterorias()
	{
		var resultOperation = await _dominioService.ListarCategorias();
		if (!resultOperation.Any())
		{
			var error = new RestClientVndErrors { VndErros = new Embedded() };
			error.VndErros.Errors = new List<ErrorDetail>();
			error.VndErros.Errors.Add(new ErrorDetail { ErrorCode = StatusCodes.Status404NotFound.ToString(), Message = "Nenhum registro encontrado." });
			return StatusCode(StatusCodes.Status404NotFound, error);
		}
		return StatusCode(StatusCodes.Status200OK, resultOperation);
	}	
}
