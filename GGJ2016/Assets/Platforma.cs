using UnityEngine;
using System.Collections;

public class Platforma : MonoBehaviour
{

    public Pole[] fields;

    public int ID;
    public Pole schody1;//dla id nieparzystych
    public Pole schody2;//dla id nieparzystych

    const float RotationDuration = 0.5f;

    public Pole[] polaCentralneScian;// dla id nparzystych

    public int rotacja;

    private int lastRotacja;
    private bool isRotating = false;
    private Quaternion targetRotation;
    private Quaternion sourceRotation;
    private float rotationStartTime;

	// Use this for initialization
	void Start ()
    {

        fields = GetComponents<Pole>();
        lastRotacja = rotacja;

	}

	// Update is called once per frame
	void Update ()
    {

        if (rotacja != lastRotacja)
        {

            sourceRotation = transform.localRotation;
            targetRotation = Quaternion.AngleAxis(90f * rotacja, Vector3.up);
            rotationStartTime = Time.time;
            isRotating = true;
            lastRotacja = rotacja;

        }

        if (isRotating)
        {

            float rotationProgress = Mathf.Clamp01((Time.time - rotationStartTime) / RotationDuration);
            rotationProgress = Mathf.SmoothStep(0f, 1f, rotationProgress);
            transform.localRotation = Quaternion.Slerp(sourceRotation, targetRotation, rotationProgress);
            if (rotationProgress == 1f)
                isRotating = false;

        }

    }
}
