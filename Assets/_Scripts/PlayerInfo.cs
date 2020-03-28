using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;

    public int mySelectedCharacter;
    public static string myCharacter = "MyCharacter";
    public GameObject[] allCharacters;

    private void OnEnable()
    {
        if (PI==null)
        {
            PI = this;
        }
        else
        {
            if (PI!=this)
            {
                Destroy(gameObject);
                PI = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(myCharacter))
        {
            mySelectedCharacter = PlayerPrefs.GetInt(myCharacter);
        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt(myCharacter, mySelectedCharacter);
        }
    }
}
