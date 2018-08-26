using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC1.Models.Catalog;
using LibraryData;
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
        public IActionResult Index() {
            var assetModels = _assets.GetAll();
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
    }
}