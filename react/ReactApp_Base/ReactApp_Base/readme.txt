************
SPA
**********
- Install Microsoft.AspNetCore.SpaServices.Extensions package
- Add spa configuration to appsettings.json, e.g.:
  "SpaConfiguration": {
    "DefaultPage": "public/index.html"
  }
- Register configuration IOption<SpaOptions> in Startup.cs for spa middleware:
    services.AddOptions<SpaOptions>("SpaConfiguration");
- Add SPA middleware in Startup.cs (which will redirect all reaquests to your SPA page; must be last in pipeline):
    app.UseSpa(x=> { });


**************
API
*****************
- Register Controllers in Startup:
    services.AddControllers();

- Add controllers routing middleware in Startup:
    app.UseEndpoints(endpoints =>
    {
       endpoints.MapControllers();
    });
- Create 'Controllers' folder
- Add some controller:
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
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

        //[HttpGet("Getauto/{id}")] -- example api/home/getauto/3
        [HttpGet("{id}")] //-- example //example api/home/3
        public JsonResult GetAuto(int id)
        {
            var result = _autos.SingleOrDefault(x => x.Id == id);
            return new JsonResult(result);
        }
    }
