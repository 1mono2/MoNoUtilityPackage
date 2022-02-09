using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtility
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {

            DontDestroyOnLoad(gameObject);
        }
    }
}