using PokeAPI.Views;
using PokeAPI.Views.Responses;
using Refit;
using System.Threading.Tasks;

namespace PokeAPI.Interfaces
{
    public interface IPokeApi
    {
        [Get("/pokemon-species/{name}")]
        Task<PokeApiPokemonSpeciesRespDto> GetPokemonSpeciesByNameAsync(string name);
    }
}
