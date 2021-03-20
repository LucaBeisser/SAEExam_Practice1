using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryEvent : UnityEvent<CollectableItem> { }

public static class Inventory 
{
    static List<CollectableItem> interactables = new List<CollectableItem>();

    public static float MaximumWeight = 0f;
    public static float CurrentWeight = 0f;
    public static bool IsFull = false;

    public static InventoryEvent OnAddItemToInventoryEvent = new InventoryEvent();

    public static void AddInteractable(CollectableItem interactable)
    {
        interactables.Add(interactable);
        CurrentWeight += interactable.Weight < 0 ? 0 : interactable.Weight;

        if (CurrentWeight >= MaximumWeight && MaximumWeight > 0) IsFull = true;
        else IsFull = false;

        OnAddItemToInventoryEvent?.Invoke(interactable);
    }
}
