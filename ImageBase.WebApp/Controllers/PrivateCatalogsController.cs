using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageBase.WebApp.Data.Dtos;
using ImageBase.WebApp.Data.Models.Authentication;
using ImageBase.WebApp.Services;
using ImageBase.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PrivateCatalogsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<PublicCatalogsController> _logger;
        private readonly UserManager<User> _userManager;

        public PrivateCatalogsController(ILogger<PublicCatalogsController> logger, ICatalogService catalogService, UserManager<User> userManager)
        {
            _logger = logger;
            _catalogService = catalogService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogs()
        {
            IEnumerable<CatalogDto> allCatalogs = await _catalogService.GetCatalogsAsync(await CurrentUser());
            return Ok(allCatalogs);
        }

        [HttpGet("sub/{id:int}")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetSubCatalogsAsync(int id)
        {
            ServiceResponse<IEnumerable<CatalogDto>> serviceGetSubCatalog = await _catalogService.GetSubCatalogsAsync(id, await CurrentUser());
            if (serviceGetSubCatalog.Success == false)
            {
                return Forbid(serviceGetSubCatalog.Message);
            }
            return Ok(serviceGetSubCatalog);
        }

        [HttpGet("image/{id:int}")]
        public async Task<ActionResult<IEnumerable<ImagesListItemDto>>> GetImagesByCatalog(int id, int offset = 0, int limit = 4)
        {
            ServiceResponse<PaginationListDto<ImagesListItemDto>> serviceGetImagesByCatalog = await _catalogService.GetImagesFromCatalogAsync(id, offset, limit, await CurrentUser());
            if (serviceGetImagesByCatalog.Success == false)
            {
                return Forbid(serviceGetImagesByCatalog.Message);
            }
            return Ok(serviceGetImagesByCatalog);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CatalogDto>> GetCatalog(int id)
        {
            ServiceResponse<CatalogDto> serviceGetCatalog = await _catalogService.GetCatalogAsync(id, await CurrentUser());
            if (serviceGetCatalog.Success == false)
            {
                return Forbid(serviceGetCatalog.Message);
            }
            return Ok(serviceGetCatalog);
        }

        [HttpPut("{id}")]
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
        public async Task<ActionResult<CatalogDto>> AddImageToCatalog(UpdateImageCatalogDto image)
        {
            ServiceResponse<UpdateImageCatalogDto> serviceAddImageToCatalog = await _catalogService.AddImageToCatalogAsync(image, await CurrentUser());
            if (serviceAddImageToCatalog.Success == false)
            {
                return Forbid(serviceAddImageToCatalog.Message);
            }
            return Ok(serviceAddImageToCatalog);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CatalogDto>> DeleteCatalog(int id)
        {
            ServiceResponse<int> serviceDeleteCatalog = await _catalogService.DeleteCatalogAsync(id, await CurrentUser());
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
            ServiceResponse<UpdateImageCatalogDto> serviceDeleteImageFromCatalog = await _catalogService.DeleteImageFromCatalogAsync(deleteimage, await CurrentUser());
            if (serviceDeleteImageFromCatalog.Success == false)
            {
                return Forbid(serviceDeleteImageFromCatalog.Message);
            }
            return Ok(serviceDeleteImageFromCatalog);
        }

        private async Task<string> CurrentUser()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                return user.Id;
            }
            else
            {
                return null;
            }
        }

    }
}
