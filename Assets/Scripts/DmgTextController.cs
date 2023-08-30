using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgTextController : MonoBehaviour
{
    private static DmgTextController instance = null;

    public static DmgTextController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<DmgTextController>();
                if(instance == null)
                {
                    Debug.Log("DmgTextController ¾øÀ½!!");
                }
            }
            return instance;
        }
    }
    public Canvas canvas;
    public GameObject dmgTxt;

    public void CreateDmgTxt(Vector3 pos, float dmg, bool isCri)
    {
        GameObject damageTxt = Instantiate(dmgTxt, pos, Quaternion.identity, canvas.transform);
        damageTxt.GetComponent<Text>().text = dmg.ToString();
        if(isCri) damageTxt.GetComponent<Text>().color = Color.red;
    }

}
