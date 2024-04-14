using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgarioModels;

namespace ClientGUI
{
    public class WorldDrawable : IDrawable
    {
     
        private readonly World _world;
        private readonly List<Player> players = [];
        public WorldDrawable(World world) 
        { 
            this._world = world;
        }
        private void ConvertFromWorldToScreen(
                in float worldX, in float worldY, in float worldMass,
                out int screenX, out int screenY, out int playerMass)
        {
            screenX = (int)(worldX / 3000.0F * _world.Width);
            screenY = (int)(worldY / 2000.0F * _world.Height);   // fill this in
            playerMass = (int)(worldMass / 3000.0f * _world.Width);   // and this
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Green;
            canvas.FillRectangle(dirtyRect);
         
            foreach (var player in players) {
                ConvertFromWorldToScreen(player.X, player.Y, player.Mass,
                       out int screen_x, out int screen_y,
                       out int playerMass);

                canvas.StrokeColor = Colors.Black;
                canvas.DrawCircle(screen_x, screen_y, playerMass);
                canvas.FillCircle(screen_x, screen_y, playerMass);
            }
            
        }

    }
}
