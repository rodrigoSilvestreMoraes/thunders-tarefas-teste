using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using Tarefas.Core.Domain.Entities.Tarefas;
using Tarefas.Core.Domain.Repositorys.Tarefa;

namespace Tarefas.Core.Infra.Repository.Mongo.Tarefas;

[ExcludeFromCodeCoverage]
public class TarefaRepo : ITarefaRepo
{
    readonly IMongoClient _mongoClient;
    const string _collectionPrincipal = "Tarefas";

    public TarefaRepo(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<TarefaEntitie> Criar(TarefaEntitie tarefaEntitie)
    {
        tarefaEntitie.SetId(ObjectId.GenerateNewId().ToString());
        await GetCollection().InsertOneAsync(tarefaEntitie);
        return tarefaEntitie;
    }

    public async Task<bool> Alterar(TarefaEntitie tarefaEntitie)
    {
        var filter = Builders<TarefaEntitie>.Filter.Eq(_ => _.Id, tarefaEntitie.Id);
        var update = Builders<TarefaEntitie>.Update
             .Set(body => body.Nome, tarefaEntitie.Nome)
             .Set(body => body.Detalhe, tarefaEntitie.Detalhe)
             .Set(body => body.DataFinal, tarefaEntitie.DataFinal)
             .Set(body => body.Usuario, tarefaEntitie.Usuario)
             .Set(body => body.Prioridade, tarefaEntitie.Prioridade)
             .Set(body => body.CategoriaId, tarefaEntitie.CategoriaId)
             .Set(body => body.SemPrazo, tarefaEntitie.SemPrazo);

        var result = await GetCollection().UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> AlterarStatus(string tarefaId, EnumStatusTarefa status)
    {
        var filter = Builders<TarefaEntitie>.Filter.Eq(_ => _.Id, tarefaId);
        var update = Builders<TarefaEntitie>.Update
             .Set(body => body.Status, status);

        var result = await GetCollection().UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> Apagar(string tarefaId)
    {
        var builder = Builders<TarefaEntitie>.Filter;
        var filters = new List<FilterDefinition<TarefaEntitie>>
        {
            builder.Eq(x => x.Id, tarefaId)
        };
        var filter = builder.And(filters);
        var result = await GetCollection().DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task<TarefaEntitie> ExisteTarefaPorNome(string nome)
    {
		var builder = Builders<TarefaEntitie>.Filter;
		var filters = new List<FilterDefinition<TarefaEntitie>>
		{
			builder.Eq(x => x.Nome, nome.Trim().ToLower())
		};
		var filter = builder.And(filters);

		var result = await GetCollection().FindAsync(filter);
		return result.FirstOrDefault();
	}
    public async Task<List<TarefaEntitie>> Consultar(string usuario)
    {
        var builder = Builders<TarefaEntitie>.Filter;
        var filters = new List<FilterDefinition<TarefaEntitie>>
        {
            builder.Eq(x => x.Usuario, usuario)
        };
        var filter = builder.And(filters);

        var result = await GetCollection().FindAsync(filter);
        return result.ToList();
    }

    public async Task<List<TarefaEntitie>> Consultar(DateTime dataInicio, DateTime dataFinal)
    {
        var result = await GetCollection().FindAsync(GetFilterDate(dataInicio, dataFinal));
        return result.ToList();
    }

    public async Task<List<TarefaEntitie>> Consultar(string usuario, DateTime dataInicio, DateTime dataFinal)
    {
        var builder = Builders<TarefaEntitie>.Filter;
        var filters = new List<FilterDefinition<TarefaEntitie>>
        {
            builder.Eq(x => x.Usuario, usuario),
            GetFilterDate(dataInicio, dataFinal)
        };
        var filter = builder.And(filters);

        var result = await GetCollection().FindAsync(filter);
        return result.ToList();
    }

    FilterDefinition<TarefaEntitie> GetFilterDate(DateTime dataInicio, DateTime dataFinal)
    {
        var builder = Builders<TarefaEntitie>.Filter;

        var start = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day, 0, 0, 0);
        var end = new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day, 23, 59, 59);
        var filter = builder.Gte(x => x.DataInicio, start) & builder.Lt(x => x.DataFinal, end);

        return filter;
    }

    public async Task<TarefaEntitie> Pegar(string tarefaId)
    {
        var builder = Builders<TarefaEntitie>.Filter;
        var filters = new List<FilterDefinition<TarefaEntitie>>
        {
            builder.Eq(x => x.Id, tarefaId)
        };
        var filter = builder.And(filters);

        var result = await GetCollection().FindAsync(filter);
        return result.FirstOrDefault();
    }
    IMongoCollection<TarefaEntitie> GetCollection() => _mongoClient.GetDataBase().GetCollection<TarefaEntitie>(_collectionPrincipal);
}