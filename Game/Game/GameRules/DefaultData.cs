using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Helpers;
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
                    ImageURI = "unique_curvedbow.png",
                    Range = 3,
                    Damage = 3,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true},

                new ItemModel {
                    Name = "Bronze Spear",
                    Description = "A spectacular craftsmanship infused with magic that packs a punch!",
                    ImageURI = "unique_bronzespear.png",
                    Range = 2,
                    Damage = 4,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true},

                new ItemModel {
                    Name = "Bronze Maze",
                    Description = "An  metal mace to beat your enemies with",
                    ImageURI = "unique_bronzemace.png",
                    Range = 1,
                    Damage = 5,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack,
                    IsUnique = true},

                new ItemModel {
                    Name = "Andean Hat",
                    Description = "A traditional and colorful hat. Stylish and protected!",
                    ImageURI = "unique_andeanhat.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense,
                    IsUnique = true},

                new ItemModel {
                    Name = "Andean Cuff",
                    Description = "Beautiful adornments",
                    ImageURI = "unique_andeancuff.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense,
                    IsUnique = true},

                new ItemModel {
                    Name = "Andean Scarf",
                    Description = "Stylish scarf that provides extra protection!",
                    ImageURI = "unique_andeanscarf.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense,
                    IsUnique = true},

                new ItemModel {
                    Name = "Pure Gold Ring",
                    Description = "Snazzy gold ring, will make you feel like the fanciest warrior",
                    ImageURI = "unique_puregoldring.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth,
                    IsUnique = true},

                new ItemModel {
                    Name = "Incan Scarf",
                    Description = "Elegantly crafted in the depths of Machu Picchu, this scarf looks amazing on you and boosts your health.",
                    ImageURI = "unique_incanscarf.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.MaxHealth,
                    IsUnique = true},

                new ItemModel {
                    Name = "Tumi Ring",
                    Description = "Wow, you must be special! Let the power of the Incan god Inti flow through in this Tumi style ring that increase your health. ",
                    ImageURI = "unique_tumiring.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth,
                    IsUnique = true},

                new ItemModel {
                    Name = "Fedora",
                    Description = "You can be the cool camelid of party; with a speed boost no one can beat.",
                    ImageURI = "unique_fedora.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed,
                    IsUnique = true},

                new ItemModel {
                    Name = "Golden Anklet",
                    Description = "A stylish and lightweight anklet made from holy gold of the Incans to enhance you Speed. ",
                    ImageURI = "unique_puregoldenanklet.png",
                    Range = 0,
                    Damage = 0,
                    Value = 6,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed,
                    IsUnique = true},

                new ItemModel {
                    Name = "Pure Silver Anklet",
                    Description = "Anklets aren’t just for the ladies, especially when they have a need for speed!",
                    ImageURI = "unique_puresilveranklet.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed,
                    IsUnique = true},

                
                new ItemModel {
                    Name = "Dia Earrings",
                    Description = "Colorful earings",
                    ImageURI = "basic_earring.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
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
                    Value = 3,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "French beret",
                    Description = "Where's the croissant?",
                    ImageURI = "basic_hat2.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Sombrero",
                    Description = "Hola!",
                    ImageURI = "basic_sombrero.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Fuzzy Boots",
                    Description = "Fuzzy boots keeping you warm",
                    ImageURI = "basic_boots.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Ballet shoes",
                    Description = "Dance through the battlefield",
                    ImageURI = "basic_slippers.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
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
                    Name = "Gold necklace",
                    Description = "Blinding their sights",
                    ImageURI = "basic_necklace.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},

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
                    Value = 1,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ancient shield",
                    Description = "Bronze shield",
                    ImageURI = "basic_shield.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
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
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Furry hat",
                    Description = "You look stylish!",
                    ImageURI = "basic_hat.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Tree branch",
                    Description = "Savagae",
                    ImageURI = "basic_stick.png",
                    Range = 1,
                    Damage = 1,
                    Value = 1,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Rusty sword",
                    Description = "Little rusty but can do some damage",
                    ImageURI = "basic_sword.png",
                    Range = 1,
                    Damage = 1,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack },

                new ItemModel {
                    Name = "Torch",
                    Description = "Burn them to ashes!",
                    ImageURI = "basic_torch.png",
                    Range = 1,
                    Damage = 2,
                    Value = 3,
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

        public static string FindRandomBasicItem(ObservableCollection<ItemModel> collection, ItemLocationEnum location, int level = 1)
        {
            string findImageURI;
            //level 1 = stick, level 2 = sword, level 3 = torch
            switch (level){
                case 2:
                    findImageURI = "basic_sword.png";
                    break;
                case 3:
                    findImageURI = "basic_torch.png";
                    break;
                default:
                    findImageURI = "basic_stick.png";
                    break;
            }

            //Find the requested item from the default item pool, but only the basic ones
            if(location == ItemLocationEnum.PrimaryHand)
            {
                var returnItem = collection.Where(item => item.ImageURI == findImageURI).FirstOrDefault();
                return returnItem.Id;
            }

            //otherwise find the item as specified by location, but only the basic ones
            var myList = collection.Where(item => item.Location == location && item.IsUnique == false);
            var toReturn = myList.ElementAt(DiceHelper.RollDice(1, myList.Count() - 1) );
            return toReturn.Id;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var AvailableItems = ItemIndexViewModel.Instance.Dataset;
            //var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            //var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            //var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            //var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            //var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            //var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            //var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Bill",
                    Description = "Majestic",
                    Level = 3,
                    MaxHealth = 10,
                    ImageURI = "alpaca1_animation.gif",
                    Clan = CharacterClanEnum.Alpaca,
                    Head = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Head),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 3),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                },

                 new CharacterModel {
                    Name = "Mandy",
                    Description = "Elegant",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "alpaca2_animation.gif",
                    Clan = CharacterClanEnum.Alpaca,
                    Head = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Head),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 1),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                },
                  new CharacterModel {
                    Name = "Sunny",
                    Description = "Dimwitted",
                    Level = 2,
                    MaxHealth = 7,
                    ImageURI = "alpaca3_animation.gif",
                    Clan = CharacterClanEnum.Alpaca,
                    Head = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Head),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 2),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                },

                new CharacterModel {
                    Name = "Jill",
                    Description = "Wild",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "llama2_animation.gif",
                    Clan = CharacterClanEnum.Llama,
                    Head = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Head),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 1),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                },

                new CharacterModel {
                    Name = "Steffy",
                    Description = "Sardonic",
                    Level = 2,
                    MaxHealth = 7,
                    ImageURI = "llama1_animation.gif",
                    Clan = CharacterClanEnum.Llama,
                    Head = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Head),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 2),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                },

                new CharacterModel {
                    Name = "Peter",
                    Description = "Pretentious",
                    Level = 3,
                    MaxHealth = 10,
                    ImageURI = "llama3_animation.gif",
                    Clan = CharacterClanEnum.Llama,
                    Head = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Head),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 3),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                },

                new CharacterModel {
                    Name = "Will",
                    Description = "Untaimed",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "vicuna3_animation.gif",
                    Clan = CharacterClanEnum.Vicuna,
                    Necklass = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Necklass),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 1),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    Feet = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Feet),
                },

                new CharacterModel {
                    Name = "Dan",
                    Description = "Cautious",
                    Level = 2,
                    MaxHealth = 7,
                    ImageURI = "vicuna2_animation.gif",
                    Clan = CharacterClanEnum.Vicuna,
                    Necklass = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Necklass),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 1),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    RightFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    Feet = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Feet),
                },

                 new CharacterModel {
                    Name = "Jack",
                    Description = "Jovial",
                    Level = 3,
                    MaxHealth = 10,
                    ImageURI = "vicuna1_animation.gif",
                    Clan = CharacterClanEnum.Vicuna,
                    Necklass = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Necklass),
                    PrimaryHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.PrimaryHand, 1),
                    OffHand = FindRandomBasicItem(AvailableItems, ItemLocationEnum.OffHand),
                    LeftFinger = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Finger),
                    Feet = FindRandomBasicItem(AvailableItems, ItemLocationEnum.Feet),
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Monsters
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
                    ImageURI = "monster1_animation.gif",
                },

                new MonsterModel {
                    Name = "Furious Bear",
                    Description = "Big and Ugly",
                    ImageURI = "monster1_animation.gif",
                    Job = CharacterJobEnum.RoundBoss
                },

                new MonsterModel {
                    Name = "Saavy Coyote",
                    Description = "Old and Powerfull",
                    ImageURI = "monster2_animation.gif",
                },

                new MonsterModel {
                    Name = "Wicked Coyote",
                    Description = "Old and Powerfull",
                    ImageURI = "monster2_animation.gif",
                    Job = CharacterJobEnum.RoundBoss
                },

                new MonsterModel {
                    Name = "Cunning Culpeo",
                    Description = "Don't mess with",
                    ImageURI = "monster3_animation.gif",
                },

                new MonsterModel {
                    Name = "Witty Culpeo",
                    Description = "Don't mess with",
                    ImageURI = "monster3_animation.gif",
                    Job = CharacterJobEnum.RoundBoss
                },

                new MonsterModel {
                    Name = "Great Leader",
                    Description = "Camelid with a grudge",
                    ImageURI = "greatLeader_animation.gif",
                    Job = CharacterJobEnum.GreatLeader
                },

                new MonsterModel {
                    Name = "Ferocious Lion",
                    Description = "He's from the mountain",
                    ImageURI = "monster5_animation.gif",
                },

                new MonsterModel {
                    Name = "Hangry Lion",
                    Description = "He's from the mountain",
                    ImageURI = "monster5_animation.gif",
                    Job = CharacterJobEnum.RoundBoss
                },

                new MonsterModel {
                    Name = "Puma Pam",
                    Description = "Sleek and fast",
                    ImageURI = "monster6_animation.gif",
                },

                new MonsterModel {
                    Name = "Superior Puma",
                    Description = "Sleek and fast",
                    ImageURI = "monster6_animation.gif",
                    Job = CharacterJobEnum.RoundBoss
                },

            };

            return datalist;
        }
    }
}