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

namespace ImageBase.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imagesService;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ILogger<ImagesController> logger, IImageService imagesService)
        {
            _logger = logger;
            _imagesService = imagesService;
        }

        [HttpPost]
        public async Task<ActionResult<ImageDto>> CreateImage(AddImageDto image)
        {
            ImageDto createdImage;
            try
            {
                createdImage =  await _imagesService.CreateImageAsync(image);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            if (createdImage == null)
            {
                return BadRequest();
            }
            return Ok(createdImage);
        }

    }
}
