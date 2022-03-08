using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Helpers;
using Game.ViewModels;
using Game.GameRules;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using System;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : EngineBase.TurnEngineBase, ITurnEngineInterface
    {
        #region Algrorithm
        /* 
            Need to decide who takes the next turn
            Target to Attack
            Should Move, or Stay put (can hit with weapon range?)
            Death
            Manage Round...
          
            Attack or Move
            Roll To Hit
            Decide Hit or Miss
            Decide Damage
            Death
            Drop Items
            Turn Over
        */
        #endregion Algrorithm

        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            var result = false;

            // If the action is not set, then try to set it or use Attact
            if (EngineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                EngineSettings.CurrentAction = DetermineActionChoice(Attacker);

                // When in doubt, attack...
                if (EngineSettings.CurrentAction == ActionEnum.Unknown)
                {
                    EngineSettings.CurrentAction = ActionEnum.Attack;
                }
            }

            switch (EngineSettings.CurrentAction)
            {
                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Ability:
                    result = UseAbility(Attacker);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    break;

                case ActionEnum.Skip:
                    result = SkipAsTurn(Attacker);
                    break;
            }

            EngineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
        }


        public override bool SkipAsTurn(PlayerInfoModel Attacker)
        {
            //Add 2 health to player
            Attacker.CurrentHealth += 2;

            return true;
        }


        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {
            var locationAttacker = EngineSettings.MapModel.GetLocationForPlayer(Attacker);
            
            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                // For Attack, Choose Who
                EngineSettings.CurrentDefender = AttackChoice(Attacker);

                if (EngineSettings.CurrentDefender == null)
                {
                    return false;
                }

                if (locationAttacker == null)
                {
                    return false;
                }


                // Get X, Y for Defender
                var locationDefender = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentDefender);

                var possibleLocations = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetAvailableLocationsFromPlayer(locationAttacker);

                if(possibleLocations.Count() <= 1)
                {
                    return false;
                }

                MapModelLocation bestLocation = null;
                double bestDistance = 10000000.0;

                //find best location from possible locations using eucledian distance
                foreach (var location in possibleLocations)
                {
                    double x1 = location.Column;
                    double y1 = location.Row;
                    double x2 = locationDefender.Column;
                    double y2 = locationDefender.Row;
                    var distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                    bestLocation = (bestDistance > distance) ? location : bestLocation;
                    bestDistance = (bestDistance > distance) ? distance : bestDistance;
                }

                //Don't let them move within their own cell, they disappear!!!!
                if(bestLocation == locationAttacker)
                {
                    return false;
                }

                Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, bestLocation.Column, bestLocation.Row));

                return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, bestLocation);
            }
            else if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var cell = BattleEngineViewModel.Instance.Engine.EngineSettings.MoveMapLocation;
                var emptyMapObject = EngineSettings.MapModel.MapGridLocation[cell.Column, cell.Row];
                Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, cell.Column, cell.Row));
                return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, emptyMapObject);
            }

            return true;
        }

        /// <summary>
        /// Decide to use an Ability or not
        /// 
        /// Set the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool ChooseToUseAbility(PlayerInfoModel Attacker)
        {
            return base.ChooseToUseAbility(Attacker);
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character).Count() < 1)
            {
                return null;
            }

            // Sort By Distance
            var attacker = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentAttacker);
            var availableCharactters = EngineSettings.PlayerList.Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character);

            if (attacker == null)
            {
                return availableCharactters.ElementAt(0);
            }

            Func<PlayerInfoModel, double> lambda = (PlayerInfoModel character) => {
                var defender = EngineSettings.MapModel.GetLocationForPlayer(character);
                var distance = Math.Sqrt(Math.Pow((int?)attacker.Column??0 - (int?)defender.Column??0, 2) + Math.Pow((int?)attacker.Row??0 - (int?)defender.Row??0, 2));
                return distance;
            };

            var OrderedByDistance = availableCharactters.OrderBy(character => lambda(character));
            var chosenCharacter = OrderedByDistance.ElementAt(0);

            foreach (var character in OrderedByDistance)
            {
                //if next closest is more desirable, change target 
                if (character.GetAttackTotal > chosenCharacter.GetAttackTotal * 1.5)
                {
                    chosenCharacter = character;
                }
            }

            return chosenCharacter;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster).Count() < 1)
            {
                return null;
            }

            // Sort By Distance
            var attacker = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentAttacker);
            var availableMonsters = EngineSettings.PlayerList.Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster);

            if (attacker == null)
            {
                return availableMonsters.ElementAt(0);
            }

            Func<PlayerInfoModel, double> lambda = (PlayerInfoModel monster) => {
                var defender = EngineSettings.MapModel.GetLocationForPlayer(monster);
                var distance = Math.Sqrt(Math.Pow(attacker.Column - defender.Column, 2) + Math.Pow(attacker.Row - defender.Row, 2));
                return distance;
            };

            var OrderedByDistance = availableMonsters.OrderBy(character => lambda(character));
            var chosenMonster = OrderedByDistance.ElementAt(0);

            foreach (var monster in OrderedByDistance)
            {
                //if next closest is more desirable, change target 
                if (monster.GetAttackTotal > chosenMonster.GetAttackTotal * 1.5)
                {
                    chosenMonster = monster;
                }
            }

            return chosenMonster;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            _ = EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            _ = CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    _ = DetermineCriticalMissProblem(Attacker);

                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Update weapon's durability 
                    if (EngineSettingsModel.Instance.BattleSettingsModel.AllowItemDurability && Attacker.PlayerType == PlayerTypeEnum.Character && Attacker.PrimaryHand != null)
                    {
                        var counter = Attacker.ItemUseTracker[Attacker.PrimaryHand];
                        Attacker.ItemUseTracker[Attacker.PrimaryHand] = counter - 1;

                        //Remove the item from the player's hand entirely if the item runs out of usage
                        if ((counter - 1) == 0)
                        {
                            //Find the item's name
                            var itemName = ItemIndexViewModel.Instance.Dataset.Where(m => m.Id == Attacker.PrimaryHand).FirstOrDefault();

                            Debug.WriteLine("Item being dropped: " + itemName.Id);
                            //Update turn special message
                            EngineSettingsModel.Instance.BattleMessagesModel.ItemCrackedMessage = itemName.Name + " cracked and cannot be used anymore.";

                            Attacker.PrimaryHand = null;
                        }
                    }

                    //Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // If critical Hit, double the damage
                    if (EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                    }

                    // Apply the Damage
                    _ = ApplyDamage(Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    _ = RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    _ = CalculateExperience(Attacker, Target);

                    break;
            }

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.AttackStatus + Target.Name + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {
            return base.TargetDied(Target);
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // Monsters only drop items
            if(Target.PlayerType == PlayerTypeEnum.Monster)
            {
                myItemList.AddRange(GetRandomMonsterItemDrops(EngineSettings.BattleScore.RoundCount));
            }
            

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                EngineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            EngineSettings.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            EngineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            EngineSettings.BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            return base.RollToHitTarget(AttackScore, DefenseScore);
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        public override List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            PlayerInfoModel Target = EngineSettings.CurrentDefender;
            // The Number drop can be Up to the character Count and a half, but may be less.  
            // Negative results in nothing dropped
            var upperBound = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Count;
            var NumberToDrop = (DiceHelper.RollDice(1, upperBound * 2) - upperBound);

            var result = new List<ItemModel>();

            //Drop basic items
            if (Target.Job == CharacterJobEnum.Unknown)
            {
                for (var i = 0; i < NumberToDrop; i++)
                {
                    result.Add(ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetRandomBasicItem()));
                }
            }

            //Special drops
            // Get a random Unique Item if there's a boss in the round boss - every 3 rounds, there's a 70% chance of boss dropping an item
            if (Target.Job == CharacterJobEnum.RoundBoss && DiceHelper.RollDice(1, 10) >= 3)
            {
                result.Add(ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetRandomUniqueItem()));
            }
            //Every 10th round, drop unqiue item is 100%
            if (Target.Job == CharacterJobEnum.GreatLeader)
            {
                result.Add(ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetRandomUniqueItem()));
            }
            return result;
        }

        /// <summary>
        /// Determine what Actions to do
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            return base.DetermineActionChoice(Attacker);
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return base.DetermineCriticalMissProblem(attacker);
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            return base.BattleSettingsOverride(Attacker);
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            return base.BattleSettingsOverrideHitStatusEnum(myEnum);
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override bool ApplyDamage(PlayerInfoModel Target)
        {
            return base.ApplyDamage(Target);
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateAttackStatus(Attacker, Target);
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateExperience(Attacker, Target);
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {
            return base.RemoveIfDead(Target);
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        public override bool UseAbility(PlayerInfoModel Attacker)
        {
            return base.UseAbility(Attacker);
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        public override bool Attack(PlayerInfoModel Attacker)
        {
            return base.Attack(Attacker);
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            return base.AttackChoice(data);
        }
    }
}