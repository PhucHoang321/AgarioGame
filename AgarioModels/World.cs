using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AgarioModels
{
//the world Width and Height(please use read only 'constants') defaulting to 5000 by 5000
//A list of players in the game.
//A list of food in the game.
//You may of course store additional information.How your code chooses to store the game objects is an important SE design decision.

//A logger object.

    internal class World
    {
        public readonly int Width = 5000;
        public readonly int Height = 5000;

        private readonly Dictionary<long, Player> players;
        private readonly Dictionary<long, Food> foods;

        public IReadOnlyCollection<Player> Players => players.Values;
        public IReadOnlyCollection<Food> Foods => foods.Values;

        public World()
        {
            players = new Dictionary<long, Player>();
            foods = new Dictionary<long, Food>();
        }
    }
}
