using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Tarefas.Core.Domain.Models;

[ExcludeFromCodeCoverage]
public class FlatData
{
	[JsonIgnore]
	public ObjectId Id { get; set; }

	public string Codigo
	{
		get
		{
			return Id.ToString();
		}
	}		
	public string Nome { get; set; }
}
