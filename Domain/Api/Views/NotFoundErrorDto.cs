using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Api.Views
{
    public class NotFoundErrorDto : ErrorDto
    {
        public NotFoundErrorDto() : base("NotFoundError")
        {

        }
    }
}
