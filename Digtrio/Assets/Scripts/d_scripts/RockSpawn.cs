using UnityEngine;
using System.Collections;

public class RockSpawn : MonoBehaviour {
    GameObject rockPrefab;
    float interval;
    float time;

    public float lowerTime, upperTime;
    public float distanceOffScreen;

	// Use this for initialization
	void Awake () {
	    rockPrefab = Resources.Load<GameObject>("Prefabs/stone");        
	}

    void Start()
    {
        interval = GetRandomTime();
    }
	
	// Update is called once per frame
	void Update () {
	    time += Time.deltaTime;

        if (time > interval)
        {
            SpawnStone();
            
            interval = GetRandomTime();
            time = 0.0f;
        }
	}

    void SpawnStone()
    {
        Instantiate(rockPrefab, GetSpawnPosition(), Quaternion.identity); 
    }

    Vector3 GetSpawnPosition()
    {
        float x = Random.Range(0.0f, 1.0f);
        float y = Mathf.Clamp(distanceOffScreen+1.0f, 1.0f, 2.0f);

        Vector3 position = new Vector3(x, y, 1);

        return Camera.main.ViewportToWorldPoint(position);
    }

    float GetRandomTime()
    {
        return Random.Range(lowerTime, upperTime);
    }
}
