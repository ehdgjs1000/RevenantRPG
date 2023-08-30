using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public SpawnController spawnController;

    public GameObject[] characterSets;
    public Text characterLevelTxt;
    public Slider characterLevelSlider;
    public Slider spawnRateSlider;

    public Text[] upgradeStatsTxtSet;
    public Text[] statsTxtSet;
    public Text pointTxt;
    public Text levelUpTxt;
    public Text upgradeTxt;
    public Text warningTxt;
    public Text noPointTxt;
    public Text criMaxTxt;

    public Text needExpTxt;
    public Text nowExpTxt;
    public Text enemyAppTxt;

    public static float dmg;

    public float startPower;
    public float startHP;
    public float startLuck;

    private float nowPower;
    public static float nowHP; //���ӿ��� �Ҹ�Ǵ� hp
    private int inGameTurn; //���ӿ��� �Ҹ�Ǵ� ��

    public Canvas upgradeCanvas;
    public Text powerTxt;
    public Text hpTxt;
    public Text luckTxt;
    public Text levelTxt;

    public static float exp = 0;
    public float needExp = 100;
    private int inGameLevel = 1;
    public int gamePoint = 0;
    public Text coinTxt;
    public Text cristalTxt;

    public Text hpGageTxt;
    public Image hpGageImg;
    public Text turnTxt;
    public Text floorTxt;
    private int turnCount = 25;

    //Enemy Spawn ����
    public GameObject[] enemySets;
    CharacterController cc;
    private bool tempFight;

    public GameObject doors;

    void Awake()
    {
        spawnController = GetComponent<SpawnController>();
        cc = characterSets[PlayerPrefs.GetInt("CharacterNum")].GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        this.enemySets = cc.enemySets;

        //# UI ����
        powerTxt.text = startPower.ToString();
        hpTxt.text = startHP.ToString();
        turnTxt.text = turnCount.ToString();
        needExpTxt.text = needExp.ToString();
        nowExpTxt.text = exp.ToString();

        hpGageTxt.text = CharacterStats.HP.ToString();
        hpGageImg.fillAmount = CharacterStats.HP / startHP;

        hpTxt.text = CharacterStats.HP.ToString();

        coinTxt.text = PlayerPrefs.GetInt("Coin").ToString();
        cristalTxt.text = PlayerPrefs.GetInt("Cristal").ToString();

        //# ����ġ ����
        if (needExp <= exp)
        {
            exp = exp - needExp;
            LevelUp();
        }

        characterLevelSlider.value = exp / needExp;
        spawnRateSlider.value =  CharacterController.spawnRate/100;
        spawnCheck();
        if (CharacterController.spawnRate >= 35)
        {
            warningTxt.gameObject.SetActive(true);
        }else warningTxt.gameObject.SetActive(false);

        UpdateStats();
        pointTxt.text = gamePoint.ToString();
        dmg = CharacterStats.power;

        if (!CharacterController.isFight && tempFight)
        {
            EndFight();
        }

        //# ĳ���� ����
        if(CharacterStats.HP <= 0)
        {
            //���� ����
            EndGame();
        }

    }
    private void Start()
    {
        Time.timeScale = 1;

        startPower = PlayerPrefs.GetFloat("Power") * CharacterStats.itemPower/100;
        nowPower = startPower;
        startHP = PlayerPrefs.GetFloat("HP") + CharacterStats.itemHP / 10;
        nowHP = startHP;
        startLuck = PlayerPrefs.GetFloat("Luck");
        characterLevelTxt.text = 1.ToString();
        CharacterStats.power = startPower;
        CharacterStats.HP = startHP;
        CharacterStats.luck = startLuck;

        characterSets[PlayerPrefs.GetInt("CharacterNum")].SetActive(true);
    }

    private int enemyNum;
    public Transform[] enemyPrefab;
    public static int enemyCount;

    public void SpawnEnemy()
    {
        //collider �ȿ� ����ִ��� Ȯ���ϰ� �����ϱ�        
        tempFight = true;
        CharacterController.isFight = true;
        turnCount--;
        doors.SetActive(true);

        enemyAppTxt.gameObject.SetActive(true);
        enemyCount = 5;

        Invoke("randSpawnEnemy",3f);
        
    }
    private void randSpawnEnemy()
    {
        Vector3 polColPos = CharacterController.colPos;
        for (int enemyCount = 0; enemyCount < 5; enemyCount++)
        {
            float ranX = Random.Range(-5f, 5f);
            float ranY = Random.Range(-5f, 5f);
            enemyPrefab[enemyCount] = enemySets[enemyCount].transform;
            enemyPrefab[enemyCount].transform.position = new Vector2(polColPos.x + ranX, polColPos.y + ranY);
            Instantiate(enemyPrefab[enemyCount]);
            enemyPrefab[enemyCount].gameObject.SetActive(true);
        }
        enemyAppTxt.gameObject.SetActive(false);
    }

    private void EndFight()
    {
        Invoke("OpenUpgrade",0.1f);
        tempFight = false;
        doors.SetActive(false);
        //���� ���� �ʾ��� ���
        if (turnCount <= 0)
        {
            EndGame();
        }
        nowHP = startHP;
        CharacterStats.HP = startHP;
    }
    private void spawnCheck()
    {
        float characterSpawnRate = CharacterController.spawnRate;
        float rate = (characterSpawnRate * characterSpawnRate) /10000;
    }
    public void LevelUp()
    {
        needExp = Mathf.FloorToInt(needExp * 1.2f);
        inGameLevel++;
        gamePoint += inGameLevel;
        characterLevelTxt.text = inGameLevel.ToString();

        //������ Text ����
        levelUpTxt.gameObject.SetActive(true);
        Invoke("DeacitveLevelUpTxt",0.5f);

    }
    private void DeactiveUpgradeTxt()
    {
        upgradeTxt.gameObject.SetActive(false);
        noPointTxt.gameObject.SetActive(false);
        criMaxTxt.gameObject.SetActive(false);
    }
    private void DeacitveLevelUpTxt()
    {
        levelUpTxt.gameObject.SetActive(false);
    }
    private void UpdateStats()
    {
        statsTxtSet[0].text = CharacterStats.power.ToString();
        statsTxtSet[1].text = CharacterStats.HP.ToString();
        statsTxtSet[2].text = CharacterStats.luck.ToString();
        statsTxtSet[3].text = CharacterStats.critical.ToString();
        statsTxtSet[4].text = CharacterStats.criticalDmg.ToString();

        upgradeStatsTxtSet[0].text = CharacterStats.power.ToString();
        upgradeStatsTxtSet[1].text = CharacterStats.HP.ToString();
        upgradeStatsTxtSet[2].text = CharacterStats.luck.ToString();
        upgradeStatsTxtSet[3].text = CharacterStats.critical.ToString();
        upgradeStatsTxtSet[4].text = CharacterStats.criticalDmg.ToString();
    }
    public void OpenUpgrade()
    {
        //���� �� ����� �� ����
        upgradeCanvas.gameObject.SetActive(true);
        characterSets[PlayerPrefs.GetInt("CharacterNum")].SetActive(false);
    }
    public void CloseUpgrade()
    {
        if(gamePoint == 0)
        {
            upgradeCanvas.gameObject.SetActive(false);
            characterSets[PlayerPrefs.GetInt("CharacterNum")].SetActive(true);
        }
        else
        {
            upgradeTxt.gameObject.SetActive(true);
            Invoke("DeactiveUpgradeTxt",1.3f);
        }
        
    }
    
    //# ��ȭ ��ư Region
    #region
    public void PowerBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Button1")
        {
            if (gamePoint >= 1)
            {
                if(PlayerPrefs.GetInt("CharacterNum") == 1)
                {
                    CharacterStats.power += 3;
                }else if (PlayerPrefs.GetInt("CharacterNum") == 2)
                {
                    CharacterStats.power += 2;
                }else
                {
                    CharacterStats.power += 1;
                }
                    gamePoint--;
            }else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }else if (EventSystem.current.currentSelectedGameObject.name == "Button10")
        {
            if(gamePoint >= 10)
            {
                if (PlayerPrefs.GetInt("CharacterNum") == 1)
                {
                    CharacterStats.power += 30;
                }
                else if (PlayerPrefs.GetInt("CharacterNum") == 2)
                {
                    CharacterStats.power += 20;
                }
                else
                {
                    CharacterStats.power += 10;
                }
                gamePoint -= 10;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button100")
        {
            if (gamePoint >= 100)
            {
                if (PlayerPrefs.GetInt("CharacterNum") == 1)
                {
                    CharacterStats.power += 300;
                }
                else if (PlayerPrefs.GetInt("CharacterNum") == 2)
                {
                    CharacterStats.power += 200;
                }
                else
                {
                    CharacterStats.power += 100;
                }
                gamePoint -= 100;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ButtonAll")
        {
            if (PlayerPrefs.GetInt("CharacterNum") == 1)
            {
                CharacterStats.power += gamePoint * 3;
            }
            else if (PlayerPrefs.GetInt("CharacterNum") == 2)
            {
                CharacterStats.power += gamePoint * 2;
            }
            else
            {
                CharacterStats.power += gamePoint;
            }
            gamePoint = 0;
        }
    }
    public void HPBtn()
    {
        Debug.Log(PlayerPrefs.GetInt("CharacterNum"));
        if (EventSystem.current.currentSelectedGameObject.name == "Button1")
        {
            if (gamePoint >= 1)
            {
                if (PlayerPrefs.GetInt("CharacterNum") == 0)
                {
                    CharacterStats.HP += 3;
                    startHP += 3;
                }
                else
                {
                    CharacterStats.HP++;
                    startHP++;
                }
                gamePoint--;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Button10")
        {
            if (gamePoint >= 10)
            {
                if (PlayerPrefs.GetInt("CharacterNum") == 0)
                {
                    CharacterStats.HP += 30;
                    startHP += 30;
                }
                else
                {
                    CharacterStats.HP += 10;
                    startHP += 10;
                }
                gamePoint -= 10;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button100")
        {
            if (gamePoint >= 100)
            {
                if (PlayerPrefs.GetInt("CharacterNum") == 0)
                {
                    CharacterStats.HP += 300;
                    startHP += 300;
                }
                else
                {
                    CharacterStats.HP += 100;
                    startHP += 100;
                }
                gamePoint -= 100;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ButtonAll")
        {
            if (PlayerPrefs.GetInt("CharacterNum") == 0)
            {
                CharacterStats.HP += gamePoint*3;
                startHP += gamePoint*3;
            }
            else
            {
                CharacterStats.HP += gamePoint;
                startHP += gamePoint;
            }
            gamePoint = 0;
        }
    }
    public void LuckBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Button1")
        {
            if (gamePoint >= 1)
            {
                CharacterStats.luck++;
                gamePoint--;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button10")
        {
            if (gamePoint >= 10)
            {
                CharacterStats.luck += 10;
                gamePoint -= 10;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button100")
        {
            if (gamePoint >= 100)
            {
                CharacterStats.luck += 100;
                gamePoint -= 100;
            }
            else
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ButtonAll")
        {
            CharacterStats.luck += gamePoint;
            gamePoint = 0;
        }
    }
    private int criCost = 1000;
    public void CriBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Button1")
        {
            if (gamePoint >= criCost && CharacterStats.critical < 100)
            {
                CharacterStats.critical++;
                gamePoint -= criCost;
                criCost += 100;
            }
            else if (gamePoint <= criCost)
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }else if(CharacterStats.critical >= 100)
            {
                criMaxTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
    }
    public void CriDBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Button1")
        {
            if (gamePoint >= 100)
            {
                CharacterStats.criticalDmg += 10;
                gamePoint -= 100;
            }
            else if (gamePoint <= 100)
            {
                noPointTxt.gameObject.SetActive(true);
                Invoke("DeactiveUpgradeTxt", 1.3f);
            }
        }
    }
    #endregion

    public GameObject endPanel;
    public void EndGame()
    {
        //���� ���� �� �� ����
        Time.timeScale = 0;

        int playerCoin = PlayerPrefs.GetInt("Coin");
        playerCoin += inGameLevel;
        PlayerPrefs.SetInt("Coin", playerCoin);

        endPanel.SetActive(true);
        //Image endImg = endPanel.GetComponent<Image>();
        //endImg.color = new Color(0, 0, 0, 255);
    }

    //�κ� ��ư ������ ��
    public void ToLobby()
    {
        endPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
