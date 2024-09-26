using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardController : MonoBehaviour
{
    //public delegate void ActiveMechanics();
    public PlayerMechanics.Mechanics activeMechanics;
    public string MechanicsName;
    void Start()
    {
        activeMechanics = PlayerMechanics.allMechanics[MechanicsName];
        if (gameObject.name == "StartCard") return;
        transform.position = PlatformManager.GivePositionToItem(PlatformManager.ToCard);
        transform.position += new Vector3(0, 1.25f, 0);
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Player"){
            coll.GetComponent<Player>().TakeMechanics(activeMechanics); 
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
