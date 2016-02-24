using UnityEngine;
using System.Collections;

public class RandomBackground : MonoBehaviour {

    public GameObject[] backgroundElements;
    public int numToSpawn;

	// Use this for initialization
	void Start () {
        SpawnBackgroundElements();
	}

    public void SpawnBackgroundElements()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject temp = Instantiate(backgroundElements[Random.Range(0, backgroundElements.Length)],
                                new Vector2(Random.Range(WorldBounds.Get.left, WorldBounds.Get.right),
                                Random.Range(WorldBounds.Get.top - 10, WorldBounds.Get.bottom)),
                                Quaternion.identity) as GameObject;
            temp.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            temp.transform.parent = this.transform;
        }
    }
	
}
