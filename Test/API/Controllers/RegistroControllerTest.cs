using Tarefas.Api.Controllers;
using Tarefas.Api.Models.Transacao;
using Tarefas.Core.Domain.Models.Transacao;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Tarefas.Core.Domain.ServiceBusiness.RegistroFluxo;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Tarefas.Test.API.Controllers
{
	public class RegistroControllerTest
	{
		readonly Mock<IRegistroFluxoService> _registroFluxoService;
		readonly Mock<IDominioService> _dominioService;

		public RegistroControllerTest()
		{
			_registroFluxoService = new Mock<IRegistroFluxoService>();
			_dominioService = new Mock<IDominioService>();
		}

		[Fact]
		public async void Deveria_CriarDespesa()
		{
			var request = new DespesaRequest { Codigo = "sadsdsd", CodigoFornecedor = "dsdsds", DataPagamento = "2023-05-22", Descricao = "TESTE", Valor = 100 };
			var mock = new ResultadoRegistro<RegistroDespesa>();
			mock.Erro = false;

			_registroFluxoService.Setup(x => x.RegistrarDespesa(It.IsAny<RegistroDespesa>())).Returns(Task.FromResult(mock));

			var callResult = await GetController().CriarDespesa(request);
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.Created, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_CriarDespesa()
		{
			var request = new DespesaRequest { Codigo = "sadsdsd", CodigoFornecedor = "dsdsds", DataPagamento = "202305-22", Descricao = "TESTE", Valor = 100 };
			var mock = new ResultadoRegistro<RegistroDespesa>();
			mock.Erro = true;

			_registroFluxoService.Setup(x => x.RegistrarDespesa(It.IsAny<RegistroDespesa>())).Returns(Task.FromResult(mock));

			var callResult = await GetController().CriarDespesa(request);
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.UnprocessableEntity, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_CriarReceita()
		{
			var request = new ReceitaRequest { Codigo = "sadsdsd", CodigoCliente = "dsdsds", DataPagamento = "2023-05-22", Descricao = "TESTE", Valor = 100 };
			var mock = new ResultadoRegistro<RegistroReceita>();
			mock.Erro = false;

			_registroFluxoService.Setup(x => x.RegistrarReceita(It.IsAny<RegistroReceita>())).Returns(Task.FromResult(mock));

			var callResult = await GetController().CriarReceita(request);
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.Created, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_CriarReceita()
		{
			var request = new ReceitaRequest { Codigo = "sadsdsd", CodigoCliente = "dsdsds", DataPagamento = "202305-22", Descricao = "TESTE", Valor = 100 };
			var mock = new ResultadoRegistro<RegistroReceita>();
			mock.Erro = true;

			_registroFluxoService.Setup(x => x.RegistrarReceita(It.IsAny<RegistroReceita>())).Returns(Task.FromResult(mock));

			var callResult = await GetController().CriarReceita(request);
			var result = callResult as ObjectResult;

			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.UnprocessableEntity, result.StatusCode.Value);
		}

		RegistroController GetController() => new RegistroController(_registroFluxoService.Object, _dominioService.Object);
	}
}
