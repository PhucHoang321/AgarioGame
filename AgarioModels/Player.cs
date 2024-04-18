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
        public Boolean isDead {  get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; }
        public Player(string name, float x, float y, int argbColor, long id, float mass) : 
            base(x, y, argbColor, id, mass)
        {
            Name = name;
            isDead = false;
        }
    }
}
