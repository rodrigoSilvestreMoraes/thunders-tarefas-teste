using Tarefas.Core.Domain.Models.Cliente;
using MongoDB.Bson;

namespace Tarefas.Test.Stubs
{
	public static class ClienteViewMock
	{
		public static List<CategoriaView> Listar()
		{
			var result = new List<CategoriaView>();

			for (int i = 0; i < 5; i++)
			{
				result.Add(MockClienteView());
			}
			return result;
		}

		public static CategoriaView MockClienteView()
		{
			return new CategoriaView
			{
				Id = ObjectId.GenerateNewId(),
				Nome = "TESTE"
			};
		}
	}
}
