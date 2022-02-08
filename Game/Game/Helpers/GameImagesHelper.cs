using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Helpers
{
    /// <summary>
    /// This helper class is used for selecting preset images in the 
    /// game for Creating or Updating Items, Characters, and Monsters
    /// </summary>
    class GameImagesHelper
    {
        /// <summary>
        /// Creates a list of images for viewing in items CRUDI layouts
        /// </summary>
        /// <returns></returns>
        public static List<String> GetItemImage()
        {
            List<String> ItemImageList = new List<String> { "hat1.png", "hat2.png", "ring1.png", "ring2.png", "shield1.png", "shield2.png", "shield3.png", "shield4.png",
            "sword1.png", "sword2.png", "sword3.png", "sword4.png", "sword5.png", "sword6.png", "sword7.png", "sword8.png", "sword9.png", "sword10.png", "sword11.png"};

            return ItemImageList;
        }

        /// <summary>
        /// Creates a list of images for viewing in character CRUDi layouts
        /// </summary>
        /// <returns></returns>
        public static Dictionary<CharacterClanEnum, List<string>> GetCharacterImage()
        {
            Dictionary<CharacterClanEnum, List<string>> ImageList = new Dictionary<CharacterClanEnum, List<string>>{
                { CharacterClanEnum.Alpaca, new List<string>{ "alpaca1.png", "alpaca2.png", "alpaca3.png" } },
                { CharacterClanEnum.Llama,  new List<string>{ "llama1.png",  "llama2.png",  "llama3.png" } },
                { CharacterClanEnum.Vicuna, new List<string>{ "vicuna1.png", "vicuna2.png", "vicuna3.png" } },
            };

            return ImageList;
        }

        /// <summary>
        /// Creates a list of images for viewing in monster Crudi layouts
        /// </summary>
        /// <returns></returns>
        public static List<String> GetMonsterImage()
        {
            List<String> ImageList = new List<String> { "monster.png", "monster1.png", "monster2.png", "monster3.png", "monster4.png", "monster5.png", "monster6.png" };

            return ImageList;
        }
    }
}