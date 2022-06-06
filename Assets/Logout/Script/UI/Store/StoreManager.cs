using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Transform catalogParent;
    [SerializeField] private Button catalogButton;
    [SerializeField] private Transform productParent;
    [SerializeField] private ProductButton productButton;
    [SerializeField] private List<Item> items = new List<Item>();

    private void Start()
    {
        InitCatalog();
    }

    private void InitCatalog()
    {
        //for each catalog in itemtype enum create a button
        //for each button created add a listener to update the product list
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            Button button = Instantiate(catalogButton, catalogParent);
            button.GetComponentInChildren<TMP_Text>().text = itemType.ToString();
            button.onClick.AddListener(() => UpdateProductList(itemType));
        }
    }

    private void UpdateProductList(ItemType itemType)
    {
        //for each product in the item database
        //if the product type is the same as the button type
        //create a button
        //add a listener to the button
        //set the button data
        //add the button to the product list
        foreach (ItemData item in ItemContainer.Load().itemDataList)
        {
            if (item.type == itemType)
            {
                ProductButton button = Instantiate(productButton, productParent);
                button.SetProduct(item);
                button.GetComponent<Button>().onClick.AddListener(() => MoreAbout(item));
            }
        }
    }

    private void MoreAbout(ItemData item)
    {
        //open a screen with the product data
        //add a button to buy the product
        //add a listener to the button
        //add the product to the player inventory
        //remove amount of money from the player
    }
}
