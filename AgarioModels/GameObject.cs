using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AgarioModels
{
    //A unique id number.This value should be a long integer.
    //The location of the game object in the world (x, y). (e.g., 205.6, 199.9)
    //I suggest using a built-in type(e.g., System.Numerics.Vector2).  Read up on this library.
    //It is likely one you will use in many future projects.

    //Note: the position represents the center of the object (e.g., center or the circle). 
    //X and Y properties for the game object (which do not have setters) that return the appropriate location value.
    //These should reference the location value above.

    //An ARGBcolor - just for display purposes. ARGBs are integers.  
    //A mass - used to determine how big to draw the circle.  Mass is a float.

    public class GameObject
    {
        [JsonPropertyName("X")]
        public float X {  get; set; }
        [JsonPropertyName("Y")]
        public float Y { get; set; }
        [JsonPropertyName("ID")]
        public long ID { get; set; }
        [JsonPropertyName("Mass")]
        public float Mass { get; private set; }
        [JsonPropertyName("ARGBColor")]
        public int ARGBColor { get; set; }
        
        public Vector2 Location { get; set; }
       
        public float Radius { get; set; }
        // base(id, new Vector2(x, y), argbColor, mass)
        public GameObject(float x,float y, int argbColor, long id, float mass)
        {
            X = x;
            Y = y;
            ID = id;
            Location = new Vector2(X, Y);
            Mass = mass;
            ARGBColor = argbColor;
            Radius = 20;//todo radius
        }

       
    }
}
