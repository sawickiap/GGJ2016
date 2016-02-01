using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public enum EnemyType
    {
        KNIGHT = 0,
        HAMMER_MAN = 1,
        SPEAR_MAN = 2
    };

    public float speed = 0.5f; // fields /per second
    public float offsetY = 0.51f;

    private Field currField;
    private Field nextField;
    public bool isHumanoid;
    Vector3 startDirection, startPosition;
    Vector3 endDirection, endPosition;
    Vector3 currentDirection, currentPosition;
    float currentPercentOfField;
    float previousStepPhyramidHeight;

    public EnemyType enemyType;
    public int health;
    public int damage;

    // Use this for initialization
    void Start ()
    {

        // Initiate Enemy

        RaycastHit raycastHit;
        if (Physics.Raycast(new Ray(transform.position, Physics.gravity), out raycastHit))
        {

            GameObject gameObject = raycastHit.transform.gameObject;
            if (gameObject)
            {

                Field field = gameObject.GetComponent<Field>();
                if (field)
                {

                    currField = field;
                    nextField = currField.GetNastepnik();
                    endDirection = nextField.transform.position - currField.transform.position;
                    startDirection = endDirection; //wartosc umowna, moze wymyslimy lepiej z poprzednika;
                    endPosition = (currField.transform.position + nextField.transform.position)
                                  * 0.5f + new Vector3(0f, offsetY, 0f);
                    startPosition = endPosition - endDirection;
                    currentPercentOfField = 0.5f;

                }

            }

        }

        previousStepPhyramidHeight = Pyramid.singleton.transform.position.y;

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (this.currField.onPyramid)
        {

            Vector3 delta = new Vector3(0.0f, Pyramid.singleton.transform.position.y
                                              - previousStepPhyramidHeight, 0.0f);
            startPosition = startPosition + delta;
            endPosition = endPosition + delta;
            previousStepPhyramidHeight = Pyramid.singleton.transform.position.y;

        }

        previousStepPhyramidHeight = Pyramid.singleton.transform.position.y;

        float deltaPercentOfPole = speed * Time.deltaTime;

        if (currField.isStairs)
        {
            deltaPercentOfPole *= 0.5f;
        }

        currentPercentOfField += deltaPercentOfPole;

        if (currentPercentOfField > 1.0f)
        {

            currentPercentOfField = currentPercentOfField - 1.0f;
            startDirection = endDirection;
            startPosition = endPosition;
            currField = nextField;
            nextField = currField.GetNastepnik();
            endPosition = (currField.transform.position + nextField.transform.position) * 0.5f
                          + new Vector3(0f, offsetY, 0f);
            endDirection = nextField.transform.position - currField.transform.position;

            if (nextField.isStairs) //jesli to schody
            {
                endPosition.y = startPosition.y;
                //endDirection.y = 0.0f;
            }
            else if (currField.isStairs)
            {
                endPosition.y = nextField.transform.position.y + 0.75f;
                //startDirection = endPosition - startPosition;
                endDirection = endPosition - startPosition;
            }

        }

        Vector3 currentDirection;
        if (!this.currField.isStairs)
        {

            Vector3 midPoint = startPosition + startDirection * 0.5f;
            Vector3 newPosition =
                startPosition * (1f - currentPercentOfField) * (1f - currentPercentOfField) +
                midPoint * 2f * (1f - currentPercentOfField) * currentPercentOfField +
                endPosition * currentPercentOfField * currentPercentOfField;

            if (this.isHumanoid)
            {
                newPosition.y = Mathf.Lerp(startPosition.y, endPosition.y, currentPercentOfField);
            }

            this.transform.position = newPosition;

        }
        else
        {
            this.transform.position = Vector3.Lerp(startPosition,endPosition,currentPercentOfField);
        }

        currentDirection = Vector3.Lerp(startDirection, endDirection, currentPercentOfField);

        if (nextField.isStairs)
        {

            currentDirection.y = startDirection.y;
            const float threshold = 0.6f;

            if(currentPercentOfField > threshold)
            {
                currentDirection.y = Mathf.Lerp(startDirection.y, endDirection.y,
                    (currentPercentOfField - threshold) / (1f - threshold));
            }

        }

        if (this.isHumanoid)
        {
            currentDirection.y = 0.0f;
        }

        this.transform.LookAt(this.transform.position + currentDirection);
//        this.transform.position = startPosition + startDirection*
//            Vector3.Lerp(startPosition, endPosition, currentPercentOfPole);//startPosition + Vector3.Lerp(startDirection, endDirection, currentPercentOfPole);


        /* RaycastHit raycastHit;
         if (Physics.Raycast(new Ray(transform.position, Physics.gravity), out raycastHit))
         {
             GameObject gameObject = raycastHit.transform.gameObject;
             if (gameObject)
             {
                 Pole pole = gameObject.GetComponent<Pole>();
                 if (pole)
                 {
                     if (currPole == null && nextPole == null)
                         nextPole = pole;
                     else if (currPole != null && nextPole == null)
                         nextPole = currPole.GetNastepnik();
                 }
             }
         }
         if (nextPole)
         {
             Vector3 srcPos = transform.position;
             Vector3 dstPos = nextPole.transform.position +
                 new Vector3(0f, 0.75f, 0f);
             Vector3 move = dstPos - srcPos;
             // Not yet reached destination.
             if (move.sqrMagnitude > Mathf.Epsilon)
             {
                 transform.LookAt(dstPos);
                 float distance = Speed * Time.deltaTime;
                 if (distance * distance < move.sqrMagnitude)
                 {
                     move.Normalize();
                     move *= distance;
                 }
                 transform.position = srcPos + move;
             }
             // Already reached destination.
             else
             {
                 currPole = nextPole;
                 nextPole = null;
             }
         }*/

        Debug.DrawRay(
                startPosition,
                startDirection * 0.25f,
                Color.green,
                0f,
                false);

        Debug.DrawRay(
                endPosition,
                endDirection * 0.25f,
                Color.red,
                0f,
                false);

    }

}
