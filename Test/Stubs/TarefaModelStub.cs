using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Models.Tarefas;

namespace Tarefas.Test.Stubs
{
	public static class TarefaModelStub
	{
		public static TarefaRegistro MockCriarTarefa()
		{
			return new TarefaRegistro()
			{
				 CategoriaId = Guid.NewGuid().ToString(),
 				 DataInicio = "2024-01-01",
				 DataFinal = "2024-02-01",
				 Detalhe = "teste",
				 Nome = "teste",
				 Prioridade = 1,
				 SemPrazo = false,
				 Usuario = "usuario_teste"				 
			};
		}

		public static TarefaAlteracao MockAlterarTarefa()
		{
			return new TarefaAlteracao()
			{
				CategoriaId = Guid.NewGuid().ToString(),
				DataInicio = "2024-01-01",
				DataFinal = "2024-02-01",
				Detalhe = "teste",
				Nome = "teste",
				Prioridade = 1,
				SemPrazo = false,
				Usuario = "usuario_teste",
				Id = Guid.NewGuid().ToString()
			};
		}

		public static ITarefaDefinition MockConsultaTarefa()
		{
			return new TarefaConsulta
			{
				Id = Guid.NewGuid().ToString(),
				Status = 1,
				CategoriaId = Guid.NewGuid().ToString(),
				DataInicio = "2024-01-01",
				DataFinal = "2024-02-01",
				Detalhe = "teste",
				Nome = "teste",
				Prioridade = 1,
				SemPrazo = false,
				Usuario = "usuario_teste"
				
			};
		}

		public static List<ITarefaDefinition> MockListaTarefas()
		{
			var list = new List<ITarefaDefinition>();
			list.Add(MockConsultaTarefa());
			return list;
		}
	}
}
