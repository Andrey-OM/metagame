using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpaceManager : MonoBehaviour
{
    public Enemy enemy;
    public PlayerMechanics playerMechanics;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        playerMechanics = FindObjectOfType<PlayerMechanics>();
    }

    public void OnTriggerEnter2D(Collider2D coll){
        if (coll.tag == "Player"){
            if (name == "HeadChecker"){
                playerMechanics.DoJump();
                enemy.TakeDamage();
            } 
            if (name == "AttackChecker" && !enemy.isSliding){
                StartCoroutine(enemy.Sliding());
                enemy.isSliding = true;
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.tag == "Player" && name == "BodyChecker"){
                playerMechanics.TakeDamage(enemy.damage);
                if (coll.transform.position.x < transform.position.x) playerMechanics.Discarding(-1);
                else playerMechanics.Discarding(1);
        }
    }
    public void OnTriggerExit2D(Collider2D coll){
        if (coll.tag == "Platform"){
            enemy.dirretionMove *= -1;
        }
    }
    void Update()
    {
        
    }
}
