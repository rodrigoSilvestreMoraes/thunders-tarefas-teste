using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Core.Domain.Models.CustomLog;

/// <summary>
/// Estou usando um modelo simples, mas poderia adotar modelos de mercado, como Gelf do graylog
/// </summary>

[ExcludeFromCodeCoverage]
public class LogDetail
{
	public const string _collectionName = "CustomLog";
	public static LogDetail Build(
	   string nomeModulo,
	   string nomeAcao,
	   string descricao,
	   string erro,
	   string payload,
	   bool isError)
	{
		return new LogDetail
		{
			NomeModulo = nomeModulo,
			NomeAcao = nomeAcao,
			Descricao = descricao,
			DataCadastro = DateTime.Now,
			Erro = erro,
			Payload = payload,
			IsError = isError
		};
	}
	public string NomeModulo { get; private set; }
	public string NomeAcao { get; private set; }
	public string Descricao { get; private set; }
	public string Erro { get; private set; }
	public string Payload { get; private set; }
	public DateTime DataCadastro { get; private set; }
	public bool IsError { get; private set; }
}
