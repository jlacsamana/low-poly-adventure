using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Load3DMdlDataBase : MonoBehaviour {
    //this place is where all item 3d model prefabs are stored and referenced from for instantiation

    //game's entire equipped item directory
    public GameObject[] equipItemDirectory;

    //game's entire dropped item directory
    public GameObject[] droppedItemDirectory;

    //spawned projectiles
    public GameObject[] spawnedProjectilesDirectory;

    //equipped item lists organised by type
    //armor
    public GameObject[] equippedHeadGearList;
    public GameObject[] equippedTorsoGearList;
    public GameObject[] equippedLegGearList;
    public GameObject[] equippedFootGearList;
    public GameObject[] equippedNecklaceList;
    public GameObject[] equippedRingList;

    //weaponry & related 
    public GameObject[] equippedOneHandedWeaponList;
    public GameObject[] equippedTwoHandedWeaponList;
    public GameObject[] equippedProjectileWeaponList;
    public GameObject[] equippedStaffList;

    public GameObject[] equippedShieldList;
    public GameObject[] equippedTomeList;

    //dropped item list
    //armor
    public GameObject[] droppedHeadGearList;
    public GameObject[] droppedTorsoGearList;
    public GameObject[] droppedLegGearList;
    public GameObject[] droppedFootGearList;
    public GameObject[] droppedNecklaceList;
    public GameObject[] droppedRingList;

    //weaponry & related 
    public GameObject[] droppedOneHandedWeaponList;
    public GameObject[] droppedTwoHandedWeaponList;
    public GameObject[] droppedProjectileWeaponList;
    public GameObject[] droppedStaffList;

    public GameObject[] droppedShieldList;
    public GameObject[] droppedTomeList;

    public GameObject[] droppedAmmunitionList;

    //other
    public GameObject[] droppedConsumableList;
    public GameObject[] droppedMaterialList;
    public GameObject[] droppedQuestItemList;

    //spawned projectile lists
    public GameObject[] spawnedPhysicalProjectileList;
    public GameObject[] spawnedMagicalProjectileList;
    public GameObject[] spawnedConstantMagicalProjectileList;

    //outfits
    public GameObject[] outFitList;

    //accessories 
    public GameObject[] hairList;
    public GameObject[] facialHairList;

    //relative paths to folders inside "Resources"
    readonly string equippedPath = "Item Prefabs/As Equipped/";
    readonly string droppedPath = "Item Prefabs/Dropped Items/";
    readonly string spawnedProjectilePath = "Item Prefabs/Spawned Projectiles/";

    // Use this for initialization
    void Start () {
        //loads all accessories
        hairList = Resources.LoadAll<GameObject>("Item Prefabs/Accessories/Hair");
        facialHairList = Resources.LoadAll<GameObject>("Item Prefabs/Accessories/Facial Hair");


        //loads all outfits
        outFitList = Resources.LoadAll<GameObject>("Item Prefabs/Outfits");

        //loads all equipped item prefabs
        equippedHeadGearList = Resources.LoadAll<GameObject>(equippedPath + "Headgear");
        equippedTorsoGearList = Resources.LoadAll<GameObject>(equippedPath + "Torsogear");
        equippedLegGearList = Resources.LoadAll<GameObject>(equippedPath + "Legwear");
        equippedFootGearList = Resources.LoadAll<GameObject>(equippedPath + "Footwear");
        equippedNecklaceList = Resources.LoadAll<GameObject>(equippedPath + "Necklaces");
        equippedRingList = Resources.LoadAll<GameObject>(equippedPath + "Rings");

        equippedOneHandedWeaponList = Resources.LoadAll<GameObject>(equippedPath+ "1H Weapons");
        equippedTwoHandedWeaponList = Resources.LoadAll<GameObject>(equippedPath + "2H Weapons");
        equippedProjectileWeaponList = Resources.LoadAll<GameObject>(equippedPath + "Projectile Weapons");
        equippedStaffList = Resources.LoadAll<GameObject>(equippedPath + "Staffs");
        equippedShieldList = Resources.LoadAll<GameObject>(equippedPath + "Shields");
        equippedTomeList = Resources.LoadAll<GameObject>(equippedPath + "Tomes");

        //merges all equipped item prefab arrays into one
        equipItemDirectory = equippedHeadGearList.Concat(equippedTorsoGearList).Concat(equippedLegGearList).Concat(equippedFootGearList).
            Concat(equippedNecklaceList).Concat(equippedRingList).Concat(equippedOneHandedWeaponList).Concat(equippedTwoHandedWeaponList).
            Concat(equippedProjectileWeaponList).Concat(equippedStaffList).Concat(equippedShieldList).Concat(equippedTomeList).ToArray();

        //loads all dropped item prefabs
        droppedHeadGearList = Resources.LoadAll<GameObject>(droppedPath + "Headgear");
        droppedTorsoGearList = Resources.LoadAll<GameObject>(droppedPath + "Torsogear");
        droppedLegGearList = Resources.LoadAll<GameObject>(droppedPath + "Legwear");
        droppedFootGearList = Resources.LoadAll<GameObject>(droppedPath + "Footwear");
        droppedNecklaceList = Resources.LoadAll<GameObject>(droppedPath + "Necklaces");
        droppedRingList = Resources.LoadAll<GameObject>(droppedPath + "Rings");

        droppedOneHandedWeaponList = Resources.LoadAll<GameObject>(droppedPath + "1H Weapons");
        droppedTwoHandedWeaponList = Resources.LoadAll<GameObject>(droppedPath + "2H Weapons");
        droppedProjectileWeaponList = Resources.LoadAll<GameObject>(droppedPath + "Projectile Weapons");
        droppedStaffList = Resources.LoadAll<GameObject>(droppedPath + "Staffs");
        droppedShieldList = Resources.LoadAll<GameObject>(droppedPath + "Shields");
        droppedTomeList = Resources.LoadAll<GameObject>(droppedPath + "Tomes");

        droppedAmmunitionList = Resources.LoadAll<GameObject>(droppedPath + "Ammunition");

        droppedConsumableList = Resources.LoadAll<GameObject>(droppedPath + "Consumables");
        droppedMaterialList = Resources.LoadAll<GameObject>(droppedPath + "Raw Materials");
        droppedQuestItemList = Resources.LoadAll<GameObject>(droppedPath + "Quest Items");

        //merges all dropped item prefab arrays into one
        droppedItemDirectory = droppedHeadGearList.Concat(droppedTorsoGearList).Concat(droppedLegGearList).Concat(droppedFootGearList).
            Concat(droppedNecklaceList).Concat(droppedRingList).Concat(droppedOneHandedWeaponList).Concat(droppedTwoHandedWeaponList).
            Concat(droppedProjectileWeaponList).Concat(droppedStaffList).Concat(droppedShieldList).Concat(droppedTomeList).Concat
            (droppedAmmunitionList).Concat(droppedConsumableList).Concat(droppedMaterialList).Concat(droppedQuestItemList).ToArray();

        //loads all spawned projectiles
        spawnedPhysicalProjectileList = Resources.LoadAll<GameObject>(spawnedProjectilePath + "Physical Projectiles");
        spawnedMagicalProjectileList = Resources.LoadAll<GameObject>(spawnedProjectilePath + "Magical Projectiles");
        spawnedConstantMagicalProjectileList = Resources.LoadAll<GameObject>(spawnedProjectilePath + "Constant Magical Projectiles");

        //merges all spawned projectiles prefab arrays into one
        spawnedProjectilesDirectory = spawnedPhysicalProjectileList.Concat(spawnedMagicalProjectileList).
        Concat(spawnedConstantMagicalProjectileList).ToArray();

    }

    //finds and returns the named item from the appropriate list
    public GameObject FetchPrefab(GameObject[] list, string itemName)
    {
        GameObject toReturn = new GameObject();       
        foreach (GameObject m in list)
        {
            if (m.gameObject.name == itemName)
            {
                toReturn = m;               
            }
        }

        return toReturn;
    }


}
