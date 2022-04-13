using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStatus : MonoBehaviour
{
    [SerializeField] private Slider mood_slider, energy_slider, food_slider;

    private void Update()
    {
        mood_slider.value = GameManager._ong.mood / GameManager._ong.MAX_VALUE;
        energy_slider.value = GameManager._ong.energy / GameManager._ong.MAX_VALUE;
        food_slider.value = GameManager._ong.food / GameManager._ong.MAX_VALUE;
    }
}
