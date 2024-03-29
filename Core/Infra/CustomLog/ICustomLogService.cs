using Tarefas.Core.Domain.Models.CustomLog;

namespace Tarefas.Core.Infra.CustomLog;

public interface ICustomLogService
{
	void SaveLog(LogDetail logDetail);
	void SaveLogAlert(string modulo, string acao, string message);
}
