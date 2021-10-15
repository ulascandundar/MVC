using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IImageService
    {
        IDataResult<List<Image>> GetAll();
        IResult Add(IFormFile image, Image img);
        IResult Update(IFormFile image, Image img);
        IResult Delete(Image img);
        IDataResult<Image> Get(Image img);
        IResult GetList(List<Image> list);
        IDataResult<Image> FindByID(int Id);
        IDataResult<List<Image>> GetListByProductId(int productI);
    }
}
