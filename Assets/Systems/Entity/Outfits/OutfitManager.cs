using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitManager : MonoBehaviour
{
    //this class handles outfit equipping

    //reference to anim equip handler
    public AnimEquiphandler entityEquipHandler;

    //stats added by current outfit
    public int outfitAddedHealth = 0;
    public int outfitAddedMana = 0;
    public int outfitAddedStamina = 0;
    public float outfitAddedSpeed = 0;

    public int outfitAddedArmor = 0;
    public int outfitAddedMagicalArmor = 0;

    public int outfitAddedPhysicalDamage = 0;
    public int outfitAddedMagicalDamage = 0;
    public float outfitAddedCritChance = 0;

    //mesh body parts 
    public GameObject headMesh;
    public GameObject armMesh;
    public GameObject handsMesh;
    public GameObject torsoMesh;
    public GameObject legsMesh;
    public GameObject feetMesh;

    //list of equippable outfits
    public List<ItemData> acquiredOutfits = new List<ItemData>();

    //current equipped outfit; default should be "Simple Outfit"
    public ItemData equippedOutfit;

    // Start is called before the first frame update
    void Start()
    {
        entityEquipHandler = gameObject.GetComponent<AnimEquiphandler>();
        Invoke("InitDefaultEquipment",0.01f);
        headMesh = gameObject.transform.Find("Humanoid M Template/Head").gameObject;
        armMesh = gameObject.transform.Find("Humanoid M Template/Arms").gameObject;
        handsMesh = gameObject.transform.Find("Humanoid M Template/Hands").gameObject;
        torsoMesh = gameObject.transform.Find("Humanoid M Template/Torso").gameObject;
        legsMesh = gameObject.transform.Find("Humanoid M Template/Legs").gameObject;
        feetMesh = gameObject.transform.Find("Humanoid M Template/Feet").gameObject;


    }




    // Update is called once per frame
    void Update()
    {
        
    }

    //updates bonuses added by outfit && mesh parts culled
    public void UpdateOutfitBonuses()
    {
        //updates added health
        outfitAddedHealth = equippedOutfit.Health;
        outfitAddedMana = equippedOutfit.Mana;
        outfitAddedStamina = equippedOutfit.Stamina;
        outfitAddedSpeed = equippedOutfit.Speed;

        outfitAddedArmor = equippedOutfit.Armor;
        outfitAddedMagicalArmor = equippedOutfit.MagicResistance;

        outfitAddedPhysicalDamage = equippedOutfit.PhysicalDamage;
        outfitAddedMagicalDamage = equippedOutfit.MagicalDamage;
        outfitAddedCritChance = equippedOutfit.CriticalChance;

        //updates culled meshes 
        if (equippedOutfit.coversHead == true)
        {
            headMesh.SetActive(false);
        }
        else
        {
            headMesh.SetActive(true);
        }

        if (equippedOutfit.coversArms == true)
        {
            armMesh.SetActive(false);
        }
        else
        {
            armMesh.SetActive(true);
        }

        if (equippedOutfit.coversHands == true)
        {
            handsMesh.SetActive(false);
        }
        else
        {
            handsMesh.SetActive(true);
        }

        if (equippedOutfit.coversTorso)
        {
            torsoMesh.SetActive(false);
        }
        else
        {
            torsoMesh.SetActive(true);
        }

        if (equippedOutfit.coversLegs)
        {
            legsMesh.SetActive(false);
        }
        else
        {
            legsMesh.SetActive(true);
        }

        if (equippedOutfit.coversFeet)
        {
            feetMesh.SetActive(false);
        }
        else
        {
            feetMesh.SetActive(true);
        }












    }
    
    //initializes default equipment
    public void InitDefaultEquipment()
    {
        acquiredOutfits.Add(LoadDataBase.FindoutfitData("Simple Outfit"));
        entityEquipHandler.EquipItem(FindOutfitinAcquired("Simple Outfit"));

    }

    //finds an outfit in acquiredOutfits by name
    public ItemData FindOutfitinAcquired(string outfitName)
    {
        ItemData returnOutfit = new ItemData();
        foreach (ItemData outFit in acquiredOutfits)
        {
            if (outFit.ItemName == outfitName)
            {
                returnOutfit = outFit;
            }
        }
        
        return returnOutfit;
    }

   

}
