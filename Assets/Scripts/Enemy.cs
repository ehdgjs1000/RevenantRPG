using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //몬스터 정보
    public float speed;
    public float enemyDmg;
    public float enemyHp;
    public float enemyExp;
    private float startHp;

    //public GameObject[] targetSets;
    private GameObject nowCharacter;
    public GameObject hpGage;

    private bool canAttack = true;
    private bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;
    public EnemyScan scan;

    public AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scan = GetComponent<EnemyScan>();

        wait = new WaitForFixedUpdate(); 
        startHp = enemyHp;
    }
    private void FixedUpdate()
    {
        if (!canAttack)
            return;
        MoveToTarget();
        CheckGetAttack();

        if (!this.scan.nearestTarget)
            return;

        if(enemyHp <=0)
        {
            EnemyDie();
        }
        
    }
    private void LateUpdate()
    {
        if (scan.nearestTarget != null)
        {
            if (scan.nearestTarget.position.x > this.transform.position.x)
            {
                this.transform.localScale = new Vector3(-2f, 2f, 2f);
                if (this.transform.tag == "Boss")
                {
                    this.transform.localScale = new Vector3(-5f, 5f,2f);
                }
            }
            else
            {
                this.transform.localScale = new Vector3(2f, 2f, 2f);
                if (this.transform.tag == "Boss")
                {
                    this.transform.localScale = new Vector3(5f, 5f, 2f);
                }
            }
        }
        
    }
    private void MoveToTarget()
    {
        if(scan.nearestTarget != null)
        {
            //캐릭터 쪽으로 이동
            Vector2 dirVec = new Vector2(scan.nearestTarget.position.x,
                scan.nearestTarget.position.y) - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }
        
    }
    private void OnCollisionStay2D(Collision2D co)
    {
        if (co.transform.tag == "Player" && co.transform.name != "Archer")
        {
            if (!CharacterController.canAttack && canAttack)
            {
                StartCoroutine(KnockBack());

                //크리티컬 확률이랑 dmg 넣기
                GetAttack(CharacterStats.power, CharacterStats.critical, CharacterStats.criticalDmg);
                CharacterStats.HP -= enemyDmg;
                canAttack = false;
                Invoke("CanAttack", 0.5f);
            }
        }
    }

    public FXController dieFX;
    public DmgTxt dmgTxt;
    public void EnemyDie()
    {
        dieFX.MakeFx(transform.position); 

        InGame.exp += enemyExp;
        InGame.enemyCount--;
        this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = scan.nearestTarget.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse);
    }
    public void GetAttack(float dmg, float cri, float criD)
    {
        float criChance = Random.Range(1, 101);
        bool isCri = false; 
        if(criChance <= cri)
        {
            Debug.Log(criChance + " : " + cri + " : " +criD);
            isCri = true;
            dmg = dmg * criD / 100;
        }

        audioSource.clip = clips[0];
        audioSource.Play();

        Vector3 pos = transform.position;
        DmgTextController.Instance.CreateDmgTxt(pos,dmg, isCri);

        enemyHp -= dmg;
        hpGage.transform.localScale = new Vector3((enemyHp / startHp) * 4, 3, 1);
        if (enemyHp <= 0)
        {
            EnemyDie();
        }
    }
    private void CheckGetAttack()
    {
        hpGage.transform.localScale = new Vector3((enemyHp / startHp) * 4, 3, 1);
    }
    private void CanAttack()
    {
        canAttack = true;
    }



}
