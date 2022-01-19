using System;
using System.Collections.Generic;
using System.Text;
using Game.Helpers;

namespace Game.Models
{
    /// <summary>
    /// This model is to extend the existing ItemModel to implement the 
    /// functionality for displaying images in the items CRUDI pages where 
    /// users need to select an image in Create and Update
    /// </summary>
    public class ItemImageModel : ItemModel
    {
        // The list of images for items to use for viewing
        public List<String> ImageList { get; set; } = GameImagesHelper.GetItemImage();
    }
}
