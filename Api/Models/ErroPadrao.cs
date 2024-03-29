namespace Tarefas.Api.Models
{
	public class ErroPadrao
	{
		public string Code { get; set; }
		public List<string> Message { get; set; } = new List<string>();
	}
}
