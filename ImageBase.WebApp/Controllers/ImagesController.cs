using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImageBase.WebApp.Data.Models;
using ImageBase.WebApp.Repositories;
using ImageBase.WebApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ImageBase.WebApp.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using ImageBase.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using ImageBase.WebApp.Data.Models.Authentication;

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imagesService;
        private readonly ILogger<ImagesController> _logger;
        private readonly UserManager<User> _userManager;

        public ImagesController(ILogger<ImagesController> logger, IImageService imagesService, UserManager<User> userManager)
        {
            _logger = logger;
            _imagesService = imagesService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<ImageDto>> CreateImage(AddImageDto image)
        {
            ServiceResponse<ImageDto> serviceCreateImage = await _imagesService.CreateImageAsync(image, await CurrentUser());
            if (serviceCreateImage.Success == false)
            {
                return Forbid(serviceCreateImage.Message);
            }
            return Ok(serviceCreateImage);
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
        [HttpGet("[action]")]
        public async Task<ActionResult<ImageDto>> Search(FullTextSeacrhDto fullTextSeacrhDto)
        {
            ServiceResponse<PaginationListDto<Image>> serviceSearch = await _imagesService.FullTextSearchByImagesAsync(fullTextSeacrhDto);
            if (serviceSearch.Success == false)
            {
                return Forbid(serviceSearch.Message);
            }
            return Ok(serviceSearch);
        }

    }
}
