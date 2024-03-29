using Tarefas.Core.Domain.Models.CustomLog;

namespace Tarefas.Core.Infra.Repository.CustomLog;

public interface ICustomLogRepo
{
	void GravarLog(LogDetail logDetail);
}
