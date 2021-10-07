using AutoMapper;
using FunTranslationsAPI.Interfaces;
using FunTranslationsAPI.Views.Requests;
using FunTranslationsAPI.Views.Responses;
using Moq;
using Newtonsoft.Json;
using PokeAPI.Interfaces;
using PokeAPI.Views.Responses;
using PokedexAPI.Controllers;
using PokedexAPI.Utils;
using PokedexAPI.Views.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PokedexAPITests
{
    public class PokemonControllerTests
    {
        private readonly IMapper mapper;

        public PokemonControllerTests()
        {
            var mapperConfig = MappingUtils.GetMapperConfiguration();
            mapper = mapperConfig.CreateMapper();
        }

        #region PokemonInfoAPITests

        /// <summary>
        /// Tests the /pokemon/{name} API with mewtwo
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestPokemonInfoApi()
        {
            //Test values
            var pokemonName = "mewtwo";
            var pokemonDescription = "Pokemon description";
            var pokemonDescriptionLanguage = "en";
            var pokemonHabitatName = "rare";
            var pokemonId = 150;
            var pokemonIsLegendary = true;

            //Create the mock for the pockemon info API
            var pokeApiMock = new Mock<IPokeApi>();
            pokeApiMock.Setup(s => s.GetPokemonSpeciesByNameAsync(pokemonName))
                .ReturnsAsync(new PokeApiPokemonSpeciesRespDto()
                {
                    Id = pokemonId,
                    Name = pokemonName,
                    FlavorTextEntries = new List<PokeAPI.Views.Responses.FlavorTextEntry>()
                    {
                        new PokeAPI.Views.Responses.FlavorTextEntry()
                        {
                            FlavorText = pokemonDescription,
                            Language = new PokeAPI.Views.Responses.Language()
                            {
                                Name = pokemonDescriptionLanguage,
                            }
                        }
                    },
                    IsLegendary = pokemonIsLegendary,
                    Habitat = new PokeAPI.Views.Responses.Habitat()
                    {
                        Name = pokemonHabitatName,
                    }
                });

            var controllerResp = await new PokemonController(pokeApiMock.Object, null, mapper)
                .GetPokemonInfo(pokemonName);

            Assert.Equal(pokemonId, controllerResp.Id);
            Assert.Equal(pokemonDescription, controllerResp.Description);
            Assert.Equal(pokemonHabitatName, controllerResp.Habitat);
            Assert.Equal(pokemonName, controllerResp.Name);
            Assert.Equal(pokemonIsLegendary, controllerResp.IsLegendary);
        }

        #endregion

        #region PokemonInfoTranslatedAPITests

        /// <summary>
        /// Tests the /pokemon/translated/{name} API with mewtwo
        /// that requires Yoda translation
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestPokemonYodaTranslatedInfoApi()
        {
            //Test values
            var pokemonName = "mewtwo";
            var pokemonDescription = "Pokemon description";
            var pokemonDescriptionLanguage = "en";
            var pokemonHabitatName = "rare";
            var pokemonId = 150;
            var pokemonIsLegendary = true;

            //Create the mock for the pockemon Info API
            var pokeApiMock = new Mock<IPokeApi>();
            pokeApiMock.Setup(s => s.GetPokemonSpeciesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new PokeApiPokemonSpeciesRespDto()
                {
                    Id = pokemonId,
                    Name = pokemonName,
                    FlavorTextEntries = new List<PokeAPI.Views.Responses.FlavorTextEntry>()
                    {
                        new PokeAPI.Views.Responses.FlavorTextEntry()
                        {
                            FlavorText = pokemonDescription,
                            Language = new PokeAPI.Views.Responses.Language()
                            {
                                Name = pokemonDescriptionLanguage,
                            }
                        }
                    },
                    IsLegendary = pokemonIsLegendary,
                    Habitat = new PokeAPI.Views.Responses.Habitat()
                    {
                        Name = pokemonHabitatName,
                    }
                });

            var pokemonTranslatedDescription = "Yoda translated Pokemon description";
            //Create the mock for the translations API
            var translationsApiMock = new Mock<ITranslationsApi>();
            translationsApiMock.Setup(s => s.GetYodaTranslationAsync(It.IsAny<TranslationReqDto>()))
                .ReturnsAsync(new TranslationRespDto()
                {
                    Success = new Success()
                    {
                        Total = 1,
                    },
                    Contents = new Contents()
                    {
                        Text = pokemonDescription,
                        Translated = pokemonTranslatedDescription,
                    }
                });

            var controllerResp = await new PokemonController(pokeApiMock.Object, translationsApiMock.Object, mapper)
                .GetTranslatedPokemonInfo(pokemonName);

            Assert.Equal(pokemonId, controllerResp.Id);
            Assert.Equal(pokemonTranslatedDescription, controllerResp.Description);
            Assert.Equal(pokemonHabitatName, controllerResp.Habitat);
            Assert.Equal(pokemonName, controllerResp.Name);
            Assert.Equal(pokemonIsLegendary, controllerResp.IsLegendary);
        }

        /// <summary>
        /// Tests the /pokemon/translated/{name} API with pikachu
        /// that requires Shakespeare translation
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestPokemonShakespeareTranslatedInfoApi()
        {
            //Test values
            var pokemonName = "pikachu";
            var pokemonDescription = "Pokemon description";
            var pokemonDescriptionLanguage = "en";
            var pokemonHabitatName = "forest";
            var pokemonId = 25;
            var pokemonIsLegendary = false;

            //Create the mock for the pockemon Info API
            var pokeApiMock = new Mock<IPokeApi>();
            pokeApiMock.Setup(s => s.GetPokemonSpeciesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new PokeApiPokemonSpeciesRespDto()
                {
                    Id = pokemonId,
                    Name = pokemonName,
                    FlavorTextEntries = new List<PokeAPI.Views.Responses.FlavorTextEntry>()
                    {
                        new PokeAPI.Views.Responses.FlavorTextEntry()
                        {
                            FlavorText = pokemonDescription,
                            Language = new PokeAPI.Views.Responses.Language()
                            {
                                Name = pokemonDescriptionLanguage,
                            }
                        }
                    },
                    IsLegendary = pokemonIsLegendary,
                    Habitat = new PokeAPI.Views.Responses.Habitat()
                    {
                        Name = pokemonHabitatName,
                    }
                });

            var pokemonTranslatedDescription = "Shakespare translated Pokemon description";
            //Create the mock for the translations API
            var translationsApiMock = new Mock<ITranslationsApi>();
            translationsApiMock.Setup(s => s.GetShakespeareTranslationAsync(It.IsAny<TranslationReqDto>()))
                .ReturnsAsync(new TranslationRespDto()
                {
                    Success = new Success()
                    {
                        Total = 1,
                    },
                    Contents = new Contents()
                    {
                        Text = pokemonDescription,
                        Translated = pokemonTranslatedDescription,
                    }
                });

            var controllerResp = await new PokemonController(pokeApiMock.Object, translationsApiMock.Object, mapper)
                .GetTranslatedPokemonInfo(pokemonName);

            Assert.Equal(pokemonId, controllerResp.Id);
            Assert.Equal(pokemonTranslatedDescription, controllerResp.Description);
            Assert.Equal(pokemonHabitatName, controllerResp.Habitat);
            Assert.Equal(pokemonName, controllerResp.Name);
            Assert.Equal(pokemonIsLegendary, controllerResp.IsLegendary);
        }

        /// <summary>
        /// Tests the /pokemon/translated/{name} API in case 
        /// the translated API gives an error
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestPokemonNotTranslatedInfoApi()
        {
            //Test values
            var pokemonName = "pikachu";
            var pokemonDescription = "Pokemon description";
            var pokemonDescriptionLanguage = "en";
            var pokemonHabitatName = "forest";
            var pokemonId = 25;
            var pokemonIsLegendary = false;

            //Create the mock for the pockemon Info API
            var pokeApiMock = new Mock<IPokeApi>();
            pokeApiMock.Setup(s => s.GetPokemonSpeciesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new PokeApiPokemonSpeciesRespDto()
                {
                    Id = pokemonId,
                    Name = pokemonName,
                    FlavorTextEntries = new List<PokeAPI.Views.Responses.FlavorTextEntry>()
                    {
                        new PokeAPI.Views.Responses.FlavorTextEntry()
                        {
                            FlavorText = pokemonDescription,
                            Language = new PokeAPI.Views.Responses.Language()
                            {
                                Name = pokemonDescriptionLanguage,
                            }
                        }
                    },
                    IsLegendary = pokemonIsLegendary,
                    Habitat = new PokeAPI.Views.Responses.Habitat()
                    {
                        Name = pokemonHabitatName,
                    }
                });

            //Create the mock for the translations API (case of too many requests)
            var translationsApiMock = new Mock<ITranslationsApi>();
            translationsApiMock.Setup(s => s.GetShakespeareTranslationAsync(It.IsAny<TranslationReqDto>()))
                .Throws(ApiException.Create(null, null, new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.TooManyRequests }, null).Result);

            var controllerResp = await new PokemonController(pokeApiMock.Object, translationsApiMock.Object, mapper)
                .GetTranslatedPokemonInfo(pokemonName);

            Assert.Equal(pokemonId, controllerResp.Id);
            Assert.Equal(pokemonDescription, controllerResp.Description); //The check that the description remains the same
            Assert.Equal(pokemonHabitatName, controllerResp.Habitat);
            Assert.Equal(pokemonName, controllerResp.Name);
            Assert.Equal(pokemonIsLegendary, controllerResp.IsLegendary);
        }


        #endregion

    }
}
