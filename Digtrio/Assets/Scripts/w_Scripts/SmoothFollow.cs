using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    public float dampTime = 0.15f;
    public Transform target;
    [Tooltip("these clamp the X camera so it does not bleed offscreen")]
    public float minPosX = -16;
    public float maxPosX = 16;
    [Tooltip("these clamp the Y camera so it does not bleed offscreen")]
    public float minPosY = -16;
    public float maxPosY = 16;
    private Vector3 velocity = Vector3.zero;
    private Vector3 pos;
    Camera camera;

    void Start() {
        pos = Camera.main.transform.position;
        camera = GetComponent<Camera>();
    }

    void Update() {
        if (target) {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
    void LateUpdate() {
        pos.x = Mathf.Clamp(transform.position.x, minPosX, maxPosX);
        pos.y = Mathf.Clamp(transform.position.y, minPosY, maxPosY);
        Camera.main.transform.position = pos;

    }
}