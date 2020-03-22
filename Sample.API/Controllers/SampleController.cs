using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Controllers
{
    public class SampleController : Controller
    {
        [Route("/secret")]
        public string Index()
        {
            return "Anish is a good boy";
        }
    }
}