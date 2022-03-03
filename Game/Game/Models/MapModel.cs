using Game.Helpers;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;


namespace Game.Models
{
    /// <summary>
    /// Represent the Map
    /// 
    /// The Cordinates
    /// What is at that location
    /// 
    /// </summary>
    public class MapModel
    {
        // The X axies Size
        public int MapXAxiesCount = 6;

        // The Y axies Size
        public int MapYAxiesCount = 5;

        // The Map Locations
        public MapModelLocation[,] MapGridLocation;

        public PlayerInfoModel EmptySquare = new PlayerInfoModel { PlayerType = PlayerTypeEnum.Unknown, ImageURI = "mapcell.png" };

        // random number generator
        private Random rand = new Random();

        public MapModel()
        {
            // Create the Map
            MapGridLocation = new MapModelLocation[MapXAxiesCount, MapYAxiesCount];

            _ = ClearMapGrid();
        }

        /// <summary>
        /// Create an Empty Map
        /// </summary>
        /// <returns></returns>
        public bool ClearMapGrid()
        {
            //Populate Map with Empty Values
            for (var x = 0; x < MapXAxiesCount; x++)
            {
                for (var y = 0; y < MapYAxiesCount; y++)
                {
                    // Populate the entire map with blank
                    MapGridLocation[x, y] = new MapModelLocation { Row = y, Column = x, Player = EmptySquare };
                }
            }
            return true;
        }

        /// <summary>
        /// Initialize the Data Structure
        /// Add Characters, Monsters, and Obstacles to the Map
        /// </summary>
        /// <param name="PlayerList"></param>
        /// <returns></returns>
        public bool PopulateMapModel(List<PlayerInfoModel> PlayerList, bool addObstacles = true)
        {
            _ = ClearMapGrid();

            var rnd = new Random();


            // Randomly position Characters in the first two columns
            var characters = PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character);
            var characterCols = (int)Math.Ceiling((double)characters.Count() / MapYAxiesCount);
            var randomCharacterCells = Enumerable.Range(0, MapYAxiesCount * characterCols).OrderBy(c => rnd.Next()).Take(characters.Count()).ToList();

            for (var c = 0; c < characters.Count(); c++)
            {
                var row = (int)randomCharacterCells[c] % MapYAxiesCount;
                var col = (int)Math.Floor((double)randomCharacterCells[c] / MapYAxiesCount);
                MapGridLocation[col, row].Player = characters.ElementAt(c);
            }

            // populate the map with the monsters randomly in the laster two columns
            var monsters = PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster);
            var monsterCols = (int)Math.Ceiling((double)monsters.Count() / MapYAxiesCount);
            var randomMonsterCells = Enumerable.Range((MapYAxiesCount* MapXAxiesCount)-(MapYAxiesCount * monsterCols),
                MapYAxiesCount * monsterCols).OrderBy(m => rnd.Next()).Take(monsters.Count()).ToList();

            for (var m = 0; m < monsters.Count(); m++)
            {
                var row = (int)randomMonsterCells[m] % MapYAxiesCount;
                var col = (int)Math.Floor((double)randomMonsterCells[m] / MapYAxiesCount);
                MapGridLocation[col, row].Player = monsters.ElementAt(m);
            }

            if (addObstacles)
            { 
                var obstacle = GameImagesHelper.GetObstacleImage();
                var totalObstacles = 3;

                // Populate the map with obstacles randomly in spaces that aren't populated by characters or monsters
                var randomObstacleCells = Enumerable.Range(0, MapYAxiesCount * MapXAxiesCount).OrderBy(c => rnd.Next()).Take(MapYAxiesCount * MapXAxiesCount).ToList();
                
                for (var o = 0; o < randomObstacleCells.Count(); o++)
                {
                    if (totalObstacles <= 0)
                    {
                        break;
                    }
                
                    var row = (int)randomObstacleCells[o] % MapYAxiesCount;
                    var col = (int)Math.Floor((double)randomObstacleCells[o] / MapYAxiesCount);
                
                    // Add the obstacles to emtpy cells that aren't covered by an a player or mosnter
                    if (MapGridLocation[col, row].Player.PlayerType == EmptySquare.PlayerType)
                    {
                        // Create a new obstacle at the randomly selected location
                        MapGridLocation[col, row].Player = new PlayerInfoModel { PlayerType = PlayerTypeEnum.Obstacle, ImageURI = obstacle[rnd.Next(0, 3)] };
                        totalObstacles--;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Changes the Row and Column for the Player
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool MovePlayerOnMap(MapModelLocation data, MapModelLocation target)
        {
            if (target.Column < 0)
            {
                return false;
            }

            if (target.Row < 0)
            {
                return false;
            }

            if (target.Column >= MapXAxiesCount)
            {
                return false;
            }

            if (target.Row >= MapYAxiesCount)
            {
                return false;
            }

            MapGridLocation[target.Column, target.Row].Player = data.Player;

            // Clear out the old location
            MapGridLocation[data.Column, data.Row].Player = EmptySquare;

            return true;
        }

        /// <summary>
        /// Remove the Player from the Map
        /// Replaces with Empty Square
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RemovePlayerFromMap(PlayerInfoModel data)
        {
            if (data == null)
            {
                return false;
            }

            for (var x = 0; x < MapXAxiesCount; x++)
            {
                for (var y = 0; y < MapYAxiesCount; y++)
                {
                    if (MapGridLocation[x, y].Player.Guid.Equals(data.Guid))
                    {
                        MapGridLocation[x, y] = new MapModelLocation { Column = x, Row = y, Player = EmptySquare };
                        return true;
                    }
                }
            }

            // Not found
            return false;
        }

        /// <summary>
        /// Clear all Locations of the Selected Bool
        /// 
        /// Mike does not use this in the example battle grammar
        /// 
        /// TODO: INFO  Could be helpfull to track what is selected for actions etc
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ClearSelection()
        {
            foreach (var data in MapGridLocation)
            {
                data.IsSelectedTarget = false;
            }

            return true;
        }

        /// <summary>
        /// Find the Player on the map
        /// Return their information
        /// 
        /// If they don't exist, return null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MapModelLocation GetLocationForPlayer(PlayerInfoModel player)
        {
            if (player == null)
            {
                return null;
            }

            foreach (var data in MapGridLocation)
            {
                if (data.Player.Guid.Equals(player.Guid))
                {
                    return data;
                }
            }

            return null;
        }

        /// <summary>
        /// Return List of cells next to player that are empty
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public List<MapModelLocation> GetPlayerEmptyAdjacentCells(MapModelLocation player)
        {
            var Result = new List<MapModelLocation>();

            if (player.Row > 0 && MapGridLocation[player.Column, player.Row - 1].Player.PlayerType == PlayerTypeEnum.Unknown)
            {
                Result.Add(MapGridLocation[player.Column, player.Row - 1]);
            }
            if (player.Row + 1 < MapYAxiesCount && MapGridLocation[player.Column, player.Row + 1].Player.PlayerType == PlayerTypeEnum.Unknown)
            {
                Result.Add(MapGridLocation[player.Column, player.Row + 1]);
            }
            if (player.Column > 0 && MapGridLocation[player.Column - 1, player.Row].Player.PlayerType == PlayerTypeEnum.Unknown)
            {
                Result.Add(MapGridLocation[player.Column - 1, player.Row]);
            }
            if (player.Column + 1 < MapXAxiesCount && MapGridLocation[player.Column + 1, player.Row].Player.PlayerType == PlayerTypeEnum.Unknown)
            {
                Result.Add(MapGridLocation[player.Column + 1, player.Row]);
            }
            return Result;
        }


        /// <summary>
        /// Return cells the player can move to using Breadth-First-Seach 
        /// default max distance of 2
        /// </summary>
        /// <param name="player"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public HashSet<MapModelLocation> GetAvailableLocationsFromPlayer(MapModelLocation player, int distance = 2)
        {
            var Result = new HashSet<MapModelLocation>();
            var gridLocationQueue = new Queue<MapModelLocation>();
            var depths = new Dictionary<MapModelLocation, int>();
            gridLocationQueue.Enqueue(player);
            depths[player] = 0;

            while (gridLocationQueue.Count() > 0)
            {
                var currentCell = gridLocationQueue.Dequeue();
                if (depths[currentCell] == distance + 1)
                {
                    break;
                }
                Result.Add(currentCell);

                foreach (var child in GetPlayerEmptyAdjacentCells(currentCell))
                {
                    if (depths.ContainsKey(child))
                    {
                        continue;
                    }
                    gridLocationQueue.Enqueue(child);
                    depths[child] = depths[currentCell] + 1;
                }
            }
            return Result;
        }


        /// <summary>
        /// Return who is at the location
        /// Could be Character, Monster, Obstacle or Empty
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerAtLocation(int x, int y)
        {
            return MapGridLocation[x, y].Player;
        }

        /// <summary>
        /// Is the location empty?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsEmptySquare(int x, int y)
        {
            var player = MapGridLocation[x, y].Player;

            // Unknown is Empty
            if (player.PlayerType == PlayerTypeEnum.Unknown)
            {
                return true;
            }

            // Occupied
            return false;
        }

        /// <summary>
        /// Return all Empty Locations on the map
        /// </summary>
        /// <returns></returns>
        public List<MapModelLocation> GetEmptyLocations()
        {
            var Result = new List<MapModelLocation>();

            foreach (var data in MapGridLocation)
            {
                if (data.Player.PlayerType == PlayerTypeEnum.Unknown)
                {
                    Result.Add(data);
                }
            }

            return Result;
        }

        /// <summary>
        /// Walk the Map and Find the Location that is close to the target
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public MapModelLocation ReturnClosestEmptyLocation(MapModelLocation Target)
        {
            MapModelLocation Result = null;

            var LowestDistance = int.MaxValue;

            foreach (var data in GetEmptyLocations())
            {
                var distance = CalculateDistance(data, Target);
                if (distance < LowestDistance)
                {
                    Result = data;
                    LowestDistance = distance;
                }
            }

            return Result;
        }

        /// <summary>
        /// See if the Attacker is next to the Defender by the distance of Range
        /// 
        /// If either the X or Y distance is less than or equal the range, then they can hit
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Defender"></param>
        /// <returns></returns>
        public bool IsTargetInRange(PlayerInfoModel Attacker, PlayerInfoModel Defender)
        {
            var locationAttacker = GetLocationForPlayer(Attacker);
            var locationDefender = GetLocationForPlayer(Defender);

            if (locationAttacker == null)
            {
                return false;
            }

            if (locationDefender == null)
            {
                return false;
            }

            // Get X distance in absolute value
            var distance = Math.Abs(CalculateDistance(locationAttacker, locationDefender));

            var AttackerRange = Attacker.GetRange();

            // Can Reach on X?
            if (distance <= AttackerRange)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculate distance between two map locations
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int CalculateDistance(MapModelLocation start, MapModelLocation end)
        {
            if (start == null)
            {
                return int.MaxValue;
            }

            if (end == null)
            {
                return int.MaxValue;
            }

            return Distance(start.Column, start.Row, end.Column, end.Row);
        }

        /// <summary>
        /// Calculate Distance between locations
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public int Distance(int x1, int y1, int x2, int y2)
        {
            return ((int)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        }
    }
}