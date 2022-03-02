using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using System.Linq;
using Game.Helpers;
using System;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1

        #region Scenario2

        [Test]
        public async Task HackathonScenario_Scenario_2_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      2
            *      
            * Description: 
            *      Doug always misses. Doug can do other action such as move or use abilities as appropriate.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Doug
            *      Set die to 1 whenever it's Doug's turn, so he always misses
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerDoug = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 1, // Will go first...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Doug",
                            });

            var Monster = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = -1, // Will go last...
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 10,
                    ExperienceRemaining = 10,
                    Name = "monster",
                });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);

            EngineViewModel.Engine.EngineSettings.MonsterList.Add(Monster);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario2

        #region Scenario33
        [Test]
        public void HackathonScenario_Scenario_33_Character_Should_Skip_Turn_And_Restores_2_Health()
        {
            /* 
            * Scenario Number:  
            *      33
            *      
            * Description: 
            *      Make a Character called Mike and A monster called Pat.
            *      On Mike's turn, he decides to skip his turn allowing Pat to start his next turn.
            *      Mike gains 2 health points for skipping.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Added SkipAsTurn method in the Turn engine,
            *      Action Enum for skip
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Create Monster named Pat
            *      Set speed of Mike to 3 so he goes first 
            *      Set speed of Pat to 1 
            *      Create Map Grid and add players
            *      Play Mike's turn
            *      Asserts to check that Mike Skipped
            *  
            *      Startup Battle
            *      Run one turn
            * 
            * Test Conditions:
            *      Default condition is sufficient
            *      
            *      
            * 
            * Validation:
            *      Check if Pat's turn after Mike played his turn
            *      Check Mike didn't move
            *      Check Mike didn't cause damage to Pat
            *      Check Mike current health increased by 2
            *  
            */

            //Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 3, // Will go first...
                                Level = 1,
                                CurrentHealth = 10,
                                MaxHealth = 20,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                PrimaryHand = ItemIndexViewModel.Instance.Dataset.Where(m => m.Range > 1).ToList().OrderByDescending(m => m.Range).FirstOrDefault().Id
                            });

            var MonsterPlayerPat = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 1, // Will go last...
                                Level = 1,
                                CurrentHealth = 10,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Pat",
                                PrimaryHand = ItemIndexViewModel.Instance.Dataset.Where(m => m.Range > 1).ToList().OrderByDescending(m => m.Range).FirstOrDefault().Id
                            });

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.MapFull;

            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayerPat);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayerMike);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(MonsterPlayerPat);

            _ = EngineViewModel.Engine.EngineSettings.MapModel.PopulateMapModel(EngineViewModel.Engine.EngineSettings.PlayerList);

            var monsterHealth = MonsterPlayerPat.GetCurrentHealth();
            var characterHealth = CharacterPlayerMike.GetCurrentHealth();
            var characterLocation = EngineViewModel.Engine.EngineSettings.MapModel.GetLocationForPlayer(CharacterPlayerMike);

            //Act
            EngineViewModel.Engine.EngineSettings.CurrentAttacker = EngineViewModel.Engine.Round.GetNextPlayerTurn();
            EngineViewModel.Engine.EngineSettings.CurrentAction = ActionEnum.Skip;
            var RoundCondition = EngineViewModel.Engine.Round.RoundNextTurn();

            //Reset
            EngineViewModel.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            EngineViewModel.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.Unknown;

            //Assert
            Assert.AreEqual(MonsterPlayerPat.Name, EngineViewModel.Engine.Round.GetNextPlayerTurn().Name); //Check Pat's turn is next
            Assert.AreEqual(characterLocation, EngineViewModel.Engine.EngineSettings.MapModel.GetLocationForPlayer(CharacterPlayerMike)); //Check Mike didn't move
            Assert.AreEqual(monsterHealth, MonsterPlayerPat.GetCurrentHealth()); //Check Mike didn'dt cause damage
            Assert.AreEqual(characterHealth + 2, CharacterPlayerMike.GetCurrentHealth()); //Check Mike health increased by 2
        }
        #endregion Scenario33

        #region Scenario4
        [Test]
        public void HakathonScenario_Scenario_4_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      4
            *      
            * Description: 
            *      Attacker always has a critical hit that causes double damage
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      None required for this test
            * 
            * Test Algrorithm:
            *      Create Character named Dave
            *      Create Monster named Steve
            *      Set speed of Dave to 3 so he goes first 
            *      Set speed of Steve to 1 
            *      Create Map Grid and add players
            *      Play Dave's turn to attack
            *      Asserts to check that Dave's attack was Critical
            *      Assert to check Dave's attack was doubled
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify we got a CriticalHit when a 20 was rolled
            *  
            */

            // Arrange
            var CharacterPlayerDave = new PlayerInfoModel(
                new CharacterModel
                {
                    Speed = 3, // Will go first...
                    Level = 2,
                    CurrentHealth = 15,
                    MaxHealth = 20,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Dave",
                    PrimaryHand = ItemIndexViewModel.Instance.Dataset.Where(m => m.Range > 1).ToList().OrderByDescending(m => m.Range).FirstOrDefault().Id
                });

            var MonsterPlayerSteve = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 2, // Will go last...
                    Level = 2,
                    CurrentHealth = 20,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Steve",
                    PrimaryHand = ItemIndexViewModel.Instance.Dataset.Where(m => m.Range > 1).ToList().OrderByDescending(m => m.Range).FirstOrDefault().Id
                });

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.MapFull;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = true; // Allow critical hits
            _ = DiceHelper.EnableForcedRolls(); // Enable forced rolls

            // Clear the player, character, and monster list
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();

            // Add our character and monsters to our lists of players 
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerDave);
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayerSteve);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayerDave);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(MonsterPlayerSteve);

            // Populate the map with the players
            _ = EngineViewModel.Engine.EngineSettings.MapModel.PopulateMapModel(EngineViewModel.Engine.EngineSettings.PlayerList);

            var monsterHealth = MonsterPlayerSteve.GetCurrentHealth();
            var characterHealth = CharacterPlayerDave.GetCurrentHealth();
            var characterLocation = EngineViewModel.Engine.EngineSettings.MapModel.GetLocationForPlayer(CharacterPlayerDave);

            //Act
            EngineViewModel.Engine.EngineSettings.CurrentAttacker = EngineViewModel.Engine.Round.GetNextPlayerTurn();
            EngineViewModel.Engine.EngineSettings.CurrentDefender = EngineViewModel.Engine.Round.GetNextPlayerInList(); // Set target to be attacked to next player in list
            EngineViewModel.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;
            _ = DiceHelper.SetForcedRollValue(20); // force attacker roll to 20
            var RoundCondition = EngineViewModel.Engine.Round.RoundNextTurn();
            Console.WriteLine(EngineViewModel.Engine.EngineSettings.BattleMessagesModel.AttackStatus);

            //Reset
            EngineViewModel.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            EngineViewModel.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            EngineViewModel.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.Unknown;

            //Assert
            Assert.AreEqual(HitStatusEnum.CriticalHit, EngineViewModel.Engine.EngineSettings.BattleMessagesModel.HitStatus); // confirm the battle message after attack is critical attack
        }
        #endregion Scenario4

        #region Scenario32
        [Test]
        public void HakathonScenario_Scenario_32_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number: 
            *      32
            *      
            * Description: 
            *      The map has obstacles that need to be either destroyed to open the map or moved around to 
            *      reach the final destination
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario32

        #region Scenario34
        [Test]
        public void HackathonScenario_Scenario_34_Player_With_Speed_2_Should_Have_Options_To_Move_Only_A_Distance_Of_Up_To_2_Spaces()
        {
            /* 
            * Scenario Number:  
            *      34
            *      
            * Description: 
            *      Make a Character called Mike and and add it to the map gris.
            *      Get available locations mike can move to
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      GetAvailableLocationsFromPlayer; gets all available locations player can move to within given distance
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Add mike to map grid
            *      Call GetAvailableLocationsFromPlayer given mike and mike's speed
            *      function returns all available locations mike can move to where distance =  mike's speed
            * 
            * Test Conditions:
            *      Default condition is sufficient
            *      
            * Validation:
            *      Check distance from mike to locations returned from GetAvailableLocationsFromPlayer and assert that they are within the distance of mike's speed 
            *  
            */

            ///Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 2,
                                Level = 1,
                                CurrentHealth = 10,
                                MaxHealth = 20,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike"
                            });

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.MapFull;
            EngineViewModel.Engine.EngineSettings.PlayerList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            EngineViewModel.Engine.EngineSettings.PlayerList.Add(CharacterPlayerMike);
            _ = EngineViewModel.Engine.EngineSettings.MapModel.PopulateMapModel(EngineViewModel.Engine.EngineSettings.PlayerList);

            ///Act
            var playerLocation = EngineViewModel.Engine.EngineSettings.MapModel.GetLocationForPlayer(CharacterPlayerMike);
            var locations = EngineViewModel.Engine.EngineSettings.MapModel.GetAvailableLocationsFromPlayer(playerLocation, CharacterPlayerMike.Speed);

            ///Reset

            ///Assert
            foreach (var location in locations)
            {
                var distance = Math.Sqrt(Math.Pow(playerLocation.Column - location.Column, 2) + Math.Pow(playerLocation.Row - location.Row, 2));
                Assert.LessOrEqual(distance, CharacterPlayerMike.Speed);
            }
        }

        #endregion Scenario34
    }
}