using Tarefas.Core.Domain.Models.Receita;
using MongoDB.Bson;

namespace Tarefas.Test.Stubs
{
	public static class ReceitaViewMock
	{
		public static List<ReceitaView> Listar()
		{
			var result = new List<ReceitaView>();

			for (int i = 0; i < 5; i++)
			{
				result.Add(MockReceitaView());
			}
			return result;
		}
		public static ReceitaView MockReceitaView()
		{
			return new ReceitaView
			{
				Id = ObjectId.GenerateNewId(),
				Nome = "TESTE"
			};
		}
	}
}