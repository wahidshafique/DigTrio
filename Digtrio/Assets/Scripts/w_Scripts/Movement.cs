using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 2.5f;
    private Vector3 target;
    Quaternion targetRot;

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
		string test = "";
		GUI.Label(new Rect(0 + 130 * 2 , 0, 120, 100), test);

    }

    void Start() {
        target = transform.position;
    }

    void Update() {
        MoveToClick();
        Sniff();
    }

    void MoveToClick() {
		if (Application.isEditor) {
			if (Input.GetMouseButtonDown (0)) {
				target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				target.z = transform.position.z;
				targetRot = Quaternion.LookRotation (target - transform.position, Vector3.forward);
				targetRot.x = 0.0f;
				targetRot.y = 0.0f;
			} else if (Input.touchCount > 0) {
				target = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				target.z = transform.position.z;
				targetRot = Quaternion.LookRotation (target - transform.position, Vector3.forward);
				targetRot.x = 0.0f;
				targetRot.y = 0.0f;
			}
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * 8);
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		} else {
			if (Input.touchCount > 0) {
				target = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				target.z = transform.position.z;
				targetRot = Quaternion.LookRotation (target - transform.position, Vector3.forward);
				targetRot.x = 0.0f;
				targetRot.y = 0.0f;
			}
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * 8);
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		}
	}

    void Sniff() {
        RaycastHit2D foundHit = Physics2D.Raycast(transform.position, transform.right, 2, 1 << 8);
        //Debug.DrawRay(transform.position, target, Color.green, 2);
    }
}