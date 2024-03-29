using Tarefas.Api.Filters;
using Tarefas.Api.Host;
using Tarefas.Api.Models;
using Tarefas.Core.Infra.BootStrap;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Net.Mime;

namespace Tarefas.Api
{
	[ExcludeFromCodeCoverage]
	public class Startup
	{
		public IConfiguration Configuration { get; set; }
		const string _title = "Tarefas - API";
		const string _version = "v1.0.0";

		public Startup()
		{
			var builder = new ConfigurationBuilder();
			builder.AddJsonFile("appsettings.json").AddEnvironmentVariables();
			builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true);
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvcCore().AddApiExplorer();
			services.AddMvcCore().AddControllersAsServices();
			
			Configuration = StartupBuilderTarefasCore.BuildSettings(Configuration);			
			services = StartupBuilderTarefasCore.BuildServices(services, Configuration);

			services.AddHealthChecks();

			services.AddHostedService<EventQueueService>();

			services.AddControllersWithViews()
				.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

			services.AddScoped<ControllerValidationsFilter>();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = _title,
					Version = _version
				});

				c.EnableAnnotations();
				c.CustomSchemaIds(x => x.FullName);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		[ExcludeFromCodeCoverage]
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHealthChecks("/health",
				new HealthCheckOptions()
				{
					ResponseWriter = async (context, report) =>
					{
						var result = new
							{
								currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
								statusApplication = report.Status.ToString(),
							};

						context.Response.ContentType = MediaTypeNames.Application.Json;
						await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
					}
				});

			app.UseExceptionHandler(a => a.Run(async context =>
			{
				var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
				var exception = exceptionHandlerPathFeature.Error;

				var vndErrors = new ErroPadrao { Code = "InternalServerError", Message = new List<string> { "Falha na API, tente novamente, se o erro persistir entre em contato com suporte." } };
				
				var result = JsonConvert.SerializeObject(vndErrors);

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsync(result);

			}));

			const string pathBase = "/fluxo-caixa";
			app.UsePathBase(new PathString(pathBase));

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", _title);
				c.RoutePrefix = string.Empty;
			});

			app.UseRouting();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });			
	

			var cultureinfo = new CultureInfo("pt-BR");
			CultureInfo.DefaultThreadCurrentCulture = cultureinfo;
			CultureInfo.DefaultThreadCurrentUICulture = cultureinfo;
		}
	}
}
