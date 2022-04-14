using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetThePet : MonoBehaviour
{
    [SerializeField] private PetMovementation _petMovementation;
    [SerializeField] private PetStatusManager _petStatusManager;

    public void StartPet()
    {
        _petMovementation.enabled = false;
        _petStatusManager.status.Mood += 10;

    }
    public void StopPet()
    {
        _petMovementation.enabled = true;
    }
}
