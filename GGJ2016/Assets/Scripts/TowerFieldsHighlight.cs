using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerFieldsHighlight : MonoBehaviour
{

    public Piramida pyramid;
    LinkedList<GameObject> gameBlocks;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void HighlightTowerPossibleFields()
    {

        Platforma[] platformy = pyramid.platformy;

        Shader fieldShader = Shader.Find("Standard");
        Material towerFieldHighlightedMat = new Material(fieldShader);

        towerFieldHighlightedMat.color = Color.green;


        for( int i = 0; i < platformy.Length; i ++)
        {

            Platforma platforma = platformy[i];

            AssignTowerFieldsToPlatform(platforma);

            foreach( Pole pole in platforma.fields )
            {

                Renderer renderer = pole.gameObject.GetComponent<Renderer>();
                renderer.material = towerFieldHighlightedMat;

            }

        }

    }

    private void AssignTowerFieldsToPlatform( Platforma platforma )
    {

        if( platforma.fields == null || platforma.fields.Length == 0 )
        {
            Pole[] fields = platforma.GetComponentsInChildren<Pole>();
            platforma.fields = fields;
        }

    }

}
