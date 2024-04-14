using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AgarioModels
{
    //the world Width and Height(please use read only 'constants') defaulting to 5000 by 5000
    //A list of players in the game.
    //A list of food in the game.
    //You may of course store additional information.How your code chooses to store the game objects is an important SE design decision.

    //A logger object.

    public class World
    {
        public readonly int Width = 5000;
        public readonly int Height = 5000;

        private readonly Dictionary<long, Player> _players;
        private readonly Dictionary<long, Food> _foods;
        public World()
        {

            _players = new Dictionary<long, Player>();
            _foods = new Dictionary<long, Food>();
        }

        public void AddFood(string message)
        {
            message = message[Protocols.CMD_Food.Length..]!;
            Food[]? foods = JsonSerializer.Deserialize<Food[]>(message);
            foreach (Food food in foods)
            {
                _foods.Add(food.ID, food);
            }
        }

        public void AddPlayer(string message)
        {
            message = message[Protocols.CMD_Update_Players.Length..]!;
            Player[]? players = JsonSerializer.Deserialize<Player[]>(message);
            foreach (Player player in players)
            {
                if (player.ID != null) { _players.Add(player.ID, player); }
                //else { }
            }
        }
        private void UpdatePlayer() { }
    }
}
