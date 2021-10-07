# PokedexAPI

## Requirements
To develop this APIs the following components are required:
1) Visual Studio Community 2019, Version 16.11.3
2) .Net Core 3.1
3) ASP.NET Core Web API project model (to check in the visual studio installed modules)
4) xUnit Test project model (to check in the visual studio installed modules)

## Project Structure
1) PokedexAPI: The Web API project that exposes the requested APIs
2) PokedexAPITests: The xUnit project that contains all the unit tests to perform on the implemented APIs
3) PokeAPI: The API project that interacts with the https://pokeapi.co/ APIs
4) FunTranslationsAPI: The API project that interacts with the https://funtranslations.com/ APIs
5) Domain: The project that contains generic logic that can be used between projects. For now it contains only the API error handling, but in a production environment it can be used between several APIs

## Production Environment Changes
The followings are the changes that I would make if this project would go to a production environment

1) Authentication: All the calls to the Web API server must be authenticated, principally to guarantee a maximum number of requests per seconds and to define the price plan for each different requests rate. This is also important if in the future other user related APIs needs to be added (i.e.: The list of my favorite pokemons)
