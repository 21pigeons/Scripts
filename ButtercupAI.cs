using UnityEngine;
using System.Collections;

public class ButtercupAI : MonoBehaviour {
    public GameObject player;
    public int state = 2;
    public int stateArround = 0;
    public string State;
    public int neardistance = 10;
    public int mushroomdistance = 100;
    public int fardistance = 150;
    public float speed = 30.0f;
    public float playerDist;

    public bool nearplayer;
    public bool awayfromplayer;
    public bool called = false;

    private GameObject nearistItem;
    private bool makewaytoplayer = false;
    private GameObject[] findableitems;
    private readonly string[] states = new string[7] {"RunningToPlayer", "AtPlayer","AroundPlayer","ComingBackToArround","FoundItem", "ShowingPlayerItem","AtItem"};

    // Use this for initialization
    void Start () {
        State = states[state];
        print("ButterCup Starting State is " + State);
        findableitems = GameObject.FindGameObjectsWithTag("FindAble");
    }

    void RunningToPlayer() {
        speed = 20.0f;
        transform.LookAt(player.transform);
        transform.position += transform.forward * Time.deltaTime * speed;
        if (nearplayer) {
            SetState("AtPlayer");
        }
    }

    void AtPlayer() {
        if (!nearplayer) {
            SetState("AroundPlayer");
        }
        makewaytoplayer = false;
    }

    void AroundPlayer() {



        speed = 20.0f;
        getClosestInRange();
        if (nearistItem != null) {
            SetState("FoundItem");           
        }
        if (called || makewaytoplayer) {
            SetState("RunningToPlayer");
        }
        if (awayfromplayer) {
            SetState("ComingBackToArround");
        }
    }

    void ComingBackToArround() {
        speed = 30.0f;
        transform.LookAt(player.transform);
        transform.position += transform.forward * Time.deltaTime * speed;
        if (playerDist< fardistance-40) {
            SetState("AroundPlayer");
        }
        if (called){
            makewaytoplayer = true;
        }
    }

    void FoundItem() {
        this.transform.Rotate(Vector3.up * Time.deltaTime*40);
        if (awayfromplayer) {
            SetState("ComingBackToArround");
        }
        if (nearplayer) {
            SetState("ShowingPlayerItem");
        }
    }

    void ShowingPlayerItem() {
        if (awayfromplayer) {
            SetState("ComingBackToArround");
        }
        if (Vector3.Distance(transform.position, nearistItem.transform.position) < neardistance) {
            SetState("AtItem");
        }
        transform.LookAt(nearistItem.transform);
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void AtItem() {
        this.transform.Rotate(Vector3.down * Time.deltaTime * 90);
        if (awayfromplayer) {
            SetState("ComingBackToArround");
        }
        if (!nearistItem.activeInHierarchy) {
            print("Congrats on getting the item");
            SetState("AroundPlayer");
        }
    }

    // Update is called once per frame
    void Update() {
        playerDist = Vector3.Distance(transform.position, player.transform.position);

        nearplayer = (playerDist < neardistance);
        awayfromplayer = (playerDist > fardistance);

        called = (Input.GetKeyDown(KeyCode.P));

        if (state == 0) {
            RunningToPlayer();
        }else if(state == 1) {
            AtPlayer();
        }else if(state == 2) {
            AroundPlayer();
        }else if(state == 3) {
            ComingBackToArround();
        }else if(state == 4) {
            FoundItem();
        }else if(state == 5) {
            ShowingPlayerItem();
        }else if(state == 6) {
            AtItem();
        }



        RaycastHit hitInfo;

        LayerMask layer = 1 << LayerMask.NameToLayer("Terrain");
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo)) {
            float y = hitInfo.point.y;
            print(y+":"+ transform.position.y);
            Vector3 temp = new Vector3(transform.position.x, y+4.6f, transform.position.z);
            transform.position = temp;
        }
        

    }


    GameObject getClosestInRange() {
        if(findableitems == null) {
            return null;
        }
        float minDistance = mushroomdistance;
        GameObject SmallistDistance = null;
        for (int i = 0;i< findableitems.Length;i++) {
            if (findableitems[i].activeInHierarchy) {
                float distance = Vector3.Distance(transform.position, findableitems[i].transform.position);
                if (distance < minDistance) {
                    minDistance = distance;
                    SmallistDistance = findableitems[i];
                }
            }
        }
        nearistItem = SmallistDistance;
        return SmallistDistance;
    }

    void SetState(string setstate) {
        for (int i = 0;i<states.Length;i++) {
            if (states[i].Equals(setstate)) {
                state = i;
                State = setstate;
                print("State is now " + State);
                if(state == 2) {
                    stateArround = 0;
                }
                return;
            }
        }
        print("State "+setstate+" is an Invalid state");
    }
}
