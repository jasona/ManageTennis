using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Services;
using Oasis.Lib.Models;

namespace Oasis.Lib.Controllers
{
    public class GalleryController : Controller
    {

        public IGalleryService GalleryService { get; set; }

        public GalleryController(IGalleryService galleryService)
        {
            GalleryService = galleryService;
        }

        public ActionResult Index()
        {
            var model = new GalleryModel();
            var photoSets = GalleryService.GetPhotosets();

            model.PhotoSets = photoSets;

            return View(model);
        }

        public ActionResult ViewPhotoSet(string photoSetName)
        {
            PhotosModel model = new PhotosModel();
            var photoSet = GalleryService.GetPhotosetByName(photoSetName);
            var photos = GalleryService.GetPhotos(photoSet.PhotosetId);

            model.Photoset = photoSet;
            model.Photos = photos;

            return View(model);
        }

        public ActionResult ViewPhoto(string photoSetName, string photoId)
        {
            return View();
        }
    }
}
