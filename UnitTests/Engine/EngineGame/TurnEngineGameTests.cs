
using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using System.Collections.Generic;
using Game.Helpers;
using Game.Engine.EngineModels;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class TurnEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
            Engine.Round = new RoundEngine();
            Engine.Round.Turn = new TurnEngine();
            //Engine.StartBattle(true);   // Start engine in auto battle mode
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void TurnEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region MoveAsTurn
        [Test]
        public void RoundEngine_MoveAsTurn_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_MoveAsTurn_Valid_Monster_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(new PlayerInfoModel(new MonsterModel()));

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_MoveAsTurn_Default_Valid_Character_And_Move_Location_Should_Pass()
        {
            // Arrange
            var Character = new PlayerInfoModel(new CharacterModel());

            // Remove everyone
            Engine.EngineSettings.PlayerList.Clear();
            Engine.EngineSettings.PlayerList.Add(Character);

            Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Player = Character;
            Engine.EngineSettings.MoveMapLocation = new CordinatesModel {Row=0,Column=1 };

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(Character);

            // Reset
            Engine.EngineSettings.PlayerList.Clear();
            Engine.EngineSettings.MapModel.ClearMapGrid();
            Engine.EngineSettings.CurrentDefender = null;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_MoveAsTurn_Default_Valid_Monster_Invalid_Defender_Should_Fail()
        {
            // Arrange
            var monster = new PlayerInfoModel(new MonsterModel());

            // Remove everyone
            Engine.EngineSettings.PlayerList.Clear();

            Engine.EngineSettings.PlayerList.Add(monster);

            _ = Engine.EngineSettings.MapModel.PopulateMapModel(Engine.EngineSettings.PlayerList, false);

            Engine.EngineSettings.CurrentDefender = null;

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(monster);

            // Reset
            Engine.EngineSettings.PlayerList.Clear();
            Engine.EngineSettings.MapModel.ClearMapGrid();
            Engine.EngineSettings.CurrentDefender = null;

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_MoveAsTurn_Default_Valid_Monster_No_Possible_Locations_Should_Fail()
        {
            // Arrange
            var monster = new PlayerInfoModel(new MonsterModel());
            var character = new PlayerInfoModel(new CharacterModel());

            // Remove everyone
            Engine.EngineSettings.PlayerList.Clear();

            Engine.EngineSettings.PlayerList.Add(monster);
            Engine.EngineSettings.PlayerList.Add(character);
            Engine.EngineSettings.PlayerList.Add(character);

            Engine.EngineSettings.MapModel.ClearMapGrid();
            Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Player = monster;
            Engine.EngineSettings.MapModel.MapGridLocation[1, 0].Player = character;
            Engine.EngineSettings.MapModel.MapGridLocation[0, 1].Player = character;

            Engine.EngineSettings.CurrentDefender = character;
            
            // Act
            var result = Engine.Round.Turn.MoveAsTurn(monster);

            // Reset
            Engine.EngineSettings.PlayerList.Clear();
            Engine.EngineSettings.MapModel.ClearMapGrid();
            Engine.EngineSettings.CurrentDefender = null;

            // Assert
            Assert.AreEqual(false, result);
        }


        [Test]
        public void TurnEngine_MoveAsTurn_Default_Valid_Monster_Possible_Locations_Should_Pass()
        {
            // Arrange
            var monster = new PlayerInfoModel(new MonsterModel());
            var character = new PlayerInfoModel(new CharacterModel());

            // Remove everyone
            Engine.EngineSettings.PlayerList.Clear();

            Engine.EngineSettings.PlayerList.Add(monster);
            Engine.EngineSettings.PlayerList.Add(character);

            Engine.EngineSettings.MapModel.ClearMapGrid();
            Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Player = monster;
            Engine.EngineSettings.MapModel.MapGridLocation[1, 0].Player = character;

            Engine.EngineSettings.CurrentDefender = character;

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(monster);

            // Reset
            Engine.EngineSettings.PlayerList.Clear();
            Engine.EngineSettings.MapModel.ClearMapGrid();
            Engine.EngineSettings.CurrentDefender = null;

            // Assert
            Assert.AreEqual(true, result);
        }


        #endregion MoveAsTurn

        #region ApplyDamage
        [Test]
        public void RoundEngine_ApplyDamage_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.ApplyDamage(new PlayerInfoModel(new MonsterModel()));

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion ApplyDamage

        #region Attack
        [Test]
        public void RoundEngine_Attack_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.Attack(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion Attack

        #region AttackChoice
        [Test]
        public void RoundEngine_AttackChoice_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.AttackChoice(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }
        #endregion AttackChoice

        #region SelectCharacterToAttack
        [Test]
        public void RoundEngine_SelectCharacterToAttack_Valid_Default_Should_Pass()
        {
            // Arrange

            // remember the list
            var saveList = Engine.EngineSettings.PlayerList;

            Engine.EngineSettings.PlayerList = new List<PlayerInfoModel>();

            var data = new PlayerInfoModel(new CharacterModel());
            Engine.EngineSettings.PlayerList.Add(data);

            // Act
            var result = Engine.Round.Turn.SelectCharacterToAttack();

            // Reset

            // Restore the List
            Engine.EngineSettings.PlayerList = saveList;
            _ = Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreNotEqual(null, result);
        }
        #endregion SelectCharacterToAttack

        #region UseAbility
        [Test]
        public void RoundEngine_UseAbility_Valid_Default_Should_Pass()
        {
            // Arrange

            var characterPlayer = new PlayerInfoModel(new CharacterModel { Job = CharacterJobEnum.Unknown });

            // remove it so it is not found
            characterPlayer.AbilityTracker.Add(AbilityEnum.Heal, 1);
            Engine.EngineSettings.CurrentActionAbility = AbilityEnum.Heal;

            // Act
            var result = Engine.Round.Turn.UseAbility(characterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion UseAbility

        #region BattleSettingsOverrideHitStatusEnum
        [Test]
        public void RoundEngine_BattleSettingsOverrideHitStatusEnum_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.BattleSettingsOverrideHitStatusEnum(HitStatusEnum.Unknown);

            // Reset

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, result);
        }
        #endregion BattleSettingsOverrideHitStatusEnum

        #region BattleSettingsOverride
        [Test]
        public void RoundEngine_BattleSettingsOverride_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.BattleSettingsOverride(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, result);
        }
        #endregion BattleSettingsOverride

        #region CalculateExperience
        [Test]
        public void RoundEngine_CalculateExperience_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.CalculateExperience(new PlayerInfoModel(), new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion CalculateExperience

        #region CalculateAttackStatus
        [Test]
        public void RoundEngine_CalculateAttackStatus_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.CalculateAttackStatus(new PlayerInfoModel(), new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, result);
        }
        #endregion CalculateAttackStatus

        #region RemoveIfDead
        [Test]
        public void RoundEngine_RemoveIfDead_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.RemoveIfDead(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion RemoveIfDead

        #region ChooseToUseAbility
        [Test]
        public void RoundEngine_ChooseToUseAbility_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.ChooseToUseAbility(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion ChooseToUseAbility

        #region SelectMonsterToAttack
        [Test]
        public void RoundEngine_SelectMonsterToAttack_Valid_Default_Should_Pass()
        {
            // Arrange

            // remember the list
            var saveList = Engine.EngineSettings.PlayerList;

            Engine.EngineSettings.PlayerList = new List<PlayerInfoModel>();

            var data = new PlayerInfoModel(new MonsterModel());
            Engine.EngineSettings.PlayerList.Add(data);

            // Act
            var result = Engine.Round.Turn.SelectMonsterToAttack();

            // Reset

            // Restore the List
            Engine.EngineSettings.PlayerList = saveList;
            _ = Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreNotEqual(null, result);
        }
        #endregion SelectMonsterToAttack

        #region DetermineActionChoice
        [Test]
        public void RoundEngine_DetermineActionChoice_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.DetermineActionChoice(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(ActionEnum.Move, result);
        }
        #endregion DetermineActionChoice

        #region TurnAsAttack
        [Test]
        public void RoundEngine_TurnAsAttack_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(new PlayerInfoModel(), new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion TurnAsAttack

        #region TargetDied
        [Test]
        public void RoundEngine_TargetDied_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.TargetDied(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion TargetDied

        #region TakeTurn
        [Test]
        public void RoundEngine_TakeTurn_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.TakeTurn(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion TakeTurn

        #region RollToHitTarget
        [Test]
        public void RoundEngine_RollToHitTarget_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.RollToHitTarget(1,1);

            // Reset

            // Assert
            Assert.AreEqual(HitStatusEnum.Hit, result);
        }
        #endregion RollToHitTarget

        #region GetRandomMonsterItemDrops
        [Test]
        public void RoundEngine_GetRandomMonsterItemDrops_Valid_Default_Should_Pass()
        {
            // Arrange 
            PlayerInfoModel monster = new PlayerInfoModel(new MonsterModel
            {
                Job = CharacterJobEnum.GreatLeader
            });
            Engine.Round.SetCurrentDefender(monster);

            // Act
            var result = Engine.Round.Turn.GetRandomMonsterItemDrops(10);

            // Reset

            // Assert
            Assert.LessOrEqual(result.Count,10);
        }

        [Test]
        public void RoundEngine_GetRandomMonsterItemDrops_Valid_Round_3_Should_Pass()
        {
            // Arrange 
            PlayerInfoModel monster = new PlayerInfoModel(new MonsterModel
            {
                Job = CharacterJobEnum.RoundBoss
            });
            Engine.Round.SetCurrentDefender(monster);
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(8);

            // Act
            var result = Engine.Round.Turn.GetRandomMonsterItemDrops(3);

            // Reset
            DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result.Find(m => m.IsUnique == true).IsUnique);
        }

        [Test]
        public void RoundEngine_GetRandomMonsterItemDrops_Valid_Round_10_Should_Pass()
        {
            // Arrange 
            PlayerInfoModel monster = new PlayerInfoModel(new MonsterModel
            {
                Job = CharacterJobEnum.GreatLeader
            });
            Engine.Round.SetCurrentDefender(monster);

            // Act
            var result = Engine.Round.Turn.GetRandomMonsterItemDrops(10);

            // Reset
            
            // Assert
            Assert.AreEqual(true, result.Find(m => m.IsUnique == true).IsUnique);
        }


        #endregion GetRandomMonsterItemDrops

        #region DetermineCriticalMissProblem
        [Test]
        public void RoundEngine_DetermineCriticalMissProblem_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.DetermineCriticalMissProblem(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion DetermineCriticalMissProblem

        #region DropItems
        [Test]
        public void RoundEngine_DropItems_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.DropItems(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.LessOrEqual(result, 7);
        }
        #endregion DropItems
    }
}