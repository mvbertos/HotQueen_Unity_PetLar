using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoodEffect : MonoBehaviour
{
    [Range(0, 10)][SerializeField] private float moodEffect;
    public float MoodEffect { get { return moodEffect; } }
}
