using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script depends on the SpriteRenderer component attatched to the same GameObject
[RequireComponent(typeof(SpriteRenderer))] //requires it if the game object doesnot have it hence added to game object


public class ScreenWrap : MonoBehaviour 
{
	//Zprite
	private SpriteRenderer spriteRenderer; //name of the specific variable to be called a letter
	//Camera
	private Bounds camBounds;
	private float camWidth;
	private float camHeight;

	void Awake() //starts before start on the first half
	{
		spriteRenderer = GetComponent<SpriteRenderer> (); //Getting the component
		
	}


	void UpdateCameraBounds ()
	{
		//Calculate camera Bounds
		Camera cam =Camera.main;
		camHeight =2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		camBounds = new Bounds (cam.transform.position, new Vector2 (camWidth, camHeight));
	}
	
	// late updates are called before updates hence storing position sizes
	//Use lateUpdates since we AreaEffector2D using the camera to wrap the objects back around
	void LateUpdate()
	{
		UpdateCameraBounds ();
		//store position and size in shorter variable names
		Vector3 pos = transform.position;//position of our enemy
		Vector3 size =  spriteRenderer.bounds.size;//the size of our enemy
		//calculate the sprites half width and height
		float halfWidth =size.x / 2f;
		float halfHeight = size.y / 2f;
		//check Left
		if (pos.x + halfWidth < camBounds.min.x) 
		{
			
			pos.x = camBounds.max.x + halfWidth;
		}
		//check Right
		if (pos.x - halfWidth > camBounds.max.x) 
		{
			pos.x = camBounds.min.x - halfWidth;
		}
		//Check Top
		if (pos.y - halfHeight > camBounds.max.y) 
			{
			pos.y = camBounds.min.y - halfHeight;
		}

		//check Bottom 
		if(pos.y + halfHeight < camBounds.min.y)
		{
			pos.y = camBounds.max.y + halfHeight;

		}
		// set new position
		transform.position = pos;

		
	}
}
	


