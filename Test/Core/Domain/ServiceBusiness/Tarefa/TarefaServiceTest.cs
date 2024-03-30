using Moq;
using System.ComponentModel.DataAnnotations;
using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Domain.Repositorys.Tarefa;
using Tarefas.Core.Domain.ServiceBusiness.Tarefas;
using Tarefas.Core.Infra.Repository.Mongo.Tarefas;
using Tarefas.Test.Stubs;

namespace Tarefas.Test.Core.Domain.ServiceBusiness.Tarefa
{
	public class TarefaServiceTest
	{
		readonly Mock<ITarefaRepo> _tarefaRepo;
		public TarefaServiceTest() 
		{
			_tarefaRepo = new Mock<ITarefaRepo>();
		}

		[Fact]
		public async void Deveria_Criar()
		{
			TarefaEntitie existeTarefa = null;
			_tarefaRepo.Setup(x => x.ExisteTarefaPorNome(It.IsAny<string>())).Returns(Task.FromResult(existeTarefa));

			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			var entitie = TarefaEntitie.BuilderForInsert(mockInsercao);
			entitie.SetId(Guid.NewGuid().ToString());

			_tarefaRepo.Setup(x => x.Criar(It.IsAny<TarefaEntitie>())).Returns(Task.FromResult(entitie));

			var result = await GetService().Criar(mockInsercao);
			Assert.NotNull(result);
		}

		[Fact]
		public async void Nao_Deveria_Criar_Existe_Tarefa_MesmoNome()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			TarefaEntitie existeTarefa = TarefaEntitie.BuilderForInsert(mockInsercao);

			_tarefaRepo.Setup(x => x.ExisteTarefaPorNome(It.IsAny<string>())).Returns(Task.FromResult(existeTarefa));
		
			var result = await GetService().Criar(mockInsercao);
			Assert.NotNull(result);
		}

		[Fact]
		public async void Deveria_Alterar()
		{
			TarefaEntitie existeTarefa = null;
			_tarefaRepo.Setup(x => x.ExisteTarefaPorNome(It.IsAny<string>())).Returns(Task.FromResult(existeTarefa));

			var mockAlteracao = TarefaModelStub.MockAlterarTarefa();
			
			_tarefaRepo.Setup(x => x.Alterar(It.IsAny<TarefaEntitie>())).Returns(Task.FromResult(true));

			var result = await GetService().Alterar(mockAlteracao);
			Assert.True(result);
		}

		[Fact]
		public async void Nao_Deveria_Alterar_Existe_Tarefa_MesmoNome()
		{
			var mockAlteracao = TarefaModelStub.MockAlterarTarefa();
			TarefaEntitie existeTarefa = TarefaEntitie.BuilderForUpdate(mockAlteracao);
			existeTarefa.SetId(Guid.NewGuid().ToString());

			_tarefaRepo.Setup(x => x.ExisteTarefaPorNome(It.IsAny<string>())).Returns(Task.FromResult(existeTarefa));

			var result = await GetService().Alterar(mockAlteracao);
			Assert.False(result);
		}

		[Fact]
		public async void Deveria_Apagar()
		{
			_tarefaRepo.Setup(x => x.Apagar(It.IsAny<string>())).Returns(Task.FromResult(true));
			var result = await GetService().Apagar(Guid.NewGuid().ToString());
			Assert.True(result);
		}


		[Fact]
		public async void Deveria_Alterar_Status()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			var entitie = TarefaEntitie.BuilderForInsert(mockInsercao);
			entitie.SetId(Guid.NewGuid().ToString());

			_tarefaRepo.Setup(x => x.Pegar(It.IsAny<string>())).Returns(Task.FromResult(entitie));
			_tarefaRepo.Setup(x => x.AlterarStatus(It.IsAny<string>(), It.IsAny<EnumStatusTarefa>())).Returns(Task.FromResult(true));

			var result = await GetService().AlterarStatus(Guid.NewGuid().ToString(), (int)EnumStatusTarefa.Pausa);
			Assert.True(result);
		}

		[Fact]
		public async void Deveria_Alterar_Status_Tarefa_NaoExiste()
		{
			TarefaEntitie entitie = null;

			_tarefaRepo.Setup(x => x.Pegar(It.IsAny<string>())).Returns(Task.FromResult(entitie));

			var result = await GetService().AlterarStatus(Guid.NewGuid().ToString(), (int)EnumStatusTarefa.Pausa);
			Assert.False(result);
		}

		[Fact]
		public async void Deveria_Alterar_Status_StatusInvalido()
		{
			var entitie = GenerateMock();

			_tarefaRepo.Setup(x => x.Pegar(It.IsAny<string>())).Returns(Task.FromResult(entitie));
			_tarefaRepo.Setup(x => x.AlterarStatus(It.IsAny<string>(), It.IsAny<EnumStatusTarefa>())).Returns(Task.FromResult(true));

			var result = await GetService().AlterarStatus(Guid.NewGuid().ToString(), 9);
			Assert.False(result);
		}

		[Fact] 
		public async void Deveria_Pegar()
		{
			var entitie = GenerateMock();
			_tarefaRepo.Setup(x => x.Pegar(It.IsAny<string>())).Returns(Task.FromResult(entitie));

			var result = await GetService().Pegar(Guid.NewGuid().ToString());
			Assert.NotNull(result);
		}

		[Fact]
		public async void Deveria_Consultar_Usuario()
		{
			var entitie = GenerateMock();
			List<TarefaEntitie> listTarefas = new List<TarefaEntitie> { entitie };

			_tarefaRepo.Setup(x => x.Consultar(It.IsAny<string>())).Returns(Task.FromResult(listTarefas));

			var result = await GetService().Consultar(Guid.NewGuid().ToString());
			Assert.NotNull(result);
			Assert.NotEmpty(result);
		}

		[Fact]
		public async void Deveria_Consultar_Periodo()
		{
			var entitie = GenerateMock();
			List<TarefaEntitie> listTarefas = new List<TarefaEntitie> { entitie };

			_tarefaRepo.Setup(x => x.Consultar(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(listTarefas));

			var result = await GetService().Consultar(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));
			Assert.NotNull(result);
			Assert.NotEmpty(result);
		}

		[Fact]
		public async void Deveria_Consultar_Periodo_Usuario()
		{
			var entitie = GenerateMock();
			List<TarefaEntitie> listTarefas = new List<TarefaEntitie> { entitie };

			_tarefaRepo.Setup(x => x.Consultar(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(listTarefas));

			var result = await GetService().Consultar(Guid.NewGuid().ToString(), DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));
			Assert.NotNull(result);
			Assert.NotEmpty(result);
		}

		TarefaEntitie GenerateMock()
		{
			var mockInsercao = TarefaModelStub.MockCriarTarefa();
			var entitie = TarefaEntitie.BuilderForInsert(mockInsercao);
			entitie.SetId(Guid.NewGuid().ToString());
			return entitie;
		}

		ITarefaService GetService() => new TarefaService(_tarefaRepo.Object);
	}
}
