using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text price;

    public void SetProduct(ItemData product)
    {
        image.sprite = product.image;
        name.text = product.name;
        price.text = product.cost.ToString();
    }
}
