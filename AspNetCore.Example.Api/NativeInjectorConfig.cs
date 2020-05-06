using AspNetCore.Example.Application.Behaviors;
using AspNetCore.Example.Application.Contracts.Implements;
using AspNetCore.Example.Application.Contracts.Repositories;
using AspNetCore.Example.Application.Handler;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Infra.Repositories;
using AspNetCore.Example.Infra.Repositories.Interfaces;
using AspNetCore.Example.Infra.Services.Contracts;
using AspNetCore.Example.Infra.Services.Implements;
using EasyNetQ;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AspNetCore.Example.Api
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyGateway, CompanyGateway>();
            services.AddScoped<ICacheService, CacheService>();
            //services.AddScoped<IMessageService, MessageService>();
            //services.AddScoped<IMessageReceiverService, MessageReceiverService>();
            //services.AddScoped<IUserContextAcessorRepository, UserContextAcessorRepository>();

            //Handlers
            services.AddMediatR(typeof(GetInfomationByDocumentHandler).Assembly);
            services.AddMediatR(typeof(UserHandler).Assembly);

            const string applicationAssemblyName = "AspNetCore.Example.Application";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner
               .FindValidatorsInAssembly(assembly)
               .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));


            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            //BUS
            //string rabbitmqConnection = "";
            //services.AddSingleton<IBus>(RabbitHutch.CreateBus(rabbitmqConnection));
            //services.BuildServiceProvider().GetService<IMessageReceiverService>().Receiver<object>();
        }
    }
}
