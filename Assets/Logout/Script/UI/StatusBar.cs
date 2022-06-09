using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider moodSlider, foodSlider;
    [SerializeField] private TMP_Text moneyText;
    private void Update()
    {
        ONG ong = GameObject.FindObjectOfType<ONG>();

        //get and update monetText
        if (moneyText)
            moneyText.text = "$ " + ong.Money.ToString();
            
        //get and update foodSlider
        if (foodSlider)
            foodSlider.value = ong.Food / ong.maxFoodValue;
    }
}
