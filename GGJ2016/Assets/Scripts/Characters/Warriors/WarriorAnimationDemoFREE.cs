using UnityEngine;
using System.Collections;

public class WarriorAnimationDemoFREE : MonoBehaviour 
{

	public Animator animator;

	float rotationSpeed = 30;

	Vector3 inputVec;
	Vector3 targetDirection;

    private float speed = 0.0f;
    private float previousSpeed = 0.0f;
    public bool isAttacking = false;
    private Enemy enemy;
	
	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress,
                        Knight, Mage, Archer, TwoHanded, Swordsman,
                        Spearman, Hammer, Crossbow};

	public Warrior warrior;
	
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

	void Update()
	{

        if (enemy != null)
        {
            this.speed = enemy.speed;
        }

        if( Input.GetKeyDown(KeyCode.Space) )
        {
            isAttacking = true;
        }

        if( speed > 0 && previousSpeed <= 0 )
        {
            animator.SetFloat("Speed", speed);
            previousSpeed = speed;
        }

        if( isAttacking )
        {

            animator.SetTrigger("Attack1Trigger");
            if (warrior == Warrior.Brute)
                StartCoroutine(COStunPause(1.2f));
            else if (warrior == Warrior.Sorceress)
                StartCoroutine(COStunPause(1.2f));
            else
                StartCoroutine(COStunPause(.6f));

            isAttacking = false;

        }

		//UpdateMovement();  //update character position and facing

	}

	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}

	//converts control input vectors into camera facing vectors
	void GetCameraRelativeMovement()
	{  

		Transform cameraTransform = Camera.main.transform;

		// Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		//directional inputs
		float v= Input.GetAxisRaw("Vertical");
		float h= Input.GetAxisRaw("Horizontal");

		// Target direction relative to the camera
		targetDirection = h * right + v * forward;

	}

	//face character along input direction
	void RotateTowardMovementDirection()  
	{

		if (inputVec != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(targetDirection),
                                                  Time.deltaTime * rotationSpeed);
		}

	}

	void UpdateMovement()
	{

		//get movement input from controls
		Vector3 motion = inputVec;

		//reduce input for diagonal movement
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? .7f : 1;

		RotateTowardMovementDirection();
		GetCameraRelativeMovement();

	}

	void OnGUI () 
	{

		if (GUI.Button (new Rect (25, 85, 100, 30), "Attack1")) 
		{
			animator.SetTrigger("Attack1Trigger");

			if (warrior == Warrior.Brute || warrior == Warrior.Sorceress)  //if character is Brute or Sorceress
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}

	}
}