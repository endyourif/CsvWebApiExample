using CsvWebApiExample.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CsvWebApiExample.Controllers.Api
{
    public class CsvController : ApiController
    {
        public List<CsvViewModel> Get()
        {
            return new List<CsvViewModel>
            {
                new CsvViewModel 
                {
                    FirstName = "John",
                    LastName = "Doe"
                }
            };
        }
    }
}
