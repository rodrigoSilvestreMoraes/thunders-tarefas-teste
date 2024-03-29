using Tarefas.Api.Controllers;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Tarefas.Test.Stubs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Tarefas.Test.API.Controllers
{
	public class DomainControllerTest
	{
		readonly Mock<IDominioService> _dominioService;
		public DomainControllerTest()
		{
			_dominioService = new Mock<IDominioService>();
		}

		[Fact]
		public async void Deveria_ConsultarTodosClientes()
		{
			_dominioService.Setup(x => x.ListarCategorias()).Returns(Task.FromResult(CategoriaViewMock.Listar()));

			var callResult = await GetController().ListarCaterorias();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_ConsultarTodosClientes()
		{
			_dominioService.Setup(x => x.ListarCategorias()).Returns(Task.FromResult(new List<Tarefas.Core.Domain.Models.Cliente.CategoriaView>()));

			var callResult = await GetController().ListarCaterorias();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode.Value);
		}	

		DomainController GetController() => new DomainController(_dominioService.Object);
	}
}
