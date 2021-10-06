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

            var mapperConfig = GetMapperConfiguration();
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

        private MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(mc =>
            {
                //Mapping for PokeApiPokemonSpecies attributes
                mc.CreateMap<PokeApiPokemonSpeciesRespDto, PokemonInfoRespDto>()
                    .ForMember(map => map.Habitat, opt => opt.MapFrom(src => src.Habitat == null ? null : src.Habitat.Name));
                mc.CreateMap<PokeAPI.Views.Responses.Color, Views.Responses.Color>();
                mc.CreateMap<PokeAPI.Views.Responses.Area, Views.Responses.Area>();
                mc.CreateMap<PokeAPI.Views.Responses.EggGroup, Views.Responses.EggGroup>();
                mc.CreateMap<PokeAPI.Views.Responses.EvolutionChain, Views.Responses.EvolutionChain>();
                mc.CreateMap<PokeAPI.Views.Responses.FlavorTextEntry, Views.Responses.FlavorTextEntry>();
                mc.CreateMap<PokeAPI.Views.Responses.Genera, Views.Responses.Genera>();
                mc.CreateMap<PokeAPI.Views.Responses.Generation, Views.Responses.Generation>();
                mc.CreateMap<PokeAPI.Views.Responses.GrowthRate, Views.Responses.GrowthRate>();
                mc.CreateMap<PokeAPI.Views.Responses.Language, Views.Responses.Language>();
                mc.CreateMap<PokeAPI.Views.Responses.Name, Views.Responses.Name>();
                mc.CreateMap<PokeAPI.Views.Responses.PalParkEncounter, Views.Responses.PalParkEncounter>();
                mc.CreateMap<PokeAPI.Views.Responses.Pokedex, Views.Responses.Pokedex>();
                mc.CreateMap<PokeAPI.Views.Responses.PokedexNumber, Views.Responses.PokedexNumber>();
                mc.CreateMap<PokeAPI.Views.Responses.Pokemon, Views.Responses.Pokemon>();
                mc.CreateMap<PokeAPI.Views.Responses.Shape, Views.Responses.Shape>();
                mc.CreateMap<PokeAPI.Views.Responses.Variety, Views.Responses.Variety>();
                mc.CreateMap<PokeAPI.Views.Responses.Version, Views.Responses.Version>();
            });
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
