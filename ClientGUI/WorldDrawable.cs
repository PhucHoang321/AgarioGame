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
            

            playerMass = (int)Math.Sqrt(worldMass/Math.PI);   
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Green;
            canvas.FillRectangle(dirtyRect);
            
            foreach (var player in _players) {
                //ConvertFromWorldToScreen(player.Value.X, player.Value.Y, player.Value.Mass,
                //       out int screen_x, out int screen_y,
                //       out int playerMass);


                canvas.StrokeColor = Colors.Black;
                canvas.DrawCircle(player.Value.X, player.Value.Y, player.Value.Radius);

                // Draw player is color
                int argbColor = player.Value.ARGBColor;
                Color fillColor = ConvertArgbToColor(argbColor);
                canvas.FillColor = fillColor;

                // Draw player is circle
                canvas.FillCircle(player.Value.X, player.Value.Y, player.Value.Radius);
                
                // Draw player is name
                canvas.DrawString(player.Value.Name, player.Value.X, player.Value.Y, HorizontalAlignment.Center);
            }

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
            // Extract individual color components from the ARGB integer
            int alpha = (argbColor >> 24) & 0xFF; 
            int red = (argbColor >> 16) & 0xFF;   
            int green = (argbColor >> 8) & 0xFF;  
            int blue = argbColor & 0xFF;

            string hexColor = (argbColor & 0xFFFFFF).ToString("X6");
            // Create a Color object using the extracted color components
            // Color color = Color.FromRgba(red, green, blue, alpha);
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
