using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour {

    private const int SEEDLING = 0;
    private const int MATURE = 1;
    private const int DEAD = 2;

    public PlantData data;
    private SpriteRenderer _renderer;

    private int _stageOfLife;
    private int _waterLevel;

    private bool _wilted;

    // Tickers
    private int _ticksAsSeedling;
    private int _ticksAsMature;
    private int _ticksWilted;

    public delegate void OnPlantWasWateredHandler(Plant plant);
    public event OnPlantWasWateredHandler OnPlantWasWatered;

    private void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        _stageOfLife = SEEDLING;
        _waterLevel = data.startingValues.startingWater;
        _ticksAsSeedling = 0;
        _ticksAsMature = 0;
        _ticksWilted = 0;
        _wilted = false;
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

    public void WaterPlant() {
        if (OnPlantWasWatered != null) {
            OnPlantWasWatered(this);
        }
        _waterLevel += 5;
    }

    public void ClockTick() {
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

    public void ConsumeWater() {
        _waterLevel = Mathf.Clamp(_waterLevel - 5, 0, 100);
        //Debug.Log(_waterLevel);
    }

    public void CheckHealth() {
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

    public void UpdateStageOfLife() {
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

    private void Mature() {
        _stageOfLife = MATURE;
        if (_wilted) {
            _renderer.sprite = data.sprites.matureWilted;
        } else {
            _renderer.sprite = data.sprites.matureHealthy;
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
        }
    }

    private void Die() {
        _stageOfLife = DEAD;
        _renderer.sprite = data.sprites.dead;
    }

    private bool IsInHealthyRange() {
        return _waterLevel <= data.survivalVariables.waterMax && _waterLevel >= data.survivalVariables.waterMin;
    }
}
