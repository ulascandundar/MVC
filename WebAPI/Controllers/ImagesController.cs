using Business.Abstract;
using Core.Utilities.Helper;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("add")]
        public IActionResult Add(IFormFile image,int productId)
        {
            var result = _imageService.Add(image,productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addimg")]
        public IActionResult AddImg(IFormFile image)
        {
            var result = FileOperationsHelper.Add(image);
            SuccessDataResult<string> res = new SuccessDataResult<string>(result);
            return Ok(res);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] Image img)
        {
            var result = _imageService.Delete(img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile image, [FromForm] Image img)
        {
            var result = _imageService.Update(image, img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getimagebyid")]
        public IActionResult Get(int Id)
        {
            var result = _imageService.FindByID(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _imageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
