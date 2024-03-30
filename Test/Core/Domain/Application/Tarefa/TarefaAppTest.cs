using MongoDB.Driver.Linq;
using Moq;
using Tarefas.Core.Domain.Application.Tarefa;
using Tarefas.Core.Domain.Application.Tarefa.Validator;
using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.ServiceBusiness.Tarefas;
using Tarefas.Test.Stubs;

namespace Tarefas.Test.Core.Domain.Application.Tarefa
{
	public class TarefaAppTest
	{
		readonly TarefaCriacaoValidator _validatorCriarTarefa;
		readonly TarefaAtualizacaoValidator _validatorAlterarTarefa;
		readonly Mock<ITarefaService> _service;

		public TarefaAppTest() 
		{
			_validatorCriarTarefa = new TarefaCriacaoValidator();
			_validatorAlterarTarefa = new TarefaAtualizacaoValidator();
			_service = new Mock<ITarefaService>();
		}

		[Fact]
		public async void Deveria_CriarTarefa() 
		{ 
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			var mockRetorno = TarefaModelStub.MockConsultaTarefa() as ITarefaDefinition;
			_service.Setup(x => x.Criar(It.IsAny<ITarefaDefinition>())).Returns(Task.FromResult(mockRetorno));

			var app = GetApp();
			var result = await app.InserirTarefa(mockInsercao);
			Assert.False(result.Invalid);
			Assert.NotNull(result.Response);
		}

		[Fact]
		public async void Nao_Deveria_CriarTarefa()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			mockInsercao.Nome = "";
			var mockRetorno = TarefaModelStub.MockConsultaTarefa() as ITarefaDefinition;
			_service.Setup(x => x.Criar(It.IsAny<ITarefaDefinition>())).Returns(Task.FromResult(mockRetorno));

			var app = GetApp();
			var result = await app.InserirTarefa(mockInsercao);
			Assert.True(result.Invalid);
		}

		[Fact]
		public async void Deveria_AlterarTarefa()
		{
			var mockAlterar = TarefaModelStub.MockAlterarTarefa();
			_service.Setup(x => x.Alterar(It.IsAny<ITarefaDefinition>())).Returns(Task.FromResult(true));

			var app = GetApp();
			var result = await app.AlterarTarefa(mockAlterar);
			Assert.True(result.Response);
		}

		[Fact]
		public async void Nao_Deveria_AlterarTarefa()
		{
			var mockAlterar = TarefaModelStub.MockAlterarTarefa();
			mockAlterar.Nome = "";
			_service.Setup(x => x.Alterar(It.IsAny<ITarefaDefinition>())).Returns(Task.FromResult(true));

			var app = GetApp();
			var result = await app.AlterarTarefa(mockAlterar);
			Assert.True(result.Invalid);
		}

		[Fact]
		public async void Deveria_AlterarStatusTarefa()
		{
			_service.Setup(x => x.AlterarStatus(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(true));

			var app = GetApp();
			var result = await app.AlterarStatus(Guid.NewGuid().ToString(), 1);
			Assert.True(result.Response);
		}

		[Fact]
		public async void Deveria_ApagarTarefa()
		{
			_service.Setup(x => x.Apagar(It.IsAny<string>())).Returns(Task.FromResult(true));

			var app = GetApp();
			var result = await app.ApagarTarefa(Guid.NewGuid().ToString());
			Assert.True(result.Response);
		}

		[Fact]
		public async void Deveria_PegarTarefa()
		{
			var mockRetorno = TarefaModelStub.MockConsultaTarefa();

			_service.Setup(x => x.Pegar(It.IsAny<string>())).Returns(Task.FromResult(mockRetorno));

			var app = GetApp();
			var result = await app.PegarTarefa(Guid.NewGuid().ToString());
			Assert.NotNull(result.Response);
		}

		[Fact]
		public async void Deveria_ListarTarefaPorUsuario()
		{
			var mockRetorno = TarefaModelStub.MockListaTarefas();
			_service.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(mockRetorno));

			var app = GetApp();
			var result = await app.ConsultarTarefas("teste");
			Assert.NotNull(result.Response);
			Assert.NotEmpty(result.Response);
		}

		[Fact]
		public async void Deveria_ListarTarefaPorUsuarioEPeriodo()
		{
			var mockRetorno = TarefaModelStub.MockListaTarefas();
			_service.Setup(x => x.Consultar(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(mockRetorno));

			var app = GetApp();
			var result = await app.ConsultarTarefas("teste", "2024-01-01", "2024-03-03");
			Assert.NotNull(result.Response);
			Assert.NotEmpty(result.Response);
		}

		[Fact]
		public async void Deveria_ListarTarefaPorPeriodo()
		{
			var mockRetorno = TarefaModelStub.MockListaTarefas();
			_service.Setup(x => x.Consultar(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(mockRetorno));

			var app = GetApp();
			var result = await app.ConsultarTarefas("2024-01-01", "2024-03-03");
			Assert.NotNull(result.Response);
			Assert.NotEmpty(result.Response);
		}

		ITarefaApp GetApp()=> new TarefaApp(_service.Object, _validatorCriarTarefa, _validatorAlterarTarefa);
	}
}
