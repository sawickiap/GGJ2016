using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerFieldsHighlight : MonoBehaviour
{

    LinkedList<GameObject> gameBlocks;

	// Use this for initialization
	void Start ()
    {
	    


	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void HighlightTowerPossibleFields( Piramida pyramid )
    {

        Platforma[] platformy = pyramid.platformy;

        for( int i = 0; i < platformy.Length; i ++)
        {

            Platforma platforma = platformy[i];

            foreach( Pole pole in platforma.fields )
            {

                //pole.

            }

        }

    }

}
