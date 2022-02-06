using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// Clan that describes the character
    /// </summary>
    public enum CharacterClanEnum
    {
        // The Alpaca Clan
        Alpaca = 0,

        //The Llama Clan
        Llama = 1,

        //The Vicuna Clan
        Vicuna = 2,

        // Not Known
        Unknown = 3
    }

    public static class CharacterClanEnumHelper
    {
        /// <summary>
        /// Gets the list of Clans.
        /// </summary>
        public static List<string> GetClanList
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterClanEnum)).ToList();
                var myReturn = myList.Where(a =>
                                            a.ToString() != CharacterClanEnum.Unknown.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();
                return myReturn;
            }
        }
    }
}
