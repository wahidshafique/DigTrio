using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamicStretch : MonoBehaviour {
    
    [Tooltip ("A is the bubble's position, B is the speaker's position.")]
    public Transform A, B;

    // The following store values for use in calculations
    private Vector3 startPosition = new Vector3(0f, 0f, 0f);
    private Vector3 endPosition = new Vector3(5f, 1f, 0f);

    [Tooltip("Mirro Sprite if necessary.")]
    public bool mirrorZ = true;

    [Tooltip("Shift the bubble's tail's center position on the x axis.")]
    public float xOffset = 1.0f;
    [Tooltip("Shift the bubble's tail's center position on the y axis.")]
    public float yOffset = 1.0f;

    // APosition stores the position of the A point(bubble's position) for calculations
    private Vector3 APosition = Vector3.zero;

    [Tooltip("X axis' scale is multiplied by this to fit better between the speaker and the bubble.")]
    public float xScaleFactor = 0.4f;
    [Tooltip("Scales the whole bubble's tail's sprite if necessary.")]
    public float overallScaleFactor = 8.0f;

    void Update()
    {
        UpdateAPosition();
        startPosition = A.position;
        endPosition = B.position;
        ScaleBubbleTail(gameObject, startPosition, endPosition, mirrorZ);
    }

    /// <summary>
    /// Scales the buuble's tail between the speaker and the bubble.
    /// </summary>
    private void ScaleBubbleTail(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;

        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;

        if (_mirrorZ)
            _sprite.transform.right *= -1f;

        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition) * xScaleFactor;
        scale *= overallScaleFactor;
        _sprite.transform.localScale = scale;
    }

    /// <summary>
    /// Updates the bubble's tail's bubble-end position to match the camera's + the offsets
    /// </summary>
    private void UpdateAPosition()
    {
        APosition.x = Camera.main.transform.position.x + xOffset;
        APosition.y = Camera.main.transform.position.y + yOffset;
        APosition.z = 0;

        A.position = APosition;
    }
}
