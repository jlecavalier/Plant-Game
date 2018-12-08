using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "PlantData")]
public class PlantData : ScriptableObject {
    public string plantName = "My awesome plant";

    [System.Serializable]
    public class Sounds {
        public AudioClip musicalSound;
        public AudioClip watering;
        public AudioClip placing;
        public AudioClip growth;
        public AudioClip maturingFlowering;
    }
    public Sounds sounds;
    
    [System.Serializable]
    public class Sprites {
        public Sprite seedlingHealthy;
        public Sprite seedlingWilted;
        public Sprite matureHealthy;
        public Sprite matureWilted;
        public Sprite dead;
    }
    public Sprites sprites;

    [System.Serializable]
    public class TimingVariables {
        public float secondsToMaturity = 60f;
        public float secondsToNaturalDeath = 60f;
    }
    public TimingVariables timingVariables;

    [System.Serializable]
    public class SurvivalVariables {
        public int perfectSunlightValue = 50;
        public int sunlightTolerancePositive = 10;
        public int sunlightToleranceNegative = 10;
        public int perfectWaterValue = 50;
        public int waterTolerancePositive = 10;
        public int waterToleranceNegative = 10;
    }
    public SurvivalVariables survivalVariables;

    [System.Serializable]
    public class StartingValues {
        public int startingWater = 50;
        public int startingSunlight = 50;
    }
    public StartingValues startingValues;
}
