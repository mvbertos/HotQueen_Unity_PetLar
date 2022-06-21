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

    public void UpdateInfo(EntityData data)
    {
        current_data = data;
        text_name.text = current_data.Name;
        image_userImage.sprite = current_data.Picture;
        text_personality.text = current_data.Personality.ToString();
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
    public bool isDog = true;
    public Species specie;
    public PetData(string name, Sprite picture, Personalities personality, Species species,bool isDog) : base(name, picture, personality)
    {
        this.specie = species;
        this.isDog = isDog;
    }
}

public enum Species
{
    DEFAULT = 0,
    OrangeCat = 1,
    GrayCat = 2,
    BlackDog = 3,
    BrownDog = 4,
    DarkBrownDog = 5,
}