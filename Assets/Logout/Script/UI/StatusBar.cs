using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider mood_slider, food_slider;
    [SerializeField] private TMP_Text money_Text;
    private void Update()
    {
        mood_slider.value = GameManager._ong.mood / GameManager._ong.MAX_VALUE;
        money_Text.text = "$" + GameManager._ong.money.ToString();
        food_slider.value = GameManager._ong.food / GameManager._ong.MAX_VALUE;
    }
}
