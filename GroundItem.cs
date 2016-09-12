using UnityEngine;
using System.Collections;

public class GroundItem : MonoBehaviour {

    public string ItemName;
    public GameObject Player;
    public float MaxPickUpdistance = 25.0f;
    public bool WithinPickup = false;
    private UserInventory UsrInv;

    // Use this for initialization
    void Start () {
        UsrInv = (UserInventory)Player.GetComponent("UserInventory");
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (WithinPickup){
            if (distance > MaxPickUpdistance+5) {
                WithinPickup = false;
                UsrInv.removenearby(this.gameObject);
            }
        }else{
            if (distance < MaxPickUpdistance)  {
                WithinPickup = true;
                UsrInv.addnearby(this.gameObject);
            }
        } 
    }
}

