﻿using Domain.Api.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Api.Errors
{
    /// <summary>
    /// This class converts exceptions
    /// that can occur in API controllers to Dtos
    /// to have a unique error structure for all APIs in case an exception occurs 
    /// </summary>
    public class ApiExceptionConverter
    {
        public static ErrorDto ConvertToDto(Exception ex)
        {
            if (ex.GetType() != typeof(ApiException))
                return new ErrorDto("GenericError");

            var refitEx = (ApiException)ex;
            switch(refitEx.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new NotFoundErrorDto();
                //this cases could be extended to other errors
                default:
                    return new ErrorDto("GenericError");
            }
        }
    }
}
