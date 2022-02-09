using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtility
{
    public class SetActiveFalse : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.SetActive(false);
        }

    }
}
