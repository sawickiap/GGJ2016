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

    public float Speed = 0.5f; //pola/sekunde
    public float offsetY = 0.51f;

    private Pole currPole;
    private Pole nextPole;
    public bool humanoid;
    Vector3 startDirection, startPosition;
    Vector3 endDirection, endPosition;
    Vector3 currentDirection, currentPosition;
    float currentPercentOfPole;
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
                Pole pole = gameObject.GetComponent<Pole>();
                if (pole)
                {
                    currPole = pole;
                    nextPole = currPole.GetNastepnik();
                    endDirection = nextPole.transform.position - currPole.transform.position;
                    startDirection = endDirection;//wartosc umowna, moze wymyslimy lepiej z poprzednika;
                    endPosition = (currPole.transform.position + nextPole.transform.position)*0.5f + new Vector3(0f, offsetY, 0f);
                    startPosition = endPosition - endDirection;
                    currentPercentOfPole = 0.5f;
                }
            }
        }

        previousStepPhyramidHeight = Piramida.singleton.transform.position.y;

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (this.currPole.onPyramid)
        {
            Vector3 delta = new Vector3(0.0f, Piramida.singleton.transform.position.y - previousStepPhyramidHeight, 0.0f);
            startPosition = startPosition + delta;
            endPosition = endPosition + delta;
            previousStepPhyramidHeight = Piramida.singleton.transform.position.y;
        }

        previousStepPhyramidHeight = Piramida.singleton.transform.position.y;

        float deltaPercentOfPole = Speed * Time.deltaTime;
        if (currPole.isSchody) deltaPercentOfPole *= 0.5f;
        currentPercentOfPole += deltaPercentOfPole;
        if (currentPercentOfPole > 1.0f)
        {
            currentPercentOfPole = currentPercentOfPole - 1.0f;
            startDirection = endDirection;
            startPosition = endPosition;
            currPole = nextPole;
            nextPole = currPole.GetNastepnik();
            endPosition = (currPole.transform.position + nextPole.transform.position) * 0.5f + new Vector3(0f, offsetY, 0f);
            endDirection = nextPole.transform.position - currPole.transform.position;

            if (nextPole.isSchody) //jesli to schody
            {
                endPosition.y = startPosition.y;
                //endDirection.y = 0.0f;
            }
            else if (currPole.isSchody)
            {
                endPosition.y = nextPole.transform.position.y + 0.75f;
                //startDirection = endPosition - startPosition;
                endDirection = endPosition - startPosition;
            }
        }

        Vector3 currentDirection;
        if (!this.currPole.isSchody) {
            Vector3 midPoint = startPosition + startDirection * 0.5f;
            Vector3 newPosition =
                startPosition * (1f - currentPercentOfPole) * (1f - currentPercentOfPole) +
                midPoint * 2f * (1f - currentPercentOfPole) * currentPercentOfPole +
                endPosition * currentPercentOfPole * currentPercentOfPole;
            if (this.humanoid)
                newPosition.y = Mathf.Lerp(startPosition.y, endPosition.y, currentPercentOfPole);
            this.transform.position = newPosition;
        }
        else
        {
            this.transform.position = Vector3.Lerp(startPosition,endPosition,currentPercentOfPole);
        }

        currentDirection = Vector3.Lerp(startDirection, endDirection, currentPercentOfPole);

        if (nextPole.isSchody)
        {
            currentDirection.y = startDirection.y;
            const float threshold = 0.6f;
            if(currentPercentOfPole > threshold)
            {
                currentDirection.y = Mathf.Lerp(startDirection.y, endDirection.y,
                    (currentPercentOfPole - threshold) / (1f - threshold));
            }
        }

        if (this.humanoid)
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
