using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IIG_School.Data;
using IIG_School.Models;
using IIG_School.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using IIG_School.Helpers;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml.Style;

namespace IIG_School.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Authorize(Roles = Constants.Roles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile upload)
        {
            try
            {
                await _productService.Add(product, upload);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile upload)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            try
            {
                await _productService.Update(product, upload);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return NotFound();
        }
      
    }
}
