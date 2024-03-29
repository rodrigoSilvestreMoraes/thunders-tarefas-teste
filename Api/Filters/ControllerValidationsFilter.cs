using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Tarefas.Core.Infra.Rest.Error;

namespace Tarefas.Api.Filters;

[ExcludeFromCodeCoverage]
public class ControllerValidationsFilter : IResultFilter
{
	public void OnResultExecuted(ResultExecutedContext context)
	{
	}
	public void OnResultExecuting(ResultExecutingContext context)
	{
		if (!context.ModelState.IsValid)
		{
			List<string> errors = new List<string>();

			foreach (ModelStateEntry val in context.ModelState.Values)
			{
				if (val.ValidationState == ModelValidationState.Invalid && val.Errors.Count > 0)
					errors.Add("Invalid argument: " + val.Errors[0].ErrorMessage);
			}

			var error = new RestClientVndErrors { VndErros = new Embedded() };
			error.VndErros.Errors = new List<ErrorDetail>();

			errors.ForEach((item) =>
			{
				error.VndErros.Errors.Add(new ErrorDetail { ErrorCode = "422", Message = item });
			});

			var objResult = new ObjectResult(error);

			objResult.StatusCode = 422;
			context.Result = objResult;
		}
	}
}
