using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class CameraUtil : MonoBehaviour
    {
        public Camera cam;
        public float baseWidth = 9.0f;
        public float baseHeight = 16.0f;

        void Awake()
        {
            // 幅固定+高さ可変
            //var scaleWidth = (Screen.height / this.baseHeight) * (this.baseWidth / Screen.width);
            //this.camera.fieldOfView = Mathf.Atan(Mathf.Tan(this.camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * scaleWidth) * 2.0f * Mathf.Rad2Deg;
            // ベース維持
            var scaleWidth = (Screen.height / this.baseHeight) * (this.baseWidth / Screen.width);
            var scaleRatio = Mathf.Max(scaleWidth, 1.0f);
            this.cam.fieldOfView = Mathf.Atan(Mathf.Tan(this.cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * scaleRatio) * 2.0f * Mathf.Rad2Deg;
        }
    }
}
