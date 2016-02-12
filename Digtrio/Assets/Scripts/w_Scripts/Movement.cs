using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //TODO replace with touch
    public float speed = 2.5f;
    private Vector3 target;

    void Start() {
        target = transform.position;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}