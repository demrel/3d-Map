using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControler : MonoBehaviour
{
    public void OnClickCharacterPick(int whitchCharachter)
    {
        if (PlayerInfo.PI!=null)
        {
            PlayerInfo.PI.mySelectedCharacter = whitchCharachter;
            PlayerPrefs.SetInt(PlayerInfo.myCharacter, whitchCharachter);
        }
    }
}
