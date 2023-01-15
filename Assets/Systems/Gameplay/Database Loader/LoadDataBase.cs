using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;

public class LoadDataBase : MonoBehaviour {
    //loads all databases

    public static JsonData unifiedItemDir;

    //publically accessible item databases
    public static JsonData headGearList;
    public static JsonData torsoGearList;
    public static JsonData legGearList;
    public static JsonData footGearList;
    public static JsonData ringList;
    public static JsonData necklaceList;
    public static JsonData shieldList;


    public static JsonData oneHandedWeaponsList;
    public static JsonData twoHandedWeaponsList;
    public static JsonData projectileWeaponsList;
    public static JsonData staffsList;
    public static JsonData tomesList;
    public static JsonData ammunitionList;

    public static JsonData questItemList;
    public static JsonData consumableItemList;
    public static JsonData rawMaterialList;

    //effect databases
    public static JsonData statModifyingEffects;
    public static JsonData durationEffects;

    //outfit databases
    public static JsonData outfitsList;

    //pre-loaded item databases
    public static List<ItemData> armorData = new List<ItemData>();
    public static List<ItemData> weaponData = new List<ItemData>();
    public static List<ItemData> questItemData = new List<ItemData>();
    public static List<ItemData> consumableItemData = new List<ItemData>();
    public static List<ItemData> rawMaterialData = new List<ItemData>();

    public static List<ItemData> combinedItemDataDirectory = new List<ItemData>();

    public static List<ItemData> outfitsDataDirectory = new List<ItemData>();


    private void Awake()
    {
        LoadAllDatabases();
    }


    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void Update() {


    }

    void LoadAllDatabases()
    {
        //loads outfits
        outfitsList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Outfits") as TextAsset).ToString());

        //loads items
        headGearList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Head Gear") as TextAsset).ToString());
        torsoGearList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Torso Gear") as TextAsset).ToString());
        legGearList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Leg Gear") as TextAsset).ToString());
        footGearList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Foot Gear") as TextAsset).ToString());
        ringList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Rings") as TextAsset).ToString());
        necklaceList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Necklaces") as TextAsset).ToString());

        oneHandedWeaponsList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "One Handed Weapons") as TextAsset).ToString());
        twoHandedWeaponsList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Two Handed Weapons") as TextAsset).ToString());
        shieldList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Shields") as TextAsset).ToString());
        projectileWeaponsList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Projectile Weapons") as TextAsset).ToString());
        staffsList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Staffs") as TextAsset).ToString());
        tomesList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Tomes") as TextAsset).ToString());
        ammunitionList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Ammunition") as TextAsset).ToString());

        questItemList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Quest Items") as TextAsset).ToString());
        consumableItemList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Consumable Items") as TextAsset).ToString());
        rawMaterialList = JsonMapper.ToObject((Resources.Load("Databases/Items/" + "Raw Materials") as TextAsset).ToString());

        //loads effects
        statModifyingEffects = JsonMapper.ToObject((Resources.Load("Databases/Effects/" + "Status Effects-Buff&Debuff") as TextAsset).ToString());
        durationEffects = JsonMapper.ToObject((Resources.Load("Databases/Effects/" + "Status Effects-Duration") as TextAsset).ToString());

        //converts json data to item data class instances
        for(int iterator = 0;iterator < headGearList.Count; iterator ++)
        {
            armorData.Add(JsonToItemData.ParseItemFromDatabase(headGearList[iterator]));
        }
        for (int iterator = 0; iterator < torsoGearList.Count; iterator++)
        {
            armorData.Add(JsonToItemData.ParseItemFromDatabase(torsoGearList[iterator]));
        }
        for (int iterator = 0; iterator < legGearList.Count; iterator++)
        {
            armorData.Add(JsonToItemData.ParseItemFromDatabase(legGearList[iterator]));
        }
        for (int iterator = 0; iterator < footGearList.Count; iterator++)
        {
            armorData.Add(JsonToItemData.ParseItemFromDatabase(footGearList[iterator]));
        }

        for (int iterator = 0; iterator < oneHandedWeaponsList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(oneHandedWeaponsList[iterator]));
        }
        for (int iterator = 0; iterator < twoHandedWeaponsList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(twoHandedWeaponsList[iterator]));
        }
        for (int iterator = 0; iterator < shieldList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(shieldList[iterator]));
        }
        for (int iterator = 0; iterator < projectileWeaponsList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(projectileWeaponsList[iterator]));
        }
        for (int iterator = 0; iterator < staffsList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(staffsList[iterator]));
        }
        for (int iterator = 0; iterator < tomesList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(tomesList[iterator]));
        }
        for (int iterator = 0; iterator < ammunitionList.Count; iterator++)
        {
            weaponData.Add(JsonToItemData.ParseItemFromDatabase(ammunitionList[iterator]));
        }

        for (int iterator = 0; iterator < questItemList.Count; iterator++)
        {
            questItemData.Add(JsonToItemData.ParseItemFromDatabase(questItemList[iterator]));
        }

        for (int iterator = 0; iterator < consumableItemList.Count; iterator++)
        {
            consumableItemData.Add(JsonToItemData.ParseItemFromDatabase(consumableItemList[iterator]));
        }

        for (int iterator = 0; iterator < rawMaterialList.Count; iterator++)
        {
            rawMaterialData.Add(JsonToItemData.ParseItemFromDatabase(rawMaterialList[iterator]));
        }

        combinedItemDataDirectory = armorData.Concat(weaponData).Concat(questItemData).Concat(consumableItemData).Concat(rawMaterialData).ToList();

        for (int iterator = 0; iterator < outfitsList.Count; iterator++)
        {
            outfitsDataDirectory.Add(JsonToItemData.ParseItemFromDatabase(outfitsList[iterator]));
        }

    }

    //returns an item by name
    public static ItemData FindItemData(string itemNameInput)
    {
        ItemData returnedItem = new ItemData();
        foreach (ItemData item in combinedItemDataDirectory)
        {
            if (item.ItemName == itemNameInput)
            {
                returnedItem = item;
            }
        }

        return returnedItem;
    }

    //returns an outfit by name
    public static ItemData FindoutfitData(string OutfitName)
    {
        ItemData returnedItem = new ItemData();
        foreach (ItemData item in outfitsDataDirectory)
        {
            if (item.ItemName == OutfitName)
            {
                returnedItem = item;
            }
        }

        return returnedItem;
    }

}
