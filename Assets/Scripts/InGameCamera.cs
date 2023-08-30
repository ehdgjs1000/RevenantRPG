using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour
{
    public GameObject[] characterSets;

    private void FixedUpdate()
    {
        GameObject character = characterSets[PlayerPrefs.GetInt("CharacterNum")];
        this.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 3 ,-1);
    }

}
