using PokeAPI.Views;
using PokeAPI.Views.Responses;
using Refit;
using System.Threading.Tasks;

namespace PokeAPI.Interfaces
{
    /// <summary>
    /// The Refit interface that contains all the used PokeAPI methods
    /// </summary>
    public interface IPokeApi
    {
        [Get("/pokemon-species/{name}")]
        Task<PokeApiPokemonSpeciesRespDto> GetPokemonSpeciesByNameAsync(string name);
    }
}
