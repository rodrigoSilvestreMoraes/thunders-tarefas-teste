using Tarefas.Core.Domain.Models.Transacao;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using MongoDB.Bson;
using Moq;

namespace Tarefas.Test.Stubs
{
	public static class RegistroDespesaMock
	{
		public static RegistroDespesa MockRegistroDespesa(Mock<IDominioService> dominioService)
		{
			return new RegistroDespesa(dominioService.Object, codigoDespesa: "dsdsdsds", codigoFornecedor: "dsdsdsd")
			{
				 Id = ObjectId.GenerateNewId(),
				 DataPagamento = DateTime.Now.AddDays(4),
				 Descricao = "teste",
				 Valor = 100				 
			};
		}
	}
}
