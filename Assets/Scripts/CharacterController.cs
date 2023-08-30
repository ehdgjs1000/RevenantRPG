using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 5;
    public GameObject arrow;

    public static bool canAttack = true;
    public static bool isFight = false;

    public float arrowSpeed;
    float timer;

    //Enemy 스폰 확률
    public static float spawnRate;
    public float spawnTimeCheck;
    private int spwanSpeed = 5;

    public EnemyScan scan;
    Rigidbody2D rigid;
    Animator anim;

    private float tempHP;
    private float nowHP;

    public AudioSource audioSource;
    [SerializeField] private AudioClip[] clips; //캐릭터 별 공격 Sound 넣기
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        scan = GetComponent<EnemyScan>();
    }
    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        if(inputVec.x < 0)
        {
            transform.localScale = new Vector3(2,2,1);
        }
        else if (inputVec.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        if((inputVec.x != 0 || inputVec.y != 0) && !isFight)
        {
            spawnRate += Time.deltaTime * spwanSpeed;
            spawnTimeCheck += Time.deltaTime;
            if (spawnTimeCheck >= 1)
            {
                spawnTimeCheck = 0;
                TrySpawn();
            }
        }

        if (spawnRate >= 100) spawnRate = 100;
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

   
    public void TrySpawn()
    {
        float rate = spawnRate * spawnRate;
        float randSpawnRate = Random.Range(0.0f,10000.0f);

        if (randSpawnRate <= rate)
        {
            //몬스터 스폰하기
            Debug.Log("몬스터 스폰");

            InGame ig = GameObject.Find("GameManager").GetComponent<InGame>();
            ig.SpawnEnemy();

            isFight = true; // 싸움 끝나면 isFight = false로 만들기
            spawnTimeCheck = 0;
            spawnRate = 0;
        }
    }
    
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if(canAttack == true)
            {
                canAttack = false;
                if (this.gameObject.name == "Archer") //궁수
                {
                    if(timer > arrowSpeed)
                    {
                        //speed = 0;
                        timer = 0f;
                        anim.SetTrigger("Attack");
                        Invoke("Fire",0.4f);
                    }
                }
                else //다른 직업 공격
                {
                    anim.SetTrigger("Attack");
                }
                //Character 별 공격 Sound 넣기
                //audioSource.clip = clips[PlayerPrefs.GetInt("CharacterNum")];
            }
            Invoke("AttacStateChange",0.8f);
        }

        Vector2 nextVector = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        anim.SetFloat("Speed", inputVec.magnitude);
    }
    
    private void AttacStateChange()
    {
        canAttack=true;
    }

    void Fire()
    {
        if (!this.scan.nearestTarget)
            return;

        Vector3 targetPos = this.scan.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        Transform arrowPrefab = arrow.transform;
        arrowPrefab.position = this.transform.position;
        arrowPrefab.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        arrowPrefab.GetComponent<Arrow>().Init(CharacterStats.power,dir);
        Instantiate(arrowPrefab);
    }

    public GameObject[] enemySets;
    public static Vector3 colPos;
    public Text levelTxt;
    void OnTriggerEnter2D(Collider2D co)
    {
        if(co.tag == "Area")
        {
            MapArea mapArea = co.GetComponent<MapArea>();
            levelTxt.text = ("Level : " + mapArea.areaLevel.ToString());
            Debug.Log(levelTxt.text);
            colPos = mapArea.transform.position;
            this.enemySets = mapArea.enemyPrefabs;
        }
    }
    void OnCollisionEnter2D(Collision2D co)
    {
        if (co.transform.tag == "Trap")
        {
            Vector3 dirVec = this.transform.position - co.transform.position;
            CharacterStats.HP -= 5f;
        }
    }


}
