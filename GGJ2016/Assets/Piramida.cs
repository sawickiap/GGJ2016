using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piramida : MonoBehaviour {

    public static Piramida singleton;
    public Platforma[] platformy;

	const int maxLevel = 2;
	public float currentHeight; //publc, only for read so that enemies can check it and update own height

	public int currentLevel;

	public int GetLevel(){
		return currentLevel;
		currentLevel = 0;
	}
	
	//Methods to control the phyramid:-------------------------
	
	//controlLevel level (0-2):
	public void SetLevel(int level){
		currentLevel = level;
		if (currentLevel > maxLevel) currentLevel = maxLevel; 
		else{
			if (currentLevel < 0) currentLevel = 0; 
		}
	}
	//try to increase level +1;
	public void Rise(){
		currentLevel = currentLevel+1;
		if (currentLevel > maxLevel) currentLevel = maxLevel; 
	}
	//try to decrease level -1:
	public void Lower(){
		currentLevel = currentLevel-1;
		if (currentLevel < 0) currentLevel = 0; 
	}
	//set rotation of level to 0,1,2 or 3. possible levels are 0,1 and 2
	public void setRotationOfLevel(int level, int rotation){
		this.platformy[level*2].rotacja = rotation;
	}

	//---------------------------------------------------------

    Piramida()
    {
        singleton = this;
    }

	// Use this for initialization
	void Start () {
		currentLevel = 0;
	}

	bool isMoving = false;
	int previousLevel = 0;
	public float timePerLevel = 2.0f;
	float movingStartTime;


	// Update is called once per frame
	void Update () {
		if(currentLevel != previousLevel){
			if(!isMoving){
				movingStartTime = Time.realtimeSinceStartup;
				isMoving = true;
			}
			float progress = (Time.realtimeSinceStartup - movingStartTime) / timePerLevel;
			if(progress > 1.0f){
				progress = progress - 1.0f;
				previousLevel = previousLevel + (int)Mathf.Sign(currentLevel-previousLevel);
				movingStartTime = Time.realtimeSinceStartup;
			}
			transform.position = Vector3.Lerp(
				new Vector3(0.0f,previousLevel*2.0f,0.0f),
				new Vector3(0.0f,(previousLevel+Mathf.Sign(currentLevel-previousLevel))*2.0f,0.0f),
				progress);

		}
		else{
			isMoving = false;
			transform.position = new Vector3(0.0f,currentLevel*2.0f,0.0f);
		}
	
	}
}
