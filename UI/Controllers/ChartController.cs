using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

namespace UI.Controllers
{
    public class ChartController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;

        public ChartController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult VisualizeProductResult()
        {
            return Json(ProList());
        }

        public List<Class1> ProList()
        {
            List<Class1> cs = new List<Class1>();
            cs = _productService.GetAll().Data.Select(p => new Class1
            {
                proname = p.Name,
                stock = p.Stock
            }).ToList();
            return cs;
        }

        public IActionResult Statistics()
        {
            var deger1 = _productService.GetAll().Data.Count();
            ViewBag.d1 = deger1;

            var deger2 = _categoryService.GetList().Data.Count();
            ViewBag.d2 = deger2;

            var deger3 = _productService.GetAll().Data.Where(p => p.CategoryId == 1).Count(); 
            ViewBag.d3 = deger3;

            var deger4 = _productService.GetAll().Data.Where(p => p.CategoryId == 2).Count();
            ViewBag.d4 = deger4;

            var deger5 = _productService.GetAll().Data.Sum(p => p.Stock);
            ViewBag.d5 = deger5;

            var deger6 = _productService.GetAll().Data.Where(p => p.CategoryId == 3).Count();
            ViewBag.d6 = deger6;
            

            var deger7 = _productService.GetAll().Data.OrderByDescending(p => p.Stock).Select(u => u.Name).FirstOrDefault();
            ViewBag.d7 = deger7;

            var deger8 = _productService.GetAll().Data.OrderBy(p => p.Stock).Select(u => u.Name).FirstOrDefault();
            ViewBag.d8 = deger8;

            var deger9 = _productService.GetAll().Data.Average(p => p.Price).ToString("0.00");
            ViewBag.d9 = deger9;

            var deger10 = _productService.GetAll().Data.Where(p => p.CategoryId == 1).Sum(p => p.Stock);
            ViewBag.d10 = deger10;

            var deger11 = _productService.GetAll().Data.Where(p => p.CategoryId == 2).Sum(p => p.Stock);
            ViewBag.d11 = deger11;

            var deger12 = _productService.GetAll().Data.OrderByDescending(p => p.Price).Select(p => p.Name).FirstOrDefault();
            ViewBag.d12 = deger12;
            return View();
        }
    }
}
