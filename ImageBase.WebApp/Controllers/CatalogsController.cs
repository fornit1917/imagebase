using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Repositories;
using Microsoft.Extensions.Logging;
using ImageBase.WebApp.Services.Interfaces;
using AutoMapper;

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly AspPostgreSQLContext _context;
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogsController> _logger;
        private readonly IMapper _mapper;

        public CatalogsController(AspPostgreSQLContext context, ILogger<CatalogsController> logger, IMapper mapper, ICatalogService catalogService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogs()
        {
            var allCatalog = await _catalogService.GetCatalogsAsync();
            if (allCatalog == null)
            {
                return NotFound();
            }
            return Ok(allCatalog);
        }

        [HttpGet("sub/{id}")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetSubCatalogsAsync(int id)
        {
            var allCatalog = await _catalogService.GetSubCatalogsAsync(id);
            if (allCatalog == null)
            {
                return NotFound();
            }
            return Ok(allCatalog);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogDto>> GetCatalog(int id)
        {
            var catalog = await _catalogService.GetCatalogAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return Ok(catalog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog(int id, CatalogDto catalog)
        {
            if (id != catalog.Id)
            {
                return BadRequest();
            }
            var updatecatalog = await _catalogService.UpdateCatalogAsync(id,catalog);
            return Ok(updatecatalog);
        }

        [HttpPost]
        public async Task<ActionResult<Catalog>> PostCatalog(CatalogDto catalog)
        {
            try
            {
                await _catalogService.CreateCatalogAsync(catalog);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Catalog>> DeleteCatalog(int id)
        {
            var catalog = await _catalogService.DeleteCatalogAsync(id);
            if (catalog == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("image/{id}")]
        public async Task<ActionResult<Catalog>> DeleteImageFromCatalog(UpdateImageCatalogDto deleteimage)
        {
            try
            {
                await _catalogService.DeleteImageFromCatalogAsync(deleteimage);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        }

        [HttpGet("image/{id}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByCatalog(int id, int offset, int limit)
        {
            var allCatalog = await _catalogService.GetImagesByCatalogAsync(id,offset,limit);
            if (allCatalog == null)
            {
                return NotFound();
            }
            return Ok(allCatalog);
        }

        [HttpPost("image")]
        public async Task<ActionResult<Catalog>> PostImage(UpdateImageCatalogDto catalog)
        {
            try
            {
                await _catalogService.AddImageToCatalogAsync(catalog);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        }
    }
}
