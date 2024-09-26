using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class PlayerMechanics : MonoBehaviour
{
    public static Player player;
    public delegate void Mechanics();
    public static Dictionary<string, Mechanics> allMechanics;
    private static Rigidbody2D rb;
    public PlayerMechanics()
    {
        allMechanics = new Dictionary<string, Mechanics>();
        allMechanics["Walljump"] = Walljump;
        allMechanics["BasicMove"] = BasicMove;
        allMechanics["DoubleJump"] = DoubleJump;
		allMechanics["ChangeGravity"] = ChangeGravity; 
    }
    public void Start(){
        rb = GetComponent<Player>().GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    //Базовая механика передвижения
    private int directionMove;
    private int shiftSpeed = 1;
    [SerializeField] private int speed = 10;
    public void BasicMove(){
        if (Input.GetKey(KeyCode.LeftShift) && discardingDirrection == 0){
            shiftSpeed = 2;
        } else shiftSpeed = 1;

        if (!Player.isBLockOnRight && Input.GetKey(KeyCode.D) || discardingDirrection > 0 ) directionMove = 1;
        else if (!Player.isBLockOnLeft && Input.GetKey(KeyCode.A) || discardingDirrection < 0 ) directionMove = -1;
        else directionMove = 0;

        rb.velocity = new Vector2(directionMove * shiftSpeed * 10, rb.velocity.y);
        if (Player.isBLockOnBottom && Input.GetKey(KeyCode.Space)){
            DoJump();
        }
    }
    //Прыжок
    public void DoJump(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, 15 * Math.Sign(_gravityScale)), ForceMode2D.Impulse);
    }
    //Получение урона
    public int health = 10;
    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0) Destroy(player.gameObject);
    }
    //Отбрасывание
    public int discardingDirrection;
    public void Discarding(int dirrection){ 
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        discardingDirrection = dirrection;
    }
    
    //#Прыжок от стен
    private bool isWalljumpDone = false;
    public void Walljump(){
        if (Input.GetKey(KeyCode.Space)){
            if(!isWalljumpDone && Input.GetKeyDown(KeyCode.Space) && !Player.isBLockOnBottom && (Player.isAllowedJumpOnRightBlock || Player.isAllowedJumpOnLeftBlock)){
                isWalljumpDone = true;
                DoJump();
            }
        }
        if (Player.isBLockOnBottom) isWalljumpDone = false;
    }

    //Двойной прыжок
    private bool _enableDoubleJump = true;
    public void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Player.isBLockOnBottom && _enableDoubleJump)
        {
            if (!isWalljumpDone && (Player.isBLockOnLeft || Player.isBLockOnRight))
                return;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0, 15 * transform.up.y) , ForceMode2D.Impulse);
		    _enableDoubleJump = false;
        }
        if(Player.isBLockOnBottom)
            _enableDoubleJump = true;
    }

    //Переключение гравитации    //Переключение гравитации
    private float _gravityScale = 4;
    public void ChangeGravity()
    {
		if (Input.GetKeyDown(KeyCode.F))
		{
            rb.gravityScale = _gravityScale *= -1;
            Vector3 rotation = transform.rotation.eulerAngles + new Vector3(0, 180, 180);
            transform.rotation = Quaternion.Euler(rotation);
		}
	}

}
