using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InLobby : MonoBehaviour
{
    public GameObject[] characterPrefabs;

    private GameObject nowCharacter;
    private int characterNum = 0;

    public void Start()
    {
        characterNum = PlayerPrefs.GetInt("CharacterNum");
        
        if (characterNum == 0)
        {
            characterPrefabs[0].SetActive(true);
        }else if (characterNum == 1)
        {
            characterPrefabs[1].SetActive(true);
        }else if (characterNum == 2)
        {
            characterPrefabs[2].SetActive(true);
        }else
        {
            characterPrefabs[3].SetActive(true);
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene(2);

    }
    public void ToStore()
    {
        SceneManager.LoadScene(4);
    }
    public void ChooseCharacter()
    {
        SceneManager.LoadScene(1);
    }


}
