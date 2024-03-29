using MongoDB.Driver;

namespace Tarefas.Core.Infra.Repository.Mongo;

public interface IMongoClient
{
    IMongoDatabase GetDataBase();
}
