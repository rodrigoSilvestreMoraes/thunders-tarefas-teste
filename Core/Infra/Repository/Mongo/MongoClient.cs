using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Core.Infra.Repository.Mongo;

[ExcludeFromCodeCoverage]
public class MongoClient : IMongoClient
{
    private IMongoDatabase _database = null;
    private readonly MongoConfig _mongoConfig;

    private string _authMechanism = "SCRAM-SHA-1";

    public MongoClient(MongoConfig mongoConfig)
    {
        _mongoConfig = mongoConfig;

        if (!string.IsNullOrEmpty(mongoConfig.ConnectionString))
        {
            MongoInternalIdentity internalIdentity = new MongoInternalIdentity(_mongoConfig.DataBaseName, _mongoConfig.User);
            PasswordEvidence passwordEvidence = new PasswordEvidence(_mongoConfig.Password);

            MongoClientSettings settings = new MongoClientSettings();
            settings.ConnectionMode = ConnectionMode.Automatic;
            settings.Credential = new MongoCredential(_authMechanism, internalIdentity, passwordEvidence);

            var clientOne = new MongoDB.Driver.MongoClient(settings);
            if (clientOne == null)
                throw new MongoClientException("Falha ao conectar no mongo");

            _database = clientOne.GetDatabase(mongoConfig.DataBaseName);
        }
    }
    public IMongoDatabase GetDataBase() => _database;
}
