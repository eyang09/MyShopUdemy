using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {

        //ProductCategoryRepository context;
        //InMemoryRepository<ProductCategory> context;
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            //context = new ProductCategoryRepository();
            //context = new InMemoryRepository<ProductCategory>();
            this.context = context;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();

            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategories);

            }
            else
            {
                context.Insert(productCategories);
                context.Commit();

                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategories = context.Find(Id);
            if (productCategories == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(productCategories);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategories, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategories);
                }
                productCategoryToEdit.Category = productCategories.Category;
                context.Commit();
                return RedirectToAction("Index");
            }



        }
        public ActionResult Delete(string Id)
        {
            ProductCategory producCategorytToDelete = context.Find(Id);
            if (producCategorytToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                return View(producCategorytToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory producCategorytToDelete = context.Find(Id);
            if (producCategorytToDelete == null)
            {
                return HttpNotFound();

            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}