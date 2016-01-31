using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class TowerFieldsHighlight : MonoBehaviour
{

    public Piramida pyramid;
    LinkedList<GameObject> gameBlocks;

    public Tower fireTower;
    public Tower ghostTower;
    public Tower cannonTower;
    public Tower mageTower;

	// Use this for initialization
	void Start ()
    {

        // Change color of field.
        Shader fieldShader = Shader.Find("Standard");
        Material towerFieldHighlightedMat = new Material(fieldShader);

        towerFieldHighlightedMat.color = Color.blue;

        Renderer renderer = currentRaytracedField.gameObject.GetComponent<Renderer>();
        renderer.material = towerFieldHighlightedMat;

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

    public void GenerateTowers()
    {

        Pole currentRaytracedField = GetCurrentlyRaytracedField();

        if (currentRaytracedField != null)
        {

            if (!currentRaytracedField.poleNaWieze)
            {
                return;
            }

            


            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            Tower.TowerType towerType = Tower.TowerType.MAGE_TOWER;

            Vector3 fieldPos = currentRaytracedField.gameObject.transform.position;
            Vector3 towerPos = new Vector3();
            Quaternion towerRotation = Quaternion.Euler(0, 0, 0);

            // Choose appropiate prefab.
            Tower tower = null;
            switch (towerType)
            {

                case Tower.TowerType.CANNON_TOWER:
                    {
                        if (cannonTower == null) { return; }
                        towerPos = new Vector3(fieldPos.x, fieldPos.y + 1.55f, fieldPos.z + 1.7f);
                        tower = cannonTower;
                        tower.damage = 10;
                        tower.fireSpeed = 1;
                        break;
                    }

                case Tower.TowerType.FIRE_TOWER:
                    {
                        if (fireTower == null) { return; }
                        towerPos = new Vector3(fieldPos.x, fieldPos.y + 1.55f, fieldPos.z + 1.7f);
                        tower = fireTower;
                        tower.damage = 30;
                        tower.fireSpeed = 3;
                        break;
                    }

                case Tower.TowerType.GHOST_TOWER:
                    {
                        if (ghostTower == null) { return; }
                        towerPos = new Vector3(fieldPos.x + 0.21f, fieldPos.y + 0.79f, fieldPos.z + 0.38f);
                        tower = ghostTower;
                        tower.damage = 60;
                        tower.fireSpeed = 6;
                        break;
                    }

                case Tower.TowerType.MAGE_TOWER:
                    {
                        if (mageTower == null) { return; }
                        towerPos = new Vector3(fieldPos.x, fieldPos.y + 0.5f, fieldPos.z - 0.02f);
                        tower = mageTower;
                        tower.damage = 100;
                        tower.fireSpeed = 10;
                        break;
                    }

            }

            Instantiate(tower, towerPos, towerRotation);

            // Change color of field.
            Shader fieldShader2 = Shader.Find("Standard");
            Material towerFieldHighlightedMat2 = new Material(fieldShader2);

            towerFieldHighlightedMat2.color = Color.white;

            Renderer renderer2 = currentRaytracedField.gameObject.GetComponent<Renderer>();
            renderer2.material = towerFieldHighlightedMat2;

        }

    }

    private Pole GetCurrentlyRaytracedField()
    {

        // Check currently selected object by mouse.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {

            GameObject hitObject = hit.collider.gameObject;

            Pole field = hitObject.GetComponent<Pole>() as Pole;

            return field;

        }

        return null;

    }

}
