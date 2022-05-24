using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TaskButton : MonoBehaviour
{
    [SerializeField] Image taskImage;
    [SerializeField] TMP_Text taskTittle;
    public Slider taskSlider;
    [SerializeField] private Button button;
    private void Start()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    public void InitButton(Sprite image, string tittle, Action onclick)
    {
        taskImage.sprite = image;
        taskTittle.text = tittle;
        taskSlider.value = 0;
        button.onClick.AddListener(delegate { onclick?.Invoke(); });
    }
}
