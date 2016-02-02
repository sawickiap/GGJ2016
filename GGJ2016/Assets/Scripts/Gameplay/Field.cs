using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Field : MonoBehaviour
{

    public Platform platform;
	public bool onPyramid = true;

	public Field successor;

	// For fields on path that could touch pyramid when it raises up;
	public bool hasAlternativeOnPyramid = false;
	public int minPyramidLevelForAlternativeNastepnik;
	public Field alternativeSuccessorOnPyramid;

    public bool onCrossroads;
    public int crossroadID;
    public bool isFieldForTower;
    public bool isStairs;

    public Tower tower;

    //public GameObject 

    public Field getSuccessor()
    {

		if(onPyramid)
        {

            if (this.isFieldForTower)
            {
                return null;
            }
            else if (this.onCrossroads)//platforms without stairs and not zero
            {

                int myPlatformID = this.platform.ID;
                Platform nextPlatform = Pyramid.singleton.platforms[myPlatformID - 1];

                if (nextPlatform.rotation % 2 != crossroadID % 2)
                {
                    return successor;
                }
                else
                {

                    if (nextPlatform.rotation == this.crossroadID)
                    {
                        return nextPlatform.stairs1;
                    }
                    else
                    {
                        return nextPlatform.stairs2;
                    }

                }

            }
            else
            {

                if (this.platform.ID % 2 == 1 || this.platform.ID == 0)
                {
                    return this.successor; //platforms without stairs
                }
                else
                {

                    int nextCenterFieldID = 0;

                    //jesli jestem schodkiem 1
                    if (this == this.platform.stairs1)
                    {
                        nextCenterFieldID = this.platform.rotation;
                    }
                    //jesli jestem schodkiem 2
                    else
                    {

                        if (this == this.platform.stairs2)
                        {
                            nextCenterFieldID = (this.platform.rotation + 2) % 4;
                        }

                    }

                    return Pyramid.singleton.platforms[this.platform.ID - 1].wallsCenterFields[nextCenterFieldID];

                }

            }

		}
		else
        {
			if(hasAlternativeOnPyramid)
            {

                int pyramidLevel = Pyramid.singleton.GetLevel();
                if ( pyramidLevel >= this.minPyramidLevelForAlternativeNastepnik)
                {
                    return this.alternativeSuccessorOnPyramid;
                }
                else
                {
                    return successor;
                }

			}
			else return successor;

		}

    }


	// Use this for initialization
	void Start ()
    
    {
		if(onPyramid){
			this.platform = transform.parent.GetComponent<Platform>();
			Debug.Assert(this.platform != null);

			if(platform.ID % 2 == 1){
				int idOnPlatform = transform.GetSiblingIndex();
				this.successor = transform.parent.GetChild( (idOnPlatform+1)%platform.getNumberOfFieldsOnPlatform() ).GetComponent<Field>();
			}
			//else this.successor = this;
		}

	}
	
	// Update is called once per frame
	void Update ()
    {

        Field localSuccessor = getSuccessor();
        if (localSuccessor)
        {

            Debug.DrawLine(
                transform.position,
                localSuccessor.transform.position,
                Color.blue,
                0f,
                false);

        }

	}

}
