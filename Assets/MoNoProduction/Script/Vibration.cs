using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MoNo.Utility
{
    public static class Vibration
    {
        public enum Length
        {
            Short,
            //Midium,
            //Long,
        }

        public static void Vibrate(Length length)
        {

            switch (length)
            {
                case Length.Short:
#if UNITY_ANDROID && !UNITY_EDITOR
                    AndroidVibration.Vibrate(35);
#elif !UNITY_EDITOR && UNITY_IOS
                    IOSVibration.PlaySystemSound(1519);
#endif

                    break;
                    //case Length.Midium:
                    //    AndroidUtili.Vibrate(2000);
                    //    iOSUtili.PlaySystemSound(1000);
                    //    break;
                    //case Length.Long:
                    //    break;
            }

        }
    }
}