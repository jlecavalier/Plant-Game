using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHorizontal : MonoBehaviour {

    public float speed;
    public float maximum;

    private Vector3 _translation;
    private Vector3 _startingPosition;

    private void Start() {
        _startingPosition = transform.position;
        _translation = new Vector3(speed, 0, 0);
    }

    void Update() {
        transform.Translate(_translation * Time.deltaTime);
        if (transform.position.x >= _startingPosition.x + maximum) {
            transform.position = _startingPosition;
        }
    }
}
