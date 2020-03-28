using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttonclicker : MonoBehaviour
{
    Button startbtn;
    // Start is called before the first frame update
     IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        PhotonLoby.lobby.OnBattletButtonClicked();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
