using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpaceManager : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D coll){
        if (coll.tag == "Blocks"){
            if (this.gameObject.name == "LeftBlockChecker") Player.isBLockOnLeft = true;
            else if (this.gameObject.name == "RightBlockChecker") Player.isBLockOnRight = true;
            if (this.gameObject.name == "DownBlockChecker") Player.isBLockOnBottom = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if (coll.tag == "Blocks"){
            if (this.gameObject.name == "LeftBlockChecker") Player.isBLockOnLeft = false;
            else if (this.gameObject.name == "RightBlockChecker") Player.isBLockOnRight = false;
            if (this.gameObject.name == "DownBlockChecker") Player.isBLockOnBottom = false;
        }
    }
}
