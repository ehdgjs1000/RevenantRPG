using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int areaLevel;
    public int finishGold;

    public PolygonCollider2D PC;
    public static Vector3 randColPos;

    public void Awake()
    {
        finishGold = Random.RandomRange(finishGold/2, finishGold);
        PC = GetComponent<PolygonCollider2D>();
    }

    public Vector2 RandColPos()
    {
        return PC.transform.position;
    }
    private void FixedUpdate()
    {
        // charactercontroller.isfight == true 일때 door 만든후 setactive true하기
        Vector2[] vPoints = PC.GetPath(0);
        randColPos = PC.transform.position;

    }
}
