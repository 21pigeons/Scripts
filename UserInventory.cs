using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class UserInventory : MonoBehaviour {
    public Texture2D RedMushroom, GreenMushroom, BlueMushroom,Potion;
    private CraftingUIManager craftingUIManager;
    
    private bool InvOpen = false;
    private List<GameObject> nearbyItems = new List<GameObject>();
    private List<Item> items = new List<Item>(13);
    private Dictionary<string, Item> itemPresets = new Dictionary<string, Item>();
    public GameObject DisplayText;
    public GameObject CraftingUI;

    void OnGUI() {
        if (InvOpen)
        {
            craftingUIManager.updateItems(items);
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
            UpdateBottemText("");
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
        foreach (Item item in items) {
            if (item != null) {
                item.PrintInfo();
            }
        }
    }

    public void addPotion() {
        AddNew("PurePotion");
    }

    public Item AddNew(string name)
    {
        Item preset = itemPresets[name]; // Get the preset
        if (preset.stackable == true) {
            foreach (Item item in items) {
                if (item != null) {
                    if (item.Name.Equals(preset.Name)) {
                        item.Amount = item.Amount + 1;
                        return item;
                    }
                }
            }
        }
        Item newItem = new Item(preset); // Create a clone,                        
        AddItem(newItem);              // Register to database
        return newItem;                  // And return it since you want to use it right now
    }

    private void AddItem(Item newItem) {
        for(int i = 0; i < items.Count; i++) {
            if(items[i] == null){
                items[i] = newItem;
                return;
            }
        }
    }

    public void addnearby(GameObject go)
    {
        nearbyItems.Add(go);
        String name = ((GroundItem)go.GetComponent("GroundItem")).ItemName;
        UpdateBottemText("Press \"e\" to pickup "+name);
    }

    public void removenearby(GameObject go)
    {
        nearbyItems.Remove(go);
        UpdateBottemText("");
    }

    public void SetPreset(string name, Item preset)
    {
        itemPresets[name] = preset;

    }

    void Start()
    {
        craftingUIManager = (CraftingUIManager)CraftingUI.GetComponent("CraftingUIManager");
        PopulateItems();
        CraftingUI.SetActive(false);
        for(int i = 0; i < 13; i++) {
            items.Add(null);
        }
    }

    public void UpdateBottemText(string text) {
        DisplayText.SetActive(true);
        (DisplayText.GetComponent<Text>()).text = text;
    }

    private void PopulateItems() {
        Item preset;
        preset = new Item();
        preset.Name = "RedMushroom";
        preset.HealAmount = 25;
        preset.stackable = true;
        preset.itemIcon = RedMushroom;
        SetPreset("RedMushroom", preset);

        preset = new Item();
        preset.Name = "GreenMushroom";
        preset.HealAmount = -20;
        preset.stackable = true;
        preset.itemIcon = GreenMushroom;
        SetPreset("GreenMushroom", preset);

        preset = new Item();
        preset.Name = "BlueMushroom ";
        preset.HealAmount = 0;
        preset.stackable = true;
        preset.itemIcon = BlueMushroom;
        SetPreset("BlueMushroom", preset);

        preset = new Item();
        preset.Name = "PurePotion ";
        preset.HealAmount = 0;
        preset.stackable = true;
        preset.itemIcon = Potion;
        SetPreset("PurePotion", preset);
    }
}
