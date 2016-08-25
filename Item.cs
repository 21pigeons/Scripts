using UnityEngine;
using System.Collections;
using System;

public class Item{
    public int HealAmount { get; internal set; }
    public int weight { get; internal set; }
    public string Name { get; internal set; }
    public Texture2D itemIcon;

    public Item()
    {

    }

    public Item(Item OtherItem)
    {
        this.HealAmount = OtherItem.HealAmount;
        this.weight = OtherItem.weight;
        this.Name = OtherItem.Name;
        this.itemIcon = OtherItem.itemIcon;
    }

    public void PrintInfo() {
        Debug.Log(String.Format("Item Name:{0} Item weight {1} HealNum {2:F}", Name, weight, HealAmount));
    }
	
	
	
}
