using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] LayerMask collectableMask;
    [SerializeField] TextMeshProUGUI pickUpText;

    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.CanInteract)
        {
            if (!Inventory.IsFull)
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 10f, collectableMask))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null)
                    {
                        pickUpText.text = $"{interactable.CollectableItem.ItemName} (E to Pickup)";

                        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2))
                        {
                            if (Inventory.MaximumWeight > 0)
                            {
                                if (Inventory.CurrentWeight + interactable.CollectableItem.Weight > Inventory.MaximumWeight) return;
                            }

                            playerController.PlayPickUpAnimation();
                            interactable.OnInteract();
                            pickUpText.text = "";
                        }               
                    }
                    else
                    {
                        Debug.LogError("Collectable with no Interactable script");
                    }
                }
                else
                {
                    pickUpText.text = "";
                }
            }
        }
    }
}
