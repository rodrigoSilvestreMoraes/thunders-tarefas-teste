using Tarefas.Core.Domain.Models.Transacao;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using MongoDB.Bson;
using Moq;

namespace Tarefas.Test.Stubs
{
	public static class RegistroReceitaMock
	{
		public static RegistroReceita MockRegistroReceita(Mock<IDominioService> dominioService)
		{
			return new RegistroReceita(dominioService.Object, codigoReceita: "fdfdfdf", codigoCliente: "dsdsdsd")
			{
				 Id = ObjectId.GenerateNewId(),
				 DataPagamento = DateTime.Now.AddDays(4),
				 Descricao = "teste",
				 Valor = 100				 
			};
		}
	}
}
