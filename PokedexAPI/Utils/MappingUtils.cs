using AutoMapper;
using PokeAPI.Views.Responses;
using PokedexAPI.Views.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexAPI.Utils
{
    public class MappingUtils
    {
        public static MapperConfiguration GetMapperConfiguration()
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
    }
}
