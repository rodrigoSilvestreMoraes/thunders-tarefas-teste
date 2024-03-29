using Tarefas.Api.Models;
using Tarefas.Core.Domain.Models.Cliente;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
	[SwaggerResponse(StatusCodes.Status404NotFound, "Sem resultados.", typeof(ErroPadrao))]
	public async Task<IActionResult> ListarCaterorias()
	{
		var resultOperation = await _dominioService.ListarCategorias();
		if (!resultOperation.Any())
			return StatusCode(StatusCodes.Status404NotFound, new ErroPadrao { Code = $"{StatusCodes.Status404NotFound}", Message = new List<string> { "Nenhum registro encontrado." } } );

		return StatusCode(StatusCodes.Status200OK, resultOperation);
	}	
}
