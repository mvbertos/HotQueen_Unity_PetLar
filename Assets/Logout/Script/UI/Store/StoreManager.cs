using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    //CATALOG
    [SerializeField] private Transform catalogParent;
    [SerializeField] private Button catalogButton;
    //PRODUCT
    [SerializeField] private Transform productParent;
    [SerializeField] private ProductButton productButton;
    //MORE
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cost;
    //BUY
    [SerializeField] private Button buyButton;

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
        //clear product in product parent
        //for each child in productParent destroy it
        foreach (Transform item in productParent)
        {
            Destroy(item.gameObject);
        }


        //for each product in the item container
        //if the product type is the same as the button type
        //create a button
        //add a listener to the button
        //set the button data
        //add the button to the product list
        ItemContainer.SaveObjectList saveObjectList = ItemContainer.Load();
        Debug.Log(saveObjectList.saveObjects.Count);

        foreach (ItemContainer.SaveObject save in saveObjectList.saveObjects)
        {
            if (save.itemData.type == itemType)
            {
                ProductButton button = Instantiate(productButton, productParent);
                button.SetProduct(save.itemData);
                button.GetComponent<Button>().onClick.AddListener(() => MoreAbout(save.itemData, save.prefab));
            }
        }
    }

    private void MoreAbout(ItemData itemData, GameObject prefab)
    {
        //open a screen with the product data
        //add a button to buy the product
        //add a listener to the button
        //add the product to the player inventory
        //remove amount of money from the player
        title.text = itemData.name;
        description.text = itemData.description;
        cost.text = itemData.cost.ToString();
        buyButton.onClick.AddListener(() => Buy(prefab, itemData.cost));
    }

    private void Buy(GameObject prefab, float cost)
    {
        //if player have money
        //reduce money from the player
        //instantiate prefab in the world
        ONG playerONG = GameObject.FindObjectOfType<ONG>();
        if (playerONG.Money >= cost)
        {
            playerONG.Money -= cost;
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}