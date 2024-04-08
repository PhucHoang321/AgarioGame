using System.Numerics;

namespace TowardAgarioStepOne
{

    internal class WorldModel
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Radius { get; private set; }
        public Vector2 Direction { get;  set; }
        public float WindowWidth { get;  set; }
        public float WindowHeight { get;  set; }
        public WorldModel(float windowWidth, float windowHeight) {
            X = 100;
            Y = 100;
            Radius = 50;
            Direction = new Vector2(50, 25);
            WindowWidth = windowWidth;
            WindowHeight = windowHeight; AdvanceGameOneStep();
        }

       

        public void AdvanceGameOneStep()
        {
            X += Direction.X;
            Y += Direction.Y;
            if(X - Radius  < 0 || X + Radius > WindowWidth)
            {
                Direction = new Vector2(-Direction.X, Direction.Y);
            }else if(Y - Radius < 0 || Y + Radius > WindowHeight)
            {
                Direction = new Vector2(Direction.X, -Direction.Y);
            }
        }
    }
}
