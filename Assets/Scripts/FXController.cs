using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    GameObject fxGo;
    public float fxAliveTime = 0f;
    void FixedUpdate()
    {
        fxAliveTime += Time.deltaTime;
        if(fxAliveTime >= 0.5f)
        {
            Destroy(fxGo);
            Destroy(gameObject);
        }
    }
    public void MakeFx(Vector3 pos)
    {
        fxGo = (GameObject)Instantiate(this.gameObject,pos,Quaternion.Euler(0,0,0)) ;
    }
}
