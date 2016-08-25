using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CraftingUIManager : MonoBehaviour {
    public GUISkin InvSkin;
    public RectTransform[] SlotRecs;
    List<Item> thisitems = new List<Item>(15);

    private bool draggingItem;
    private Item draggedItem;
    private int previndex;

    public void updateItems(List<Item> items) {
        thisitems = items;
        GUI.skin = InvSkin;
        RectTransform Crafting = ((RectTransform)GetComponent("RectTransform"));
        Vector2 V2 = Crafting.anchoredPosition;

        for (int i = 0; i < SlotRecs.Length; i++) {
            RectTransform slotRect = SlotRecs[i];


            Rect newRect = new Rect((V2.x+slotRect.anchoredPosition.x+ Crafting.rect.width*0.5f)-0.5f*slotRect.rect.width, -1*(V2.y + slotRect.anchoredPosition.y) + Crafting.rect.width*0.5f  - slotRect.rect.x*-2.5f, -2*slotRect.rect.x, -2*slotRect.rect.y);
            //print(i+"x,y:"+ slotRect.anchoredPosition.x + ","+ slotRect.anchoredPosition.y);
            //print(newRect);

            if (items.ElementAtOrDefault(i) != null) {
                Item item = items[i];
                GUI.Box(newRect, item.Amount.ToString(), InvSkin.GetStyle("Slot"));
                if (item != null) {     
                    GUI.DrawTexture(newRect, item.itemIcon);
                }
            }else {
                GUI.Box(newRect, "", InvSkin.GetStyle("Slot"));
            }
        }
    }

    public void Checkcrafting() {
        int rednum = 0;
        int bluenum = 0;
        int greennum = 0;
        for (int i = 10; i < 13; i++) {
            if (thisitems[i] != null) {
                if ((thisitems[i].Name).Equals("RedMushroom")) {
                    rednum = rednum + thisitems[i].Amount;
                }
                if ((thisitems[i].Name).Equals("BlueMushroom")) {
                    bluenum = bluenum + thisitems[i].Amount;
                }
                if ((thisitems[i].Name).Equals("GreenMushroom")) {
                    greennum = greennum + thisitems[i].Amount;
                }
            }
        }
        print(rednum);
        print(greennum);
        print(bluenum);

        if (rednum > 3 && greennum > 1) {
            Item i = new Item();
            print("CRAFTED ITEM");
        }
        for (int i = 10; i < 13; i++) {
            if (thisitems[i] != null) {
                if ((thisitems[i].Name).Equals("RedMushroom")) {
                    if(thisitems[i].Amount == 3) {
                        thisitems[i] = null;
                    }
                    else {
                        thisitems[i].Amount = thisitems[i].Amount - 3;
                    }
                }
                if ((thisitems[i].Name).Equals("GreenMushroom")) {
                    if (thisitems[i].Amount == 1) {
                        thisitems[i] = null;
                    }
                    else {
                        thisitems[i].Amount = thisitems[i].Amount - 1;
                    }
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
    int getSlotfromMouse(float x, float y) {
        for (int i = 0; i < SlotRecs.Length; i++) {
            if (RectTransformUtility.RectangleContainsScreenPoint(SlotRecs[i], new Vector2(x,y))) {
                return i;
            }
        }
        return -1;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            int i = getSlotfromMouse(Input.mousePosition.x, Input.mousePosition.y);
            thisitems[i].PrintInfo();
        }
        if (Input.GetMouseButtonDown(0)) {
            int i = getSlotfromMouse(Input.mousePosition.x, Input.mousePosition.y);
            if (i != -1) {
                draggingItem = true;
                draggedItem = thisitems[i];
                thisitems[i] = null;
                print("Dragging Item From Index:" + i);
                previndex = i;
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            if (draggingItem) {
                draggingItem = false;
                print("Stopped Dragging Item");
                int i = getSlotfromMouse(Input.mousePosition.x, Input.mousePosition.y);
                if (i != -1) {
                    print("Placeing Item at Index: "+i);
                    var temp = thisitems[i];
                    thisitems[i] = draggedItem;
                    thisitems[previndex] = temp;
                }
            }
        }
    }

    /*
    public void updateItems(List<Item> items) {
        GUI.skin = InvSkin;

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 5; j++) {
                RectTransform Crafting = ((RectTransform)GetComponent("RectTransform"));
                RectTransform ItemArea = ((RectTransform)(transform.Find("ItemSlot").GetComponent("RectTransform")));
                Vector2 ItemXY = ItemArea.anchoredPosition;
                Vector2 V2 = Crafting.anchoredPosition;
                int XSize = (int)(0.45 * (ItemArea.rect.x * -1));
                int YSize = (int)(0.18 * (ItemArea.rect.y) * -1);
                int Xspacing = (int)(0.1 * (ItemArea.rect.x) * -1);
                int Yspaceing = (int)(0.0375 * (ItemArea.rect.y) * 1);
                //print("x,y:"+ ItemArea.rect.x + ","+ ItemArea.rect.y);
                Rect slotRect = new Rect(V2.x + ItemXY.x + (i * (XSize + Xspacing)) + 557, ((V2.y + ItemXY.y) * -1) + (j * (XSize + Xspacing)), XSize, YSize);
                if (items.Count > (i * 5) + j) {
                    Item item = items[(i * 5) + j];
                    GUI.Box(slotRect, item.Amount.ToString(), InvSkin.GetStyle("Slot"));
                    if (item != null) {
                        GUI.DrawTexture(slotRect, item.itemIcon);
                    }
                }
                else {
                    GUI.Box(slotRect, "", InvSkin.GetStyle("Slot"));
                }
            }
        }
    }
    */
}
