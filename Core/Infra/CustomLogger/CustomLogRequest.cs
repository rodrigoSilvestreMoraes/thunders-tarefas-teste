namespace Tarefas.Core.Infra.CustomLogger;
public class CustomLogRequest
{
	private CustomLogRequest() { }
	public static CustomLogRequest Create(string api, string route, Exception ex) => new CustomLogRequest { API = api, Route = route, Ex = ex };
	public DateTime DataCadastro { get; set; } = DateTime.Now;
	public Exception Ex { get; set; }
	public string API { get; set; }
	public string Route { get; set; }
}
