using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    private SpriteRenderer sr;
    public void Start(){
        sr = GetComponent<SpriteRenderer>();
    }
    public void NextSprite(int heartForce){
        if (sr.sprite.name == "fullHeart"){
            sr.sprite = halfHeart;
            if (heartForce == 1) {
                sr.sprite = emptyHeart;
                Debug.Log("Проход");
                }
        } else if (sr.sprite.name == "halfHeart"){
            sr.sprite = emptyHeart;
        } else {
            sr.sprite = emptyHeart;
        }
    }
}
