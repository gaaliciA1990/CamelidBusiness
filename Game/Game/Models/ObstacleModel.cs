using Game.GameRules;

namespace Game.Models
{
    /// <summary>
    /// Obstacles that appear on a map. They are able to be destroyed but do not 
    /// affect the game play in any other way than blocking a path.
    /// 
    /// Derive from BasePlayerModel
    /// </summary>
    public class ObstacleModel : BasePlayerModel<ObstacleModel>
    {

        /// <summary>
        /// Default obstacle
        /// 
        /// Gets a type, guid, name and description
        /// </summary>
        public ObstacleModel()
        {
            PlayerType = PlayerTypeEnum.Obstacle;
            Guid = Id;
            ImageURI = "obstacle1.png";
        }

        /// <summary>
        /// Create a copy
        /// </summary>
        /// <param name="data"></param>
        public ObstacleModel(ObstacleModel data)
        {
            _ = Update(data);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public override bool Update(ObstacleModel newData)
        {
            if (newData == null)
            {
                return false;
            }

            Guid = newData.Guid;
            ImageURI = newData.ImageURI;
            CurrentHealth = newData.CurrentHealth;
            MaxHealth = newData.MaxHealth;

            // Update the Job
            Job = newData.Job;

            return true;
        }
    }
}