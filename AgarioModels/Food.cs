using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{
    internal class Food: GameObject
    {
        public Food(long id, Vector2 location, int argbColor, float mass)
         : base(id, location, argbColor, mass)
        {
        }
    }
}
