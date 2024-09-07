using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class abuttonpresser : MonoBehaviour
{
    public UnityEvent OnButtonClicked;
    void Update()
    {
        // A tuþuna basýldýðýnda ve henüz spawn edilmediyse
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            OnButtonClicked.Invoke();
        }
    }
}

