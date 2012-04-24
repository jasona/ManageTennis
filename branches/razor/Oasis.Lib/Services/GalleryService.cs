using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using System.Configuration;
using FTW.Web.Caching;
using Oasis.Lib.Components;
using Oasis.Lib.Util;
using DMag.Components;

namespace Oasis.Lib.Services
{
    public class GalleryService : IGalleryService
    {
        private static Flickr _flickr;

        private static Flickr Gallery
        {
            get
            {
                if (_flickr == null)
                {
                    Flickr.CacheDisabled = true;
                    _flickr = new Flickr(ConfigurationManager.AppSettings["FlickrKey"], ConfigurationManager.AppSettings["FlickrSecret"]);
                }

                
                return _flickr;
            }
        }

        private static PhotosetCollection Photosets
        {
            get
            {
                return OCache<PhotosetCollection>.Get("Oasis.Photosets",
                    CacheOptions.MediumRefresh,
                    () =>
                    {
                        var user = Gallery.PeopleFindByEmail("oasis.tennis@yahoo.com");

                        return Gallery.PhotosetsGetList(user.UserId);
                    });
            }
        }

        public PhotosetCollection GetPhotosets()
        {
            return Photosets;
        }

        public PhotosetPhotoCollection GetPhotosByPhotosetName(string photoSetName)
        {
            Photoset photoSet = null;


            photoSet = GetPhotosetByName(photoSetName);

            if (photoSet == null) return new PhotosetPhotoCollection();

            return GetPhotos(photoSet.PhotosetId);
        }


        public Photoset GetPhotosetByName(string photoSetName)
        {
            Photoset foundPhotoset = null;


            foreach (var set in Photosets)
            {
                if (set.ToFriendlyUrl() == photoSetName)
                {
                    foundPhotoset = set;
                    break;
                }
            }

            return foundPhotoset;
        }


        public PhotosetPhotoCollection GetPhotos(string photoSetId)
        {
            string keyName;
            keyName = "Oasis.Photos." + photoSetId;


            return OCache<PhotosetPhotoCollection>.Get(keyName,
                CacheOptions.MediumRefresh,
                () =>
                {
                    return Gallery.PhotosetsGetPhotos(photoSetId);
                });
        }
    }
}
