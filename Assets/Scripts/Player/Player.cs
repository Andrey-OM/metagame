using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public static bool isBLockOnRight = false;
    public static bool isBLockOnLeft = false;
    public static bool isBLockOnBottom = false;
    public PlayerMechanics.Mechanics attachedMechanics;

    public void TakeMechanics(PlayerMechanics.Mechanics newMechanics){
        attachedMechanics += newMechanics;
    }
    void Update(){
        attachedMechanics?.Invoke();
    }
}
