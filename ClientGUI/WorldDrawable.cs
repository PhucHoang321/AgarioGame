using AgarioModels;
using System.Text;
namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {
        
        private readonly World _world;
        private readonly Dictionary<long, Player> _players;
        private readonly Dictionary<long, Food> _foods;
        private readonly GraphicsView PlaySurface;
    

        private float _viewportWidth = 500; // Fixed viewport width
        private float _viewportHeight = 500; // Fixed viewport height
        private float _playerX = 250; // Initial player X position (center of viewport)
        private float _playerY = 250; // Initial player Y position (center of viewport)

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
            screenX = (int)( 500 * worldX / _world.Width);
            screenY = (int)( 500 * worldY / _world.Height);             
            playerMass = (int)(Math.Sqrt(worldMass/Math.PI) * 500) / _world.Width;   
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Green;
            canvas.FillRectangle(dirtyRect);


            // Clear canvas with a background color (e.g., white)
            canvas.FillColor = Colors.White;
            canvas.FillRectangle(dirtyRect);

            // Calculate the world coordinates visible within the viewport
            float worldX = _playerX - _viewportWidth / 2;
            float worldY = _playerY - _viewportHeight / 2;

            

            // Draw the "portal" border around the viewport
            canvas.StrokeColor = Colors.Black;
            canvas.DrawRectangle(0, 0, _viewportWidth, _viewportHeight);

             


            //draw player 
            foreach (var player in _players) {
                ConvertFromWorldToScreen(player.Value.X, player.Value.Y, player.Value.Mass,
                       out int screenX, out int screenY,
                       out int playerMass);


                canvas.StrokeColor = Colors.Black;

                canvas.DrawCircle(screenX, screenY, playerMass);

               // canvas.DrawCircle(player.Value.X, player.Value.Y, player.Value.Radius);

                // Draw player is color
                int argbColor = player.Value.ARGBColor;
                Color fillColor = ConvertArgbToColor(argbColor);
                canvas.FillColor = fillColor;

                // Draw player is circle
                canvas.FillCircle(screenX, screenY, playerMass);
                
                // Draw player is name
                canvas.DrawString(player.Value.Name, screenX,screenY, HorizontalAlignment.Center);
            }

            //draw food
            foreach (var food in _foods) {
                //ConvertFromWorldToScreen(food.Value.X, food.Value.Y, food.Value.Mass,
                //      out int screen_x, out int screen_y,
                //      out int foodMass);
                canvas.StrokeColor = Colors.Black;
                canvas.DrawCircle(food.Value.X, food.Value.Y, food.Value.Radius);
                int argbColor = food.Value.ARGBColor;
                Color fillColor = ConvertArgbToColor(argbColor);
                canvas.FillColor = fillColor;
                canvas.FillCircle(food.Value.X, food.Value.Y , food.Value.Radius);
            }
        }



        // Private method to convert into color
        private static Color ConvertArgbToColor(int argbColor)
        {
            string hexColor = (argbColor & 0xFFFFFF).ToString("X6");            
            Color color = Color.FromArgb(hexColor);
            return color;
        }

     

      

        public void PointerChanged(object sender,PointerEventArgs e)
        {
            Point? pointerPosition = e.GetPosition(PlaySurface);
            float mouseX = (float)pointerPosition.Value.X;
            float mouseY = (float)pointerPosition.Value.Y;

            //TODO MOVE CLIENT TO THE MOUSE POINTER

        }
    }
}
