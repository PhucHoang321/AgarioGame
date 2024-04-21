using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {

        /// <summary>
        /// The scale used for zooming.
        /// </summary>
        private float _zoomScale;

        /// <summary>
        /// The networking component for handling communication with the server.
        /// </summary>
        private readonly Networking _client;

        /// <summary>
        /// Indicates whether the application has been initialized.
        /// </summary>
        private bool initialized;

        /// <summary>
        /// The world containing game data.
        /// </summary>
        private readonly World _world;

        /// <summary>
        /// The drawable representation of the game world.
        /// </summary>
        private WorldDrawable? worldDrawable;

        /// <summary>
        /// Timer for updating the game state periodically.
        /// </summary>
        private System.Timers.Timer? Timer;

        /// <summary>
        /// The logger for logging messages.
        /// </summary>
        private readonly ILogger? _logger;

        /// <summary>
        /// Gets or sets the target frames per second (FPS) for the application.
        /// </summary>
        /// <remarks>
        /// This property determines the desired frame rate at which the application should render graphics.
        /// </remarks>
        public double TargetFPS { get; private set; } = 30;

        /// <summary>
        /// Initializes a new instance of the MainPage class with the specified logger.
        /// </summary>
        /// <param name="logger">The logger for logging messages.</param>
        public MainPage(ILogger<MainPage> logger)
        {
            _world = new World();
            _logger = logger;
            _client = new Networking(logger, OnConnect, OnDisconnect, OnMessage);
            InitializeComponent();
        }
        /// <summary>
        /// Called when the window is resized.  
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

        /// <summary>
        /// Initializes the game logic including world rendering and game loop setup.
        /// </summary>
        private void InitializeGameLogic()
        {
            worldDrawable = new WorldDrawable(_world, PlaySurface);
            PlaySurface.Drawable = worldDrawable;
            Timer = new System.Timers.Timer(1000 / 60);
               Timer.Interval = TargetFPS;
            Timer.Elapsed += (s, e) => GameStep();
            Timer.Start();
        }

        /// <summary>
        /// Performs a single step of the game loop, updating the UI and displaying FPS.
        /// </summary>
        private void GameStep()
        {
            Dispatcher.Dispatch(() =>
            {
                PlaySurface.Invalidate();
                fps.Text = "FPS: " + Timer?.Interval;
            });
        }

        /// <summary>
        /// Handles pointer movement events on the graphics surface.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments containing pointer information.</param>
        private void PointerChanged(object sender, PointerEventArgs e)
        {          
                Point pointerPosition = (Point)e.GetPosition(PlaySurface);
                float? gameX = (float)pointerPosition.X / (float)PlaySurface.Width * (float)_world.Width;
                float? gameY = (float)pointerPosition.Y / (float)PlaySurface.Height * (float)_world.Height;
                myEntry.Focus();
                _client.SendAsync(String.Format(Protocols.CMD_Move, (int)gameX, (int)gameY));                  
        }

        /// <summary>
        /// Handles tap events on the game surface, sending a split command to the server.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnTap(object sender, EventArgs e) 
        {
            //_client.SendAsync(String.Format(Protocols.CMD_Split, this.X, this.Y));
        }

        /// <summary>
        /// Handles pan gesture events for adjusting the zoom scale of the game surface.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments containing pan gesture information.</param>
        private void PanUpdated(object sender, PanUpdatedEventArgs e) 
        {
            if (e.StatusType == GestureStatus.Running)
            {
                double? totalX = e.TotalX;

                _zoomScale += (float)(totalX * 0.009); 

                if (_zoomScale < 10)
                {
                    _zoomScale = 10;
                }
                if (_zoomScale > 250) 
                {
                    _zoomScale = 250;
                }
                worldDrawable?.UpdateZoomScale(_zoomScale);
            }
        }

        /// <summary>
        /// Event handler for the Start Game button click, initiates game setup and connection.
        /// </summary>
        /// <param name="sender">The object that raised the event (the Start Game button).</param>
        /// <param name="e">The event arguments.</param>
        private async void StartGame_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Warning", "Please enter a name.", "OK");
                    return;
                }
                Spinner.IsVisible = true;
                await _client.ConnectAsync(HostEntry.Text, 11000);
                WelcomeScreen.IsVisible = false;

                Spinner.IsVisible = false;
                GameScreen.IsVisible = true;

                await _client.HandleIncomingDataAsync(true);
                
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Warning", "Error Connecting, please try again", "OK");
            }
        }


        /// <summary>
        /// Event handler for incoming messages from the server, processes and updates game state accordingly.
        /// </summary>
        /// <param name="channel">The networking channel used for communication.</param>
        /// <param name="message">The message received from the server.</param>
        private async void OnMessage(Networking channel, string message)
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
                    _world.MarkPlayersAsDead(message);
                    if (_world.Players[_world.clientID].isDead)
                    {
                        ShowDeadAlert();
                    }                   
                }else if (message.StartsWith(Protocols.CMD_HeartBeat))
                {
                    message = message[Protocols.CMD_HeartBeat.Length..];
                    Dispatcher.Dispatch(() =>
                    {
                        hb.Text = "Heartbeat: " + message;
                    });                
                }
                Dispatcher.Dispatch(() =>
                {
                    Mass.Text = "Mass: " + _world.Players[_world.clientID].Mass;
                });
            }
            catch (Exception ex) 
            { 
                _client.Disconnect();
                _logger?.LogDebug(ex.Message, ex);
            }            
        }

        /// <summary>
        /// Event handler for when the client disconnects from the server.
        /// </summary>
        /// <param name="channel">The networking channel used for communication.</param>

        private async void OnDisconnect(Networking channel)
        {
            string title = "Disconnect from server";
            string message = "Press OK to go back to main screen";

            await DisplayAlert(title, message, "OK");
           
            WelcomeScreen.IsVisible = true;
            GameScreen.IsVisible = false;
        }

        /// <summary>
        /// Event handler for when the client connects to the server.
        /// </summary>
        /// <param name="channel">The networking channel used for communication.</param>

        private void OnConnect(Networking channel)
        {
            string name = NameEntry.Text;
            _client?.SendAsync(String.Format(Protocols.CMD_Start_Game, name));
        }

        private async void ShowDeadAlert()
        {
            string title = "GAME OVER";
            string message = "Your mass: " + _world.Players[_world.clientID].Mass + "\n" + "Play again?";

            bool result = await DisplayAlert(title, message, "Play again", "Quit");

            if (result)
            {
                string name = NameEntry.Text;
                _client?.SendAsync(String.Format(Protocols.CMD_Start_Game, name));
            }
            else
            {
                _client.Disconnect();
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            _client.SendAsync(String.Format(Protocols.CMD_Split, X, Y));
        }
    }

}
