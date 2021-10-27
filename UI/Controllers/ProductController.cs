using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
     
        public IActionResult Index()
        {
            return View(_productService.GetAll().Data);
            
        }
        [HttpGet]
        public IActionResult ProductAdd()
        {
            List<SelectListItem> values = (from c in _categoryService.GetList().Data
                                           select new SelectListItem
                                           {
                                               Text = c.Name,
                                               Value = c.Id.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
                                         
            return View();
        }
        [HttpPost]
        public IActionResult ProductAdd(Product product)
        {
            _productService.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult ProductGet(int id)
        {
            List<SelectListItem> values = (from c in _categoryService.GetList().Data
                                           select new SelectListItem
                                           {
                                               Text = c.Name,
                                               Value = c.Id.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            var p = _productService.GetById(id).Data;
            Product p1 = new Product()
            {
                CategoryId = p.CategoryId,
                Name = p.Name,
                Price = p.Price,
                Id=p.Id,
                Stock=p.Stock
            };
            return View(p1);
        }
        [HttpPost]
        public IActionResult ProductUpdate(Product p)
        {
            var x = _productService.GetById(p.Id).Data;
            x.Name = p.Name;
            x.CategoryId = p.CategoryId;
            x.Price = p.Price;
            x.Stock = p.Stock;
            _productService.Update(x);

            return RedirectToAction("Index");
        }
    }
}
