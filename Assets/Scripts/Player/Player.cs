using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public static bool isBLockOnRight = false;
    public static bool isBLockOnLeft = false;
    public static bool isBLockOnBottom = false;

    public static bool isAllowedJumpOnLeftBlock = false;
    public static bool isAllowedJumpOnRightBlock = false;

    private static Rigidbody2D rb;
    public void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    public PlayerMechanics.Mechanics attachedMechanics;

    public void TakeMechanics(PlayerMechanics.Mechanics newMechanics){
        attachedMechanics += newMechanics;
    }
    public void DellMechanics(PlayerMechanics.Mechanics newMechanics){
        attachedMechanics -= newMechanics;
    }
    public bool BLockOnRight = false;
    public bool BLockOnLeft = false;
    public bool BLockOnBottom = false;
    void Update(){
        BLockOnBottom = isBLockOnBottom;
        BLockOnRight = isBLockOnRight;
        BLockOnLeft = isBLockOnLeft;
        attachedMechanics?.Invoke();
    }
}
