using System;
using System.Collections.Generic;
using System.Linq;

using Game.Engine.EngineBase;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.GameRules;
using Game.Models;
using Game.ViewModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Manages the Rounds
    /// </summary>
    public class RoundEngine : RoundEngineBase, IRoundEngineInterface
    {
        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        // The Turn Engine
        public new ITurnEngineInterface Turn
        {
            get
            {
                if (base.Turn == null)
                {
                    base.Turn = new TurnEngine();
                }
                return base.Turn;
            }
            set { base.Turn = Turn; }
        }

        public object BaseEngine { get; private set; }

        /// <summary>
        /// Clear the List between Rounds
        /// </summary>
        /// <returns></returns>
        public override bool ClearLists()
        {
            return base.ClearLists();
        }

        /// <summary>
        /// Set the Current Attacker
        /// </summary>
        public override bool SetCurrentAttacker(PlayerInfoModel player)
        {
            return base.SetCurrentAttacker(player);
        }

        /// <summary>
        /// Set the Current Attacker
        /// </summary>
        public override bool SetCurrentDefender(PlayerInfoModel player)
        {
            return base.SetCurrentDefender(player);
        }

        /// <summary>
        /// Call to make a new set of monsters...
        /// </summary>
        /// <returns></returns>
        public override bool NewRound()
        {
            return base.NewRound();
        }

        /// <summary>
        /// Add Monsters to the Round
        /// 
        /// Because Monsters can be duplicated, will add 1, 2, 3 to their name
        ///   
        /*
            * Hint: 
            * I don't have crudi monsters yet so will add 6 new ones...
            * If you have crudi monsters, then pick from the list

            * Consdier how you will scale the monsters up to be appropriate for the characters to fight
            * 
            */
        /// </summary>
        /// <returns></returns>
        public override int AddMonstersToRound()
        {
            var round = EngineSettings.BattleScore.RoundCount;

            //Get a round boss every 3 rounds
            bool getRoundBoss = (round > 0 && round % 3 == 0);

            //Get a great boss every 10 rounds
            bool getGreatBoss= (round > 0 && round % 10 == 0);

            var TargetLevel = 1;

            if (EngineSettings.CharacterList.Count() > 0)
            {
                // Get the Average character level
                TargetLevel = Convert.ToInt32(EngineSettings.CharacterList.Average(m => m.Level));
            }

            for (var i = 0; i < EngineSettings.MaxNumberPartyMonsters; i++)
            {
                MonsterModel data = null;

                //Get regular minions
                if (getGreatBoss == false && getRoundBoss == false)
                {
                    data = RandomPlayerHelper.GetRandomMonster(TargetLevel, EngineSettings.BattleSettingsModel.AllowMonsterItems);
                }

                //Add great leader
                if (getGreatBoss)
                {
                    data = MonsterIndexViewModel.Instance.Dataset.Where(m => m.Job == CharacterJobEnum.GreatLeader).First();

                    data = RandomPlayerHelper.GetRandomMonster(TargetLevel, EngineSettings.BattleSettingsModel.AllowMonsterItems, CharacterJobEnum.GreatLeader);
                    //Reset so it only generates 1 great leader
                    getGreatBoss = false;
                }

                //Add round boss
                if(getRoundBoss)
                {
                    data = RandomPlayerHelper.GetRandomMonster(TargetLevel, EngineSettings.BattleSettingsModel.AllowMonsterItems, CharacterJobEnum.RoundBoss);
                    //Reset so that only 1 boss gets generated
                    getRoundBoss = false;
                }
                
                // Help identify which Monster it is
                data.Name += " " + EngineSettings.MonsterList.Count() + 1;

                EngineSettings.MonsterList.Add(new PlayerInfoModel(data));
            }

            return EngineSettings.MonsterList.Count();
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public override bool EndRound()
        {
            return base.EndRound();
        }

        /// <summary>
        /// For each character pickup the items
        /// </summary>
        public override bool PickupItemsForAllCharacters()
        {
            // In Auto Battle this happens and the characters get their items
            // When called manualy, make sure to do the character pickup before calling EndRound

            return base.PickupItemsForAllCharacters();
        }

        /// <summary>
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public override RoundEnum RoundNextTurn()
        {
            return base.RoundNextTurn();
        }

        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public override PlayerInfoModel GetNextPlayerTurn()
        {
            return base.GetNextPlayerTurn();
        }

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public override List<PlayerInfoModel> RemoveDeadPlayersFromList()
        {
            return base.RemoveDeadPlayersFromList();
        }

        /// <summary>
        /// Order the Players in Turn Sequence
        /// </summary>
        public override List<PlayerInfoModel> OrderPlayerListByTurnOrder()
        {
            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before MonsterModel (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending
            return base.OrderPlayerListByTurnOrder();
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public override List<PlayerInfoModel> MakePlayerList()
        {
            return base.MakePlayerList();
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public override PlayerInfoModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            return base.GetNextPlayerInList();
        }

        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="character"></param>
        public override bool PickupItemsFromPool(PlayerInfoModel character)
        {
            // For auto battle, automatically assign items based on item value
            if (EngineSettings.BattleScore.AutoBattle)
            {
                return base.PickupItemsFromPool(character);
            }

            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        public override bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation)
        {
            return base.GetItemFromPoolIfBetter(character, setLocation);
        }

        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="PoolItem"></param>
        /// <returns></returns>
        public override ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem)
        {
            return base.SwapCharacterItem(character, setLocation, PoolItem);
        }

        /// <summary>
        /// For all characters in player list, remove their buffs
        /// </summary>
        /// <returns></returns>
        public override bool RemoveCharacterBuffs()
        {
            return base.RemoveCharacterBuffs();
        }
    }
}