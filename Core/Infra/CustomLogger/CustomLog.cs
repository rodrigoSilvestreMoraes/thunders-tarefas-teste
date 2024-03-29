using Tarefas.Core.Domain.Models.CustomLog;
using Tarefas.Core.Infra.Repository.CustomLog;

namespace Tarefas.Core.Infra.CustomLogger;

public class CustomLog : ICustomLog
{
	readonly ICustomLogRepo _customLogRepo;
	public CustomLog(ICustomLogRepo customLogRepo)
	{
		_customLogRepo = customLogRepo;
	}

	public void GravarLog(CustomLogRequest request)
	{
		Console.WriteLine($"API: {request.API} - Route:{request.Route} - Message:{request.Ex.Message}");
		var log = LogDetail.Build(request.API, request.Route, request.Ex.Message, "", "", true);
		_customLogRepo.GravarLog(log);
	}
}
