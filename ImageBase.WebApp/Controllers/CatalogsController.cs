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
using Microsoft.AspNetCore.Authorization;
using ImageBase.WebApp.Data.Models.Authentication;
using ImageBase.WebApp.Services;

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogsController> _logger;

        public CatalogsController(ILogger<CatalogsController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogs()
        {
            IEnumerable<CatalogDto> allCatalogs = await _catalogService.GetCatalogsAsync();
            if (allCatalogs == null)
            {
                return NotFound();
            }
            return Ok(allCatalogs);
        }

        [Route("autor/{userId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogsByUser(string userId)
        {
            IEnumerable<CatalogDto> allCatalogs = await _catalogService.GetCatalogsByUserAsync(userId);
            if (allCatalogs == null)
            {
                return NotFound();
            }
            return Ok(allCatalogs);
        } 

        [HttpGet("sub/{id:int}")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetSubCatalogsAsync(int id)
        {
            IEnumerable<CatalogDto> allCatalogs = await _catalogService.GetSubCatalogsAsync(id);
            if (allCatalogs == null)
            {
                return NotFound();
            }
            return Ok(allCatalogs);
        }

        [HttpGet("autor/sub/{parentId:int}/{userId}")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetSubCatalogsByUserAsync(int parentId, string userId)
        {
            ServiceResponse<IEnumerable<CatalogDto>> allCatalogs = await _catalogService.GetSubCatalogsByUserAsync(parentId, userId);
            if (allCatalogs == null)
            {
                return NotFound();
            }
            return Ok(allCatalogs);
        }

        [HttpGet("image/{id:int}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByCatalog(int id, int offset = 0, int limit = 4)
        {
            PaginationListDto<ImageDto> allImages = await _catalogService.GetImagesByCatalogAsync(id, offset, limit);
            if (allImages == null)
            {
                return NotFound();
            }
            return Ok(allImages);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CatalogDto>> GetCatalog(int id)
        {
            CatalogDto catalog = await _catalogService.GetCatalogAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return Ok(catalog);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> UpdateCatalog(CatalogDto catalog)
        {
            if (catalog == null)
            {
                return NotFound();
            }
            var updatecatalog = await _catalogService.UpdateCatalogAsync(catalog);
            return Ok(updatecatalog);
        }

        [HttpPost]
        public async Task<ActionResult<CatalogDto>> CreateCatalog(CatalogDto catalog)
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

        [HttpPost("image")]
        public async Task<ActionResult<CatalogDto>> AddImageToCatalog(UpdateImageCatalogDto image)
        {
            try
            {
                await _catalogService.AddImageToCatalogAsync(image);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> DeleteCatalog(int id)
        {
            bool isCatalogDeleted = await _catalogService.DeleteCatalogAsync(id);
            if (isCatalogDeleted == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("image/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> DeleteImageFromCatalog(UpdateImageCatalogDto deleteimage)
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
        
    }
}
