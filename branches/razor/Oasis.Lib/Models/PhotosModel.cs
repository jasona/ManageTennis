using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;

namespace Oasis.Lib.Models
{
    public class PhotosModel
    {
        public Photoset Photoset { get; set; }
        public PhotosetPhotoCollection Photos { get; set; }
    }
}
