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
        // A tu�una bas�ld���nda ve hen�z spawn edilmediyse
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            OnButtonClicked.Invoke();
        }
    }
}

