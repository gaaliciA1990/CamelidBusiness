using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Helpers
{
    /// <summary>
    /// This helper class is used for selecting preset images in the 
    /// game for Creating or Updating Items, Characters, and Monsters
    /// </summary>
    class GameImagesHelper
    {
        /// <summary>
        /// Creates a list of images for viewing in CRUDI layouts
        /// </summary>
        /// <returns></returns>
        public static List<String> GetItemImage()
        {
            List<String> ItemImageList = new List<String> { "hat1.png", "hat2.png", "ring1.png", "ring2.png", "shield1.png", "shield2.png", "shield3.png", "shield4.png",
            "sword1.png", "sword2.png", "sword3.png", "sword4.png", "sword5.png", "sword6.png", "sword7.png", "sword8.png", "sword9.png", "sword10.png", "sword11.png"};

            return ItemImageList;
        }

        public static List<String> GetCharacterImage()
        {
            List<String> ImageList = new List<String> { "elf1.png", "elf2.png", "elf3.png", "elf4.png", "elf5.png", "elf6.png", "elf7.png", "knight.png", "troll1.png", "troll4.png" };

            return ImageList;
        }
    }
}
