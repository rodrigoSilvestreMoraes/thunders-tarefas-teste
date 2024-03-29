using Tarefas.Core.Domain.Models.Cliente;

namespace Tarefas.Core.Domain.ServiceBusiness.Dominios;
public interface IDominioService
{
	Task<List<CategoriaView>> ListarCategorias();		
}
