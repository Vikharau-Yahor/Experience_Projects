using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp_Base.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApp_Base.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Home2Controller : ControllerBase
    {
        private static List<AutomobileDocument> _autos = new List<AutomobileDocument>
        {
            new AutomobileDocument{Id = 1, Color = "Green", Name = "Mercedes", Value = 21350 },
            new AutomobileDocument{Id = 2, Color = "Red", Name = "Wolkswagen", Value = 11550 },
            new AutomobileDocument{Id = 3, Color = "Black", Name = "BMW", Value = 34500 },
        };

        [HttpGet]
        public JsonResult GetAuto()
        {
            return new JsonResult(_autos.First());
        }

        //example api/home/getauto/3
        [HttpGet("{id}")]
        public JsonResult GetAuto(int id)
        {
            var result = _autos.SingleOrDefault(x => x.Id == id);
            return new JsonResult(result);
        }
    }
}
