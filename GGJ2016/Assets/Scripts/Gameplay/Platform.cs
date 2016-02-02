using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{

    public Field[] fields;

    public int ID;
    public Field stairs1; // Dla id nieparzystych
    public Field stairs2; // Dla id nieparzystych

    const float rotationDuration = 0.5f;

    public Field[] wallsCenterFields; // Dla id nieparzystych

    public int rotation;

    private int lastRotation;
    private bool isRotating = false;
    private Quaternion targetRotation;
    private Quaternion sourceRotation;
    private float rotationStartTime;


    public Platform()
    {
    }

	public int getNumberOfFieldsOnPlatform(){
		if(ID == 0) return 1;
		else return 8*ID;
	}

	public static int getNumberOfFieldsOnPlatform(int platformID){
		if(platformID == 0) return 1;
		else return 8*platformID;
	}


	// Use this for initialization
	void Start ()
    {

        fields = GetComponents<Field>();
        lastRotation = rotation;

	}

	// Update is called once per frame
	void Update ()
    {

        if (rotation != lastRotation)
        {

            sourceRotation = transform.localRotation;
            targetRotation = Quaternion.AngleAxis(90f * rotation, Vector3.up);
            rotationStartTime = Time.time;
            isRotating = true;
            lastRotation = rotation;

        }

        if (isRotating)
        {

            float rotationProgress = Mathf.Clamp01((Time.time - rotationStartTime) / rotationDuration);
            rotationProgress = Mathf.SmoothStep(0f, 1f, rotationProgress);
            transform.localRotation = Quaternion.Slerp(sourceRotation, targetRotation, rotationProgress);
            if (rotationProgress == 1f)
                isRotating = false;

        }

    }
}
