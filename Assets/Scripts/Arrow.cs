using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowDmg;
    public float arrowSpeed;
    public Vector2 arrDir;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("DestroyArr",1.5f);
    }

    public void Init(float dmg, Vector3 dir)
    {
        rigid = GetComponent<Rigidbody2D>();
        this.arrowDmg = dmg;
        rigid.velocity = dir * 15f;
        arrDir.x = dir.x;
        arrDir.y = dir.y;
    }
    public void FixedUpdate()
    {
        rigid.AddForce(new Vector2(arrDir.x, arrDir.y).normalized * arrowSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (!(co.CompareTag("Enemy")||co.CompareTag("Boss")))
            return;


        Enemy enemy = co.GetComponent<Enemy>();
        DmgTextController.Instance.CreateDmgTxt(transform.position, CharacterStats.power, false);
        enemy.enemyHp -= arrowDmg;

        Destroy(gameObject);
    }
    private void DestroyArr()
    {
        Destroy(gameObject);
    }
}
