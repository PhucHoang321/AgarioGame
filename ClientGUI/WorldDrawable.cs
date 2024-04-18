using AgarioModels;

using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {

        private readonly World _world;
        private readonly Dictionary<long, Player> _players;
        private readonly Dictionary<long, Food> _foods;
        private readonly GraphicsView PlaySurface;
    

        public float _zoomScale = 40;

        public WorldDrawable(World world, GraphicsView gv)
        {
            _world = world;
            _players = world.Players;
            _foods = world.Foods;
            PlaySurface = gv;
        }
        private void ConvertFromWorldToScreen(
                in float worldX, in float worldY, in float worldMass,
                out int screenX, out int screenY, out int playerMass)
        {
            screenX = (int)(500 * worldX / _world.Width);
            screenY = (int)(500 * worldY / _world.Height);
            playerMass = (int)(Math.Sqrt(worldMass / Math.PI) * 500) / _world.Width;
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float screenW = (float)PlaySurface.Width;
            float screenH = (float)PlaySurface.Height;
            canvas.FillColor = Colors.Green;
            canvas.FillRectangle(dirtyRect);

            canvas.FillColor = Colors.White;
            canvas.FillRectangle(dirtyRect);

            Player client = _world.Players[_world.clientID];
            float viewPortWidth = client.Radius * _zoomScale;

            BoundedPoint(client, viewPortWidth, out float leftBound, out float rightBound, out float topBound, out float bottomBound);



            //draw player 
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
                        canvas.FillCircle(xRatio * screenW, yRatio * screenH, playerRadius * screenW / viewPortWidth);
                        canvas.FontColor = Colors.Black;
                        canvas.DrawString(player.Value.Name, xRatio * screenW, yRatio * screenH, HorizontalAlignment.Center);

                    }
                }                            
            }

            //draw food
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

        private static void BoundedPoint(Player client, float viewPortWidth, out float leftBound, out float rightBound, out float topBound, out float bottomBound)
        {
            leftBound = client.X - viewPortWidth / 2;
            rightBound = client.X + viewPortWidth / 2;
            topBound = client.Y - viewPortWidth / 2;
            bottomBound = client.Y + viewPortWidth / 2;
        }
    }
}
