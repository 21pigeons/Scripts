using UnityEngine;
using System.Collections;
using System;

public class Item{
    public int Amount { get; internal set; }
    
    public int HealAmount { get; internal set; }
    public bool stackable { get; internal set; }
    public string Name { get; internal set; }
    public Texture2D itemIcon;

    public Item()
    {
        Amount = 1;
    }

    public Item(Item OtherItem)
    {
        this.HealAmount = OtherItem.HealAmount;
        this.stackable = OtherItem.stackable;
        this.Name = OtherItem.Name;
        this.itemIcon = OtherItem.itemIcon;
        this.Amount = OtherItem.Amount;
    }

    public void PrintInfo() {
        Debug.Log(String.Format("Item Name:{0} Stackable {1} HealNum {2:F}", Name, stackable, HealAmount));
    }
	
	
	
}
