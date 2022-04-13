using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetThePet : MonoBehaviour
{
    public void StartPet()
    {
        if (this.gameObject.TryGetComponent<PetMovementation>(out PetMovementation _petMovementation))
        {
            _petMovementation.enabled = false;
        }
        if (this.gameObject.TryGetComponent<PetStatusManager>(out PetStatusManager _petStatusManager))
        {
            _petStatusManager.status.Mood += 10;
        }
    }
    public void StopPet()
    {
        if (this.gameObject.TryGetComponent<PetMovementation>(out PetMovementation _petMovementation))
        {
            _petMovementation.enabled = true;
        }
        // if (this.gameObject.TryGetComponent<PetStatusManager>(out PetStatusManager _petStatusManager))
        // {
        //     _petStatusManager.status.AddMood(10);
        // }
    }
}
