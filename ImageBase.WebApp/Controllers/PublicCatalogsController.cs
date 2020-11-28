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
using Microsoft.AspNetCore.Identity;

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PublicCatalogsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<PublicCatalogsController> _logger;

        public PublicCatalogsController(ILogger<PublicCatalogsController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogs()
        {
            IEnumerable<CatalogDto> allCatalogs = await _catalogService.GetCatalogsAsync();
            return Ok(allCatalogs);
        }

        [HttpGet("sub/{id:int}")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetSubCatalogsAsync(int id)
        {
            ServiceResponse<IEnumerable<CatalogDto>> serviceGetSubCatalog = await _catalogService.GetSubCatalogsAsync(id);
            if (serviceGetSubCatalog.Success == false)
            {
                return Forbid(serviceGetSubCatalog.Message);
            }
            return Ok(serviceGetSubCatalog);
        }

        [HttpGet("image/{id:int}")]
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetImagesByCatalog(int id, int offset = 0, int limit = 4)
        {
            ServiceResponse<PaginationListDto<ImageDto>> serviceGetImagesByCatalog = await _catalogService.GetImagesFromCatalogAsync(id, offset, limit);
            if (serviceGetImagesByCatalog.Success == false)
            {
                return Forbid(serviceGetImagesByCatalog.Message);
            }
            return Ok(serviceGetImagesByCatalog);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CatalogDto>> GetCatalog(int id)
        {
            ServiceResponse<CatalogDto> serviceGetCatalog = await _catalogService.GetCatalogAsync(id);
            if (serviceGetCatalog.Success == false)
            {
                return Forbid(serviceGetCatalog.Message);
            }
            return Ok(serviceGetCatalog);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> UpdateCatalog(CatalogDto catalog)
        {
            ServiceResponse<CatalogDto> serviceUpdateCatalog = await _catalogService.UpdateCatalogAsync(catalog);
            if (serviceUpdateCatalog.Success == false)
            {
                return Forbid(serviceUpdateCatalog.Message);
            }
            return Ok(serviceUpdateCatalog);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> CreateCatalog(CatalogDto catalog)
        {
            ServiceResponse<CatalogDto> serviceCreateCatalog = await _catalogService.CreateCatalogAsync(catalog);
            if (serviceCreateCatalog.Success == false)
            {
                return Forbid(serviceCreateCatalog.Message);
            }
            return Ok(serviceCreateCatalog);
        }

        [HttpPost("image")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> AddImageToCatalog(UpdateImageCatalogDto image)
        {
            ServiceResponse<UpdateImageCatalogDto> serviceAddImageToCatalog = await _catalogService.AddImageToCatalogAsync(image);
            if (serviceAddImageToCatalog.Success == false)
            {
                return Forbid(serviceAddImageToCatalog.Message);
            }
            return Ok(serviceAddImageToCatalog);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> DeleteCatalog(int id)
        {
            ServiceResponse<int> serviceDeleteCatalog = await _catalogService.DeleteCatalogAsync(id);
            if (serviceDeleteCatalog.Success == false)
            {
                return Forbid(serviceDeleteCatalog.Message);
            }
            return Ok(serviceDeleteCatalog);
        }

        [HttpDelete("image/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<CatalogDto>> DeleteImageFromCatalog(UpdateImageCatalogDto deleteimage)
        {
            ServiceResponse<UpdateImageCatalogDto> serviceDeleteImageFromCatalog = await _catalogService.DeleteImageFromCatalogAsync(deleteimage);
            if (serviceDeleteImageFromCatalog.Success == false)
            {
                return Forbid(serviceDeleteImageFromCatalog.Message);
            }
            return Ok(serviceDeleteImageFromCatalog);
        }
    }
}
