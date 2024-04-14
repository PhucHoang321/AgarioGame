using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Player : GameObject
    {
        [JsonPropertyName("Name")]
        public string Name { get; }
        public Player(string name, long id,float x, float y, int argbColor, float mass) : 
            base(id, x, y, argbColor, mass)
        {
            Name = name;
        }
    }
}
