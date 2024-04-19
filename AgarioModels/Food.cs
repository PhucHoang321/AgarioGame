namespace AgarioModels
{
    /// <summary>
    /// Represents a food object in the game, inheriting properties from GameObject.
    /// </summary>
    public class Food: GameObject
    {
        /// <summary>
        /// Initializes a new instance of the Food class with the specified parameters.
        /// </summary>
        /// <param name="x">The X-coordinate of the food.</param>
        /// <param name="y">The Y-coordinate of the food.</param>
        /// <param name="argbColor">The color of the food in ARGB format.</param>
        /// <param name="id">The unique identifier of the food.</param>
        /// <param name="mass">The mass of the food.</param>
        public Food(float x,float y, int argbColor, long id, float mass)
         : base(x, y, argbColor, id, mass)
        {
        }
    }
}
