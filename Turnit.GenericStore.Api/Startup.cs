using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NHibernate;
using Turnit.GenericStore.Application.Service;
using Turnit.GenericStore.Domain.Interface.Service;
using Turnit.GenericStore.Infrastructure.Mapping;

namespace Turnit.GenericStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton(CreateSessionFactory);
            
            services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStoreService, StoreService>();
            
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Turnit Store"
            }));
        }
        
        private ISessionFactory CreateSessionFactory(IServiceProvider context)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            var configuration = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    .Dialect<NHibernate.Dialect.PostgreSQL82Dialect>()
                    .ConnectionString(connectionString))
                .Mappings(x =>
                {
                    x.FluentMappings.AddFromAssemblyOf<ProductMap>();
                });

            return configuration.BuildSessionFactory();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger()
                .UseSwaggerUI(x => x.SwaggerEndpoint("v1/swagger.json", "Turnit Store V1"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}