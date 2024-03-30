using Tarefas.Core.Domain.Models.Cliente;
using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Core.Infra.Repository.Mongo.Dominio;

/// <summary>
/// Usei a Interface da service, por que não vejo necessidade de criar mais interfaces apenas para buscas dados simples.
/// </summary>
[ExcludeFromCodeCoverage]
public class DominiosRepo : IDominioService
{
    readonly IMongoClient _mongoClient;

    public DominiosRepo(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<List<CategoriaView>> ListarCategorias()
        => await _mongoClient.GetDataBase().GetCollection<CategoriaView>(CategoriaView._collectionName).FindSync(FilterDefinition<CategoriaView>.Empty).ToListAsync();
}
