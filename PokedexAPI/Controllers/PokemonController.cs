using System;
using System.Threading.Tasks;
using AutoMapper;
using FunTranslationsAPI.Interfaces;
using FunTranslationsAPI.Views.Requests;
using Microsoft.AspNetCore.Mvc;
using PokeAPI.Interfaces;
using PokedexAPI.Enums;
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
        private readonly ITranslationsApi translationsApi;

        public PokemonController(IPokeApi pokeApi,
            ITranslationsApi translationsApi,
            IMapper mapper)
        {
            this.pokeApi = pokeApi;
            this.mapper = mapper;
            this.translationsApi = translationsApi;
        }

        [HttpGet("{name}")]
        public async Task<PokemonInfoRespDto> GetPokemonInfo(string name)
        {
            var pokeApiResp = await pokeApi.GetPokemonSpeciesByNameAsync(name);           
            return mapper.Map<PokemonInfoRespDto>(pokeApiResp);
        }

        [HttpGet("translated/{name}")]
        public async Task<PokemonInfoRespDto> GetTranslatedPokemonInfo(string name)
        {
            var pokeApiResp = await pokeApi.GetPokemonSpeciesByNameAsync(name);

            //if description is empty or null, there's nothing to translate
            if(String.IsNullOrEmpty(pokeApiResp.Description))
                return mapper.Map<PokemonInfoRespDto>(pokeApiResp);

            var isYodaTranslationNeeded = pokeApiResp.Habitat.Name == "cave" || pokeApiResp.IsLegendary;
            var requiredTranslation = isYodaTranslationNeeded ?
                RequiredTranslationType.YODA : RequiredTranslationType.SHAKESPEARE;

            var resp = mapper.Map<PokemonInfoRespDto>(pokeApiResp);
            resp.Description = await TryGetTranslatedDescription(requiredTranslation, resp.Description);
            return resp;
        }

        /// <summary>
        /// Tries to call the translation APIs to translate the description.
        /// If an error occurs, then the original translation is returned.
        /// </summary>
        /// <param name="description">The original description</param>
        /// <param name="reqTranslation">The required translation</param>
        /// <returns></returns>
        private async Task<string> TryGetTranslatedDescription(RequiredTranslationType reqTranslation,
            string description)
        {
            try
            {
                var translationReq = new TranslationReqDto()
                {
                    TextToTranslate = description,
                };

                switch(reqTranslation)
                {
                    case RequiredTranslationType.YODA:
                        return (await translationsApi.GetYodaTranslation(translationReq))?
                            .Contents?.Translated;
                    case RequiredTranslationType.SHAKESPEARE:
                        return (await translationsApi.GetShakespeareTranslation(translationReq))?
                           .Contents?.Translated;
                }
            }
            catch(Exception)
            {
            }

            //If I arrive here, it's beacause an error occurred during 
            //translation or the required translation is not YODA or SHAKESPEAR
            //(it could happen if a new value is added to the enum, but the cases in the switch are not 
            //updated)
            return description;
        }

    }
}
