using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableInventory : MonoBehaviour {
    //attach this to any entity with a lootable inventory

    //lootable inventory
    public List<ItemData> entityLootableInventory = new List<ItemData>();
    public int lootableGoldCoins = 0;

    //is this inventory lootable?(Usually only true when its associated entity is dead
    public bool isInventoryLootable = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}
}
