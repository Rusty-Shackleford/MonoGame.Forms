using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Forms.Controls.Scrollers
{
    public class ScrollArgs
    {

        #region [ Constructor ]
        public ScrollArgs(float oldDistance, float newDistance, float scrollHeight)
        {
            OldDistance = oldDistance;
            NewDistance = newDistance;
            ScrollHeight = scrollHeight;
        }
        #endregion


        #region [ Members ]
        public float OldDistance { get; set; }
        public float NewDistance { get; set; }
        public float DistanceReverse => ScrollHeight - NewDistance;
        public float ScrollHeight { get; set; }

        public float CurrentPctLocation => NewDistance / ScrollHeight;
        public float DistanceChangedPixels => NewDistance - OldDistance;
        public float DistanceChangedPct => DistanceChangedPixels / ScrollHeight;
        #endregion
    }
}
