using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeAPI.Interfaces;
using PokedexAPI.Views;
using PokedexAPI.Views.Responses;

namespace PokedexAPI.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeApi pokeApi;
        private readonly IMapper mapper;

        public PokemonController(IPokeApi pokeApi, IMapper mapper)
        {
            this.pokeApi = pokeApi;
            this.mapper = mapper;
        }

        [HttpGet("{name}")]
        public async Task<PokemonInfoRespDto> GetPokemonInfo(string name)
        {
            var pokeApiResp = await pokeApi.GetPokemonSpeciesByNameAsync(name);           
            return mapper.Map<PokemonInfoRespDto>(pokeApiResp);
        }
            return mapper.Map<PokemonInfoDto>(pokeApiResp);
        }
    }
}
