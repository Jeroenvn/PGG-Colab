using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentController : MonoBehaviour
{
    private Hashtable inventory;

    private int inventorySize = 12;

    // Sets up a hashtable with slots for attachments.
    private void InitializeInventory()
    {
        inventory.Clear();
        for (int index = 0; index < inventorySize; index++)
        {
            inventory.Add("slot" + index, null);
        }
    }

    private void InitializeEquipedAttachments(Attachment[] attachments)
    {

    }

    // Adds attachment to inventory if there is space left
    public void CollectAttachment(Attachment attachment)
    {
        if (!inventory.ContainsValue(null))
        {
            print("AttachmentController: Inventory is full");
            return;
        }
        
        for (int index = 0; index < inventorySize; index++)
        {
            if (inventory["slot" + index].Equals(null))
            {
                inventory["slot" + index] = attachment;
                break;
            }
        }
    }
}
