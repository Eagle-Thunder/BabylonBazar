﻿using BabylonBazar.DSL;
using BabylonBazar.Dtos;
using BabylonBazar.Models;
using BabylonBazar.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BabylonBazar.Controllers {
	public class ProductController:Controller {
		private ProductService _productService;
		private JwtService _jwtService;

        public ProductController(ProductService productService, JwtService jwtService)
        {
			_productService = productService;
			_jwtService = jwtService;
        }
		public IActionResult Home(int page=0)
		{
			List<ProductHeadersVM> products = _productService.GetProducts(page);
			return View(products);
		}

		[EnableCors("Policy")]
		public JsonResult FilterByCategory([FromRoute]int id, [FromRoute] int page)
		{
			List <ProductHeadersVM> products = _productService.GetProductHeadersForCategory(id, page);
			return Json(products);
		}

		[EnableCors("Policy")]
		public JsonResult ProductDetails(int id)
        {
			ProductDetailsVM product = _productService.GetProductDetails(id);
			return Json(product);
        }

		[EnableCors("Policy")]
		public JsonResult FilterBySupplier(int id)
        {
			List <ProductHeadersVM> products = _productService.GetProductHeadersForSupplier(id);
			return Json(products);
        }
		[HttpGet]
		public IActionResult EditProduct(int id)
        {
			ProductDetailsVM product = _productService.GetProductDetails(id);
			return View(product);
        }
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult EditProduct([FromBody]Product product)
		{
			_productService.Update(product);
			return Ok(new { message = "success" });
		}

		public IActionResult Search(string query)
        {
			throw new NotImplementedException();
        }
        [EnableCors("Policy")]
        public JsonResult GetCategoriesGroup(int id=0)
        {
			return Json(_productService.GetCategoriesGroup(id));
        }
		[EnableCors("Policy")]
		public JsonResult GetProductImages(int id = 0) {
			List<Images> images = _productService.GetProductImages(id);
			if (images == null)
            {
				images = new List<Images>();
            }
			return Json(images);
		}
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult AddProduct([FromBody] ProductDto data) {
			Product product = new();
			product.Aproved = true;//<--this will be false, to be switched by an admin
			product.UserId = data.UserId;
			product.Name = data.Name;
			product.Description = data.Description;
			product.Price = data.Price;//<-- not sure casting is done during binding for this to work
			int productId = _productService.AddProduct(product);
			ProductCategories productCategories = new ProductCategories();
			productCategories.ProductId = productId;
			productCategories.CategoryId = data.SubSubCategoryId;
			_productService.SetProductCategory(productCategories);
			return Ok(new { message = "success" });
		}
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult DeleteProduct([FromRoute] int id) {
			_productService.DeleteProductCategory(id);
			List<Images> images = _productService.GetProductImages(id).ToList();
			foreach (Images image in images) 
			{
				_productService.DeleteProductImage(image.Id);
			}
			_productService.DeleteProduct(id);
			return Ok();
		}
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult AddProductImage([FromBody] Images image) {
			_productService.AddProductImage(image);
			return Ok(new { message = "success" });
		}
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult DeleteProductImage([FromRoute] int id) {
			_productService.DeleteProductImage(id);
			return Ok();
		}
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult SetProductCategory([FromBody] ProductCategories link) {
			_productService.SetProductCategory(link);
			return Ok();
		}
		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult DeleteProductCategory([FromRoute] int id) {
			_productService.DeleteProductCategory(id);
			return Ok();
		}

		[HttpPost]
		public IActionResult AddProductReview([FromBody] ReviewDto review)
        {
			var jwt = Request.Cookies["jwt"];
			var token = _jwtService.Verify(jwt);
			int userId = int.Parse(token.Issuer);

			Reviews newReview = new Reviews();
			newReview.ProductId = review.ProductId;
			newReview.Comment = review.Comment;
			newReview.Rating = review.Rating;
			newReview.UserId = userId;
			_productService.AddProductReview(newReview);
			return Ok();
        }

		[EnableCors("Policy")]
		[HttpPost]
		public IActionResult RemoveProductReview(int id) { 
			_productService.RemoveProductReview(id);
			return Ok();
		}
	}
}
