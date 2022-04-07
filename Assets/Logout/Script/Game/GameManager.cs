using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider MoodSlider;
    void Start()
    {

    }
    private void Update()
    {
        int count = 0;
        float moodCount = 0;

        foreach (PetStatusManager item in FindObjectsOfType<PetStatusManager>())
        {
            count += 1;
            moodCount += item.status.Mood;
        }

        //MoodBar
        float moodMedia = moodCount / count;
        MoodSlider.value = moodMedia / 100;
    }
}
