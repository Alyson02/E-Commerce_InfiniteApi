using AutoMapper.EquivalencyExpression;
using FluentValidation;
using Infinite.Core.Business.Services.Base;
using Infinite.Core.Business.Services.Carrinho;
using Infinite.Core.Business.Services.Token;
using Infinite.Core.Context;
using Infinite.Core.Context.Seed;
using Infinite.Core.Infrastructure.Middleware;
using Infinite.Core.Infrastructure.Pipelines;
using Infinite.Core.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfiniteDbContext(this IServiceCollection services, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

            services.AddDbContext<InfiniteContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddCollectionMappers();
            });

            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            foreach (var implementationType in typeof(InfiniteContext)
            .Assembly
            .ExportedTypes
            .Where(t => t.IsClass && !t.IsAbstract))
            {
                foreach (var serviceType in implementationType
                    .GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                {
                    services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped));
                }
            }

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            return services;
        }

        public static void AddErrorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static void RunMigration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<InfiniteContext>();
            context.Database.Migrate();

            foreach (var entity in InitialSeed.Categoria)
            {
                if (!context.Categoria.Any(x => x.Categoria.Equals(entity.Categoria)))
                {
                    context.Categoria.Add(entity);
                    context.SaveChanges();
                }
            }

            foreach (var entity in InitialSeed.TipoCupom)
            {
                if (!context.TipoCupom.Any(x => x.Descricao.Equals(entity.Descricao)))
                {
                    context.TipoCupom.Add(entity);
                    context.SaveChanges();
                }
            }

            foreach (var entity in InitialSeed.Produto)
            {
                if (!context.Produto.Any(x => x.Nome.Equals(entity.Nome)))
                {
                    context.Produto.Add(entity);
                    context.SaveChanges();
                }
            }

            foreach (var entity in InitialSeed.FormaPag)
            {
                if (!context.FormaPag.Any(x => x.Frm.Equals(entity.Frm)))
                {
                    context.FormaPag.Add(entity);
                    context.SaveChanges();
                }
            }

            foreach (var entity in InitialSeed.TipoUsuario)
            {
                if (!context.TipoUsuario.Any(x => x.Role.Equals(entity.Role)))
                {
                    context.TipoUsuario.Add(entity);
                    context.SaveChanges();
                }
            }

            foreach (var entity in InitialSeed.Funcionario)
            {
                if (!context.Funcionario.Any(x => x.Nome.Equals(entity.Nome)))
                {
                    context.Funcionario.Add(entity);
                    context.SaveChanges();
                }
            }

            foreach (var entity in InitialSeed.Cliente)
            {
                if (!context.Cliente.Any(x => x.Nome.Equals(entity.Nome)))
                {
                    context.Cliente.Add(entity);
                    context.SaveChanges();
                }
            }

        }
    }
}
