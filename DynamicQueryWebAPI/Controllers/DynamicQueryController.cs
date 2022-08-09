using DynamicQueryWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicQueryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicQueryController : ControllerBase
    {
        private readonly static List<Person> People = new()
    {
            new Person { Id = new Guid(), Name = "Ömer Furkan", Surname = "Doğruyol", BirthDate = DateTime.Now.AddYears(-30), Salary = 9000,  ChildCount = 5 },
            new Person { Id = new Guid(), Name = "Mehmet", Surname = "Hekimoğlu", BirthDate = DateTime.Now.AddYears(-12), Salary = 8000, ChildCount = 2 },
            new Person { Id = new Guid(), Name = "Ayhan", Surname = "Akif" , BirthDate = DateTime.Now.AddYears(-35), Salary = 9000, ChildCount = 7 },
            new Person { Id = new Guid(), Name = "Selim", Surname = "Şentürk", BirthDate = DateTime.Now.AddYears(-32), Salary = 7000, ChildCount = 1 }
    };

        [HttpPost]
        public IActionResult Post([FromBody] QueryBuilderDto queryBuilderDto)
        {
            var queryBuilderMethod = new Builder.QueryBuilder<Person>();
            var resultQuery = queryBuilderMethod.Build(queryBuilderDto);
            var result = People.Where(resultQuery.Compile()).ToList();
            return Ok(result);
        }
    }
}
