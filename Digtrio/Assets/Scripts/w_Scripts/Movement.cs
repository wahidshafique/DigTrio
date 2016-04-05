using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 2.5f;
    [Tooltip("type your collect clip name here")]
    public string audioCollectName = "gold";
    [Tooltip("type your hit clip name here")]
    public string audioHitName = "hit1";
    private float accStartY;
    private Animator anim;
    private AudioClip pickSound;
    private AudioClip hitSound;
    private Vector3 target;
    private Rigidbody2D rig;
    Quaternion targetRot;
    TrailRenderer[] trails;
    static public bool canMove = true;
    public bool debugAccelMove;
    //void OnGUI() {
    //    foreach (Touch touch in Input.touches) {
    //        string message = "";
    //        message += "ID: " + touch.fingerId + "\n";
    //        message += "Phase: " + touch.phase.ToString() + "\n";
    //        message += "TapCount: " + touch.tapCount + "\n";
    //        message += "Pos X : " + touch.position.x + "\n";
    //        message += "Pos Y : " + touch.position.y + "\n";

    //        int num = touch.fingerId;
    //        GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
    //    }
    //    string test = "";
    //    GUI.Label(new Rect(0 + 130 * 2, 0, 120, 100), test);

    //}
    void OnGUI() {
        string message = speed.ToString();
        GUI.Label(new Rect(0 + 130 * 2, 0, 120, 100), message);
    }

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        pickSound = Resources.Load("Media/" + audioCollectName) as AudioClip;
        hitSound = Resources.Load("Media/" + audioHitName) as AudioClip;
        trails = GetComponentsInChildren<TrailRenderer>();
        anim = GetComponent<Animator>();
        target = transform.position;
        accStartY = Input.acceleration.y;
    }

    void FixedUpdate() {
        Vector2 pos = rig.position;
        pos.y = Mathf.Clamp(pos.y, -126f, -0.19f);
        pos.x = Mathf.Clamp(pos.x, -12, 12);
        rig.position = pos;
    }

    void Update() {
        SpeedToggle();
        if (canMove) {
            if (Menu.accelActive && debugAccelMove) Accel(); else MoveToClick();
            Sniff();
            CheckColor();
        } else {
            target = this.transform.position;
        }
    }

    void Accel() {
        float x = Input.acceleration.x;
        float y = Input.acceleration.y - accStartY;
        Vector2 dir = new Vector2(x, y);
        //transform.Translate(Input.acceleration.x, Input.acceleration.y, -Input.acceleration.z);
        if (dir.sqrMagnitude > 1) dir.Normalize();
        Vector2 pos = transform.position;
        pos += dir * speed * Time.deltaTime;
        transform.position = pos;

        print("using accel input now");
    }

    void MoveToClick() {
        if (Application.isEditor) {
            if (Input.GetMouseButtonDown(0)) {
                anim.SetBool("IsMoving", true);
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                targetRot = Quaternion.LookRotation(target - transform.position, Vector3.forward);
                targetRot.x = 0.0f;
                targetRot.y = 0.0f;
            } else if (Input.touchCount > 0) {
                target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                target.z = transform.position.z;
                targetRot = Quaternion.LookRotation(target - transform.position, Vector3.forward);
                targetRot.x = 0.0f;
                targetRot.y = 0.0f;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 8);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        } else {
            if (Input.touchCount > 0) {
                anim.SetBool("IsMoving", true);
                target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                target.z = transform.position.z;
                targetRot = Quaternion.LookRotation(target - transform.position, Vector3.forward);
                targetRot.x = 0.0f;
                targetRot.y = 0.0f;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 8);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (gameObject.transform.position == target) anim.SetBool("IsMoving", false);
    }

    void Sniff() {
        RaycastHit2D foundHit = Physics2D.Raycast(transform.position, transform.right, 2, 1 << 8);
        //Debug.DrawRay(transform.position, target, Color.green, 2);
    }

    void CheckColor() {
        int locale = (int)this.transform.position.y;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        string currentTag = null;
        if (hit.collider.tag == "bg" && hit.collider.tag != currentTag) {
            int spliced = int.Parse(hit.collider.name.Substring(2, 1));
            Enabler(spliced - 1);
            currentTag = hit.collider.tag;
        }
    }
    void Enabler(int index) {//enable the index, disable the other children
        for (int i = 0; i < trails.Length; i++) {
            if (i == index) {
                trails[index].enabled = true;
                continue;
            }
            trails[i].enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "item") {
            AudioSource.PlayClipAtPoint(pickSound, Vector3.zero);
        }
        if (other.tag == "enemy") {
            AudioSource.PlayClipAtPoint(hitSound, Vector3.zero);
        }
    }

    void SpeedToggle() { //super secret speed toggle thing
        if (Input.GetKeyDown(KeyCode.Escape)) {
            speed++;
        }
    }
}
