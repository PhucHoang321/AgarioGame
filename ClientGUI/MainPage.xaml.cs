﻿using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Text.Json;


namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        private Networking _client;
        private bool initialized;
        private readonly World _world;
        private WorldDrawable worldDrawable;
        private System.Timers.Timer Timer;
        private readonly ILogger _logger;
        private Dictionary<long, Food> _foods;
        private Dictionary<long, Player> _players;
        public MainPage(ILogger<MainPage> logger)
        {
            _world = new World();
            _logger = logger;
            _client = new Networking(logger, OnConnect, OnDisconnect, OnMessage);
            InitializeComponent();
        }
        /// <summary>
        ///    Called when the window is resized.  
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Debug.WriteLine($"OnSizeAllocated {width} {height}");
            if (!initialized)
            {
                initialized = true;
                InitializeGameLogic();
            }
        }

        private void InitializeGameLogic()
        {
            worldDrawable = new WorldDrawable(_world);
            PlaySurface.Drawable = worldDrawable;
            Timer = new System.Timers.Timer(1000 / 60);
            Timer.Elapsed += (s, e) => GameStep();
            Timer.Start();
        }

        private void GameStep()
        {
            
        }

        private async void StartGame_Clicked(object sender, EventArgs e)
        {
            await _client.ConnectAsync("localhost", 11000);
            await _client.HandleIncomingDataAsync(true);
            // Hide the welcome screen
            WelcomeScreen.IsVisible = false;

            // Show the game screen
            GameScreen.IsVisible = true;
        }

        private void OnMessage(Networking channel, string message)
        {
            try
            {
                if (message.StartsWith(Protocols.CMD_Food))
                {
                    _world.AddFood(message);
                }
                else if (message.StartsWith(Protocols.CMD_Update_Players))
                {
                    _world.AddPlayer(message);
                }
            }
            catch (Exception ex) 
            { 
                _logger.LogDebug(ex.Message, ex);
            }
            
        }

        private void OnDisconnect(Networking channel)
        {
        
        }

        private void OnConnect(Networking channel)
        {
            // Send the player name to server
            string name = NameEntry.Text;
            _client.SendAsync(String.Format(Protocols.CMD_Start_Game, "aun"));
        }
    }

}
