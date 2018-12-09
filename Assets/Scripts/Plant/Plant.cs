using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spanner;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(DragAndDrop))]
public class Plant : MonoBehaviour {

    private const int SEEDLING = 0;
    private const int MATURE = 1;
    private const int DEAD = 2;

    public PlantData data;
    private SpriteRenderer _renderer;
    private AudioSource _audioSource;

    private int _stageOfLife;
    private int _waterLevel;

    private bool _wilted;

    // Tickers
    private int _ticksAsSeedling;
    private int _ticksAsMature;
    private int _ticksWilted;

    private Vector3 _startPosition;

    public delegate void OnPlantWasWateredHandler(Plant plant);
    public event OnPlantWasWateredHandler OnPlantWasWatered;

    private void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
        Init();
    }

    private Vector3 _lastGridPosition;
    public Vector3 LastGridPosition {
        get { return _lastGridPosition; }
        set { _lastGridPosition = value; }
    }

    private bool _hasBeenPlaced;
    public bool HasBeenPlaced {
        get { return _hasBeenPlaced; }
        set { _hasBeenPlaced = value; }
    }

    private DragAndDrop _dragAndDrop;

    public void Init() {
        _stageOfLife = SEEDLING;
        _waterLevel = data.startingValues.startingWater;
        _ticksAsSeedling = 0;
        _ticksAsMature = 0;
        _ticksWilted = 0;
        _wilted = false;
    }

    private void OnEnable() {
        _dragAndDrop = GetComponent<DragAndDrop>();
        _dragAndDrop.OnGrabbed += Grab;
    }

    private void OnDisable() {
        _dragAndDrop.OnGrabbed -= Grab;
    }

    public void WaterPlant() {
        if (OnPlantWasWatered != null) {
            OnPlantWasWatered(this);
        }
        _waterLevel += 5;

        if (_audioSource && data.sounds.watering != null) {
            _audioSource.PlayOneShot(data.sounds.watering);
        }
    }

    public void ClockTick() {
        if (_stageOfLife != DEAD) {
            if (_wilted) {
                _ticksWilted += 1;
            }

            switch (_stageOfLife) {
            case SEEDLING:
                _ticksAsSeedling += 1;
                break;
            case MATURE:
                _ticksAsMature += 1;
                break;
            case DEAD:
                break;
            default:
                break;
            }
        }
    }

    public void ConsumeWater() {
        if (_stageOfLife != DEAD) {
            _waterLevel = Mathf.Clamp(_waterLevel - 5, 0, 100);
            //Debug.Log(_waterLevel);
        }
    }

    public void CheckHealth() {
        if (_stageOfLife != DEAD) {
            if (_wilted) {
                // Die if wilted for too long
                if (_ticksWilted >= data.timingVariables.ticksToDeathAfterWilting) {
                    Die();
                } else {
                    // Heal if wilted and is back in healthy range
                    if (IsInHealthyRange()) {
                        Heal();
                    }
                }
            } else {
                // Wilt if water level is wrong
                if (_waterLevel > data.survivalVariables.waterMax || _waterLevel < data.survivalVariables.waterMin) {
                    Wilt();
                }
            }
        }
    }

    public void UpdateStageOfLife() {
        if (_stageOfLife != DEAD) {
            switch (_stageOfLife) {
            case SEEDLING:
                // Mature if survived as seedling for long enough.
                if (_ticksAsSeedling >= data.timingVariables.ticksToMaturity) {
                    Mature();
                }
                break;
            case MATURE:
                // Die if survived as mature for long enough.
                if (_ticksAsMature >= data.timingVariables.ticksToNaturalDeath) {
                    Die();
                }
                break;
            case DEAD:
                break;
            default:
                break;
            }
        }
    }

    private void Mature() {
        _stageOfLife = MATURE;
        if (_wilted) {
            _renderer.sprite = data.sprites.matureWilted;
        } else {
            _renderer.sprite = data.sprites.matureHealthy;
        }

        if (_audioSource && data.sounds.maturingFlowering != null) {
            _audioSource.PlayOneShot(data.sounds.maturingFlowering);
        }
    }

    private void Wilt() {
        _wilted = true;
        if (_stageOfLife == SEEDLING) {
            _renderer.sprite = data.sprites.seedlingWilted;
        } else {
            _renderer.sprite = data.sprites.matureWilted;
        }
    }

    private void Heal() {
        if (_wilted) {
            _wilted = false;
            _ticksWilted = 0;
            if (_stageOfLife == SEEDLING) {
                _renderer.sprite = data.sprites.seedlingHealthy;
            } else {
                _renderer.sprite = data.sprites.matureHealthy;
            }

            if (_audioSource && data.sounds.growth != null) {
                _audioSource.PlayOneShot(data.sounds.growth);
            }
        }
    }

    private void Die() {
        _stageOfLife = DEAD;
        _renderer.sprite = data.sprites.dead;
    }

    private bool IsInHealthyRange() {
        return _waterLevel <= data.survivalVariables.waterMax && _waterLevel >= data.survivalVariables.waterMin;
    }

    private void Grab(Vector3 mousePosition) {
        GameObject plantObject = MasterObjectPooler.Instance.Get(data.plantName);
        if (plantObject) {
            Plant plant = plantObject.GetComponent<Plant>();
            plant.Init();
            plantObject.transform.position = _startPosition;
        }

        if (_audioSource && data.sounds.musicalSound != null) {
            _audioSource.PlayOneShot(data.sounds.musicalSound);
        }
    }
}
