using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Perfil : MonoBehaviour
{


    [SerializeField] private TMP_Text text_name;
    [SerializeField] private Image image_userImage;
    [SerializeField] private TMP_Text text_personality;

    [HideInInspector] public EntityData current_data;

    private void OnEnable()
    {
        text_name.text = current_data.Name;
        image_userImage.sprite = current_data.Picture;
        text_personality.text = current_data.Personality.ToString();
    }
    private void OnDisable()
    {
        text_name.text = "";
        image_userImage.sprite = null;
        text_personality.text = "";
    }
}
[System.Serializable]
public class EntityData
{

    public string Name;
    public Sprite Picture;
    public Personalities Personality;

    public enum Personalities
    {
        Cute,
        Mad,
        Playfull
    }

    public EntityData(string name, Sprite picture, Personalities personality)
    {
        Name = name;
        Picture = picture;
        Personality = personality;
    }
}

[System.Serializable]
public class PetData : EntityData
{
    public Species specie;
    public PetData(string name, Sprite picture, Personalities personality,Species species) : base(name, picture, personality)
    {
        this.specie = species;
    }
}

public enum Species
{
    DEFAULT = 0,
    POODLE = 1,
    GOLDENRETRIVER = 2,
}