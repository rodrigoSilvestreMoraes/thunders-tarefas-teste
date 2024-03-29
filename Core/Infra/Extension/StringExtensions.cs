namespace Tarefas.Core.Infra.Extension
{
	public static class StringExtensions
	{
		public static string FirstCharToUpper(this string input) =>
			 string.Join(" ", input.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1)));
	}
}
