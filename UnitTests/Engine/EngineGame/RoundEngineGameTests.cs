using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using Game.ViewModels;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class RoundEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new();

            Engine.Round = new RoundEngine
            {
                Turn = new TurnEngine()
            };
            _ = Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            //Engine.StartBattle(true);   
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void RoundEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region OrderPlayListByTurnOrder
        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Speed_Higher_Should_Be_Z()
        {
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 20,
                CurrentHealth = 100,
                ExperienceTotal = 1000,
                Name = "Z",
                ListOrder = 1,
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 1,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.EngineSettings.PlayerList = Engine.EngineSettings.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("Z", result[0].Name);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Level_Higher_Should_Be_Z()
        {
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 20,
                CurrentHealth = 100,
                ExperienceTotal = 1000,
                Name = "Z",
                ListOrder = 1,
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.EngineSettings.PlayerList = Engine.EngineSettings.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("Z", result[0].Name);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Experience_Higher_Should_Be_Z()
        {
            // Arrange

            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 100,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);

            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "C",
                ListOrder = 10,
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.EngineSettings.PlayerList = Engine.EngineSettings.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("Z", result[0].Name);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_ListOrder_Should_Be_1()
        {
            // Arrange
            var Monster = new MonsterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "A",
                ListOrder = 1,
            };

            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "A",
                ListOrder = 10
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.EngineSettings.PlayerList = Engine.EngineSettings.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual(1, result[0].ListOrder);
        }

        [Test]
        public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Name_A_Z_Should_Be_Z()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Both need to be character to fall through to the Name Test
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
            };

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 2,
                ExperienceTotal = 1,
                Name = "ZZ",
                ListOrder = 10
            };

            CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Sort the list by Current Health, so it has to be resorted.
            Engine.EngineSettings.PlayerList = Engine.EngineSettings.PlayerList.OrderBy(m => m.CurrentHealth).ToList();

            // Act
            var result = Engine.Round.OrderPlayerListByTurnOrder();

            // Reset

            // Assert
            Assert.AreEqual("Z", result[0].Name);
        }
        #endregion OrderPlayListByTurnOrder

        #region GetItemFromPoolIfBetter

        //[Test]
        //public async Task RoundEngine_GetItemFromPoolIfBetter_InValid_Pool_Empty_Should_Fail()
        //{
        //    Engine.EngineSettings.MonsterList.Clear();

        //    // Both need to be character to fall through to the Name Test
        //    // Arrange
        //    var Character = new CharacterModel
        //    {
        //        Speed = 20,
        //        Level = 1,
        //        CurrentHealth = 1,
        //        ExperienceTotal = 1,
        //        Name = "Z",
        //        ListOrder = 1,
        //        Guid = "me"
        //    };

        //    // Add each model here to warm up and load it.
        //    _ = Game.Helpers.DataSetsHelper.WarmUp();

        //    var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
        //    var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

        //    _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
        //    _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

        //    //Engine.EngineSettings.ItemPool.Add(item1);
        //    //Engine.EngineSettings.ItemPool.Add(item2);

        //    // Put the Item on the Character
        //    _ = Character.AddItem(ItemLocationEnum.Head, item2.Id);

        //    var CharacterPlayer = new PlayerInfoModel(Character);
        //    Engine.EngineSettings.CharacterList.Clear();
        //    Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

        //    // Make the List
        //    Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

        //    // Act
        //    var result = Engine.Round.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Head);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}

        #endregion GetItemFromPoolIfBetter

        #region PickupItemsFromPool
        [Test]
        public void RoundEngine_PickupItemsFromPool_Valid_Default_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));
            Engine.EngineSettings.BattleScore.AutoBattle = true;


            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.PickupItemsFromPool(CharacterPlayer);

            // Reset
            Engine.EngineSettings.BattleScore.AutoBattle = false;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_PickupItemsFromPool_NotAutoBattle_Valid_Default_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Zed",
                ListOrder = 1,
                Guid = "me2"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));
            Engine.EngineSettings.BattleScore.AutoBattle = false;

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.PickupItemsFromPool(CharacterPlayer);

            // Reset
            Engine.EngineSettings.BattleScore.AutoBattle = true;


            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion PickupItemsFromPool

        #region EndRound
        [Test]
        public void RoundEngine_EndRound_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.Round.EndRound();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion EndRound

        #region RoundNextTurn
        [Test]
        public void RoundEngine_RoundNextTurn_Valid_No_Characters_Should_Return_GameOver()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.GameOver, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Valid_No_Monsters_Should_Return_NewRound()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            //Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NewRound, result);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Valid_Characters_Monsters_Should_Return_NewRound()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Characer",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.RoundNextTurn();

            // Reset

            // Assert
            Assert.AreEqual(RoundEnum.NextTurn, result);
        }
        #endregion RoundNextTurn

        #region GetNextPlayerInList

        [Test]
        public void RoundEngine_GetNextPlayerInList_Valid_Sue_Should_Return_Monster()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new MonsterModel
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrentHealth = 1,
                                        ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerSue);

            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Set Sue as the Player
            Engine.EngineSettings.CurrentAttacker = CharacterPlayerSue;

            // Act
            var result = Engine.Round.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.AreEqual("Monster", result.Name);
        }

        [Test]
        public void RoundEngine_GetNextPlayerInList_Valid_Monster_Should_Return_Mike()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new MonsterModel
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrentHealth = 1,
                                        ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerSue);

            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.GetNextPlayerInList();

            // Reset


            // Assert
            Assert.AreEqual("Mike", result.Name);
        }

        #endregion GetNextPlayerInList

        #region PlayerList
        //[Test]
        //public void RoundEngine_PlayerList_Valid_Default_Should_Pass()
        //{
        //    // Act
        //    var result = Engine.Round.PlayerList();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result.Any());
        //}
        #endregion PlayerList

        #region SwapCharacterItem
        [Test]
        public async Task RoundEngine_SwapCharacterItem_Valid_Default_Should_Pass()
        {
            Engine.EngineSettings.MonsterList.Clear();

            var Character = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 1,
                ExperienceTotal = 1,
                Name = "Z",
                ListOrder = 1,
                Guid = "me"
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Finger };
            var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Finger };

            _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
            _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

            Engine.EngineSettings.ItemPool.Add(item1);
            Engine.EngineSettings.ItemPool.Add(item2);

            // Put the Item on the Character
            _ = Character.AddItem(ItemLocationEnum.Head, item1.Id);

            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var dropped = Engine.Round.SwapCharacterItem(CharacterPlayer, ItemLocationEnum.Head, item2);
            
            // Reset

            // Assert
            Assert.AreEqual(item1, dropped);
            Assert.AreEqual(item2.Id, CharacterPlayer.Head);
        }
        #endregion SwapCharacterItem

        #region GetItemFromPoolIfBetter
        [Test]
        public void RoundEngine_GetItemFromPoolIfBetter_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.GetItemFromPoolIfBetter(null, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion GetItemFromPoolIfBetter

        #region RemoveDeadPlayersFromList
        [Test]
        public void RoundEngine_RemoveDeadPlayersFromList_Valid_Default_Should_Pass()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });


            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Act
            var result = Engine.Round.RemoveDeadPlayersFromList();

            // Reset


            // Assert
            Assert.AreEqual(0, result.Count());




        }
        #endregion RemoveDeadPlayersFromList

        #region GetNextPlayerTurn
        [Test]
        public void RoundEngine_GetNextPlayerTurn_Valid_Default_Should_Pass()
        {
            Engine.EngineSettings.MonsterList.Clear();

            // Arrange
            var CharacterPlayerMike = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Mike",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerDoug = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Doug",
                                            ListOrder = 2,
                                        });

            var CharacterPlayerSue = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 2,
                                            Level = 1,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Sue",
                                            ListOrder = 3,
                                        });

            var MonsterPlayer = new PlayerInfoModel(
                                    new MonsterModel
                                    {
                                        Speed = 1,
                                        Level = 1,
                                        CurrentHealth = 1,
                                        ExperienceTotal = 1,
                                        Name = "Monster",
                                        ListOrder = 4,
                                    });

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerSue);

            Engine.EngineSettings.MonsterList.Clear();
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Make the List
            Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

            // Set Mike as the Player
            Engine.EngineSettings.CurrentAttacker = CharacterPlayerMike;

            // Act
            var result = Engine.Round.GetNextPlayerTurn();

            // Reset


            // Assert
            Assert.AreEqual("Doug", result.Name);
        }
        #endregion GetNextPlayerTurn

        #region AddMonstersToRound
        [Test]
        public void AddMonstersToRound_Round_1_Valid_Should_Pass()
        {
            // Arrange
            Engine.EngineSettings.BattleScore.RoundCount = 1;
            Engine.EngineSettings.MonsterList.Clear();

            var CharacterPlayerEvie = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 4,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Evie",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerEfie = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 2,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Efie",
                                            ListOrder = 2,
                                        });


            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerEvie);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerEfie);

            // Act
            var result = Engine.Round.AddMonstersToRound();

            var maxMonsterLevel = Engine.EngineSettings.MonsterList.Max(m => m.Level);

            // Reset

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void AddMonstersToRound_Round_3_Valid_Should_Return_1_Round_Boss()
        {
            // Arrange
            Engine.EngineSettings.BattleScore.RoundCount = 8;
            Engine.EngineSettings.MonsterList.Clear();

            var CharacterPlayerEvie = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 4,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Evie",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerEfie = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 2,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Efie",
                                            ListOrder = 2,
                                        });


            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerEvie);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerEfie);

            // Act
            var result = Engine.Round.AddMonstersToRound();

            var maxMonsterLevel = Engine.EngineSettings.MonsterList.Max(m => m.Level);

            // Reset

            // Assert
            //Test if the max level in that monster list is lesser or equal to character list average level

            Assert.AreEqual(6, result);
            //Assert.LessOrEqual(maxMonsterLevel, 4);
            Assert.AreEqual(1, Engine.EngineSettings.MonsterList.ToList().Where(m => m.Job == CharacterJobEnum.RoundBoss).Count());
        }

        [Test]
        public void AddMonstersToRound_Round_10_Valid_Should_Return_1_Great_Boss()
        {
            // Arrange
            Engine.EngineSettings.BattleScore.RoundCount = 9;
            Engine.EngineSettings.MonsterList.Clear();

            var CharacterPlayerEvie = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 200,
                                            Level = 4,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Evie",
                                            ListOrder = 1,
                                        });

            var CharacterPlayerEfie = new PlayerInfoModel(
                                        new CharacterModel
                                        {
                                            Speed = 20,
                                            Level = 2,
                                            Alive = false,
                                            CurrentHealth = 1,
                                            ExperienceTotal = 1,
                                            Name = "Efie",
                                            ListOrder = 2,
                                        });


            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            Engine.EngineSettings.CharacterList.Clear();

            Engine.EngineSettings.CharacterList.Add(CharacterPlayerEvie);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayerEfie);

            // Act
            var result = Engine.Round.AddMonstersToRound();

            var maxMonsterLevel = Engine.EngineSettings.MonsterList.Max(m => m.Level);

            // Reset

            // Assert
            //Test if the max level in that monster list is lesser or equal to character list average level

            Assert.AreEqual(6, result);
            Assert.AreEqual(true, Engine.EngineSettings.MonsterList.ToList().Any(m => m.Job == CharacterJobEnum.GreatLeader));
        }

        #endregion
    }
}