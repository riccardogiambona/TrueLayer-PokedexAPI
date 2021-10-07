using FunTranslationsAPI.Views.Requests;
using FunTranslationsAPI.Views.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunTranslationsAPI.Interfaces
{
    /// <summary>
    /// The Refit interface that contains all the used Translations API methods
    /// </summary>
    public interface ITranslationsApi
    {
        [Post("/translate/shakespeare")]
        Task<TranslationRespDto> GetShakespeareTranslationAsync([Body] TranslationReqDto translationReq);

        [Post("/translate/yoda")]
        Task<TranslationRespDto> GetYodaTranslationAsync([Body] TranslationReqDto translationReq);
    }
}
