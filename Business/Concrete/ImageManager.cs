using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Helper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ImageManager : IImageService
    {
        IImageDal _imageDal;
        IProductService _productService;

        private readonly string DefaultImage = "default.jpeg";
        public ImageManager(IImageDal imageDal, IProductService productService)
        {
            _imageDal = imageDal;
            _productService = productService;
        }
        public IResult Add(IFormFile image, Image img)
        {
            img.ImagePath = FileOperationsHelper.Add(image);
            img.CreatedAt = DateTime.Now;
            _imageDal.Add(img);
            return new SuccessResult("Image" + "Eklendi");
        }

        public IResult Delete(Image img)
        {
            _imageDal.Delete(img);
            FileOperationsHelper.Delete(img.ImagePath);
            return new SuccessResult("Image" + " silindi");
        }

        public IDataResult<Image> FindByID(int Id)
        {
            Image img = new Image();
            if (_imageDal.GetAll().Any(x => x.Id == Id))
            {
                img = _imageDal.GetAll().FirstOrDefault(x => x.Id == Id);
            }
            else Console.WriteLine("No such car image was found.");
            return new SuccessDataResult<Image>(img);
        }

        public IDataResult<Image> Get(Image img)
        {
            return new SuccessDataResult<Image>(_imageDal.Get(x => x.Id == img.Id));
        }

        public IDataResult<List<Image>> GetAll()
        {
            return new SuccessDataResult<List<Image>>(_imageDal.GetAll());
        }

        public IResult GetList(List<Image> list)
        {
            Console.WriteLine("\n------- Car Image List -------");

            foreach (var img in list)
            {
                Console.WriteLine("{0}- Car ID: {1}\n    Image Path: {2}\n    CratedAt: {3}\n", img.Id,  img.ImagePath, img.CreatedAt);
            }
            return new SuccessResult();
        }

        public IResult Update(IFormFile image, Image img)
        {
          
            var carImg = _imageDal.Get(x => x.Id == img.Id);
            carImg.CreatedAt = DateTime.Now;
            carImg.ImagePath = FileOperationsHelper.Add(image);
            FileOperationsHelper.Delete(img.ImagePath);
            _imageDal.Update(carImg);
            return new SuccessResult("Image" + " güncellendi");
        }

        public IDataResult<List<Image>> GetListByProductId(int productId)
        {
            if (!_imageDal.GetAll().Any(x => x.ProductId == productId))
            {
                List<Image> img = new List<Image>
                {
                    new Image
                    {
                        ProductId = productId,
                        ImagePath = DefaultImage
                    }
                };
                return new SuccessDataResult<List<Image>>(img);
            }
            return new SuccessDataResult<List<Image>>(_imageDal.GetAll(x => x.ProductId == productId));
        }
    }
}
