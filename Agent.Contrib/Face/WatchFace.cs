using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.Contrib.Face
{
    public class WatchFace : IWatchFace
    {
        public IFace Face { get; set; }
        public Bitmap Screen { get; set; }

        public void Render()
        {

            //clear the display
            Screen.Clear();


            //call the user code
            Face.Render(Screen);


            //flush the image out to the device
            Screen.Flush();


        }
        public WatchFace(IFace face, Bitmap screen = null)
        {            
            Face = face;
            if(screen == null) screen = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
            Screen = screen;
        }

        public void Start()
        {
            if (Face == null) throw new ArgumentNullException("Face cannot be null");

            // Included font is used in the clock

            var timer = new Timer(state =>
            {

               Render();

            }, null, 1, Face.UpdateSpeed);
            
            Thread.Sleep(Timeout.Infinite);
        }


    }
}
