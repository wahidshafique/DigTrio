using UnityEngine;
using System.Collections;

public class ItemShine : MonoBehaviour {

    public float effectDuration;
    private SpriteRenderer sr;
    private Color32 a, b;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        a = sr.color;
        b = Color.white;
        StartCoroutine(WaitRandomTime());
	}

    private IEnumerator Flash()
    {
        sr.color = b;
        yield return new WaitForSeconds(effectDuration);
        sr.color = a;
        yield return new WaitForSeconds(effectDuration);
        sr.color = b;
        yield return new WaitForSeconds(effectDuration);
        sr.color = a;
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(Flash());
    }

    private IEnumerator WaitRandomTime()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 2.0f));
        StartCoroutine(Flash());
    }
}
