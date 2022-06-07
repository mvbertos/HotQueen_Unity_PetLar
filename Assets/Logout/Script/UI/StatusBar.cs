using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider mood_slider, food_slider;
    [SerializeField] private TMP_Text moneyText;
    private void Update()
    {
        //get ong
        ONG ong = GameObject.FindObjectOfType<ONG>();
        moneyText.text = "$ " + ong.Money.ToString();
    }
}
