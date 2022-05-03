using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Perfil : MonoBehaviour
{
    [System.Serializable]
    public struct Data
    {

        public string Name;
        public Sprite Picture;
        public string Personality;
    }

    [SerializeField] private TMP_Text text_name;
    [SerializeField] private Image image_userImage;
    [SerializeField] private TMP_Text text_personality;

    [HideInInspector] public Data current_data;

    private void OnEnable()
    {
        text_name.text = current_data.Name;
        image_userImage.sprite = current_data.Picture;
        text_personality.text = current_data.Personality;
    }
    private void OnDisable()
    {
        text_name.text = "";
        image_userImage.sprite = null;
        text_personality.text = "";
    }
}
