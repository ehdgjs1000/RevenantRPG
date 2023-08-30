using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InItem : MonoBehaviour
{
    public Image weaponImg;
    public Image armorImg;
    public Image[] accImg;

    public GameObject[] itemSet;

    private int accCount = 0;
    private int[] prefabWeaponWCount = new int[20];
    private int[] prefabWeaponACount = new int[20];
    private int[] prefabWeaponTCount = new int[20];
    private int[] prefabWeaponMCount = new int[20];
    private int[] prefabArmorCount = new int[20];

    public GameObject[] weaponWSet;
    public GameObject[] weaponASet;
    public GameObject[] weaponTSet;
    public GameObject[] weaponMSet;
    public GameObject[] armorSet;


    private void Start()
    {
        itemSet[PlayerPrefs.GetInt("CharacterNum")].SetActive(true);
        for (int a = 0; a < 20; a++)
        {
            prefabWeaponWCount[a] = PlayerPrefs.GetInt("weapon"+"0"+ a.ToString());
            if(prefabWeaponWCount[a] == 0)
            {
                weaponWSet[a].SetActive(false);
            }
        }
        for (int a = 0; a < 20; a++)
        {
            prefabWeaponACount[a] = PlayerPrefs.GetInt("weapon" + "1" + a.ToString());
            if (prefabWeaponACount[a] == 0)
            {
                weaponASet[a].SetActive(false);
            }
        }
        for (int a = 0; a < 20; a++)
        {
            prefabWeaponTCount[a] = PlayerPrefs.GetInt("weapon" + "2" + a.ToString());
            if (prefabWeaponTCount[a] == 0)
            {
                weaponTSet[a].SetActive(false);
            }
        }
        for (int a = 0; a < 20; a++)
        {
            prefabWeaponMCount[a] = PlayerPrefs.GetInt("weapon" + "3" + a.ToString());
            if (prefabWeaponMCount[a] == 0)
            {
                weaponMSet[a].SetActive(false);
            }
        }
        for (int a = 0; a < 20; a++)
        {
            Debug.Log(PlayerPrefs.GetInt("armor" + a.ToString()));
            prefabArmorCount[a] = PlayerPrefs.GetInt("armor" + a.ToString());
            if (prefabArmorCount[a] == 0)
            {
                armorSet[a].SetActive(false);
            }
        }


    }
    public void LobbyBtn()
    {
        SceneManager.LoadScene(0);
    }
    public void GameStartBtn()
    {
        SceneManager.LoadScene(3);
    }

    public void EquipWeaponBtn()
    {
        //이미지 바꾸기 및 공격력 증가 적용
        
        weaponImg.sprite = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Image>()[2].sprite;
        weaponImg.gameObject.SetActive(true);
        CharacterStats.itemPower = float.Parse(EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Text>()[2].text);

    }
    public void EquipArmorBtn()
    {
        armorImg.sprite = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Image>()[2].sprite;
        armorImg.gameObject.SetActive(true);
        CharacterStats.itemHP = float.Parse(EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Text>()[1].text);
    }
    public void EquipAccBtn()
    {
        accImg[accCount].sprite  = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Image>()[1].sprite;
        Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Image>()[0]);
        Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Image>()[1]);
        accImg[accCount].gameObject.SetActive(true);
        accCount++;
        if (accCount >= 3) accCount = 3;
    }



}
