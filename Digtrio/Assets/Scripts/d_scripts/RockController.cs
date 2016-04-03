using UnityEngine;
using System.Collections;

public class RockController : MonoBehaviour {
    Plane[] planes;
    bool isOnScreen = false;
	
	// Update is called once per frame
	void Update () {
	    planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        // if off the screen the destroy the gameobject
        if (isOnScreen && !CheckBounds())
        {
            Destroy(gameObject);
        }

        // make sure the rock has entered the screen
        if (!isOnScreen && CheckBounds())
        {
            isOnScreen = true;
        }
	}

    bool CheckBounds()
    {
        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider2D>().bounds))
            return true;

        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // stun the player on collision
        if (other.CompareTag("Player"))
        {
            other.SendMessage("Stun");

            Destroy(gameObject);
        }
    }
}
