using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Core.Infra.Rest.Error;

[ExcludeFromCodeCoverage]
public class RestClientVndErrors
{
	[JsonProperty("_embedded")]
	public Embedded VndErros { get; set; } = new Embedded();
	public static RestClientVndErrors CreateError(ErrorDetail errorDetail)
	{
		var result = new RestClientVndErrors();
		result.VndErros = new Embedded();
		result.VndErros.Errors = new List<ErrorDetail>();
		result.VndErros.Errors.Add(errorDetail);
		return result;
	}
}

[ExcludeFromCodeCoverage]
public class Embedded
{
	[JsonProperty("errors")]
	public List<ErrorDetail> Errors { get; set; } = new List<ErrorDetail>();
}

[ExcludeFromCodeCoverage]
public class ErrorDetail
{
	[JsonProperty("logref")]
	public string Logref { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }

	[JsonProperty("errorCode")]
	public string ErrorCode { get; set; }

	[JsonProperty("path")]
	public string Path { get; set; }
}
