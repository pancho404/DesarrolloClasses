using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChCreatorAndDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    [ContextMenu(itemName: "Create Object")]
   private void CreateObject()
    {
        GameObject theGameObject = new GameObject(name: "TheGameObject");
    }

}
