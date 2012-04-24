using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;

namespace Oasis.Lib.Services
{
    public interface IGalleryService
    {
        Photoset GetPhotosetByName(string photoSetName);
        PhotosetCollection GetPhotosets();
        PhotosetPhotoCollection GetPhotosByPhotosetName(string photoSetName);
        PhotosetPhotoCollection GetPhotos(string photoSetId);
    }
}
