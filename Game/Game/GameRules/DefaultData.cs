using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Curved Bow",
                    Description = "An enhanced, curvy bow",
                    ImageURI = "curved_bow.png",
                    Range = 6,
                    Damage = 10,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true},

                new ItemModel {
                    Name = "Bronze Spear",
                    Description = "A spectacular craftsmanship infused with magic that packs a punch!",
                    ImageURI = "bronze_spear.png",
                    Range = 3,
                    Damage = 8,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true},

                new ItemModel {
                    Name = "Bronze Maze",
                    Description = "An  metal mace to beat your enemies with",
                    ImageURI = "bronze_mace.png",
                    Range = 1,
                    Damage = 6,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true},

                new ItemModel {
                    Name = "Andean Hat",
                    Description = "A traditional and colorful hat. Stylish and protected!",
                    ImageURI = "andean_hat.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense,
                    IsUnique = true},


                new ItemModel {
                    Name = "Andean Cuff",
                    Description = "Beautiful adornments",
                    ImageURI = "andean_cuff.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense,
                    IsUnique = true},

                new ItemModel {
                    Name = "Pure Gold Ring",
                    Description = "Snazzy gold ring, will make you feel like the fanciest warrior",
                    ImageURI = "pure_gold_ring.png",
                    Range = 0,
                    Damage = 0,
                    Value = 6,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth,
                    IsUnique = true},
            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Bill",
                    Description = "Majestic",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "Alpaca1.png",
                    Clan = CharacterClanEnum.Alpaca,
                    Head = HeadString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Jill",
                    Description = "Wild",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "Llama2.png",
                    Clan = CharacterClanEnum.Llama,
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    Feet = FeetString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Will",
                    Description = "Untaimed",
                    Level = 1,
                    MaxHealth = 8,
                    ImageURI = "Vicuna3.png",
                    Clan = CharacterClanEnum.Vicuna,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Angry Bear",
                    Description = "Big and Ugly",
                    ImageURI = "monster1.png",
                },

                new MonsterModel {
                    Name = "Saavy Coyote",
                    Description = "Old and Powerfull",
                    ImageURI = "monster2.png",
                },

                new MonsterModel {
                    Name = "Cunning Culpeo",
                    Description = "Don't mess with",
                    ImageURI = "monster3.png",
                },

                new MonsterModel {
                    Name = "Great Leader",
                    Description = "Camelid with a grudge",
                    ImageURI = "monster.png",
                },

                new MonsterModel {
                    Name = "Ferocious Lion",
                    Description = "He's from the mountain",
                    ImageURI = "monster5.png",
                },

                new MonsterModel {
                    Name = "Puma Pam",
                    Description = "Sleek and fast",
                    ImageURI = "monster6.png",
                },
            };

            return datalist;
        }
    }
}