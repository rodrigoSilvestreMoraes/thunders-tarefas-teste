using Microsoft.AspNetCore;
using System.Diagnostics.CodeAnalysis;
using Tarefas.Api;

[ExcludeFromCodeCoverage]
public static class Program
{
	public static void Main(string[] args)
	{
		CreateWebHostBuilder(args).Build().Run();
	}
	public static IWebHostBuilder CreateWebHostBuilder(string[] args)
	{
		return WebHost.CreateDefaultBuilder(args).UseKestrel().UseStartup<Startup>();
	}
}