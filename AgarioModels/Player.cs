﻿using System.Text.Json.Serialization;


namespace AgarioModels
{
    /// <summary>
    /// Represents a player object in the game, inheriting properties from GameObject.
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// Gets or sets a value indicating whether the player is dead.
        /// </summary>
        public Boolean isDead {  get; set; }

        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        [JsonPropertyName("Name")]
        public string Name { get; }


        /// <summary>
        /// Initializes a new instance of the Player class with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="x">The X-coordinate of the player.</param>
        /// <param name="y">The Y-coordinate of the player.</param>
        /// <param name="argbColor">The color of the player in ARGB format.</param>
        /// <param name="id">The unique identifier of the player.</param>
        /// <param name="mass">The mass of the player.</param>
        public Player(string name, float x, float y, int argbColor, long id, float mass) : 
            base(x, y, argbColor, id, mass)
        {
            Name = name;
            isDead = false;
        }
    }
}
