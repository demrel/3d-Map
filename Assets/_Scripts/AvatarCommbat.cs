using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarCommbat : MonoBehaviour
{
    private PhotonView PV;
    private AvatarSetup avatarSetup;
    public Transform rayOrigin;

    public Text healthDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        avatarSetup = GetComponent<AvatarSetup>();
        healthDisplayText = GameSetup.GS.healthDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
        healthDisplayText.text = avatarSetup.playerHealth.ToString();
    }
    [PunRPC]
    void RPC_Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
            if (hit.transform.tag == "Avatar")
            {
                hit.transform.GetComponent<AvatarSetup>().playerHealth -= avatarSetup.playerDamage;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward) * 1000, Color.green);
            Debug.Log("Did not Hit");
        }
    }
}
