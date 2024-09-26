using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlatformManager : MonoBehaviour
{
    static List<Transform> platformsPosition;


    public void Awake(){
        platformsPosition = new List<Transform>();
        foreach (Transform child in this.transform){
            platformsPosition.Add(child);
        }
    }
    public static Vector2 GivePositionToCardGenerate(){
		int rand = Random.Range(0, platformsPosition.Count -1);
        return platformsPosition[rand].transform.position;
    }
}
