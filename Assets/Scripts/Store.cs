using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Store : MonoBehaviour
{
    public Text coinTxt, cristalTxt;
    public GameObject gachaPanel;
    public GameObject gacha1;
    public GameObject gacha10;
    public Image gacha1Img;
    public Image[] gacha10Img;
    public Sprite[] bgSet;
    public Image bg1;
    public Image[] bg10;


    public Sprite[] weaponWImgSet;
    public Sprite[] weaponAImgSet;
    public Sprite[] weaponTImgSet;
    public Sprite[] weaponMImgSet;
    public Sprite[] armorImgSet;

    public int coin, cristal;
    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");
        cristal = PlayerPrefs.GetInt("Cristal");
        coinTxt.text = coin.ToString();
        cristalTxt.text = cristal.ToString();
    }
    private void FixedUpdate()
    {
        coinTxt.text = coin.ToString();
        cristalTxt.text = cristal.ToString();
    }
    public void CloseGacha()
    {
        gachaPanel.SetActive(false);
        gacha1.SetActive(false);
        gacha10.SetActive(false);
    }
    public void ToLobby()
    {
        SceneManager.LoadScene(0);
    }

    //등급별 아이템 뽑을 확률
    #region 
    //Normal아이템 속 아이템 뽑을 확률
    private List<int> normalItems = new List<int> {0,1,2,3,4,5,6,7,8,9};
    private List<float>  normalProbabilities = new List<float> { 0.2f, 0.15f, 0.13f, 0.12f, 0.11f, 0.10f, 0.07f, 0.06f, 0.04f, 0.04f };
    public int PickItemNormal()
    {
        float randomValue = Random.value; // 0부터 1 사이의 랜덤한 값을 생성합니다.
        float cumulativeProbability = 0f;

        for (int i = 0; i < normalProbabilities.Count; i++)
        {
            cumulativeProbability += normalProbabilities[i];

            if (randomValue <= cumulativeProbability)
            {
                return normalItems[i];
            }
        }
        return normalItems[normalItems.Count - 1];
    }

    private List<int> epicItems = new List<int> { 10,11,12,13,14,15,16 };
    private List<float> epicProbabilities = new List<float> { 0.34f, 0.24f, 0.16f, 0.12f, 0.08f, 0.04f, 0.02f};
    public int PickItemEpic()
    {
        float randomValue = Random.value; // 0부터 1 사이의 랜덤한 값을 생성합니다.
        float cumulativeProbability = 0f;

        for (int i = 0; i < epicProbabilities.Count; i++)
        {
            cumulativeProbability += epicProbabilities[i];

            if (randomValue <= cumulativeProbability)
            {
                return epicItems[i];
            }
        }
        return epicItems[epicItems.Count - 1];
    }
    private List<int> uniqueItems = new List<int> { 17,18 };
    private List<float> uniqueProbabilities = new List<float> { 0.7f,0.3f };
    public int PickItemUnique()
    {
        float randomValue = Random.value; // 0부터 1 사이의 랜덤한 값을 생성합니다.
        float cumulativeProbability = 0f;

        for (int i = 0; i < uniqueProbabilities.Count; i++)
        {
            cumulativeProbability += uniqueProbabilities[i];

            if (randomValue <= cumulativeProbability)
            {
                return uniqueItems[i];
            }
        }
        return uniqueItems[uniqueItems.Count - 1];
    }
    //Normal 뽑기에서 아이템 등급별 확률
    private List<int> weaponNChance = new List<int> { 0,1,2,3 };
    private List<float> weaponNProbabilities = new List<float> { 0.8f, 0.17f, 0.029f, 0.001f};
    public int NormalItemChance()
    {
        float randomValue = Random.value; // 0부터 1 사이의 랜덤한 값을 생성합니다.
        float cumulativeProbability = 0f;

        for (int i = 0; i < weaponNProbabilities.Count; i++)
        {
            cumulativeProbability += weaponNProbabilities[i];

            if (randomValue <= cumulativeProbability)
            {
                return weaponNChance[i];
            }
        }
        return weaponNChance[weaponNChance.Count - 1];

    }
    //고급 뽑기에서 아이템 등급별 확률
    private List<int> weaponHChance = new List<int> { 0, 1, 2, 3 };
    private List<float> weaponHProbabilities = new List<float> { 0.39f, 0.45f, 0.14f, 0.02f };
    public int HighItemChance()
    {
        float randomValue = Random.value; // 0부터 1 사이의 랜덤한 값을 생성합니다.
        float cumulativeProbability = 0f;

        for (int i = 0; i < weaponHProbabilities.Count; i++)
        {
            cumulativeProbability += weaponHProbabilities[i];

            if (randomValue <= cumulativeProbability)
            {
                return weaponHChance[i];
            }
        }
        return weaponHChance[weaponHChance.Count - 1];
    }
    #endregion


    public void ChangeImg(int jobNum, int itemNum)
    {
        if(jobNum == 0)
        {
            gacha1Img.sprite = weaponWImgSet[itemNum];
        }else if (jobNum == 1)
        {
            gacha1Img.sprite = weaponAImgSet[itemNum];
        }
        else if (jobNum == 2)
        {
            gacha1Img.sprite = weaponTImgSet[itemNum];
        }
        else if (jobNum == 3)
        {
            gacha1Img.sprite = weaponMImgSet[itemNum];
        }
    }
    public void ChangeArmorImg(int itemNum)
    {
        gacha1Img.sprite = armorImgSet[itemNum];
    }
    public void ChangeImg10(int jobNum, int itemNum, int gachaCount)
    {
        if (jobNum == 0)
        {
            gacha10Img[gachaCount].sprite = weaponWImgSet[itemNum];
        }
        else if (jobNum == 1)
        {
            gacha10Img[gachaCount].sprite = weaponAImgSet[itemNum];
        }
        else if (jobNum == 2)
        {
            gacha10Img[gachaCount].sprite = weaponTImgSet[itemNum];
        }
        else if (jobNum == 3)
        {
            gacha10Img[gachaCount].sprite = weaponMImgSet[itemNum];
        }
    }
    public void ChangeArmorImg10(int itemNum, int gachaCount)
    {
        gacha10Img[gachaCount].sprite = armorImgSet[itemNum];
    }
    public void ChangeBackGounrd10(int grade, int num)
    {
        if(grade == 0)
        {
            bg10[num].sprite = bgSet[0];
        }else if(grade == 1)
        {
            bg10[num].sprite = bgSet[1];
        }
        else if(grade ==2)
        {
            bg10[num].sprite = bgSet[2];
        }
        else
        {
            bg10[num].sprite = bgSet[3];
        }
    }
    public void ChangeBackGounrd1(int grade)
    {
        if (grade == 0)
        {
            bg1.sprite = bgSet[0];
        }
        else if (grade == 1)
        {
            bg1.sprite = bgSet[1];
        }
        else if (grade == 2)
        {
            bg1.sprite = bgSet[2];
        }
        else
        {
            bg1.sprite = bgSet[3];
        }
    }

    //무기 뽑기
    #region
    public void WeaponBtn()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "BtnWN1")
        {
            if (coin >= 1)
            {
                gachaPanel.SetActive(true);
                gacha1.SetActive(true);
                //뽑기
                int chance = NormalItemChance();
                int itemNum = 0;
                if(chance == 0)
                {
                    itemNum = PickItemNormal();
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + itemNum.ToString();
                    ChangeImg(Jnum,itemNum);
                    ChangeBackGounrd1(0);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 1)
                {
                    itemNum = PickItemEpic();
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + itemNum.ToString();
                    ChangeImg(Jnum, itemNum);
                    ChangeBackGounrd1(1);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 2)
                {
                    itemNum = PickItemUnique();
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + itemNum.ToString();
                    ChangeImg(Jnum, itemNum);
                    ChangeBackGounrd1(2);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else
                {
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + "19";
                    ChangeImg(Jnum, 19);
                    ChangeBackGounrd1(3);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                coin--;
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.Save();
                
            }

            else return;
        }else if (EventSystem.current.currentSelectedGameObject.name == "BtnWN10")
        {
            if (coin >= 10)
            {
                gachaPanel.SetActive(true);
                gacha10.SetActive(true);
                //뽑기
                for (int count = 0; count <10; count++)
                {
                    int chance = NormalItemChance();
                    int itemNum = 0;
                    if (chance == 0)
                    {
                        itemNum = PickItemNormal();
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + itemNum.ToString();
                        ChangeImg10(Jnum, itemNum, count);
                        ChangeBackGounrd10(0,count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 1)
                    {
                        itemNum = PickItemEpic();
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + itemNum.ToString();
                        ChangeImg10(Jnum, itemNum, count);
                        ChangeBackGounrd10(1, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 2)
                    {
                        itemNum = PickItemUnique();
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + itemNum.ToString();
                        ChangeImg10(Jnum, itemNum, count);
                        ChangeBackGounrd10(2, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else
                    {
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + "19";
                        ChangeImg10(Jnum, 19, count);
                        ChangeBackGounrd10(3, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    coin--;
                }
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.Save();
            }
            else return;
        }else if (EventSystem.current.currentSelectedGameObject.name == "BtnWH1")
        {
            if (cristal >= 1)
            {
                gachaPanel.SetActive(true);
                gacha1.SetActive(true);
                //뽑기
                int chance = HighItemChance();
                int itemNum = 0;
                if (chance == 0)
                {
                    itemNum = PickItemNormal();
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + itemNum.ToString();
                    ChangeImg(Jnum, itemNum);
                    ChangeBackGounrd1(0);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 1)
                {
                    itemNum = PickItemEpic();
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + itemNum.ToString();
                    ChangeImg(Jnum, itemNum);
                    ChangeBackGounrd1(1);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 2)
                {
                    itemNum = PickItemUnique();
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + itemNum.ToString();
                    ChangeImg(Jnum, itemNum);
                    ChangeBackGounrd1(2);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else
                {
                    int Jnum = Random.Range(0, 4);
                    string prefItem = "weapon" + Jnum + "19";
                    ChangeImg(Jnum, 19);
                    ChangeBackGounrd1(3);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                cristal--;
                PlayerPrefs.SetInt("Cristal", cristal);
                PlayerPrefs.Save();
            }
            else return;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "BtnWH10")
        {
            
            if (cristal >= 10)
            {
                gachaPanel.SetActive(true);
                gacha10.SetActive(true);
                for (int count = 0; count < 10; count++)
                {
                    int chance = HighItemChance();
                    int itemNum = 0;
                    if (chance == 0)
                    {
                        itemNum = PickItemNormal();
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + itemNum.ToString();
                        ChangeImg10(Jnum, itemNum, count);
                        ChangeBackGounrd10(0, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 1)
                    {
                        itemNum = PickItemEpic();
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + itemNum.ToString();
                        ChangeImg10(Jnum, itemNum, count);
                        ChangeBackGounrd10(1, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 2)
                    {
                        itemNum = PickItemUnique();
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + itemNum.ToString();
                        ChangeImg10(Jnum, itemNum, count);
                        ChangeBackGounrd10(2, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else
                    {
                        Debug.Log("뽑았다");
                        int Jnum = Random.Range(0, 4);
                        string prefItem = "weapon" + Jnum + "19";
                        ChangeImg10(Jnum, 19, count);
                        ChangeBackGounrd10(3,count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    cristal--;
                }
                PlayerPrefs.SetInt("Cristal", cristal);
                PlayerPrefs.Save();
            }
            else return;
        }
    }
    #endregion

    //방어구 뽑기
    #region
    public void ArmorBtn()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "BtnAN1")
        {
            if (coin >= 1)
            {
                gachaPanel.SetActive(true);
                gacha1.SetActive(true);
                //뽑기
                int chance = NormalItemChance();
                int itemNum = 0;
                if (chance == 0)
                {
                    itemNum = PickItemNormal();
                    string prefItem = "armor" + itemNum.ToString();
                    ChangeArmorImg(itemNum);
                    ChangeBackGounrd1(0);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 1)
                {
                    itemNum = PickItemEpic();
                    string prefItem = "armor" + itemNum.ToString();
                    ChangeArmorImg(itemNum);
                    ChangeBackGounrd1(1);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 2)
                {
                    itemNum = PickItemUnique();
                    string prefItem = "armor" + itemNum.ToString();
                    ChangeArmorImg(itemNum);
                    ChangeBackGounrd1(2);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else
                {
                    ChangeArmorImg(19);
                    ChangeBackGounrd1(3);

                    int itemCount = PlayerPrefs.GetInt("armor19");
                    itemCount++;
                    PlayerPrefs.SetInt("armor19", itemCount);
                }
                coin--;
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.Save();

            }

            else return;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "BtnAN10")
        {
            if (coin >= 10)
            {
                gachaPanel.SetActive(true);
                gacha10.SetActive(true);
                //뽑기
                for (int count = 0; count < 10; count++)
                {
                    int chance = NormalItemChance();
                    int itemNum = 0;
                    if (chance == 0)
                    {
                        itemNum = PickItemNormal();
                        string prefItem = "armor" + itemNum.ToString();
                        ChangeArmorImg10(itemNum, count);
                        ChangeBackGounrd10(0, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 1)
                    {
                        itemNum = PickItemEpic();
                        string prefItem = "armor" + itemNum.ToString();
                        ChangeArmorImg10(itemNum, count);
                        ChangeBackGounrd10(1, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 2)
                    {
                        itemNum = PickItemUnique();
                        string prefItem = "armor" + itemNum.ToString();
                        ChangeArmorImg10(itemNum, count);
                        ChangeBackGounrd10(2, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else
                    {
                        ChangeArmorImg10(19, count);
                        ChangeBackGounrd10(3, count);

                        int itemCount = PlayerPrefs.GetInt("armor19");
                        itemCount++;
                        PlayerPrefs.SetInt("armor19", itemCount);
                    }
                    coin--;
                }
                PlayerPrefs.SetInt("Coin", coin);
                PlayerPrefs.Save();
            }
            else return;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "BtnAH1")
        {
            if (cristal >= 1)
            {
                gachaPanel.SetActive(true);
                gacha1.SetActive(true);
                //뽑기
                int chance = HighItemChance();
                int itemNum = 0;
                if (chance == 0)
                {
                    itemNum = PickItemNormal();
                    string prefItem = "armor" + itemNum.ToString();
                    ChangeArmorImg(itemNum);
                    ChangeBackGounrd1(0);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 1)
                {
                    itemNum = PickItemEpic();
                    string prefItem = "armor" + itemNum.ToString();
                    ChangeArmorImg(itemNum);
                    ChangeBackGounrd1(1);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else if (chance == 2)
                {
                    itemNum = PickItemUnique();
                    string prefItem = "armor" + itemNum.ToString();
                    ChangeArmorImg(itemNum);
                    ChangeBackGounrd1(2);

                    int itemCount = PlayerPrefs.GetInt(prefItem);
                    itemCount++;
                    PlayerPrefs.SetInt(prefItem, itemCount);
                }
                else
                {
                    ChangeArmorImg(19);
                    ChangeBackGounrd1(3);

                    int itemCount = PlayerPrefs.GetInt("armor19");
                    itemCount++;
                    PlayerPrefs.SetInt("armor19", itemCount);
                }
                cristal--;
                PlayerPrefs.SetInt("Cristal", cristal);
                PlayerPrefs.Save();
            }
            else return;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "BtnAH10")
        {

            if (cristal >= 10)
            {
                gachaPanel.SetActive(true);
                gacha10.SetActive(true);
                for (int count = 0; count < 10; count++)
                {
                    int chance = HighItemChance();
                    int itemNum = 0;
                    if (chance == 0)
                    {
                        itemNum = PickItemNormal();
                        string prefItem = "armor" + itemNum.ToString();
                        ChangeArmorImg10(itemNum, count);
                        ChangeBackGounrd10(0, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 1)
                    {
                        itemNum = PickItemEpic();
                        string prefItem = "armor" + itemNum.ToString();
                        ChangeArmorImg10(itemNum, count);
                        ChangeBackGounrd10(1, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else if (chance == 2)
                    {
                        itemNum = PickItemUnique();
                        string prefItem = "armor" + itemNum.ToString();
                        ChangeArmorImg10(itemNum, count);
                        ChangeBackGounrd10(2, count);

                        int itemCount = PlayerPrefs.GetInt(prefItem);
                        itemCount++;
                        PlayerPrefs.SetInt(prefItem, itemCount);
                    }
                    else
                    {
                        ChangeArmorImg10(19, count);
                        ChangeBackGounrd10(3, count);

                        int itemCount = PlayerPrefs.GetInt("armor19");
                        itemCount++;
                        PlayerPrefs.SetInt("armor19", itemCount);
                    }
                    cristal--;
                }
                PlayerPrefs.SetInt("Cristal", cristal);
                PlayerPrefs.Save();
            }
            else return;
        }
    }
    #endregion
}
