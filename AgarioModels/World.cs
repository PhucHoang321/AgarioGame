using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AgarioModels {
    //the world Width and Height(please use read only 'constants') defaulting to 5000 by 5000
    //A list of players in the game.
    //A list of food in the game.
    //You may of course store additional information.How your code chooses to store the game objects is an important SE design decision.

    //A logger object.

    public class World
    {
        /// <summary>
        /// The ID of the client player.
        /// </summary>
        public long clientID;
        /// <summary>
        /// The width of the world
        /// </summary>
        public readonly int Width = 5000;
        /// <summary>
        /// The height of the world
        /// </summary>
        public readonly int Height = 5000;
        /// <summary>
        /// An array containing all players in the game.
        /// </summary>
        private Player[]? players;
        /// <summary>
        /// An array containing all food items in the game.
        /// </summary>
        private Food[]? foods;
        /// <summary>
        /// Dictionary containing all players in the game.
        /// </summary>
        private readonly Dictionary<long, Player> _players;
        /// <summary>
        /// Dictionary containing all food items in the game.
        /// </summary>
        private readonly Dictionary<long, Food> _foods;
        /// <summary>
        /// The logger instance used for logging.
        /// </summary>
        private readonly ILogger? _logger;

        /// <summary>
        /// Initializes a new instance of the World class.
        /// </summary>
        public World()
        {          
            _players = new Dictionary<long, Player>();
            _foods = new Dictionary<long, Food>();
        }

        /// <summary>
        /// Gets the dictionary containing all players in the world.
        /// </summary>
        public Dictionary<long, Player> Players
        {
            get { return _players; }
        }

        /// <summary>
        /// Gets the dictionary containing all food items in the world.
        /// </summary>
        public Dictionary<long, Food>? Foods {
            get { return _foods; } 
        }

        /// <summary>
        /// Adds food items to the dictionary from the deserialized message.
        /// </summary>
        /// <param name="message">The serialized message containing food data</param>
        public void AddFood(string message)
        {
            message = message[Protocols.CMD_Food.Length..]!;
            foods = JsonSerializer.Deserialize<Food[]>(message);
            if(foods is not null )
            {
                foreach (Food food in foods)
                {
                    _foods.Add(food.ID, food);
                }
            }         
        }

        /// <summary>
        /// Adds player data to the dictionary from the deserialized message.
        /// </summary>
        /// <param name="message">The serialized message containing player data.</param>
        public void AddPlayer(string message)
        {
            message = message[Protocols.CMD_Update_Players.Length..]!;
            players = JsonSerializer.Deserialize<Player[]>(message);
            if(players is not null)
            {
                foreach (Player player in players)
                {
                    if (!_players.ContainsKey(player.ID))
                    {
                        _players.Add(player.ID, player);
                    }
                    else
                    {
                        _players[player.ID] = player;
                    }
                }
            }           
        }

        /// <summary>
        /// Removes food items from the dictionary based on the deserialized message.
        /// </summary>
        /// <param name="message">The serialized message containing food IDs to be removed.</param>
        public void RemoveFood(string message)
        {
            message = message[Protocols.CMD_Eaten_Food.Length..]!;
            if(message != null)
            {
                long[] foodID = JsonSerializer.Deserialize<long[]>(message);
                if(foodID is not null)
                {
                    foreach (long ID in foodID)
                    {
                        _foods.Remove(ID);
                    }
                }
              
            }          
        }

        

        /// <summary>
        /// Marks players as dead based on the deserialized message.
        /// </summary>
        /// <param name="message">The serialized message containing IDs of dead players.</param>
        public void MarkPlayersAsDead(string message)
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
