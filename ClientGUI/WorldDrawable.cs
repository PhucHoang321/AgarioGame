using AgarioModels;
/// <summary>
/// Author:    Phuc Hoang
/// Partner:   Chanphone Visathip
/// Date:      7-April-2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Phuc Hoang - This work may not 
///            be copied for use in Academic Coursework.
///
/// I, Phuc Hoang, and Chanphone Visathip certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source. All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents:
/// This file contains the definition of the WorldDrawable class, which is responsible for rendering the game world in the GUI.
/// It draws players and food items on the screen based on the provided world data and zoom scale.
/// </summary>
namespace ClientGUI
{   
    public class WorldDrawable : IDrawable
    {
        /// <summary>
        /// The World object for drawing
        /// </summary>
        private readonly World _world;
        /// <summary>
        /// Dictionary of all players
        /// </summary>
        private readonly Dictionary<long, Player> _players;
        /// <summary>
        /// Dictionary of all foods
        /// </summary>
        private readonly Dictionary<long, Food>? _foods;
        /// <summary>
        /// The graphics view representing the game screen.
        /// </summary>
        private readonly GraphicsView PlaySurface;
        /// <summary>
        /// The zoom scale used for rendering on the screen.
        /// </summary>
        private float _zoomScale = 40;

        /// <summary>
        /// Initializes a new instance of the WorldDrawable class with the specified World and GraphicsView.
        /// </summary>
        /// <param name="world">The World object containing game data.</param>
        /// <param name="gv">The GraphicsView used for rendering.</param>
        public WorldDrawable(World world, GraphicsView gv)
        {
            _world = world;
            _players = world.Players;
            _foods = world.Foods;
            PlaySurface = gv;
            _zoomScale = 40;
        }

        /// <summary>
        /// Updates the zoom scale used for rendering.
        /// </summary>
        /// <param name="zoomScale">The new zoom scale value to be applied.</param>
        public void UpdateZoomScale(float zoomScale)
        {
            _zoomScale = zoomScale;
        }

        /// <summary>
        /// Draws every game object on the canvas, including the player and food items,
        /// within the visible viewport defined by the specified dirty rectangle.
        /// </summary>
        /// <param name="canvas">The canvas used for drawing.</param>
        /// <param name="dirtyRect">The rectangle defining the visible viewport.</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {

            float screenW = (float)PlaySurface.Width;
            float screenH = (float)PlaySurface.Height;

            canvas.FillColor = Colors.White;
            canvas.StrokeColor = Colors.Black;
            canvas.FillRectangle(dirtyRect);
            canvas.StrokeSize = 3;
         
            Player client = _world.Players[_world.clientID];
            float viewPortWidth = client.Radius * _zoomScale;

            BoundedPoint(client, viewPortWidth, out float leftBound, out float rightBound, out float topBound, out float bottomBound);

            lock (_players)
            {
                foreach (var player in _players)
                {
                    if (!player.Value.isDead)
                    {
                        float playerX = player.Value.Location.X;
                        float playerY = player.Value.Location.Y;
                        float playerRadius = player.Value.Radius;

                        if (player.Value.X > leftBound
                            && player.Value.X < rightBound
                            && player.Value.Y < bottomBound
                            && player.Value.Y > topBound)
                        {
                            float xOffset = playerX - leftBound;
                            float yOffset = playerY - topBound;
                            float xRatio = xOffset / viewPortWidth;
                            float yRatio = yOffset / viewPortWidth;

                            canvas.FillColor = Color.FromInt(player.Value.ARGBColor);
                            canvas.StrokeColor = Colors.Black;
                            canvas.DrawCircle(xRatio * screenW, yRatio * screenH, playerRadius * screenW / viewPortWidth);
                            canvas.FillCircle(xRatio * screenW, yRatio * screenH, playerRadius * screenW / viewPortWidth);
                            canvas.FontColor = Colors.Black;
                            canvas.DrawString(player.Value.Name, xRatio * screenW, yRatio * screenH, HorizontalAlignment.Center);

                        }
                    }
                }
            }       
            if (_foods is not null)
            {
                lock (_foods)
                {
                    foreach (var food in _foods)
                    {
                        float foodX = food.Value.Location.X;
                        float foodY = food.Value.Location.Y;
                        float foodRadius = food.Value.Radius;
                        if (food.Value.X > leftBound
                           && food.Value.X < rightBound
                           && food.Value.Y < bottomBound
                           && food.Value.Y > topBound)
                        {
                            float xOffset = foodX - leftBound;
                            float yOffset = foodY - topBound;
                            float xRatio = xOffset / viewPortWidth;
                            float yRatio = yOffset / viewPortWidth;

                            canvas.FillColor = Color.FromInt(food.Value.ARGBColor);
                            canvas.FillCircle(xRatio * screenW, yRatio * screenH, foodRadius * screenW / viewPortWidth);
                            canvas.StrokeColor = Colors.Black;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the bounded coordinates defining the visible viewport around the player.
        /// </summary>
        /// <param name="client">The player object representing the center of the viewport.</param>
        /// <param name="viewPortWidth">The width of the viewport.</param>
        /// <param name="leftBound">Output parameter for the left boundary of the viewport.</param>
        /// <param name="rightBound">Output parameter for the right boundary of the viewport.</param>
        /// <param name="topBound">Output parameter for the top boundary of the viewport.</param>
        /// <param name="bottomBound">Output parameter for the bottom boundary of the viewport.</param>
        private static void BoundedPoint(Player client, float viewPortWidth, out float leftBound, out float rightBound, out float topBound, out float bottomBound)
        {
            leftBound = client.X - viewPortWidth / 2;
            rightBound = client.X + viewPortWidth / 2;
            topBound = client.Y - viewPortWidth / 2;
            bottomBound = client.Y + viewPortWidth / 2;
        }
    }
}
