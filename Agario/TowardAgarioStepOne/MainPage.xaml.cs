
using System.Diagnostics;
using System.Numerics;

namespace TowardAgarioStepOne
{
    public partial class MainPage : ContentPage
    {

        private bool initialized;
        WorldModel wm;
        private System.Timers.Timer Timer;
        private object key = new object();
        public float Radius { get; set; }
        public MainPage()
        {
            initialized = false;
            wm = new WorldModel(800, 800);
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
            WorldDrawable worldDrawable = new WorldDrawable(wm);
            PlaySurface.Drawable = worldDrawable;
            Timer = new System.Timers.Timer(1000 / 60);
            Timer.Elapsed += (s, e) => GameStep();
            Timer.Start();
        }

        private void GameStep()
        {

            Dispatcher.Dispatch(() =>
            {
                wm.AdvanceGameOneStep();
                UpdateLabels();
                PlaySurface.Invalidate();
            });

        }

        private void UpdateLabels()
        {
            Dispatcher.Dispatch(() =>
            {

                circlecenterLabel.Text = $"Circle Center: ({wm.X}, {wm.Y})";
                directionLabel.Text = $"Direction: ({wm.Direction.X}, {wm.Direction.Y})";


            });
        }
    }

}

