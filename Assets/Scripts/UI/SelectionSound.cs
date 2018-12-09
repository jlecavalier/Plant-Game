using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSound : MonoBehaviour {

    public List<AudioClip> sounds;
    private AudioSource _source;

    private void Start() {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound() {
        if (sounds.ToArray().Length > 0) {
            AudioClip clip = sounds[Random.Range(0, sounds.ToArray().Length)];
            if (_source) {
                _source.PlayOneShot(clip);
            }
        }
    }
}
