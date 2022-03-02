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
    public class GameImagesHelper
    {
        /// <summary>
        /// Creates a list of images for viewing in items CRUDI layouts
        /// </summary>
        /// <returns></returns>
        public static List<String> GetItemImage()
        {
            List<String> ItemImageList = new List<String> { "andean_cuff.png", "andean_hat.png", "bronze_mace.png", "bronze_spear.png", "curved_bow.png", "pure_gold_ring.png"};

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


        /// <summary>
        /// Creates a list of images for obstacles
        /// </summary>
        /// <returns></returns>
        public static List<String> GetObstacleImage()
        {
            List<String> ImageList = new List<String> { "obstacle1.png", "obstacle2.png", "obstacle3.png" };

            return ImageList;
        }
    }
}