using UnityEngine;
using System.Runtime.InteropServices;

namespace MyUtility
{
    public class iOSUtili : MonoBehaviour
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern void _playSystemSound(int n);
#endif

        public static void PlaySystemSound(int n) //引数にIDを渡す
        {
#if !UNITY_EDITOR && UNITY_IOS
        _playSystemSound(n);
#endif
        }
    }
}
