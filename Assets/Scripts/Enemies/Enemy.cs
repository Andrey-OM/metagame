using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2;
    public int dirretionMove;
    public int heartsCount = 3;
    public int heartForce;
    private int currentHeartForce;
    public int damage = 2; 

    void Start()
    {
        currentHeartForce = heartForce;
        rb = GetComponent<Rigidbody2D>();
        transform.position = PlatformManager.GivePositionToItem(PlatformManager.ToEnemies);
        transform.position += new Vector3(0, 1.25f, 0);
        dirretionMove = 1;
        GenerateHearts(heartsCount);
    }

    public Transform heartPrefab;
    private int vertivalPivot = 6;
    private float horizontalSpace = 1.5f;
    List<Transform> allHearts = new List<Transform>();
    public void GenerateHearts(int heartsCount){
        for (int i = 0; i < heartsCount; i++){
            Transform heart = Instantiate(heartPrefab, transform.position, Quaternion.identity);
            heart.name = i.ToString();
            heart.SetParent(transform);
            heart.localScale = new Vector3(1, 1, 0);
            heart.transform.localPosition = new Vector2(0, vertivalPivot);
            allHearts.Add(heart);
        }
        TakeHeartPosition(allHearts); 
    }
    public void TakeHeartPosition(List<Transform> allHearts){
        float[][] formats = new float[][]
        {
            new float[] { 0 },
            new float[] { -horizontalSpace, horizontalSpace },
            new float[] {-horizontalSpace * 2, 0, horizontalSpace * 2},
            new float[] {-horizontalSpace * 3, -horizontalSpace, horizontalSpace, horizontalSpace * 3},
            new float[] {-horizontalSpace * 4, -horizontalSpace * 2, 0, horizontalSpace * 2, horizontalSpace * 4}
        };
        for (int i = 0; i < allHearts.Count; i++){
            allHearts[i].transform.localPosition = new Vector2(formats[allHearts.Count - 1][i], vertivalPivot);
        }
    }
    public void TakeDamage(){
        --currentHeartForce;
        for (int i = allHearts.Count - 1; i >= 0; i--){
            if (allHearts[i].GetComponent<SpriteRenderer>().sprite.name != "emptyHeart"){
                allHearts[i].GetComponent<HealthBar>().NextSprite(heartForce);
                break;
            }
        }
        if (currentHeartForce == 0){
            currentHeartForce = heartForce;
            --heartsCount;
        }
        if (heartsCount == 0) Destroy(gameObject);
    }

    public bool isSliding = false;
    public IEnumerator Sliding(){
        speed *= 2.5f;
        yield return new WaitForSeconds(0.5f);
        speed /= 2.5f;
        isSliding = false;
    }
    void Update()
    {
        rb.velocity = new Vector2(dirretionMove * speed, rb.velocity.y);
    }
}
