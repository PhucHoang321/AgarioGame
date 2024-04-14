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
        public Food(long id, float x,float y, int argbColor, float mass)
         : base(id, x, y, argbColor, mass)
        {
        }
    }
}
