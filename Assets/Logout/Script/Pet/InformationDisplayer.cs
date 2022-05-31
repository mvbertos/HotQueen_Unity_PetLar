using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Slider moodSlider;

    public void UpdateInformation(string name, float mood)
    {
        nameText.text = name;
        moodSlider.value = mood;
    }
}
