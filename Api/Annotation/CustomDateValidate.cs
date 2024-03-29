using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tarefas.Api.Annotation
{
	[ExcludeFromCodeCoverage]
	public class CustomDateValidate : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			try
			{
				var date = (string)value;
				var dateValue = new DateTime(int.Parse(date.Split('-')[0]), int.Parse(date.Split('-')[1]), int.Parse(date.Split('-')[2]));
				return ValidationResult.Success;
			}
			catch
			{
				return new ValidationResult("Invalid Date Format. Expected: yyyy-mm-dd");
			}
		}
	}
}
