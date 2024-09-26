using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject ghostSprite;
    void Start()
    {
        int coutGhost = Random.Range(2, 5);
        for (int i = 0; i < coutGhost; i++){
            GameObject ghost = Instantiate(ghostSprite);
            ghost.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
