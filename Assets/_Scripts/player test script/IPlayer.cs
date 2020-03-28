using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer 
{
     Vector3 GetHandPos(int index);
     Quaternion GetHandRotation(int index);

}
