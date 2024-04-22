using Microsoft.Extensions.Logging;
using System.Text.Json;
/// <summary>
/// Author:    Phuc Hoang
/// Partner:   Chanphone Visathip
/// Date:      7-April-2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Phuc Hoang - This work may not 
///            be copied for use in Academic Coursework.
///
/// I, Phuc Hoang,and Chanphone Visathip certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
/// This file contains the definition of the World class, which represents the game world in the Agario game.
/// It stores information about the players and food items in the game, as well as provides methods for updating game state.
/// </summary>
namespace AgarioModels {
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
