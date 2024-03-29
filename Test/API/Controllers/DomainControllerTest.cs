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
			_dominioService.Setup(x => x.ListarClientes()).Returns(Task.FromResult(ClienteViewMock.Listar()));

			var callResult = await GetController().ConsultarTodosClientes();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_ConsultarTodosClientes()
		{
			_dominioService.Setup(x => x.ListarClientes()).Returns(Task.FromResult(new List<Tarefas.Core.Domain.Models.Cliente.CategoriaView>()));

			var callResult = await GetController().ConsultarTodosClientes();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_ConsultarTodosFornecedores()
		{
			_dominioService.Setup(x => x.ListarFornecedores()).Returns(Task.FromResult(FornecedorViewMock.Listar()));

			var callResult = await GetController().ConsultarTodosFornecedores();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_ConsultarTodosFornecedores()
		{
			_dominioService.Setup(x => x.ListarFornecedores()).Returns(Task.FromResult(new List<Tarefas.Core.Domain.Models.Fornecedore.FornecedorView>()));

			var callResult = await GetController().ConsultarTodosFornecedores();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode.Value);
		}

		[Fact]
		public async void Deveria_ConsultarTodasDespesas()
		{
			_dominioService.Setup(x => x.ListarDespesas()).Returns(Task.FromResult(DespesaViewMock.Listar()));

			var callResult = await GetController().ConsultarTodasDespesas();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_ConsultarTodasDespesas()
		{
			_dominioService.Setup(x => x.ListarDespesas()).Returns(Task.FromResult(new List<Tarefas.Core.Domain.Models.Despesa.DespesaView>()));

			var callResult = await GetController().ConsultarTodasDespesas();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode.Value);
		}

		[Fact]
		public async void Deveria_ConsultarTodasReceitas()
		{
			_dominioService.Setup(x => x.ListarReceitas()).Returns(Task.FromResult(ReceitaViewMock.Listar()));

			var callResult = await GetController().ConsultarTodasReceitas();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_ConsultarTodasReceitas()
		{
			_dominioService.Setup(x => x.ListarReceitas()).Returns(Task.FromResult(new List<Tarefas.Core.Domain.Models.Receita.ReceitaView>()));

			var callResult = await GetController().ConsultarTodasReceitas();
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode.Value);
		}

		DomainController GetController() => new DomainController(_dominioService.Object);
	}
}
