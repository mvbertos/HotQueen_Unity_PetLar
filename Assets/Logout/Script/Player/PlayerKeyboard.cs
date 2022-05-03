using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboard : MonoBehaviour
{
    [System.Serializable]
    private class Keys
    {
        public KeyCode firstShortcut = KeyCode.Q;
        public KeyCode secondShortcut = KeyCode.W;
        public KeyCode thirdShortcut = KeyCode.E;
        public KeyCode forthShortcut = KeyCode.R;
        public KeyCode fifthShortcut = KeyCode.T;
        public KeyCode sixthShortcut = KeyCode.Y;
        public KeyCode seventhShortcut = KeyCode.U;
        public KeyCode eighthShortcut = KeyCode.I;
    }

    [SerializeField] private Keys keycodes;
    [SerializeField] private GameObject minigameScreen;
    private void Update()
    {
        if (Input.GetKeyDown(keycodes.seventhShortcut))
        {
            minigameScreen.SetActive(!minigameScreen.activeInHierarchy);
        }
    }
}
