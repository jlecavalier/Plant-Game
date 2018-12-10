using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBackground : MonoBehaviour {

    public Sprite dayBackGround;
    public Sprite nightBackground;

    private Clock _clock;
    private SpriteRenderer _renderer;

    private void OnEnable() {
        _clock = Utils.GetClock();
        if (_clock) {
            _clock.Tick += ChangeSky;
        }
    }

    private void Start() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable() {
        if (_clock) {
            _clock.Tick -= ChangeSky;
        }
    }

    private void ChangeSky(float timeOfDay) {
        if (timeOfDay < 12f) {
            _renderer.sprite = dayBackGround;
        } else {
            _renderer.sprite = nightBackground;
        }
    }
}
