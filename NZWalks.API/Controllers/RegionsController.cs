using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Adamawa",
                    Code = "ADM",
                    RegionImageUrl =
                        "https://images.pexels.com/photos/20956963/pexels-photo-20956963/free-photo-of-islande-neige-aube-paysage.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                
                new Region()
                {
                Id = Guid.NewGuid(),
                Name = "Nord",
                Code = "ND",
                RegionImageUrl =
                    "https://images.pexels.com/photos/20956963/pexels-photo-20956963/free-photo-of-islande-neige-aube-paysage.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            }
            };

            return Ok(regions);
        }
    }
}
