
using AgarioModels;

namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {
        
        private readonly World _world;
        private readonly Dictionary<long, Player> _players;
        public WorldDrawable(World world) 
        { 
            _world = world;
            _players = world.Players;
        }
        private void ConvertFromWorldToScreen(
                in float worldX, in float worldY, in float worldMass,
                out int screenX, out int screenY, out int playerMass)
        {
            screenX = (int)( 500 * worldX / _world.Width);
            screenY = (int)( 500 * worldY / _world.Height);   // fill this in
            playerMass = (int)(50 * worldMass / _world.Width);   // and this
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.White;
            canvas.FillRectangle(dirtyRect);
            
            foreach (var player in _players) {
                ConvertFromWorldToScreen(player.Value.X, player.Value.Y, player.Value.Mass,
                       out int screen_x, out int screen_y,
                       out int playerMass);

                canvas.StrokeColor = Colors.Black;
                canvas.DrawCircle(screen_x, screen_y, playerMass);
                canvas.FillCircle(screen_x, screen_y, playerMass);
            }
            
        }

    }
}
