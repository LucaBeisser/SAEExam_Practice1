using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public CollectableItem CollectableItem;

    public void OnInteract()
    {
        Inventory.AddInteractable(CollectableItem);
        Destroy(gameObject);
    }
}
