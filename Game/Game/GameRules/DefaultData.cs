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

                new ItemModel {
                    Name = "Fuzzy Boots",
                    Description = "Fuzzy boots keeping you warm",
                    ImageURI = "basic_boots.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Dia Earrings",
                    Description = "Colorful earings",
                    ImageURI = "basic_earring.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Ruby Earrings",
                    Description = "Long ruby earings",
                    ImageURI = "basic_earring2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Soft gold earrings",
                    Description = "Shiny",
                    ImageURI = "basic_earring3.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Furry hat",
                    Description = "You look stylish!",
                    ImageURI = "basic_hat.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "French beret",
                    Description = "Where's the croissant?",
                    ImageURI = "basic_hat2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Sombrero",
                    Description = "Hola!",
                    ImageURI = "basic_sombrero.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Gold necklace",
                    Description = "Blinding their sights",
                    ImageURI = "basic_necklace.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Silver necklace",
                    Description = "So pretty!",
                    ImageURI = "basic_necklace2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Winter scarf",
                    Description = "Keep you warm",
                    ImageURI = "basic_scarf.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Ancient shield",
                    Description = "Bronze shield",
                    ImageURI = "basic_shield.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Smiling shield",
                    Description = "Scaring your enemies away",
                    ImageURI = "basic_shield2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Andean shield",
                    Description = "So colorful",
                    ImageURI = "basic_shield3.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ballet shoes",
                    Description = "Dance through the battlefield",
                    ImageURI = "basic_slippers.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Warm socks",
                    Description = "They're warm",
                    ImageURI = "basic_socks.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Tree branch",
                    Description = "Savagae",
                    ImageURI = "basic_stick.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Rusty sword",
                    Description = "Little rusty but can do some damage",
                    ImageURI = "basic_sword.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack },

                new ItemModel {
                    Name = "Torch",
                    Description = "Burn them to ashes!",
                    ImageURI = "basic_torch.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack },
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