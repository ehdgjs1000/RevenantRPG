using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTxt : MonoBehaviour
{
    public float desTime = 0f;
    private void FixedUpdate()
    {
        desTime += Time.deltaTime;
        if (desTime >= 0.2f) DestroyEvent();
    }
    public void DestroyEvent()
    {
        Destroy(gameObject);
    } 
   
}
