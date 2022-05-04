using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace excelReaderWeb.Models
{
    public enum StatusCode
    {
        Ok = 200,
        Created = 201,
        NotFound = 404,
        InternalServerError = 500,
        BadRequest = 400,
        NoContent = 204,
        Forbidden = 403,
        ServiceUnavailable = 503

    }
}
