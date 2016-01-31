using UnityEngine;
using System.Collections;

public class Camera2 : MonoBehaviour {

    private float radius = 0f;
    private float y = 1f;
    private float angle = 33f;
    public float RotationSpeed = 90f;
    public Vector3 TargetPos = new Vector3(0f, -1f, 0f);

	// Use this for initialization
	void Start () {
        Vector3 initialPos = transform.position;
        radius = (new Vector2(initialPos.x, initialPos.z)).magnitude;
        y = initialPos.y;

        UpdateTransform();
	}
	
	// Update is called once per frame
	void Update () {
        float horzAxis = Input.GetAxis("Horizontal");
        if(Mathf.Abs(horzAxis) > Mathf.Epsilon)
        {
            angle += horzAxis * RotationSpeed * Time.deltaTime;
            UpdateTransform();
        }
	}

    void UpdateTransform()
    {
        transform.position = new Vector3(
            Mathf.Cos(angle) * radius,
            this.y,
            Mathf.Sin(angle) * radius);
        transform.LookAt(TargetPos);
    }
}
