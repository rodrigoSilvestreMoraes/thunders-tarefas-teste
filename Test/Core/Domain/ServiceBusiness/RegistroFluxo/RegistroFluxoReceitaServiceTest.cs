using Tarefas.Core.Domain.Models.Transacao;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Tarefas.Core.Domain.ServiceBusiness.RegistroFluxo;
using Tarefas.Core.Infra.CustomLog;
using Tarefas.Core.Infra.Repository.Transacao;
using Tarefas.Test.Stubs;
using Moq;

namespace Tarefas.Test.Core.Domain.ServiceBusiness.RegistroFluxo
{
	public class RegistroFluxoReceitaServiceTest
	{
		readonly Mock<IDominioService> _dominioService;
		readonly Mock<IRegistroRepo> _registroRepo;
		readonly Mock<ICustomLogService> _customLogService;

		public RegistroFluxoReceitaServiceTest()
		{
			_dominioService = new Mock<IDominioService>();
			_registroRepo = new Mock<IRegistroRepo>();
			_customLogService = new Mock<ICustomLogService>();
		}

		[Fact]
		public async void Deveria_RegistrarReceita()
		{
			_dominioService.Setup(x => x.ConsultarCliente(It.IsAny<string>())).Returns(Task.FromResult(ClienteViewMock.MockClienteView()));
			_dominioService.Setup(x => x.ConsultarReceita(It.IsAny<string>())).Returns(Task.FromResult(ReceitaViewMock.MockReceitaView()));
			
			var request = RegistroReceitaMock.MockRegistroReceita(_dominioService);
			_registroRepo.Setup(x => x.CriarReceita(It.IsAny<RegistroReceita>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarReceita(request);
			Assert.False(result.Erro);
			Assert.NotNull(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarReceita_Exception()
		{
			_dominioService.Setup(x => x.ConsultarCliente(It.IsAny<string>())).Returns(Task.FromResult(ClienteViewMock.MockClienteView()));
			_dominioService.Setup(x => x.ConsultarReceita(It.IsAny<string>())).Returns(Task.FromResult(ReceitaViewMock.MockReceitaView()));

			var request = RegistroReceitaMock.MockRegistroReceita(_dominioService);
			_registroRepo.Setup(x => x.CriarReceita(It.IsAny<RegistroReceita>())).Throws(new Exception("ERROR"));

			var result = await GetService().RegistrarReceita(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarReceita_ReceitaInexistente()
		{
			_dominioService.Setup(x => x.ConsultarCliente(It.IsAny<string>())).Returns(Task.FromResult(ClienteViewMock.MockClienteView()));

			var request = RegistroReceitaMock.MockRegistroReceita(_dominioService);
			_registroRepo.Setup(x => x.CriarReceita(It.IsAny<RegistroReceita>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarReceita(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarReceita_ClienteInexistente()
		{
			_dominioService.Setup(x => x.ConsultarReceita(It.IsAny<string>())).Returns(Task.FromResult(ReceitaViewMock.MockReceitaView()));

			var request = RegistroReceitaMock.MockRegistroReceita(_dominioService);
			_registroRepo.Setup(x => x.CriarReceita(It.IsAny<RegistroReceita>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarReceita(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		[Fact]
		public async void Nao_Deveria_RegistrarReceita_Valor_Igual_0()
		{
			_dominioService.Setup(x => x.ConsultarCliente(It.IsAny<string>())).Returns(Task.FromResult(ClienteViewMock.MockClienteView()));
			_dominioService.Setup(x => x.ConsultarReceita(It.IsAny<string>())).Returns(Task.FromResult(ReceitaViewMock.MockReceitaView()));

			var request = RegistroReceitaMock.MockRegistroReceita(_dominioService);
			request.Valor = 0;
			_registroRepo.Setup(x => x.CriarReceita(It.IsAny<RegistroReceita>())).Returns(Task.FromResult(request));

			var result = await GetService().RegistrarReceita(request);
			Assert.True(result.Erro);
			Assert.NotEmpty(result.Erros);
			Assert.Null(result.Data);
		}

		IRegistroFluxoService GetService() => new RegistroFluxoService(_registroRepo.Object, _customLogService.Object);
	}
}
