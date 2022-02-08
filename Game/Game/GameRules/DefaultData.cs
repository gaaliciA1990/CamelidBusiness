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
                    Name = "Gold Sword",
                    Description = "Sword made of Gold, really expensive looking",
                    ImageURI = "sword1.png",
                    Range = 0,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true },

                new ItemModel {
                    Name = "Pirate Sword",
                    Description = "Aye matie",
                    ImageURI = "sword2.png",
                    Range = 0,
                    Damage = 8,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Dagger",
                    Description = "watch out",
                    ImageURI = "sword3.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Strong Sword",
                    Description = "watch out",
                    ImageURI = "sword4.png",
                    Range = 0,
                    Damage = 12,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Wand",
                    Description = "watch out",
                    ImageURI = "sword5.png",
                    Range = 0,
                    Damage = 4,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Mace",
                    Description = "watch out",
                    ImageURI = "sword6.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Mace of Health",
                    Description = "Feeling Good",
                    ImageURI = "sword7.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Arrows",
                    Description = "Poke your eye out",
                    ImageURI = "sword8.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Boxing",
                    Description = "watch out",
                    ImageURI = "sword9.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Bow",
                    Description = "Fast Bow",
                    ImageURI = "sword10.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Fire Bow",
                    Description = "Fast Bow",
                    ImageURI = "sword11.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Strong Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Fancy Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "shield2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Health Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "shield3.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.MaxHealth,
                    IsUnique = true },

                new ItemModel {
                    Name = "Lucky Shield",
                    Description = "Do you feel lucky punk?",
                    ImageURI = "shield4.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Bunny Hat",
                    Description = "Pink hat with fluffy ears",
                    ImageURI = "hat1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed,
                    IsUnique = true },

                new ItemModel {
                    Name = "Horned Hat",
                    Description = "Where's the Rabbit?",
                    ImageURI = "hat2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Fast Necklass",
                    Description = "And Tasty",
                    ImageURI = "neck1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed,
                    IsUnique = true },

                new ItemModel {
                    Name = "Feel the Power",
                    Description = "Love this one",
                    ImageURI = "neck2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Horned Hat",
                    Description = "Where's the Rabbit?",
                    ImageURI = "hat2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ring of Power",
                    Description = "The wearer has all the power",
                    ImageURI = "ring1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Strong Ring",
                    Description = "Watch out",
                    ImageURI = "ring2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Warm Shoes",
                    Description = "Strong Shoes",
                    ImageURI = "feet1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Cute Shoes",
                    Description = "really fast",
                    ImageURI = "feet2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},
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