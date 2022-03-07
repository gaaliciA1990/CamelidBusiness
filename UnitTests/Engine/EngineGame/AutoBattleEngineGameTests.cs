using System.Threading.Tasks;
using System.Linq;

using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using Game.ViewModels;
using Game.Helpers;
using System.Collections.ObjectModel;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class AutoBattleEngineGameTests
    {
        #region TestSetup
        AutoBattleEngine AutoBattleEngine;

        [SetUp]
        public void Setup()
        {
            AutoBattleEngine = new AutoBattleEngine();

            AutoBattleEngine.Battle.EngineSettings.CharacterList.Clear();
            AutoBattleEngine.Battle.EngineSettings.MonsterList.Clear();
            AutoBattleEngine.Battle.EngineSettings.CurrentDefender = null;
            AutoBattleEngine.Battle.EngineSettings.CurrentAttacker = null;

            AutoBattleEngine.Battle.Round = new RoundEngine();
            AutoBattleEngine.Battle.Round.Turn = new TurnEngine();

            //AutoBattleEngine.Battle.StartBattle(true);   // Clear the Engine
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void AutoBattleEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AutoBattleEngine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_Constructor_Valid_Battle_Round_Turn_Should_Pass()
        {
            // Arrange

            // Act
            var result = AutoBattleEngine;
            result.Battle = new BattleEngine();
            result.Battle.Round = new RoundEngine();
            result.Battle.Round.Turn = new TurnEngine();

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region CreateCharacterParty
        [Test]
        public void AutoBattleEngine_CreateCharacterParty_Valid_Characters_Dataset_Not_Enough_Should_Create_Random_Upto_6()
        {
            //Arrange
            AutoBattleEngine.Battle.EngineSettings.MaxNumberPartyCharacters = 6;
            //Reset the dataset
            var originalSet = CharacterIndexViewModel.Instance.Dataset;
            ObservableCollection<CharacterModel> workingSet = new ObservableCollection<CharacterModel>();
            for(int i = 0; i < 3; i++)
            {
                workingSet.Add(originalSet[i]);
            }

            CharacterIndexViewModel.Instance.Dataset = workingSet;

            AutoBattleEngine.Battle.EngineSettings.CharacterList.Clear();

            //Act
            var result = AutoBattleEngine.CreateCharacterParty();

            //Reset
            CharacterIndexViewModel.Instance.Dataset = originalSet;

            //Assert
            Assert.AreEqual(6, AutoBattleEngine.Battle.EngineSettings.CharacterList.Count);
        }

        [Test]
        public void AutoBattleEngine_CreateCharacterParty_Valid_Characters_CharacterIndex_None_Should_Create_6()
        {
            //Arrange
            AutoBattleEngine.Battle.EngineSettings.MaxNumberPartyCharacters = 6;

            AutoBattleEngine.Battle.EngineSettings.CharacterList.Clear();

            //Act
            var result = AutoBattleEngine.CreateCharacterParty();

            //Reset

            //Assert
            Assert.AreEqual(6, AutoBattleEngine.Battle.EngineSettings.CharacterList.Count);
        }

        [Test]
        public void AutoBattleEngine_CreateCharacterParty_Valid_Characters_Should_Assign_6()
        {
            //Arrange

            //Act
            var result = AutoBattleEngine.CreateCharacterParty();

            //Reset

            //Assert
            Assert.AreEqual(6, AutoBattleEngine.Battle.EngineSettings.CharacterList.Count);
        }


        #endregion CreateCharacterParty   

        #region RunAutoBattle
        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Valid_Default_Should_Pass()
        {
            //Arrange

            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(3);

            var data = new CharacterModel { Level = 1, MaxHealth = 10 };

            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));

            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset
            _ = DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_InValid_DetectInfinateLoop_Should_Return_False()
        {
            //Arrange

            // Trigger DetectInfinateLoop Loop
            var oldRoundCountMax = AutoBattleEngine.Battle.EngineSettings.MaxRoundCount;
            AutoBattleEngine.Battle.EngineSettings.MaxRoundCount = -1;

            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset
            AutoBattleEngine.Battle.EngineSettings.MaxRoundCount = oldRoundCountMax;

            //Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task AutoBattleEngine_RunAutoBattle_Valid_NewRound_Should_Return_True()
        {
            //Arrange

            AutoBattleEngine.Battle.EngineSettings.MaxNumberPartyMonsters = 1;
            AutoBattleEngine.Battle.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Attack = 100,
                                Defense = 100,
                                Level = 1,
                                CurrentHealth = 111,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            AutoBattleEngine.Battle.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            var MonsterPlayerSue = new PlayerInfoModel(
                new MonsterModel
                {
                    Speed = 1,
                    Attack = 1,
                    Defense = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperienceTotal = 1,
                    ExperienceRemaining = 1,
                    Name = "Sue",
                    ListOrder = 2,
                });

            AutoBattleEngine.Battle.EngineSettings.MonsterList.Add(MonsterPlayerSue);

            //Act
            var result = await AutoBattleEngine.RunAutoBattle();

            //Reset
            AutoBattleEngine.Battle.EngineSettings.MaxNumberPartyMonsters = 6;
            AutoBattleEngine.Battle.EngineSettings.MaxNumberPartyCharacters = 6;

            //Assert
            Assert.AreEqual(false, result);
        }
        #endregion RunAutoBattle

        #region DetectInfinateLoop
        [Test]
        public void AutoBattleEngine_DetectInfinateLoop_InValid_RoundCount_More_Than_Max_Should_Return_True()
        {
            // Arrange
            AutoBattleEngine.Battle.EngineSettings.BattleScore.RoundCount = AutoBattleEngine.Battle.EngineSettings.MaxRoundCount + 1;

            // Act
            var result = AutoBattleEngine.DetectInfinateLoop();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AutoBattleEngine_DetectInfinateLoop_InValid_TurnCount_Count_More_Than_Max_Should_Return_True()
        {
            // Arrange
            AutoBattleEngine.Battle.EngineSettings.MaxRoundCount = 1000;
            AutoBattleEngine.Battle.EngineSettings.MaxTurnCount = 1;
            AutoBattleEngine.Battle.EngineSettings.BattleScore.TurnCount = AutoBattleEngine.Battle.EngineSettings.MaxTurnCount + 1;

            // Act
            var result = AutoBattleEngine.DetectInfinateLoop();

            // Reset
            AutoBattleEngine.Battle.EngineSettings.MaxTurnCount = 1000;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AutoBattleEngine_DetectInfinateLoop_Valid_Counts_Less_Than_Max_Should_Return_false()
        {
            // Arrange
            AutoBattleEngine.Battle.EngineSettings.BattleScore.TurnCount = AutoBattleEngine.Battle.EngineSettings.MaxTurnCount - 1;
            AutoBattleEngine.Battle.EngineSettings.BattleScore.RoundCount = AutoBattleEngine.Battle.EngineSettings.MaxRoundCount - 1;

            // Act
            var result = AutoBattleEngine.DetectInfinateLoop();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion DetectInfinateLoop
    }
}