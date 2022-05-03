using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MG_Adoption : MonoBehaviour
{
    [SerializeField] private Button QuitButton;
    [SerializeField] private Perfil.Data[] newData = new Perfil.Data[2];
    [SerializeField] private Perfil perfil_pet;
    [SerializeField] private Perfil perfil_human;

    private void Start()
    {
        QuitButton.onClick.AddListener(delegate { Destroy(this.gameObject); });
    }
    private void OnEnable()
    {
        perfil_human.current_data = newData[0];
        perfil_pet.current_data = newData[1];

        perfil_pet.gameObject.SetActive(true);
        perfil_human.gameObject.SetActive(true);

    }

    private void OnDisable()
    {

        perfil_pet.gameObject.SetActive(false);
        perfil_human.gameObject.SetActive(false);
    }
}
