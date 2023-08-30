using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    WaitForFixedUpdate wait;
    Rigidbody2D rigid;

    public void Awake()
    {
        wait = new WaitForFixedUpdate();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D co)
    {
        /*if(co.transform.tag == "Player")
        {
            Vector3 playerPos = co.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            CharacterStats.HP -= 5;
        }*/
    }

}
