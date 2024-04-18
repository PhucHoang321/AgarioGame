using AgarioModels;

using Communications;

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Security.Principal;
using System.Text.Json;
using System.Text.RegularExpressions;



namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        public long _localID;
        public float _zoomScale;
        private Networking _client;
        private bool initialized;
        private readonly World _world;
        private WorldDrawable worldDrawable;
        private System.Timers.Timer Timer;
        private readonly ILogger _logger;
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
            worldDrawable = new WorldDrawable(_world, PlaySurface);
            PlaySurface.Drawable = worldDrawable;
            Timer = new System.Timers.Timer(1000 / 60);
            Timer.Elapsed += (s, e) => GameStep();
            Timer.Start();
        }

        private void GameStep()
        {
            Dispatcher.Dispatch(() =>
            {
                PlaySurface.Invalidate();
            });
        }
        private void PointerChanged(object sender, PointerEventArgs e)
        {
            Point pointerPosition = (Point)e.GetPosition(PlaySurface);

            float gameX = (float)pointerPosition.X * (float)_world.Width / (float)PlaySurface.Width;
            float gameY = (float)pointerPosition.Y * (float)_world.Height / (float)PlaySurface.Height;

            _client.SendAsync(String.Format(Protocols.CMD_Move, (int)gameX, (int)gameY));
        }
        private void OnTap(object sender, EventArgs e) 
        { 
            
        }
        private void PanUpdated(object sender, EventArgs e) 
        {
            //PanUpdatedEventArgs panEvent = (PanUpdatedEventArgs)e;

            //// Check the total distance traveled in the X and Y directions
            //double totalX = panEvent.TotalX;
            //double totalY = panEvent.TotalY;

            //// Adjust zoom scale based on the pan gesture
            //_zoomScale += (float)(totalX * 0.1); // You can adjust the sensitivity of zooming

            //// Ensure the zoom scale doesn't go below a certain threshold (e.g., minimum zoom)
            //if (_zoomScale < 1)
            //{
            //    _zoomScale = 1;
            //}

            //// Redraw the graphics with the updated zoom scale
            //PlaySurface.Invalidate();
        }
        private async void StartGame_Clicked(object sender, EventArgs e)
        {
            await _client.ConnectAsync("localhost", 11000);
            // Hide the welcome screen
            WelcomeScreen.IsVisible = false;
            // Show the game screen
            GameScreen.IsVisible = true;

            await _client.HandleIncomingDataAsync(true);       
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
                }else if (message.StartsWith(Protocols.CMD_Eaten_Food)) 
                {
                    _world.RemoveFood(message);
                }else if (message.StartsWith(Protocols.CMD_Player_Object)) 
                {
                    _world.clientID = long.Parse(message[Protocols.CMD_Player_Object.Length..]);
                }else if (message.StartsWith(Protocols.CMD_Dead_Players))
                {
                    _world.RemovePlayer(message);
                    _client.Disconnect();
                }
            }
            catch (Exception ex) 
            { 
                _logger.LogDebug(ex.Message, ex);
            }
            
        }
        public long GetLocalID()
        {
            return _localID;
        }
        private void OnDisconnect(Networking channel)
        {
            WelcomeScreen.IsVisible = true;
            GameScreen.IsVisible = false;
        }

        private void OnConnect(Networking channel)
        {
            // Send the player name to server
            string name = NameEntry.Text;
            _client.SendAsync(String.Format(Protocols.CMD_Start_Game, "HIKINEET"));
        }
    }

}
