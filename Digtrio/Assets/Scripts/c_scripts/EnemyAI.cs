using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    Kinematic enemy = new Kinematic();
    KinematicWander wander = new KinematicWander();

	// Use this for initialization
	void Start () {
        enemy.position = transform.position;
        enemy.orientation = 0;
        enemy.velocity = Vector2.zero;
        enemy.rotation = 0;
        wander.SetWander(enemy, 0.01f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        enemy.UpdatePositon(wander.GetSteering());
        //transform.position = enemy.position;
    }

}
