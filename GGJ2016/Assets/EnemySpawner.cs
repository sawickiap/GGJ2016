using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameController gameController;
    public float spawnInterval = 5f;
    public Enemy enemyPrefab;

    private float nextSpawnTime;

	// Use this for initialization
	void Start () {
        Debug.Assert(gameController);
        Debug.Assert(enemyPrefab);
        nextSpawnTime = Time.time + spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > nextSpawnTime)
        {
            nextSpawnTime += spawnInterval;
            SpawnEnemy();
        }
	}

    void SpawnEnemy()
    {
        if (enemyPrefab)
        {
            Vector3 pos = transform.position;
            pos.y += enemyPrefab.offsetY + 1f;
            Enemy newEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity) as Enemy;
            if (gameController)
                gameController.enemies.AddLast(newEnemy);
        }
    }
}
