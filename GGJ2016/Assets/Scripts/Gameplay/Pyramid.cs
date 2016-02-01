using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pyramid : MonoBehaviour
{
	
	public static Pyramid singleton;
	public Platform[] platforms;
	
	const int maxLevel = 2;
	public float currentHeight; // Public, only for read so that enemies can check it and update own height

    public int advancement = 0;
	public int currentLevel;

    bool isMoving = false;
    int previousLevel = 0;
    public float timePerLevel = 2.0f;
    float movingStartTime;

    public Pyramid()
    {
        singleton = this;
    }

    //---------------------------------------------------------

    // Use this for initialization
    void Start()
    {
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SetAdvancement(advancement);
        if (currentLevel != previousLevel)
        {
            if (!isMoving)
            {
                movingStartTime = Time.realtimeSinceStartup;
                isMoving = true;
            }
            float progress = (Time.realtimeSinceStartup - movingStartTime) / timePerLevel;
            if (progress > 1.0f)
            {
                progress = progress - 1.0f;
                previousLevel = previousLevel + (int)Mathf.Sign(currentLevel - previousLevel);
                movingStartTime = Time.realtimeSinceStartup;
            }
            transform.position = Vector3.Lerp(
                new Vector3(0.0f, previousLevel * 2.0f, 0.0f),
                new Vector3(0.0f, (previousLevel + Mathf.Sign(currentLevel - previousLevel)) * 2.0f, 0.0f),
                progress);

        }
        else
        {
            isMoving = false;
            transform.position = new Vector3(0.0f, currentLevel * 2.0f, 0.0f);
        }

    }

	public int GetLevel()
    {

		return currentLevel;
		currentLevel = 0; // Should be deleted? this code in unreachable.

	}
	
	// Methods to control the phyramid:-------------------------
	
	// ControlLevel level (0-2):
	public void SetLevel(int level)
    {

		currentLevel = level;
        if (currentLevel > maxLevel)
        {
            currentLevel = maxLevel;
        }
        else
        {
            if (currentLevel < 0)
            {
                currentLevel = 0;
            }
        }

	}

	// Try to increase level +1;
	public void Rise()
    {

		currentLevel = currentLevel+1;
		if (currentLevel > maxLevel) currentLevel = maxLevel;

	}

	// Try to decrease level by -1:
	public void Lower()
    {

		currentLevel = currentLevel-1;
        if (currentLevel < 0)
        {
            currentLevel = 0;
        }

	}

	// Set rotation of level to 0,1,2 or 3. possible levels are 0,1 and 2
	public void SetRotationOfLevel(int level, int rotation)
    {
		this.platforms[level*2].rotation = rotation;
	}

    public void IncreaseAdvancement()
    {

        if (advancement < 9)
        {
            ++advancement;
        }

    }
	
	public void SetAdvancement(int advancement)
    {

		switch (advancement)
        {

		case 0:
			SetLevel(0);
			SetRotationOfLevel(0,0);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		case 1:
			SetLevel(0);
			SetRotationOfLevel(0,1);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		case 2:
			SetLevel(1);
			SetRotationOfLevel(0,2);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		case 3:
			SetLevel(1);
			SetRotationOfLevel(0,3);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		case 4:
			SetLevel(1);
			SetRotationOfLevel(0,3);
			SetRotationOfLevel(1,1);
			SetRotationOfLevel(2,0);
			break;

		case 5:
			SetLevel(1);
			SetRotationOfLevel(0,0);
			SetRotationOfLevel(1,1);
			SetRotationOfLevel(2,0);
			break;

		case 6:
			SetLevel(2);
			SetRotationOfLevel(0,0);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		case 7:
			SetLevel(2);
			SetRotationOfLevel(0,1);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		case 8:
			SetLevel(2);
			SetRotationOfLevel(0,2);
			SetRotationOfLevel(1,1);
			SetRotationOfLevel(2,0);
			break;

		case 9:
			SetLevel(2);
			SetRotationOfLevel(0,3);
			SetRotationOfLevel(1,2);
			SetRotationOfLevel(2,1);
			break;

		default:
			SetLevel(0);
			SetRotationOfLevel(0,0);
			SetRotationOfLevel(1,0);
			SetRotationOfLevel(2,0);
			break;

		}
		
	}

}
