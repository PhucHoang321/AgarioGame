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
        public string Name { get; }
        public Player(long id, Vector2 location, int argbColor, float mass, string name) : base(id, location, argbColor, mass)
        {
            Name = name;
        }
    }
}
