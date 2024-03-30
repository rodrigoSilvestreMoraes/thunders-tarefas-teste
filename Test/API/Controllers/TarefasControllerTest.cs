using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Tarefas.Api.Controllers;
using Tarefas.Core.Domain.Application.Tarefa;
using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Infra.CustomLogger;
using Tarefas.Core.Infra.Rest;
using Tarefas.Test.Stubs;

namespace Tarefas.Test.API.Controllers
{
	public class TarefasControllerTest
	{
		readonly Mock<ICustomLog> _customLog;
		readonly Mock<ITarefaApp> _tarefaApp;
		public TarefasControllerTest() 
		{
			_customLog = new Mock<ICustomLog>();
			_tarefaApp = new Mock<ITarefaApp>();
		}

		[Fact]
		public async void Deveria_Create()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			var response = GenerateMockResponseSuccess();

			_tarefaApp.Setup(x => x.InserirTarefa(It.IsAny<TarefaRegistro>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Criar(mockInsercao);
			
			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.Created, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Create_Invalido()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			var response = GenerateMockResponseErrorValidation();

			_tarefaApp.Setup(x => x.InserirTarefa(It.IsAny<TarefaRegistro>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Criar(mockInsercao);

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Create_Exception()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();

			_tarefaApp.Setup(x => x.InserirTarefa(It.IsAny<TarefaRegistro>())).Throws(new Exception("ERROR"));

			var callResult = await GetController().Criar(mockInsercao);

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_Alterar()
		{
			var mock = TarefaModelStub.MockAlterarTarefa();
			var response = new AppResponse<bool>();
			response.Response = true;
			response.Result = true;

			_tarefaApp.Setup(x => x.AlterarTarefa(It.IsAny<TarefaAlteracao>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Atualizar(mock);

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Alterar_Invalido()
		{
			var mock = TarefaModelStub.MockAlterarTarefa();
			var response = GenerateMockResponseErrorValidationBool();

			_tarefaApp.Setup(x => x.AlterarTarefa(It.IsAny<TarefaAlteracao>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Atualizar(mock);

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Alterar_Exception()
		{
			var mock = TarefaModelStub.MockAlterarTarefa();

			_tarefaApp.Setup(x => x.AlterarTarefa(It.IsAny<TarefaAlteracao>())).Throws(new Exception("ERROR"));

			var callResult = await GetController().Atualizar(mock);

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_AlterarStatus()
		{
			var response = new AppResponse<bool>();
			response.Response = true;
			response.Result = true;

			_tarefaApp.Setup(x => x.AlterarStatus(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(response));

			var callResult = await GetController().AtualizarStatus(Guid.NewGuid().ToString(), 1);

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_AlterarStatus_Invalido()
		{
			var response = GenerateMockResponseErrorValidationBool();

			_tarefaApp.Setup(x => x.AlterarStatus(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(response));

			var callResult = await GetController().AtualizarStatus(Guid.NewGuid().ToString(), 1);

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_AlterarStatus_Exception()
		{
			_tarefaApp.Setup(x => x.AlterarStatus(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception("ERROR"));

			var callResult = await GetController().AtualizarStatus(Guid.NewGuid().ToString(), 1);

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_Apagar()
		{
			var response = new AppResponse<bool>();
			response.Response = true;
			response.Result = true;

			_tarefaApp.Setup(x => x.ApagarTarefa(It.IsAny<string>())).Returns(Task.FromResult(response));
			var callResult = await GetController().Apagar(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Apagar_Invalido()
		{
			var response = GenerateMockResponseErrorValidationBool();
			_tarefaApp.Setup(x => x.ApagarTarefa(It.IsAny<string>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Apagar(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Apagar_Exception()
		{
			_tarefaApp.Setup(x => x.ApagarTarefa(It.IsAny<string>())).Throws(new Exception("ERROR"));
			var callResult = await GetController().Apagar(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_Pegar()
		{
			var response = GenerateMockResponseSuccess();
			_tarefaApp.Setup(x => x.PegarTarefa(It.IsAny<string>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Pegar(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Pegar_Invalido()
		{
			var response = GenerateMockResponseErrorValidation();
			_tarefaApp.Setup(x => x.PegarTarefa(It.IsAny<string>())).Returns(Task.FromResult(response));

			var callResult = await GetController().Pegar(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Pegar_Exception()
		{
			_tarefaApp.Setup(x => x.PegarTarefa(It.IsAny<string>())).Throws(new Exception("ERROR"));
			var callResult = await GetController().Pegar(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_Listar()
		{
			var response = GenerateListMockResponseSuccess();

			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>())).Returns(Task.FromResult(response));

			var callResult = await GetController().ListarPorUsuario("teste");

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Listar_Invalido()
		{
			var response = GenerateMockResponseErrorValidationList();
			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>())).Returns(Task.FromResult(response));

			var callResult = await GetController().ListarPorUsuario("teste");

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Listar_Exception()
		{
			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>())).Throws(new Exception("ERROR"));

			var callResult = await GetController().ListarPorUsuario(Guid.NewGuid().ToString());

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_Listar_Periodo()
		{
			var response = GenerateListMockResponseSuccess();

			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(response));
			var callResult = await GetController().ListarPorPeriodo("2024-01-01", "2024-03-03");

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Listar_Periodo_Invalido()
		{
			var response = GenerateMockResponseErrorValidationList();

			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(response));
			var callResult = await GetController().ListarPorPeriodo("2024-01-01", "2024-03-03");

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Listar_Periodo_Exception()
		{
			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("ERROR"));			
			var callResult = await GetController().ListarPorPeriodo("2024-01-01", "2024-03-03");

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}


		[Fact]
		public async void Deveria_Listar_Usuario_Periodo()
		{
			var response = GenerateListMockResponseSuccess();

			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(response));
			var callResult = await GetController().ListarPorUsuarioPeriodo("usuario_teste", "2024-01-01", "2024-03-03");

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.OK, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Listar_Usuario_Periodo_Invalido()
		{
			var response = GenerateMockResponseErrorValidationList();

			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(response));
			var callResult = await GetController().ListarPorUsuarioPeriodo("usuario_teste", "2024-01-01", "2024-03-03");

			var result = callResult as ObjectResult;
			Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode.Value);
		}

		[Fact]
		public async void Nao_Deveria_Listar_Usuario_Periodo_Exception()
		{
			_tarefaApp.Setup(x => x.ConsultarTarefas(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("ERROR"));
			var callResult = await GetController().ListarPorPeriodo("2024-01-01", "2024-03-03");

			var result = callResult as ObjectResult;
			Assert.IsType<ObjectResult>(result);
			Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode.Value);
		}

		AppResponse<List<ITarefaDefinition>> GenerateMockResponseErrorValidationList()
		{
			var response = new AppResponse<List<ITarefaDefinition>>();
			response.Response = new List<ITarefaDefinition>();
			response.Result = false;
			response.Validation = new Tarefas.Core.Infra.Rest.Error.RestClientVndErrors();
			response.Validation.VndErros = new Tarefas.Core.Infra.Rest.Error.Embedded();
			response.Validation.VndErros.Errors = new List<Tarefas.Core.Infra.Rest.Error.ErrorDetail>();
			response.Validation.VndErros.Errors.Add(new Tarefas.Core.Infra.Rest.Error.ErrorDetail { ErrorCode = "teste", Message = "teste" });

			return response;
		}

		AppResponse<bool> GenerateMockResponseErrorValidationBool()
		{
			var response = new AppResponse<bool>();
			response.Response = false;
			response.Result = false;
			response.Validation = new Tarefas.Core.Infra.Rest.Error.RestClientVndErrors();
			response.Validation.VndErros = new Tarefas.Core.Infra.Rest.Error.Embedded();
			response.Validation.VndErros.Errors = new List<Tarefas.Core.Infra.Rest.Error.ErrorDetail>();
			response.Validation.VndErros.Errors.Add(new Tarefas.Core.Infra.Rest.Error.ErrorDetail { ErrorCode = "teste", Message = "teste" });

			return response;
		}

		AppResponse<ITarefaDefinition> GenerateMockResponseErrorValidation()
		{
			var response = new AppResponse<ITarefaDefinition>();
			response.Response = null;
			response.Result = false;
			response.Validation = new Tarefas.Core.Infra.Rest.Error.RestClientVndErrors();
			response.Validation.VndErros = new Tarefas.Core.Infra.Rest.Error.Embedded();
			response.Validation.VndErros.Errors = new List<Tarefas.Core.Infra.Rest.Error.ErrorDetail>();
			response.Validation.VndErros.Errors.Add(new Tarefas.Core.Infra.Rest.Error.ErrorDetail { ErrorCode = "teste", Message = "teste" });

			return response;
		}

		AppResponse<ITarefaDefinition> GenerateMockResponseSuccess()
		{
			var mockRetorno = TarefaModelStub.MockConsultaTarefa();
			var response = new AppResponse<ITarefaDefinition>();
			response.Response = mockRetorno;
			response.Result = true;

			return response;
		}

		AppResponse<List<ITarefaDefinition>> GenerateListMockResponseSuccess()
		{
			var mockRetorno = TarefaModelStub.MockConsultaTarefa();
			var list = new List<ITarefaDefinition>();
			list.Add(mockRetorno);

			var response = new AppResponse<List<ITarefaDefinition>>();
			response.Response = list;
			response.Result = true;

			return response;
		}
		TarefasController GetController() => new TarefasController(_tarefaApp.Object, _customLog.Object);
	}
}
