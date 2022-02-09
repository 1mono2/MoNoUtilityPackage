using UnityEngine;


namespace MyUtility
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteAlways]
    public class SafeAreaPadding : MonoBehaviour
    {
        private DeviceOrientation postOrientation;

        void Update()
        {
            if (Input.deviceOrientation != DeviceOrientation.Unknown && postOrientation == Input.deviceOrientation)
                return;

            postOrientation = Input.deviceOrientation;

            var rect = GetComponent<RectTransform>();
            var area = Screen.safeArea;
            var resolition = Screen.currentResolution;

            rect.sizeDelta = Vector2.zero;
            rect.anchorMax = new Vector2(area.xMax / resolition.width, area.yMax / resolition.height);
            rect.anchorMin = new Vector2(area.xMin / resolition.width, area.yMin / resolition.height);


        }
    }
}
