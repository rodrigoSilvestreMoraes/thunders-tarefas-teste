using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Tarefas.Api.Models;

namespace Tarefas.Api.Filters
{
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

				var vndErrors = new ErroPadrao();
				
				errors.ForEach((item) =>
				{
					vndErrors.Message.Add(item);
				});

				var objResult = new ObjectResult(vndErrors);

				objResult.StatusCode = 422;
				context.Result = objResult;
			}
		}
	}
}
