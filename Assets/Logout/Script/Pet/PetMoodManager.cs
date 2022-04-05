using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetMoodManager : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private Slider m_slider;
    //mood will be reduced, when pet is nerby bad things like empty food pot, dirty corner and else.
    public float mood { private set; get; }

    void Update()
    {
        mood = 100;

        Collider2D[] objectsDetected = Physics2D.OverlapCircleAll(this.transform.position, radius, m_layerMask);
        foreach (Collider2D item in objectsDetected)
        {
            if (item.TryGetComponent<ObjectMoodEffect>(out ObjectMoodEffect _object))
            {
                mood -= _object.MoodEffect;
            }
        }


        float newMood = mood / 100;
        if (m_slider)
        {
            if (newMood <= 0)
            {
                m_slider.value = 0;
            }
            else
            {
                m_slider.value = newMood;
            }
        }
    }
}
