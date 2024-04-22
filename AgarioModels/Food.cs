/// <summary>
/// Author:    Phuc Hoang
/// Partner:   Chanphone Visathip
/// Date:      7-April-2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Phuc Hoang - This work may not 
///            be copied for use in Academic Coursework.
///
/// I, Phuc Hoang,and Chanphone Visathip certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
/// Represents a food object in the game, inheriting properties from GameObject.
/// </summary>
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
