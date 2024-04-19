﻿using System.Numerics;
using System.Text.Json.Serialization;
namespace AgarioModels
{

    /// <summary>
    /// Represents a game object with position, color, and mass.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Gets the X-coordinate of the object.
        /// </summary>
        [JsonPropertyName("X")]
        public float X { get;  }

        /// <summary>
        /// Gets the Y-coordinate of the object.
        /// </summary>
        [JsonPropertyName("Y")]
        public float Y { get; }

        /// <summary>
        /// Gets or sets the unique ID of the object.
        /// </summary>
        [JsonPropertyName("ID")]
        public long ID { get; set; }

        /// <summary>
        /// Gets the mass of the object.
        /// </summary>
        [JsonPropertyName("Mass")]     
        public float Mass { get; private set; }

        /// <summary>
        /// Gets or sets the color of the object in ARGB format.
        /// </summary>
        [JsonPropertyName("ARGBColor")]
        public int ARGBColor { get; set; }

        /// <summary>
        /// Gets or sets the location of the object.
        /// </summary>
        public Vector2 Location { get; set; }

        /// <summary>
        /// Gets or sets the radius of the object.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Initializes a new instance of the GameObject class with the specified parameters.
        /// </summary>
        /// <param name="x">The X-coordinate of the object.</param>
        /// <param name="y">The Y-coordinate of the object.</param>
        /// <param name="argbColor">The color of the object in ARGB format.</param>
        /// <param name="id">The unique identifier of the object.</param>
        /// <param name="mass">The mass of the object.</param>
        public GameObject(float x,float y, int argbColor, long id, float mass)
        {
            X = x;
            Y = y;
            ID = id;
            Location = new Vector2(X, Y);
            Mass = mass;
            ARGBColor = argbColor;
            Radius = (float) Math.Sqrt(Mass / Math.PI);
        }    
    }
}
