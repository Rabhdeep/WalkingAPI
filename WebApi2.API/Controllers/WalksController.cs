using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WebApi2.API.CustomActionFilters;
using WebApi2.API.Models.Domain;
using WebApi2.API.Models.DTO;
using WebApi2.API.Repositories;

namespace WebApi2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walkDomainModel);
            return Ok(mapper.Map<WalkDto>(walkDomainModel)); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery]string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool isAscending,
            [FromQuery] int pageNumber =1, [FromQuery] int pageSize = 1000) 
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize);

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel)); 
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null) { return NotFound(); }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel=await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null) { return NotFound();}
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);
            if (walkDomainModel == null) { return NotFound(); }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }
    }
}
