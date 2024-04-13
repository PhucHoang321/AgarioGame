using AgarioModels;
using Communications;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        private Networking _client;
        private bool initialized;
        World world;
        private WorldDrawable worldDrawable;
        private System.Timers.Timer Timer;
        private readonly ILogger _logger;
        public MainPage(ILogger<MainPage> logger)
        {
            world = new World();
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
            worldDrawable = new WorldDrawable(world);
            PlaySurface.Drawable = worldDrawable;
            Timer = new System.Timers.Timer(1000 / 60);
            Timer.Elapsed += (s, e) => GameStep();
            Timer.Start();
        }

        private void GameStep()
        {
            
        }

        private void StartGame_Clicked(object sender, EventArgs e)
        {
            _client.ConnectAsync("192.168.50.201", 11000);

            // Hide the welcome screen
            WelcomeScreen.IsVisible = false;

            // Show the game screen
            GameScreen.IsVisible = true;
        }

        private void OnMessage(Networking channel, string message)
        {
            
        }

        private void OnDisconnect(Networking channel)
        {
        
        }

        private void OnConnect(Networking channel)
        {
            // Send the player name to server
            string name = NameEntry.Text;
            _client.SendAsync(String.Format(Protocols.CMD_Start_Game, name));
        }
    }

}
