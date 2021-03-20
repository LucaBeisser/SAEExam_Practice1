using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventory : MonoBehaviour
{
    [SerializeField] Transform inventoryListParent;
    [SerializeField] Transform categoryListParent;
    [SerializeField] GameObject inventoryUIItemList;
    [SerializeField] GameObject inventoryUICategoryList;
    [SerializeField] GameObject inventoryUISelected;

    [Header("Prefabs")]
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] GameObject categoryItemPrefab;

    [Header("Selected Object Infos")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI weightText;
    [SerializeField] TextMeshProUGUI valueText;

    [Header("PlayerStats")]
    [SerializeField] TextMeshProUGUI carryWeightText;

    List<CategoryItem> allCategories = new List<CategoryItem>();
    List<InventoryItem> allInventoryItems = new List<InventoryItem>();

    public static UIInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        inventoryUIItemList.SetActive(false);
        inventoryUICategoryList.SetActive(false);
        inventoryUISelected.SetActive(false);

        for (int i = 0; i < (int)ItemCategory.LengthOfEnum; i++)
        {
            GameObject obj = Instantiate(categoryItemPrefab, categoryListParent);
            CategoryItem item = obj.GetComponent<CategoryItem>();
            item.Set((ItemCategory)i);
            allCategories.Add(item);
        }
    }

    private void OnEnable()
    {
        Inventory.OnAddItemToInventoryEvent.AddListener(OnAddedItem);
    }
    private void OnDisable()
    {
        Inventory.OnAddItemToInventoryEvent.RemoveListener(OnAddedItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (inventoryUICategoryList.activeSelf)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void SelectCategory(ItemCategory itemCategory)
    {
        if (itemCategory == ItemCategory.LengthOfEnum)
        {
            Debug.LogError("Use of 'LengthOfEnum' as category");
            return;
        }

        inventoryUIItemList.SetActive(true);

        if (itemCategory == ItemCategory.All)
        {
            for (int i = 0; i < allInventoryItems.Count; i++)
            {
                allInventoryItems[i].transform.SetParent(inventoryListParent);
                allInventoryItems[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < allInventoryItems.Count; i++)
            {
                if (itemCategory == allInventoryItems[i].GetItemcategory())
                {
                    allInventoryItems[i].transform.SetParent(inventoryListParent);
                    allInventoryItems[i].gameObject.SetActive(true);
                }
                else
                {
                    allInventoryItems[i].transform.SetParent(null);
                    allInventoryItems[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SelectItem(CollectableItem item)
    {
        inventoryUISelected.SetActive(true);
        nameText.text = item.ItemName;
        descriptionText.text = item.Description;
        valueText.text = $"Value {item.Value}";
        weightText.text = $"Weight {item.Weight}";
    }

    private void OpenInventory()
    {
        inventoryUICategoryList.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(Inventory.MaximumWeight > 0) carryWeightText.text = $"Carry Weight {Inventory.CurrentWeight}/{Inventory.MaximumWeight}";
        else carryWeightText.text = $"Carry Weight {Inventory.CurrentWeight}/Infinite";
    }
    private void CloseInventory()
    {
        inventoryUIItemList.SetActive(false);
        inventoryUICategoryList.SetActive(false);
        inventoryUISelected.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnAddedItem(CollectableItem collectableItem)
    {
        GameObject obj = Instantiate(inventoryItemPrefab, null);
        InventoryItem item = obj.GetComponent<InventoryItem>();
        item.Set(collectableItem);
        obj.SetActive(false);
        allInventoryItems.Add(item);
    }
}
