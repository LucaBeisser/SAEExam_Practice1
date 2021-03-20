using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class InventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;

    private CollectableItem collectableItem;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickOnButton);
    }

    public void Set(CollectableItem item)
    {
        collectableItem = item;
        nameText.text = item.ItemName;
    }
    private void OnClickOnButton()
    {
        UIInventory.Instance.SelectItem(collectableItem);
    }
    public ItemCategory GetItemcategory()
    {
        return collectableItem.ItemCategory;
    }
}
