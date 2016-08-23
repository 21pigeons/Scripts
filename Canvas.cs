using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour {
    public bool isOpen = false;
    public GameObject CraftingUI;

        // Use this for initialization
    void Start()
    {

    }

        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            CraftingUI.SetActive(isOpen);
        }
    }
}


