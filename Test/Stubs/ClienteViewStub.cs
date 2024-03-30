using Tarefas.Core.Domain.Models.Cliente;
using MongoDB.Bson;

namespace Tarefas.Test.Stubs
{
	public static class ClienteViewStub
	{
		public static List<CategoriaView> Listar()
		{
			var result = new List<CategoriaView>();

			for (int i = 0; i < 5; i++)
			{
				result.Add(MockCategoriaView());
			}
			return result;
		}

		public static CategoriaView MockCategoriaView()
		{
			return new CategoriaView
			{
				Id = ObjectId.GenerateNewId(),
				Nome = "TESTE"
			};
		}
	}
}
