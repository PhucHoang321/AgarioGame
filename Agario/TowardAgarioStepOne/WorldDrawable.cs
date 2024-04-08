namespace TowardAgarioStepOne
{
    internal class WorldDrawable : IDrawable
    {
        private readonly WorldModel wm;
      

        public WorldDrawable(WorldModel model)
        {
             this.wm = model;  
          
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Aqua;
            canvas.FillRectangle(dirtyRect);           
            canvas.FillColor = Colors.Red;
            canvas.StrokeColor = Colors.Green;
            canvas.FillCircle(wm.X, wm.Y, wm.Radius);
            canvas.DrawCircle(wm.X, wm.Y, wm.Radius);
        }
    }
}
