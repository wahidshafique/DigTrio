using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 2.5f;
    private Vector3 target;
    
    void OnGUI() {
        foreach (Touch touch in Input.touches) {
            string message = "";
            message += "ID: " + touch.fingerId + "\n";
            message += "Phase: " + touch.phase.ToString() + "\n";
            message += "TapCount: " + touch.tapCount + "\n";
            message += "Pos X : " + touch.position.x + "\n";
            message += "Pos Y : " + touch.position.y + "\n";

            int num = touch.fingerId;
            GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
        }
    }

    void Start() {
        target = transform.position;
    }

    void Update() {
        if (Application.isEditor) {
            if (Input.GetMouseButtonDown(0)) {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}