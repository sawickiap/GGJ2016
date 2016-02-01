using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piramida : MonoBehaviour
{
	
	public static Piramida singleton;
	public Platforma[] platformy;
	
	const int maxLevel = 2;
	public float currentHeight; //publc, only for read so that enemies can check it and update own height

    public int advancement = 0;
	public int currentLevel;
	
	public int GetLevel()
    {
		return currentLevel;
		currentLevel = 0;
	}
	
	//Methods to control the phyramid:-------------------------
	
	//controlLevel level (0-2):
	public void SetLevel(int level)
    {
		currentLevel = level;
		if (currentLevel > maxLevel) currentLevel = maxLevel; 
		else{
			if (currentLevel < 0) currentLevel = 0; 
		}
	}

	//try to increase level +1;
	public void Rise()
    {
		currentLevel = currentLevel+1;
		if (currentLevel > maxLevel) currentLevel = maxLevel; 
	}

	//try to decrease level -1:
	public void Lower()
    {
		currentLevel = currentLevel-1;
		if (currentLevel < 0) currentLevel = 0; 
	}

	//set rotation of level to 0,1,2 or 3. possible levels are 0,1 and 2
	public void setRotationOfLevel(int level, int rotation)
    {
		this.platformy[level*2].rotacja = rotation;
	}

    public void IncreaseAdvancement()
    {
        if (advancement < 9)
            ++advancement;
    }
	
	public void setAdvancement(int advancement)
    {

		switch (advancement)
        {

		case 0:
			SetLevel(0);
			setRotationOfLevel(0,0);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		case 1:
			SetLevel(0);
			setRotationOfLevel(0,1);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		case 2:
			SetLevel(1);
			setRotationOfLevel(0,2);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		case 3:
			SetLevel(1);
			setRotationOfLevel(0,3);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		case 4:
			SetLevel(1);
			setRotationOfLevel(0,3);
			setRotationOfLevel(1,1);
			setRotationOfLevel(2,0);
			break;

		case 5:
			SetLevel(1);
			setRotationOfLevel(0,0);
			setRotationOfLevel(1,1);
			setRotationOfLevel(2,0);
			break;

		case 6:
			SetLevel(2);
			setRotationOfLevel(0,0);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		case 7:
			SetLevel(2);
			setRotationOfLevel(0,1);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		case 8:
			SetLevel(2);
			setRotationOfLevel(0,2);
			setRotationOfLevel(1,1);
			setRotationOfLevel(2,0);
			break;

		case 9:
			SetLevel(2);
			setRotationOfLevel(0,3);
			setRotationOfLevel(1,2);
			setRotationOfLevel(2,1);
			break;

		default:
			SetLevel(0);
			setRotationOfLevel(0,0);
			setRotationOfLevel(1,0);
			setRotationOfLevel(2,0);
			break;

		}
		
	}
	
	//---------------------------------------------------------
	
	Piramida()
	{
		singleton = this;
	}
	
	// Use this for initialization
	void Start ()
    {
		currentLevel = 0;
	}
	
	bool isMoving = false;
	int previousLevel = 0;
	public float timePerLevel = 2.0f;
	float movingStartTime;
	
	
	// Update is called once per frame
	void Update ()
    {
		setAdvancement(advancement);
		if(currentLevel != previousLevel)
        {
			if(!isMoving)
            {
				movingStartTime = Time.realtimeSinceStartup;
				isMoving = true;
			}
			float progress = (Time.realtimeSinceStartup - movingStartTime) / timePerLevel;
			if(progress > 1.0f)
            {
				progress = progress - 1.0f;
				previousLevel = previousLevel + (int)Mathf.Sign(currentLevel-previousLevel);
				movingStartTime = Time.realtimeSinceStartup;
			}
			transform.position = Vector3.Lerp(
				new Vector3(0.0f,previousLevel*2.0f,0.0f),
				new Vector3(0.0f,(previousLevel+Mathf.Sign(currentLevel-previousLevel))*2.0f, 0.0f),
				progress);
			
		}
		else
        {
			isMoving = false;
			transform.position = new Vector3(0.0f, currentLevel*2.0f, 0.0f);
		}
		
	}
}
