using Tarefas.Core.Domain.Models.Despesa;
using Tarefas.Core.Domain.Models.Transacao;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Tarefas.Core.Domain.ServiceBusiness.RegistroFluxo;
using Tarefas.Core.Infra.CustomLog;
using Tarefas.Core.Infra.Repository.Transacao;
using Tarefas.Test.Stubs;
using Moq;

namespace Tarefas.Test.Core.Domain.ServiceBusiness.RegistroFluxo
{
	public class RegistroFluxoDespesaServiceTest
	{
		readonly Mock<IDominioService> _dominioService;
		readonly Mock<IRegistroRepo> _registroRepo;
		readonly Mock<ICustomLogService> _customLogService;

		public RegistroFluxoDespesaServiceTest()
		{
			_dominioService = new Mock<IDominioService>();
			_registroRepo = new Mock<IRegistroRepo>();
			_customLogService = new Mock<ICustomLogService>();
		}

		[Fact]
		public async void Deveria_RegistrarDespesa()
		{
			_dominioService.Setup(x => x.ConsultarFornecedor(It.IsAny<string>())).Returns(Task.FromResult(FornecedorViewMock.MockFornecedorView()));
			_dominioService.Setup(x => x.ConsultarDespesa(It.IsAny<string>())).Returns(Task.FromResult(DespesaViewMock.MockDespesaView()));
			var request = RegistroDespesaMock.MockRegistroDespesa(_dominioService);

			_registroRepo.Setup(x => x.CriarDespesa(It.IsAny<RegistroDespesa>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarDespesa(request);
			Assert.False(result.Erro);
			Assert.NotNull(result.Data);
		}


		[Fact]
		public async void Nao_Deveria_RegistrarDespesa_Exception()
		{
			_dominioService.Setup(x => x.ConsultarFornecedor(It.IsAny<string>())).Returns(Task.FromResult(FornecedorViewMock.MockFornecedorView()));
			_dominioService.Setup(x => x.ConsultarDespesa(It.IsAny<string>())).Returns(Task.FromResult(DespesaViewMock.MockDespesaView()));
			var request = RegistroDespesaMock.MockRegistroDespesa(_dominioService);

			_registroRepo.Setup(x => x.CriarDespesa(It.IsAny<RegistroDespesa>())).Throws(new Exception("ERROR"));

			var result = await GetService().RegistrarDespesa(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarDespesa_DespesaInexistente()
		{
			_dominioService.Setup(x => x.ConsultarFornecedor(It.IsAny<string>())).Returns(Task.FromResult(FornecedorViewMock.MockFornecedorView()));
			var request = RegistroDespesaMock.MockRegistroDespesa(_dominioService);

			_registroRepo.Setup(x => x.CriarDespesa(It.IsAny<RegistroDespesa>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarDespesa(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarDespesa_FornecedorInexistente()
		{
			_dominioService.Setup(x => x.ConsultarDespesa(It.IsAny<string>())).Returns(Task.FromResult(DespesaViewMock.MockDespesaView()));
			var request = RegistroDespesaMock.MockRegistroDespesa(_dominioService);

			_registroRepo.Setup(x => x.CriarDespesa(It.IsAny<RegistroDespesa>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarDespesa(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarDespesa_Valor_Igual_0()
		{
			_dominioService.Setup(x => x.ConsultarFornecedor(It.IsAny<string>())).Returns(Task.FromResult(FornecedorViewMock.MockFornecedorView()));
			_dominioService.Setup(x => x.ConsultarDespesa(It.IsAny<string>())).Returns(Task.FromResult(DespesaViewMock.MockDespesaView()));
			var request = RegistroDespesaMock.MockRegistroDespesa(_dominioService);
			request.Valor = 0;

			_registroRepo.Setup(x => x.CriarDespesa(It.IsAny<RegistroDespesa>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarDespesa(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		IRegistroFluxoService GetService() => new RegistroFluxoService(_registroRepo.Object, _customLogService.Object);
	}
}
