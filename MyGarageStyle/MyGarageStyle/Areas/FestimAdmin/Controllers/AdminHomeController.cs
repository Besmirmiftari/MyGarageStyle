using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGarageStyle.Data;
using MyGarageStyle.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyGarageStyle.Areas.Admin.Controllers
{
    [Area("FestimAdmin")]
    [Authorize]
    public class AdminHomeController : Controller
    {

        private readonly ApplicationDbContext context;


        public AdminHomeController(ApplicationDbContext context)
        {
            this.context = context;
          
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var products = from p in context.PVCs select p;
            int pageSize = 6;

            products = products.OrderByDescending(p => p.AddedDate);

            return View(await PaginatedList<PVC>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult AddPVC()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPVC(PVCViewModel model)
        {

            if (model.DefaultImage != null && model.Images != null)
            {
              

                PVC newModel = new PVC(){

                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Dimensions = model.Dimensions,
                    Thickness = model.Thickness,
                    Weight = model.Weight,
                    Guarantee = model.Guarantee,
                    Anticipated_Service_Life = model.Anticipated_Service_Life,
                    Material = model.Material,
                    Joint = model.Joint,
                    InStock = model.InStock,
                    Price = model.Price,
                    AddedDate = DateTime.Now,
                    Description = model.Description

                };

                byte[] defaultImageBytes = null;


                using (var stream = new MemoryStream())
                {
                    await model.DefaultImage.CopyToAsync(stream);
                    defaultImageBytes = stream.ToArray();

                    newModel.DefaultImage = defaultImageBytes;
                }

                context.Add(newModel);

                foreach (var image in model.Images)
                {
                    PVCImages productImages = new PVCImages();
                    productImages.Id = Guid.NewGuid();

                    byte[] ImagesBytes = null;


                    using (var imageStream = new MemoryStream())
                    {
                        await image.CopyToAsync(imageStream);
                        ImagesBytes = imageStream.ToArray();

                    }

                    productImages.Image = ImagesBytes;
                    productImages.PVCId = newModel.Id;

                    context.Add(productImages);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();



            }
            else
            {
                return View();
            }


              return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = await context.PVCs.FindAsync(Id);

            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(await context.PVCs.FindAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid modelId,PVC model)
        {
            var oldModelData = await context.PVCs.AsNoTracking().Where(x => x.Id.Equals(modelId)).FirstOrDefaultAsync();

            if (oldModelData == null)
            {
                return RedirectToAction(nameof(Index));
            }



            model.Id = modelId;
            model.DefaultImage = oldModelData.DefaultImage;
            model.AddedDate = oldModelData.AddedDate;

            context.PVCs.Update(model);

            await context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(Guid Id)
        {
            var model = await context.PVCs.FindAsync(Id);

            if (model != null)
            {
                context.PVCs.Remove(model);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
