﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using pvone.Backend.Models;
using pvone.Common.Models;
using pvone.Backend.Helpers;
using System;

namespace pvone.Backend.Controllers
{
    public class ProductsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Products";

                if (view.ImageFile != null)
                {
                    pic = FilesHelpers.UploadPhotos(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var product = this.ToProduct(view, pic);

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Product ToProduct(ProductView view, string pic)
        {
            return new Product
            {
                Description =  view.Description,
                ImagePath = pic,
                IsAvailable = view.IsAvailable,
                Price = view.Price,
                ProductId = view.ProductId,
                PublishOn = view.PublishOn,
                Remarks = view.Remarks,
            };
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(product);

            return View(view);
        }

        private ProductView ToView(Product product)
        {
            return new ProductView
            {
                Description = product.Description,
                ImagePath = product.ImagePath,
                IsAvailable = product.IsAvailable,
                Price = product.Price,
                ProductId = product.ProductId,
                PublishOn = product.PublishOn,
                Remarks = product.Remarks,
            };
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductView view)
        {
            if (ModelState.IsValid)
            {

                var pic = view.ImagePath;
                var folder = "~/Content/Products";

                if (view.ImageFile != null)
                {
                    pic = FilesHelpers.UploadPhotos(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var product = this.ToProduct(view, pic);
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
