using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserInventory : MonoBehaviour {
    public GUISkin InvSkin;
    private bool InvOpen;
    private List<GameObject> nearbyItems = new List<GameObject>();
    private List<Item> items = new List<Item>();
    private Dictionary<string, Item> itemPresets = new Dictionary<string, Item>();
    public GameObject CraftingUI;
    public Texture2D defaultitem;


    void OnGUI() {
        if (InvOpen)
        {
            GUI.skin = InvSkin;
            DrawInventory();
        }
    }

    private void DrawInventory(){       
        for (int i = 0;i<2;i++) {
            for (int j = 0; j < 5; j++) {
                RectTransform Crafting = ((RectTransform)CraftingUI.GetComponent("RectTransform"));
                RectTransform ItemArea = ((RectTransform)(CraftingUI.transform.Find("ItemSlot").GetComponent("RectTransform")));
                Vector2 ItemXY = ItemArea.anchoredPosition;
                Vector2 V2 = Crafting.anchoredPosition;
                int XSize = (int)(0.45 * (ItemArea.rect.x*-1));
                int YSize = (int)(0.18 * (ItemArea.rect.y)*-1);
                int Xspacing = (int)(0.1 * (ItemArea.rect.x)*-1);
                int Yspaceing = (int)(0.0375 * (ItemArea.rect.y)*1);
                //print("x,y:"+ ItemArea.rect.x + ","+ ItemArea.rect.y);
                Rect slotRect = new Rect(V2.x+ ItemXY.x + (i * (XSize+ Xspacing))+557, ((V2.y+ ItemXY.y) *-1) +(j * (XSize + Xspacing)), XSize, YSize);
                GUI.Box(slotRect, "", InvSkin.GetStyle("Slot"));
                if (items.Count > (i * 5) + j){
                    Item item = items[(i * 5) + j];
                    if (item != null)   
                    {
                        GUI.DrawTexture(slotRect, item.itemIcon);
                    }
                }                
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            GameObject item = nearbyItems[nearbyItems.Count - 1];
            String name = ((GroundItem)item.GetComponent("GroundItem")).ItemName;
            print("Adding new: " + name);
            AddNew(name);
            item.SetActive(false);
            nearbyItems.Remove(item);

        }
            if (Input.GetKeyDown(KeyCode.Tab))
        {
            InvOpen = !InvOpen;
            CraftingUI.SetActive(InvOpen);
            if (InvOpen){
                PrintItemsInInv();
               
            }
        }
    }

    public void PrintItemsInInv()
    {
        foreach (Item item in items) { item.PrintInfo(); }
    }

    public Item AddNew(string name)
    {
        Item preset = itemPresets[name]; // Get the preset
        Item newItem = new Item(preset); // Create a clone,                        
        items.Add(newItem);              // Register to database
        return newItem;                  // And return it since you want to use it right now
    }

    private void PopulateItems()
    {
        Item preset;
        preset = new Item();
        preset.Name = "RedHealth";
        preset.HealAmount = 25;
        preset.weight = 1;
        preset.itemIcon = defaultitem;
        SetPreset("RedHealth", preset);
    }

    public void addnearby(GameObject go)
    {
        nearbyItems.Add(go);
    }

    public void removenearby(GameObject go)
    {
        nearbyItems.Remove(go);
    }

    public void SetPreset(string name, Item preset)
    {
        itemPresets[name] = preset;

    }

    void Start()
    {
        PopulateItems();
        CraftingUI.SetActive(false);
    }

}
