using FunTranslationsAPI.Views.Requests;
using FunTranslationsAPI.Views.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunTranslationsAPI.Interfaces
{
    public interface ITranslationsApi
    {
        [Post("/translate/shakespeare")]
        Task<TranslationRespDto> GetShakespeareTranslation([Body] TranslationReqDto translationReq);

        [Post("/translate/yoda")]
        Task<TranslationRespDto> GetYodaTranslation([Body] TranslationReqDto translationReq);
    }
}
