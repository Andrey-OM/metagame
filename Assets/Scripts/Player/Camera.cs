using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour {

	public float damping = 0;
	public Vector2 offset = Vector2.zero; //new Vector2(5, -5f);
	public bool faceLeft;
	private Transform player;
	private int lastX;

	void Start ()
	{
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
		StartCoroutine(changeCameraOffset());
	}


    IEnumerator changeCameraOffset(){
		for (int i = 0; i < 10; i++){
			yield return new WaitForSeconds(0.1f);
			offset += new Vector2(0, 0.2f);
		}

        
    }

	public void FindPlayer(bool playerFaceLeft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);
		if(playerFaceLeft)
		{
			transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
		}
		else
		{
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}

	void Update () 
	{
		if(player)
		{
			int currentX = Mathf.RoundToInt(player.position.x);
			if(currentX > lastX) faceLeft = false; else if(currentX < lastX) faceLeft = true;
			lastX = Mathf.RoundToInt(player.position.x);

			Vector3 target;
			if(faceLeft)
			{
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else
			{
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			}
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
			transform.position = currentPosition;
		}
	}
}