using System;
using System.Collections.Generic;
using System.Linq;

using Game.Helpers;
using Game.Models;
using Game.ViewModels;


namespace Game.GameRules
{
    public static class RandomPlayerHelper
    {
        //Flag to notify the class if the UTs are using it
        private static bool InTestMode = false;

        /// <summary>
        /// Function set test mode flag to true
        /// </summary>
        public static void TurnOnTestMode()
        {
            InTestMode = true;
        }

        /// <summary>
        /// Function set test mode flag to false
        /// </summary>
        public static void TurnOffTestMode()
        {
            InTestMode = false;
        }

        /// <summary>
        /// Get Health
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetHealth(int level)
        {
            // Roll the Dice and reset the Health
            return DiceHelper.RollDice(level, 10);
        }

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterUniqueItem()
        {
            string result = "";
            if (InTestMode)
            {
                DiceHelper.DisableForcedRolls();
                result = ItemIndexViewModel.Instance.Dataset.ElementAt(DiceHelper.RollDice(1, ItemIndexViewModel.Instance.Dataset.Count()) - 1).Id;
                DiceHelper.EnableForcedRolls();

            }
            if (InTestMode == false)
            {
                result = ItemIndexViewModel.Instance.Dataset.ElementAt(DiceHelper.RollDice(1, ItemIndexViewModel.Instance.Dataset.Count()) - 1).Id;
            }
            
            return result;
        }

        /// <summary>
        /// Get A Random unique item
        /// </summary>
        /// <returns></returns>
        public static string GetRandomUniqueItem()
        {

            var result = ItemIndexViewModel.Instance.UniqueItems.ElementAt(DiceHelper.RollDice(1, ItemIndexViewModel.Instance.UniqueItems.Count()) - 1).Id;

            return result;
        }

        /// <summary>
        /// Function to help generate a random basic item
        /// </summary>
        /// <returns></returns>
        public static string GetRandomBasicItem()
        {
            var listItem = ItemIndexViewModel.Instance.Dataset.Where(m => m.IsUnique == false).ToList();

            //ItemViewModel failed to load default data
            if(listItem.Count() == 0)
            {
                return null;
            }

            //In case this is being called from the unit test/hack mode
            if (DiceHelper.ForceRollsToNotRandom)
            {
                DiceHelper.DisableForcedRolls();
                var toReturn = listItem.ElementAt(DiceHelper.RollDice(1, listItem.Count()) - 1).Id;
                DiceHelper.EnableForcedRolls();
                return toReturn;
            }

            //In regular game mode
            var result = listItem.ElementAt(DiceHelper.RollDice(1, listItem.Count()) - 1).Id;

            return result;

        }
        

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static DifficultyEnum GetMonsterDifficultyValue()
        {
            var DifficultyList = DifficultyEnumHelper.GetListMonster;

            var RandomDifficulty = DifficultyList.ElementAt(DiceHelper.RollDice(1, DifficultyList.Count()) - 1);

            var result = DifficultyEnumHelper.ConvertStringToEnum(RandomDifficulty);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterImage()
        {
            //extract from our current dataset
            //var allURIs = from monster in MonsterIndexViewModel.Instance.Dataset select monster.ImageURI  ;
            //Hide the great leader
            var temp = MonsterIndexViewModel.Instance.Dataset.ToList().Where(m => m.ImageURI != "monster.png");
            var allURIs = from monster in temp select monster.ImageURI;
            var result = allURIs.ElementAt(DiceHelper.RollDice(1, allURIs.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static (String, CharacterClanEnum) GetCharacterImage()
        {
            List<(String, CharacterClanEnum)> FirstNameList = new List<(String, CharacterClanEnum)> { ("alpaca1.png", CharacterClanEnum.Alpaca), ("alpaca2.png", CharacterClanEnum.Alpaca), ("alpaca3.png", CharacterClanEnum.Alpaca),
                                                                                                   ("llama1.png", CharacterClanEnum.Llama), ("llama2.png", CharacterClanEnum.Llama), ("llama3.png", CharacterClanEnum.Llama),
                                                                                                   ("vicuna1.png", CharacterClanEnum.Vicuna), ("vicuna2.png", CharacterClanEnum.Vicuna), ("vicuna3.png", CharacterClanEnum.Vicuna)};

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Name
        /// 
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterName()
        {

            //extract from our current dataset
            var FirstNameList = from monster in MonsterIndexViewModel.Instance.Dataset select monster.Name;

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Description
        /// 
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterDescription()
        {
            //extract from our current dataset
            var StringList = from monster in MonsterIndexViewModel.Instance.Dataset select monster.Description;

            var result = StringList.ElementAt(DiceHelper.RollDice(1, StringList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Name
        /// 
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterName()
        {

            List<String> FirstNameList = new List<String> { "Sadie", "Grace", "Ruby", "Jenetta", "Liv", "Lulu", "Ella", "Imam", "Bennett", "Niel", "Marshall", "Ricky", "Natsu", "Naru", "Hyung", "Jay", "Beom", "Satsuke", "Gray", "Polo" };

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Description
        /// 
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterDescription()
        {
            List<String> StringList = new List<String> { "the terrible", "the awesome", "the lost", "the old", "the younger", "the quiet", "the loud", "the helpless", "the happy", "the sleepy", "the angry", "the clever" };

            var result = StringList.ElementAt(DiceHelper.RollDice(1, StringList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Random Ability Number
        /// </summary>
        /// <returns></returns>
        public static int GetAbilityValue()
        {
            // 0 to 9, not 1-10
            return DiceHelper.RollDice(1, 10) - 1;
        }

        /// <summary>
        /// Get a Random Level
        /// </summary>
        /// <returns></returns>
        public static int GetLevel()
        {
            // 1-20
            return DiceHelper.RollDice(1, 20);
        }

        /// <summary>
        /// Get a Random Item for the Location
        /// 
        /// Return the String for the ID
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetItem(ItemLocationEnum location)
        {
            var ItemList = ItemIndexViewModel.Instance.GetLocationItems(location);
            if (ItemList.Count == 0)
            {
                return null;
            }

            // Add None to the list
            ItemList.Add(new ItemModel { Id = null, Name = "None" });

            var result = ItemList.ElementAt(DiceHelper.RollDice(1, ItemList.Count()) - 1).Id;
            return result;
        }

        /// <summary>
        /// Create Random Character for the battle
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        public static CharacterModel GetRandomCharacter(int MaxLevel)
        {
            // If there are no characters in the system, return a default one
            if (CharacterIndexViewModel.Instance.Dataset.Count == 0)
            {
                return new CharacterModel();
            }

            var rnd = DiceHelper.RollDice(1, CharacterIndexViewModel.Instance.Dataset.Count);

            var result = new CharacterModel(CharacterIndexViewModel.Instance.Dataset.ElementAt(rnd - 1))
            {
                Level = DiceHelper.RollDice(1, MaxLevel),

                // Randomize Name
                Name = GetCharacterName(),
                Description = GetCharacterDescription(),

                // Randomize the Attributes
                Attack = GetAbilityValue(),
                Speed = GetAbilityValue(),
                Defense = GetAbilityValue(),

                // Randomize an Item for Location
                Head = GetItem(ItemLocationEnum.Head),
                Necklass = GetItem(ItemLocationEnum.Necklass),
                PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand),
                OffHand = GetItem(ItemLocationEnum.OffHand),
                RightFinger = GetItem(ItemLocationEnum.Finger),
                LeftFinger = GetItem(ItemLocationEnum.Finger),
                Feet = GetItem(ItemLocationEnum.Feet),
            };

            (result.ImageURI, result.Clan) = GetCharacterImage();


            result.MaxHealth = DiceHelper.RollDice(MaxLevel, 10);

            // Level up to the new level
            _ = result.LevelUpToValue(result.Level);

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            return result;
        }

        /// <summary>
        /// Create Random Character for the battle
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        public static MonsterModel GetRandomMonster(int MaxLevel, bool Items = false, CharacterJobEnum boss = CharacterJobEnum.Unknown)
        {
            MonsterModel result = null;
            var basicMonsters = MonsterIndexViewModel.Instance.Dataset.ToList().Where(m => m.Job != CharacterJobEnum.GreatLeader);


            // If there are no Monsters in the system, return a default one
            if (MonsterIndexViewModel.Instance.Dataset.Count == 0)
            {
                return new MonsterModel();
            }

            //var rnd = DiceHelper.RollDice(1, MonsterIndexViewModel.Instance.Dataset.Count);
            var rnd = DiceHelper.RollDice(1, basicMonsters.Count());

            //Make a Great Boss
            if (boss == CharacterJobEnum.GreatLeader)
            {
                result = new MonsterModel(MonsterIndexViewModel.Instance.Dataset.Where(m => m.Job == CharacterJobEnum.GreatLeader).First())
                {
                    Level = DiceHelper.RollDice(1, MaxLevel),

                    // Randomize the Attributes
                    Attack = GetAbilityValue(),
                    Speed = GetAbilityValue(),
                    Defense = GetAbilityValue(),

                    Difficulty = DifficultyEnum.Impossible
                };
            }

            //Make regular monster, including round boss
            if(boss != CharacterJobEnum.GreatLeader)
            {
                result = new MonsterModel(basicMonsters.ElementAt(rnd - 1))
                {
                    Level = DiceHelper.RollDice(1, MaxLevel),
                    // Randomize Name
                    Name = GetMonsterName(),
                    Description = GetMonsterDescription(),

                    // Randomize the Attributes
                    Attack = GetAbilityValue(),
                    Speed = GetAbilityValue(),
                    Defense = GetAbilityValue(),

                    ImageURI = GetMonsterImage(),

                    Difficulty = GetMonsterDifficultyValue(),
                    Job = CharacterJobEnum.Unknown
                };
            }
            
            //Set round boss job and difficulty
            if (boss == CharacterJobEnum.RoundBoss)
            {
                result.Job = CharacterJobEnum.RoundBoss;
                result.Difficulty = DifficultyEnum.Difficult;
            }

            // Adjust values based on Difficulty
            result.Attack = result.Difficulty.ToModifier(result.Attack);
            result.Defense = result.Difficulty.ToModifier(result.Defense);
            result.Speed = result.Difficulty.ToModifier(result.Speed);
            result.Level = result.Difficulty.ToModifier(result.Level);

            // Get the new Max Health
            result.MaxHealth = DiceHelper.RollDice(result.Level, 10);

            // Adjust the health, If the new Max Health is above the rule for the level, use the original
            var MaxHealthAdjusted = result.Difficulty.ToModifier(result.MaxHealth);
            if (MaxHealthAdjusted < result.Level * 10)
            {
                result.MaxHealth = MaxHealthAdjusted;
            }

            // Level up to the new level
            _ = result.LevelUpToValue(result.Level);

            // Set ExperienceRemaining so Monsters can both use this method
            result.ExperienceRemaining = LevelTableHelper.LevelDetailsList[result.Level + 1].Experience;

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            // Monsters can have weapons too....
            if (Items)
            {
                result.Head = GetItem(ItemLocationEnum.Head);
                result.Necklass = GetItem(ItemLocationEnum.Necklass);
                result.PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand);
                result.OffHand = GetItem(ItemLocationEnum.OffHand);
                result.RightFinger = GetItem(ItemLocationEnum.Finger);
                result.LeftFinger = GetItem(ItemLocationEnum.Finger);
                result.Feet = GetItem(ItemLocationEnum.Feet);
            }

            return result;
        }
    }
}