using Tarefas.Core.Domain.Models.CustomLog;
using Tarefas.Core.Infra.EventBus;
using System.Diagnostics.CodeAnalysis;

using ClientMongo = Tarefas.Core.Infra.Repository.Mongo;

namespace Tarefas.Core.Infra.Repository.CustomLog;

[ExcludeFromCodeCoverage]
public class CustomLogRepo : ICustomLogRepo
{
	readonly ClientMongo.IMongoClient _mongoClient;
	readonly BackgroundWorkerQueue _backgroundWorkerQueue;

	public CustomLogRepo(ClientMongo.IMongoClient mongoClient, BackgroundWorkerQueue backgroundWorkerQueue)
	{
		_mongoClient = mongoClient;
		_backgroundWorkerQueue = backgroundWorkerQueue;
	}
	public void GravarLog(LogDetail logDetail)
	{
		_backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
		{
			await _mongoClient.GetDataBase().GetCollection<LogDetail>(LogDetail._collectionName).InsertOneAsync(logDetail);
		});
	}
}
 