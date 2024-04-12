using Communications;
using Microsoft.Extensions.Logging;


namespace ClientGUI
{
    public partial class MainPage : ContentPage
    {
        private Networking _client;
        private readonly ILogger _logger;
        public MainPage(ILogger<MainPage> logger)
        {
            _logger = logger;
            _client = new Networking(logger, OnConnect, OnDisconnect, OnMessage);
            InitializeComponent();
        }
        private void StartGame_Clicked(object sender, EventArgs e)
        {
            _client.ConnectAsync("2620:9b::190a:2685", 11000);

            // Hide the welcome screen
            WelcomeScreen.IsVisible = false;

            // Show the game screen
            GameScreen.IsVisible = true;
        }

        private void OnMessage(Networking channel, string message)
        {
            throw new NotImplementedException();
        }

        private void OnDisconnect(Networking channel)
        {
            throw new NotImplementedException();
        }

        private void OnConnect(Networking channel)
        {
            throw new NotImplementedException();
        }
    }

}
