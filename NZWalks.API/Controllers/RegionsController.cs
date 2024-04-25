using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            var regions = await _dbContext.Regions.ToListAsync();
            
            
            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(
                    new RegionDto()
                    {
                        Id = region.Id,
                        Code = region.Code,
                        Name = region.Name,
                        RegionImageUrl = region.RegionImageUrl
                    }
                    );
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var region = await _dbContext.Regions.FindAsync(id);

            //var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            await _dbContext.Regions.AddAsync(regionDomainModel);
            await _dbContext.SaveChangesAsync();
            //M p Domain model back to Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id=regionDomainModel.Id}, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
           var regionDomainModel= await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

           if (regionDomainModel == null)
           {
               return NotFound();
           }

           regionDomainModel.Code = updateRegionRequestDto.Code;
           regionDomainModel.Name = updateRegionRequestDto.Name;
           regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

          
           await _dbContext.SaveChangesAsync();

           var regionDto = new RegionDto()
           {
               Id = regionDomainModel.Id,
               Code = regionDomainModel.Code,
               Name = regionDomainModel.Name,
               RegionImageUrl = regionDomainModel.RegionImageUrl
           };
           return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _dbContext.Regions.FindAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

             _dbContext.Regions.Remove(regionDomainModel);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
         
    }
}
