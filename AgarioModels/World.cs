using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AgarioModels {
    //the world Width and Height(please use read only 'constants') defaulting to 5000 by 5000
    //A list of players in the game.
    //A list of food in the game.
    //You may of course store additional information.How your code chooses to store the game objects is an important SE design decision.

    //A logger object.

    public class World
    {
        public long clientID;
        public readonly int Width = 5000;
        public readonly int Height = 5000;
        private Player[]? players;
        private Food[]? foods;
      
        private readonly Dictionary<long, Player> _players;
        private readonly Dictionary<long, Food> _foods;
        private readonly ILogger _logger;
        
        public World()
        {
           
            _players = new Dictionary<long, Player>();
            _foods = new Dictionary<long, Food>();
        }

        


        public Dictionary<long, Player> Players
        {
            get { return _players; }
        }

        public Dictionary<long, Food>? Foods {
            get { return _foods; } 
        }

        public void AddFood(string message)
        {
            message = message[Protocols.CMD_Food.Length..]!;
            foods = JsonSerializer.Deserialize<Food[]>(message);
            foreach (Food food in foods)
            {
                _foods.Add(food.ID, food);
            }
        }

        public void AddPlayer(string message)
        {
            message = message[Protocols.CMD_Update_Players.Length..]!;
            players = JsonSerializer.Deserialize<Player[]>(message);
            foreach (Player player in players)
            {
                if (!_players.ContainsKey(player.ID)) 
                { 
                    _players.Add(player.ID, player); 
                }else {
                    _players[player.ID] = player;
                }
            }
        }

        public void RemoveFood(string message)
        {
            message = message[Protocols.CMD_Eaten_Food.Length..]!;
            if(message != null)
            {
                long[] foodID = JsonSerializer.Deserialize<long[]>(message);
                foreach (long ID in foodID)
                {
                    _foods.Remove(ID);
                }
            }          
        }
      
        // Update the player position
        public void UpdatePlayerPosition(long playerId, float newX, float newY)
        {
            if (_players.TryGetValue(playerId, out Player player))
            {
                // Update the player's position
                player.X = newX;
                player.Y = newY;
            }
            else
            {
                // Player not found with the specified ID
                _logger.LogWarning($"Player with ID {playerId} not found.");
            }
        }

        public void RemovePlayer(string message)
        {
            message = message[Protocols.CMD_Dead_Players.Length..]!;
            long[]? idDead = JsonSerializer.Deserialize<long[]>(message);
            if(idDead != null)
            {
                foreach (long id in idDead)
                {
                    if (_players.ContainsKey(id))
                    {
                        _players[id].isDead = true;
                    }
                }
            }
          


        }
    }
}
