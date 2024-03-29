using Tarefas.Core.Domain.Models.Fornecedore;
using MongoDB.Bson;

namespace Tarefas.Test.Stubs
{
	public static class FornecedorViewMock
	{
		public static List<FornecedorView> Listar()
		{
			var result = new List<FornecedorView>();

			for (int i = 0; i < 5; i++)
			{
				result.Add(MockFornecedorView());
			}
			return result;
		}
		public static FornecedorView MockFornecedorView()
		{
			return new FornecedorView
			{
				 Id = ObjectId.GenerateNewId(),
				 Nome = "TESTE"
			};
		}
	}
}
