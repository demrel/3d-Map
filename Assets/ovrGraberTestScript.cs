using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ovrGraberTestScript : OVRGrabber
{
    public PhotonView photonView;
    void OnTriggerEnter(Collider otherCollider)
    {
        // Get the grab trigger
       
        OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
        if (grabbable == null) return;

        if (otherCollider.gameObject.GetPhotonView())
        {
            otherCollider.gameObject.GetPhotonView().TransferOwnership(photonView.Owner);
        }
        // Add the grabbable
        int refCount = 0;
        m_grabCandidates.TryGetValue(grabbable, out refCount);
        m_grabCandidates[grabbable] = refCount + 1;
    }

    void OnTriggerExit(Collider otherCollider)
    {
       
        
        OVRGrabbable grabbable = otherCollider.GetComponent<OVRGrabbable>() ?? otherCollider.GetComponentInParent<OVRGrabbable>();
        if (grabbable == null) return;

        //if (otherCollider.gameObject.GetPhotonView())
        //{
        //    otherCollider.gameObject.GetPhotonView().TransferOwnership(0);
        //}
        // Remove the grabbable
        int refCount = 0;
        bool found = m_grabCandidates.TryGetValue(grabbable, out refCount);
        if (!found)
        {
            return;
        }

        if (refCount > 1)
        {
            m_grabCandidates[grabbable] = refCount - 1;
        }
        else
        {
            m_grabCandidates.Remove(grabbable);
        }
    }
}
