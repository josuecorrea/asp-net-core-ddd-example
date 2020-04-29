using AspNetCore.Example.Application.Contracts.Implements;
using AspNetCore.Example.Application.Contracts.Repositories;
using AspNetCore.Example.Application.Handler;
using AspNetCore.Example.Domain.Contracts.Repositories;
using AspNetCore.Example.Infra.Repositories;
using AspNetCore.Example.Infra.Repositories.Interfaces;
using AspNetCore.Example.Infra.Services.Contracts;
using AspNetCore.Example.Infra.Services.Implements;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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

            //Handlers
            services.AddMediatR(typeof(GetInfomationByDocumentHandler).Assembly);
        }
    }
}
