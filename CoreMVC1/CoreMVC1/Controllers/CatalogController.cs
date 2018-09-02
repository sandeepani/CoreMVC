using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC1.Models.Catalog;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC1.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        public CatalogController(ILibraryAsset assets)
        {
            _assets = assets;
        }
        public IActionResult Index(string title="", string author="")
        {
            var assetModels = _assets.GetAll()
                .Where(a=>(string.IsNullOrEmpty( title))||(string.IsNullOrEmpty(author))|| _assets.GetAuthorOrDirecor(a.Id)==author|| a.Title == title);
            
            var l = assetModels.Select(r => new AssetIndexListingModel
            {
                Id=r.Id,
                ImageUrl=r.ImageUrl,
                DeweyCallNumber=_assets.GetDewyIndex(r.Id),
                Type=_assets.GetType(r.Id),
                Title=r.Title
            }
                );
            var model = new AssetIndexModel() {
                Assets = l
            };
            return View(model);
        }

        public PartialViewResult Edit(int id)
        {
            var assetModel = _assets.GetByid(id);
            if (_assets.GetType(id) == "Book") return PartialView("EditBook",id);
            else if (_assets.GetType(id) == "Video") PartialView("EditVideo", id);

            return PartialView(assetModel);
        }
        [HttpPost]
        public PartialViewResult SaveEdit(LibraryAsset asset)
        {
            _assets.Update(asset);

            return PartialView("Edit");
        }
        public PartialViewResult EditBook(int id)
        {
            var assetModel = _assets.GetByid(id);

            return PartialView(assetModel);
        }
        [HttpPost]
        public void SaveEditBook(Book asset)
        {
            _assets.Update(asset);

            Index();
        }
        public PartialViewResult EditVideo(int id)
        {
            var assetModel = _assets.GetByid(id);

            return PartialView(assetModel);
        }
        [HttpPost]
        public void SaveEditVideo(Video asset)
        {
            _assets.Update(asset);

            Index();
        }
    }
}