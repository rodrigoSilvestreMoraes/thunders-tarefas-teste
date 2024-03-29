using Tarefas.Core.Domain.Models.Despesa;
using MongoDB.Bson;

namespace Tarefas.Test.Stubs
{
	public static  class DespesaViewMock
	{
		public static List<DespesaView> Listar()
		{
			var result = new List<DespesaView>();

			for (int i = 0; i < 5; i++)
			{
				result.Add(MockDespesaView());
			}
			return result;
		}
		public static DespesaView MockDespesaView()
		{
			return new DespesaView
			{
			   Id = ObjectId.GenerateNewId(),
			   Nome = "Teste"
			};
		}

	}
}
