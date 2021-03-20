using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class CategoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;

    private ItemCategory category;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickOnButton);
    }

    public void Set(ItemCategory itemCategory)
    {
        category = itemCategory;
        nameText.text = itemCategory.ToString();
    }
    private void OnClickOnButton()
    {
        UIInventory.Instance.SelectCategory(category);
    }
    public ItemCategory GetItemcategory()
    {
        return category;
    }
}
