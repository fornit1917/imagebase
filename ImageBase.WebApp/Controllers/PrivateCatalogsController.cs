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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        private readonly RoleManager<IdentityRole> _roleManager;

        public PrivateCatalogsController(ILogger<PublicCatalogsController> logger, ICatalogService catalogService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _catalogService = catalogService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Route("autor/{userId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogsByUser(string userId)
        {
            if (await IsCurrentUser(userId))
            {
                IEnumerable<CatalogDto> allCatalogs = await _catalogService.GetCatalogsByUserAsync(userId);
                if (allCatalogs == null)
                {
                    return NotFound();
                }
                return Ok(allCatalogs);
            }
            else
            {
                return BadRequest(new Response { Status = "Error!", Message = "Access is denied" });
            }
        }

        [HttpGet("autor/sub/{parentId:int}/{userId}")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetSubCatalogsByUserAsync(int parentId, string userId)
        {
            if (await IsCurrentUser(userId))
            {
                ServiceResponse<IEnumerable<CatalogDto>> allCatalogs = await _catalogService.GetSubCatalogsByUserAsync(parentId, userId);
                if (allCatalogs == null)
                {
                    return NotFound();
                }
                return Ok(allCatalogs);
            }
            else
            {
                return BadRequest(new Response { Status = "Error!", Message = "Access is denied" });
            }
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
            if (await IsCurrentUser(catalog.UserId))
            { 
                return Ok(catalog);
            }
            else
            {
                return BadRequest(new Response { Status = "Error!", Message = "Access is denied" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogDto>> UpdateCatalog(CatalogDto catalog)
        {
            if (catalog == null)
            {
                return NotFound();
            }
            if (await IsCurrentUser(catalog.UserId))
            {
                var updatecatalog = await _catalogService.UpdateCatalogAsync(catalog);
                return Ok(updatecatalog);
            }
            else
            {
                return BadRequest(new Response { Status = "Error!", Message = "Access is denied" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CatalogDto>> CreateCatalog(CatalogDto catalog)
        {
            ServiceResponse<CatalogDto> createcatalog;
            if (await IsCurrentUser(catalog.UserId))
            {
                try
                {
                    createcatalog = await _catalogService.CreateCatalogAsync(catalog);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
                if (createcatalog == null)
                {
                    return BadRequest();
                }
                return Ok(createcatalog);
            }
            else
            {
                return BadRequest(new Response { Status = "Error!", Message = "Access is denied" });
            }
        }

        [HttpPost("image")]
        public async Task<ActionResult<CatalogDto>> AddImageToCatalog(UpdateImageCatalogDto image, string userId)
        {
            if (await IsCurrentUser(userId))
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
            else
            {
                return BadRequest(new Response { Status = "Error!", Message = "Access is denied" });
            }
        }

        [HttpDelete("{id:int}")]
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


        private async Task<bool> IsCurrentUser(string userId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user.Id == userId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
