﻿using Tarefas.Core.Domain.ServiceBusiness.Dominios;
using Tarefas.Core.Infra.EventBus;
using Tarefas.Core.Infra.Repository.CustomLog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Tarefas.Core.Domain.ServiceBusiness.Tarefas;
using Tarefas.Core.Infra.Repository.Mongo;
using FluentValidation;
using Tarefas.Core.Domain.Models.Tarefas;
using Tarefas.Core.Domain.Application.Tarefa.Validator;
using Tarefas.Core.Domain.Application.Tarefa;
using Tarefas.Core.Domain.Repositorys.Tarefa;
using Tarefas.Core.Infra.Repository.Mongo.Tarefas;
using Tarefas.Core.Infra.CustomLogger;
using Tarefas.Core.Infra.Repository.Mongo.Dominio;

namespace Tarefas.Core.Infra.BootStrap;

[ExcludeFromCodeCoverage]
public static class StartupBuilderTarefasCore
{
	public static IConfiguration BuildSettings(IConfiguration _configuration)
	{
		_configuration = ConfigureSettings(_configuration);
		return _configuration;
	}

	public static IServiceCollection BuildServices(IServiceCollection services, IConfiguration _configuration)
	{

		#region Application

		services.AddScoped<ITarefaApp, TarefaApp>();

		#endregion

		#region Validators

		services.AddScoped<IValidator<TarefaRegistro>, TarefaCriacaoValidator>();
		services.AddScoped<IValidator<TarefaAlteracao>, TarefaAtualizacaoValidator>();

		#endregion

		#region Services

		services.AddSingleton<BackgroundWorkerQueue>();
		services.AddScoped<ITarefaService, TarefaService>();
		services.AddSingleton<ICustomLog, CustomLog>();		

		#endregion

		#region Repositories

		var _mongoConfig = new MongoClient(new MongoConfig
		{
			User = _configuration.GetSection("MongoConfig:User").Value,
			Password = _configuration.GetSection("MongoConfig:Password").Value,
			ConnectionString = _configuration.GetSection("MongoConfig:ConnectionString").Value,
			DataBaseName = _configuration.GetSection("MongoConfig:DataBaseName").Value
		});

		services.AddSingleton<IMongoClient, MongoClient>();
		services.AddSingleton<ITarefaRepo>(x => new TarefaRepo(mongoClient: _mongoConfig));

		services.AddSingleton<IDominioService>(x => new DominiosRepo(mongoClient: _mongoConfig));
		services.AddSingleton<ICustomLogRepo>(x => new CustomLogRepo(mongoClient: _mongoConfig, x.GetRequiredService<BackgroundWorkerQueue>()));			

		#endregion			


		return services;
	}

	static IConfiguration ConfigureSettings(IConfiguration _configuration)
	{
		return _configuration;
	}
}
