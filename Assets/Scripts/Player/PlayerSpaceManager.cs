using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpaceManager : MonoBehaviour
{
    public PlayerMechanics playerMechanics;
    public void Start(){
        playerMechanics = FindObjectOfType<PlayerMechanics>();
    }
    void OnTriggerStay2D(Collider2D coll){
        if (coll.tag == "Platform"){
            playerMechanics.discardingDirrection = 0;
            if (name == "LeftBlockChecker") Player.isBLockOnLeft = true;
            else if (name == "RightBlockChecker") Player.isBLockOnRight = true;
            if (name == "DownBlockChecker") Player.isBLockOnBottom = true;
            
            if (name == "LeftJumpChecker") Player.isAllowedJumpOnLeftBlock = true;
            else if (name == "RightJumpChecker") Player.isAllowedJumpOnRightBlock = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll){
        if (coll.tag == "Platform"){
            if (name == "LeftBlockChecker") Player.isBLockOnLeft = false;
            else if (name == "RightBlockChecker") Player.isBLockOnRight = false;
            if (name == "DownBlockChecker") Player.isBLockOnBottom = false;

            if (name == "LeftJumpChecker") Player.isAllowedJumpOnLeftBlock = false;
            else if (name == "RightJumpChecker") Player.isAllowedJumpOnRightBlock = false;
        }
    }
}
