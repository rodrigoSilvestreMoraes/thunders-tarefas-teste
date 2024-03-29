using Tarefas.Core.Infra.EventBus;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Api.Host
{
	[ExcludeFromCodeCoverage]
	public class EventQueueService : BackgroundService
	{
		readonly BackgroundWorkerQueue queue;
		public EventQueueService(BackgroundWorkerQueue queue)
		{
			this.queue = queue;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				var workItem = await queue.DequeueAsync(stoppingToken);
				await workItem(stoppingToken);
			}
		}
	}
}
