using Assets.Scripts.Invironment;
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
    private Rigidbody2D rb;

	public PlayerMechanics()
    {
        allMechanics = new Dictionary<string, Mechanics>();
        allMechanics["Walljump"] = Walljump;
        allMechanics["BasicMove"] = BasicMove;
        allMechanics["DoubleJump"] = DoubleJump;
		allMechanics["ChangeGravity"] = ChangeGravity; 

	}
    public void Start(){
        //GravityManager._instance.OnGravityScaleChanged += OnGravityChanged;
        rb = GetComponent<Player>().GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    //Базовая механика передвижения
    private int directionMove;
    private int shiftSpeed = 1;
    [SerializeField] private int speed = 10;
    public void BasicMove(){
        if (Input.GetKey(KeyCode.LeftShift)){
            shiftSpeed = 2;
        } else shiftSpeed = 1;

        if (!Player.isBLockOnRight && Input.GetKey(KeyCode.D)) directionMove = 1;
        else if (!Player.isBLockOnLeft && Input.GetKey(KeyCode.A))directionMove = -1;
        else directionMove = 0;

        rb.velocity = new Vector2(directionMove * shiftSpeed * speed, rb.velocity.y);
        if (Player.isBLockOnBottom && Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(new Vector2(0, 15 * transform.up.y) , ForceMode2D.Impulse);
        }
    }

	[SerializeField]
	private float JumpForce;

	//Прыжок от стен
	private bool isWalljumpDone = false;
    public void Walljump(){

		if (Input.GetKey(KeyCode.Space)){
            if(!isWalljumpDone && Input.GetKeyDown(KeyCode.Space) && !Player.isBLockOnBottom && (Player.isBLockOnRight || Player.isBLockOnLeft)){
                StartCoroutine(UseOnBeforeNextUpdate(() => isWalljumpDone = true));     //Изменил строчку
				rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 15 * transform.up.y), ForceMode2D.Impulse);
            }
        }
        if (Player.isBLockOnBottom) isWalljumpDone = false;
	}

    //Двойной прыжок
    [SerializeField]
    private float DoubleJumpForce;

	private bool _EnableDoubleJump = true;
    public void DoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Player.isBLockOnBottom && _EnableDoubleJump)
        {
            if (!isWalljumpDone && (Player.isBLockOnLeft || Player.isBLockOnRight))
                return;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0, 15 * transform.up.y) , ForceMode2D.Impulse);
		    _EnableDoubleJump = false;
        }
        if(Player.isBLockOnBottom)
            _EnableDoubleJump = true;
	}

    //Переключение гравитации
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
	private void OnGravityChanged(float GravityScale)
	{

	}

	//Вспомогательные функции
	IEnumerator UseOnBeforeNextUpdate(Action action) //Переданный делегат будет выполнен после текущего кадра
	{                                                //но перед слудующим кадром Update()
		yield return new WaitForEndOfFrame();
		action();
	}
}
