using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyUtility
{
    [RequireComponent(typeof(Button))]
    public class UnableButtonDuration : MonoBehaviour
    {
        [SerializeField]
        float duration = 1f;
        Button button;
        private void Start()
        {
            if (button)
            {
                button.onClick.AddListener(CountDuration);
            }
        }

        void CountDuration()
        {
            StartCoroutine(_CountDuration());
        }

        IEnumerator _CountDuration()
        {
            button.enabled = false;
            yield return new WaitForSeconds(duration);
            button.enabled = true;
        }

        private void Reset()
        {
            button = GetComponent<Button>();
        }
    }
}