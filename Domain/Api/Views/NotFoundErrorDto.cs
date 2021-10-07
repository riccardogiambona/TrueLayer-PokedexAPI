using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Api.Views
{
    /// <summary>
    /// The not found error message
    /// </summary>
    public class NotFoundErrorDto : ErrorDto
    {
        public NotFoundErrorDto() : base("NotFoundError")
        {

        }
    }
}
