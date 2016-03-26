using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamicStretch : MonoBehaviour {
    public Transform A, B;
    public Vector3 startPosition = new Vector3(0f, 0f, 0f);
    public Vector3 endPosition = new Vector3(5f, 1f, 0f);
    public bool mirrorZ = true;

    public float xOffset = 1.0f;
    public float yOffset = 1.0f;
    private Vector3 APosition = Vector3.zero;

    public float scaleOffset = 10.0f;

    void Start()
    {
        UpdateAPosition();
        startPosition = A.position;
        endPosition = B.position;
        Strech(gameObject, startPosition, endPosition, mirrorZ);
    }

    void Update()
    {
        UpdateAPosition();
        startPosition = A.position;
        endPosition = B.position;
        Strech(gameObject, startPosition, endPosition, mirrorZ);
    }

    public void Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;

        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;

        if (_mirrorZ)
            _sprite.transform.right *= -1f;

        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition) * scaleOffset;
        _sprite.transform.localScale = scale;
    }

    private void UpdateAPosition()
    {
        APosition.x = Camera.main.transform.position.x + xOffset;
        APosition.y = Camera.main.transform.position.y + yOffset;
        APosition.z = 0;

        A.position = APosition;
    }
}
