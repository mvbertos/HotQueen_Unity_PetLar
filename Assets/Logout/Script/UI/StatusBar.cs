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
        // if (mood_slider)
        //     mood_slider.value = GameManager._ong.mood / GameManager._ong.MAX_VALUE;

        // if (money_Text)
        //     money_Text.text = "$" + GameManager._ong.money.ToString();

        // if (food_slider)
        //     food_slider.value = GameManager._ong.food / GameManager._ong.MAX_VALUE;
    }
}
