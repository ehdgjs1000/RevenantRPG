using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InCharacter : MonoBehaviour
{
    private int characterN;
    private float power;
    private float HP;
    private float luck;
    public void StartLobby()
    {
        //캐릭터 클릭했을떄 변화점 작성하기
        if(EventSystem.current.currentSelectedGameObject.name == "WarriorBtn")
        {
            CharacterStats.characterNum = 0;
            characterN = 0;
            power = 10;
            HP = 30;
            luck = 0;
            
        }else if (EventSystem.current.currentSelectedGameObject.name == "ArcherBtn")
        {
            CharacterStats.characterNum = 1;
            characterN = 1;
            power = 30;
            HP = 10;
            luck = 1;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "TheifBtn")
        {
            CharacterStats.characterNum = 2;
            characterN = 2;
            power = 20;
            HP = 10;
            luck = 3;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "WizardBtn")
        {
            CharacterStats.characterNum = 3;
            characterN = 3;
            power = 10;
            HP = 10;
            luck = 1;
        }
        CharacterStats.power = power;
        CharacterStats.luck = luck;
        CharacterStats.HP = HP;

        PlayerPrefs.SetInt("CharacterNum", characterN);
        PlayerPrefs.SetFloat("Power",power);
        PlayerPrefs.SetFloat("HP",HP);
        PlayerPrefs.SetFloat("Luck", luck);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
