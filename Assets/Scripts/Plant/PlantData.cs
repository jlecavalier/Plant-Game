using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyAwesomePlant", menuName = "Plant")]
public class PlantData : ScriptableObject {
    public string plantName = "My awesome plant";

    [System.Serializable]
    public class Sounds {
        public AudioClip musicalSound;
        public AudioClip watering;
        public AudioClip placing;
        public AudioClip growth;
        public AudioClip maturingFlowering;
        public AudioClip wilting;
        public AudioClip dying;
        public AudioClip error;
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
        public int ticksToMaturity = 10;
        public float ticksToNaturalDeath = 15;
        public float ticksToDeathAfterWilting = 5;
    }
    public TimingVariables timingVariables;

    [System.Serializable]
    public class SurvivalVariables {
        public int sunlightMax = 60;
        public int sunlightMin = 40;
        public int waterMax = 60;
        public int waterMin = 40;
    }
    public SurvivalVariables survivalVariables;

    [System.Serializable]
    public class StartingValues {
        public int startingWater = 50;
        public int startingSunlight = 50;
    }
    public StartingValues startingValues;
}
