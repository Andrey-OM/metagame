
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
public class PlatformManager : MonoBehaviour
{
    static List<Transform> platformsPosition;

    public PlatformManager()
    {

    }
    public void Awake(){
        platformsPosition = new List<Transform>();
        foreach (Transform child in this.transform){
            platformsPosition.Add(child);
        }
    }
    public static Vector2 GivePositionToItem(Predicate<int> predicate){
        int rand;
        do {
            rand = UnityEngine.Random.Range(0, platformsPosition.Count);
        } while (!predicate(rand));
        Vector2 pos = platformsPosition[rand].transform.position;
        platformsPosition.RemoveAt(rand);
        return pos;
    }   

    public static bool ToEnemies(int i){
        return platformsPosition[i].GetComponent<Platform>().isAllowedToEnemies;
    }

    public static bool ToCard(int i){
        return platformsPosition[i].GetComponent<Platform>().isAllowedToCard;
    }
}
