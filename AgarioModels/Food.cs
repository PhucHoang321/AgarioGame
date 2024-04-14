using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgarioModels
{
    public class Food: GameObject
    {
        public Food(float x,float y, int argbColor, long id, float mass)
         : base(x, y, argbColor, id, mass)
        {
        }
    }
}
