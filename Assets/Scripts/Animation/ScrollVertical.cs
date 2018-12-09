using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollVertical : MonoBehaviour {

    public float speed;
    public float maximum;

    private Vector3 _translation;
    private Vector3 _startingPosition;

    private void Start() {
        _startingPosition = transform.position;
        _translation = new Vector3(0, -speed, 0);
    }

    void Update () {
        transform.Translate(_translation * Time.deltaTime);
        if (transform.position.y <= _startingPosition.y + maximum) {
            transform.position = _startingPosition;
        }
	}
}
