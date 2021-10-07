using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokedexAPI.Views.Responses;
using Refit;
using PokeAPI.Views.Responses;
using PokeAPI.Interfaces;
using Newtonsoft.Json;
using FunTranslationsAPI.Interfaces;
using Domain.Api.Errors;
using PokedexAPI.Utils;

namespace PokedexAPI
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
            services.AddMvc(options =>
            {
                options.Filters.Add(new ErrorHandlingFilter());
            });

            var mapperConfig = MappingUtils.GetMapperConfiguration();
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers()
                .AddNewtonsoftJson();

            var refitSettings = new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings())
            };

            services.AddRefitClient<IPokeApi>(refitSettings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("ExternalApis:PokeApiBaseUrl").Value));
            services.AddRefitClient<ITranslationsApi>(refitSettings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("ExternalApis:FunTranslationsApiBaseUrl").Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
