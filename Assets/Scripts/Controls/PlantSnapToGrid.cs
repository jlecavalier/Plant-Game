using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragAndDrop))]
[RequireComponent(typeof(Plant))]
public class PlantSnapToGrid : MonoBehaviour {

    private DragAndDrop _dragAndDrop;
    private Plant _plant;
    private HomeGrid _grid;
    private AudioSource _audioSource;

    private void OnEnable() {
        _dragAndDrop = GetComponent<DragAndDrop>();
        _dragAndDrop.OnDropped += Snap;
    }

    private void Start() {
        _plant = GetComponent<Plant>();
        _grid = Utils.GetHomeGrid();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable() {
        _dragAndDrop.OnDropped -= Snap;
        _dragAndDrop.OnGrabbed -= Snap;
    }

    public void Snap(Vector3 desiredPosition) {

        GameObject compost = GameObject.FindGameObjectWithTag("Compost");
        if (compost) {
            if (Vector2.Distance(desiredPosition, compost.transform.position) <= .5f) {
                AudioSource src = compost.GetComponent<AudioSource>();
                if (src && _plant.data.sounds.compost != null) {
                    src.PlayOneShot(_plant.data.sounds.compost);
                }

                // Remove the plant from the old grid square.
                if (_plant.HasBeenPlaced) {
                    PlantSpace old = _grid.GetPlantSpaceAtPosition(_plant.LastGridPosition);
                    if (old) {
                        old.CurrentPlant = null;
                    }
                }

                _plant.Init();

                Spanner.MasterObjectPooler.Instance.Return(gameObject);
                return;
            }
        }

        if (!_grid) {
            _grid = Utils.GetHomeGrid();
        }

        Vector3 targetPosition = new Vector3(Mathf.Round(desiredPosition.x), Mathf.Round(desiredPosition.y), transform.position.z);
        if (targetPosition.x < 0 || targetPosition.x > 9 || targetPosition.y < 0 || targetPosition.y > 9) {
            if (_plant.HasBeenPlaced) {
                transform.position = _plant.LastGridPosition;
            } else {
                Spanner.MasterObjectPooler.Instance.Return(gameObject);
            }

            if (_audioSource && _plant.data.sounds.error != null) {
                _audioSource.PlayOneShot(_plant.data.sounds.error);
            }
        } else {
            if (_grid) {
                // Add the plant to the grid square it was dropped on.
                PlantSpace ps = _grid.GetPlantSpaceAtPosition(targetPosition);
                if (ps) {
                    if (ps.CurrentPlant == null) {
                        transform.position = targetPosition;

                        // Remove the plant from the old grid square.
                        if (_plant.HasBeenPlaced) {
                            PlantSpace old = _grid.GetPlantSpaceAtPosition(_plant.LastGridPosition);
                            if (old) {
                                old.CurrentPlant = null;
                            }
                        }

                        _plant.HasBeenPlaced = true;
                        _plant.LastGridPosition = targetPosition;
                        ps.CurrentPlant = _plant;
                        if (_audioSource && _plant.data.sounds.placing != null) {
                            _audioSource.PlayOneShot(_plant.data.sounds.placing);
                        }
                    } else {
                        if (_plant.HasBeenPlaced) {
                            transform.position = _plant.LastGridPosition;
                        } else {
                            Spanner.MasterObjectPooler.Instance.Return(gameObject);
                        }

                        if (_audioSource && _plant.data.sounds.error != null) {
                            _audioSource.PlayOneShot(_plant.data.sounds.error);
                        }
                    }
                }
            }
        }
    }
}
