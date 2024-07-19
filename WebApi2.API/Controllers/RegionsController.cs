using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2.API.CustomActionFilters;
using WebApi2.API.Data;
using WebApi2.API.Models.Domain;
using WebApi2.API.Models.DTO;
using WebApi2.API.Repositories;

namespace WebApi2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDBContext dbContext, IRegionRepository regionRepository, IMapper mapper,ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            //var regionsDto = new List<RegionDto>();
            //foreach(var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id= regionDomain.Id,
            //        Code= regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl= regionDomain.RegionImageUrl
            //    });
            //}
            //throw new Exception("Some error occured sfjm asdfkjsdabkasdjbsdaj.kb");
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) {
            //var region = dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if(regionDomain==null)
            {
                return NotFound();
            }
            //var regionsDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Code= regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl= regionDomain.RegionImageUrl
            //};

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //var regionDomainModel = new Region()
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null) { return NotFound(); }

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto); 
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null) { return NotFound(); }
            
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}
