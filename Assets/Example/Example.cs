using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoNo.Utility;


public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AppTrackingTransparencyCheck att = new AppTrackingTransparencyCheck();
        StartCoroutine(att.Check());
    }

 
}
