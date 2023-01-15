using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class AnimEquiphandler : MonoBehaviour {
    //attach to an entity that can equip weapons or armor

    //parent trasnforms for animated gear
    public Transform head;
    public Transform torso;
    public Transform legs;
    public Transform feet;

    public Transform rightHand;
    public Transform leftHand;

    public Transform necklace;
    public Transform rightHandRing;
    public Transform leftHandRing;

    public Transform outFitSlot;

    //reference to global 3D model loader
    public Load3DMdlDataBase MDLloader;

    //reference to local animation controller shell
    public AnimationControllerShell entityAnimControllerShell;

    //reference to local attack handler
    public AttackHandler entityAttackHandler;

    //reference to outfit manager
    public OutfitManager entityOutfitManager;

    // Use this for initialization
    void Start () {
        MDLloader = GameObject.Find("Databases").GetComponent<Load3DMdlDataBase>();
        entityAnimControllerShell = gameObject.GetComponent<AnimationControllerShell>();
        entityAttackHandler = gameObject.GetComponent<AttackHandler>();
        entityOutfitManager = gameObject.GetComponent<OutfitManager>();
        InitgearParentTransforms();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //initializer
    public void InitgearParentTransforms()
    {
        head = gameObject.transform.Find("Equipment").Find("Head");
        torso = gameObject.transform.Find("Equipment").Find("Torso");
        legs = gameObject.transform.Find("Equipment").Find("Legs");
        feet = gameObject.transform.Find("Equipment").Find("Feet");
        rightHand = gameObject.transform.Find("Equipment").Find("Right Hand");
        leftHand = gameObject.transform.Find("Equipment").Find("Left Hand");
        necklace = gameObject.transform.Find("Equipment").Find("Necklace");
        rightHandRing = gameObject.transform.Find("Equipment").Find("Right Hand Ring");
        leftHandRing = gameObject.transform.Find("Equipment").Find("Left Hand Ring");

        outFitSlot = gameObject.transform.Find("Outfit");

    }

    //instantiates animated gear object when gear is equipepd
    public void EquipItem(ItemData itemInfo, int ringSlotNum = 1)
    {
        switch (itemInfo.ItemType)
        {
            case "Weapon":
                string weaponType = itemInfo.WeaponType;
                if (weaponType == "Shield")
                {
                    UnequipItem(itemInfo);
                    GameObject instantiatedGear = Instantiate(MDLloader.FetchPrefab(MDLloader.equipItemDirectory,
                    itemInfo.ItemName), leftHand, false);
                    instantiatedGear.GetComponent<ToggleWeaponEnabled>().itemInfo = itemInfo;
                    instantiatedGear.GetComponent<Animator>().SetBool("IsWpnDrawn",gameObject.GetComponent<AnimationControllerShell>().coreAnim.GetBool("IsWpnDrawn"));
                    instantiatedGear.transform.localPosition = new Vector3(0, -2.476f, 0);
                    entityAnimControllerShell.animatorGroup.Add(instantiatedGear.GetComponent<Animator>());
                }
                else if (weaponType == "Tome")
                {
                    //change magical projectile here
                    switch (itemInfo.MagicType)
                    {
                        case "Constant":
                            entityAttackHandler.staffConstantObject = MDLloader.FetchPrefab(MDLloader.spawnedProjectilesDirectory,
                            itemInfo.ItemName);
                            break;
                        case "Charge":
                            entityAttackHandler.staffProjectile = MDLloader.FetchPrefab(MDLloader.spawnedProjectilesDirectory,
                            itemInfo.ItemName);
                            break;
                    }
                }
                else if (weaponType == "Ammunition")
                {
                    //change physical projectile here
                    entityAttackHandler.physicalProjectile = MDLloader.FetchPrefab(MDLloader.spawnedProjectilesDirectory,
                    itemInfo.ItemName);
                }
                else{
                    UnequipItem(itemInfo);
                    GameObject instantiatedGear = Instantiate(MDLloader.FetchPrefab(MDLloader.equipItemDirectory,
                    itemInfo.ItemName), rightHand, false);
                    instantiatedGear.GetComponent<ToggleWeaponEnabled>().itemInfo = itemInfo;
                    instantiatedGear.GetComponent<Animator>().SetBool("IsWpnDrawn", gameObject.GetComponent<AnimationControllerShell>().coreAnim.GetBool("IsWpnDrawn"));
                    instantiatedGear.transform.localPosition = new Vector3(0, -2.476f, 0);
                    entityAnimControllerShell.animatorGroup.Add(instantiatedGear.GetComponent<Animator>());
                    if (itemInfo.CombatType == "Bow")
                    {
                        entityAnimControllerShell.projectileWpnAnim = instantiatedGear.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
                       "upper_arm_R/lower_arm_R/hand_R/").GetChild(5).gameObject.GetComponent<Animator>();
                        instantiatedGear.GetComponentInChildren<ToggleWeaponEnabled>().itemInfo = itemInfo;
                    }
                    else if (itemInfo.CombatType == "Crossbow")
                    {
                        entityAnimControllerShell.projectileWpnAnim = instantiatedGear.transform.Find("Humanoid_Armature/lower_spine/middle_spine/upper_spine/" +
                        "upper_arm_L_001/lower_arm_L_001/hand_L_001/").GetChild(5).gameObject.GetComponent<Animator>();
                        instantiatedGear.GetComponentInChildren<ToggleWeaponEnabled>().itemInfo = itemInfo;
                    }
                    else if (itemInfo.CombatType == "Staff")
                    {
                        entityAnimControllerShell.projectileWpnAnim = instantiatedGear.gameObject.GetComponent<Animator>();

                    }
                }
                break;
            case "Outfit":
                //equips an outfit
                foreach (Transform child in outFitSlot)
                {
                    entityAnimControllerShell.animatorGroup.Remove(child.gameObject.GetComponent<Animator>());
                    Destroy(child.gameObject);
                }
                entityOutfitManager.equippedOutfit = entityOutfitManager.FindOutfitinAcquired(itemInfo.ItemName);
                GameObject instantiatedOutfit = Instantiate(MDLloader.FetchPrefab(MDLloader.outFitList, itemInfo.ItemName), outFitSlot, false);
                instantiatedOutfit.transform.localPosition = new Vector3(0, -2.476f, 0);
                entityAnimControllerShell.animatorGroup.Add(instantiatedOutfit.GetComponent<Animator>());
                entityOutfitManager.UpdateOutfitBonuses();
                break;

        }
    }

    //destroys animated gear object when gear is unequipped
    public void UnequipItem(ItemData itemInfo, int ringSlotNum = 1)
    {
        switch (itemInfo.ItemType)
        {
            case "Weapon":
                string weaponType = itemInfo.WeaponType;
                if (weaponType == "Shield")
                {
                    if (leftHand.childCount > 0)
                    {
                        foreach (Transform child in leftHand)
                        {
                            entityAnimControllerShell.animatorGroup.Remove(child.gameObject.GetComponent<Animator>());
                            Destroy(child.gameObject);
                        }
                    }
                }
                else if (weaponType == "Tome")
                {
                    if (leftHand.childCount > 0)
                    {
                        foreach (Transform child in leftHand)
                        {
                            entityAnimControllerShell.animatorGroup.Remove(child.gameObject.GetComponent<Animator>());
                            Destroy(child.gameObject);
                        }
                    }
                }
                else
                {
                    if (rightHand.childCount > 0)
                    {
                        foreach (Transform child in rightHand)
                        {
                            entityAnimControllerShell.animatorGroup.Remove(child.gameObject.GetComponent<Animator>());
                            Destroy(child.gameObject);
                        }
                    }
                    if (itemInfo.CombatType == "Bow")
                    {
                        entityAnimControllerShell.projectileWpnAnim = new Animator();
                    }
                    else if (itemInfo.CombatType == "Crossbow")
                    {
                        entityAnimControllerShell.projectileWpnAnim = new Animator();
                    }
                }
                break;
            case "Outfit":
                //unequips outfit and sets it back to default, "Simple Outfit"
                foreach (Transform child in outFitSlot)
                {
                    entityAnimControllerShell.animatorGroup.Remove(child.gameObject.GetComponent<Animator>());
                    Destroy(child.gameObject);
                }
                entityOutfitManager.equippedOutfit = entityOutfitManager.FindOutfitinAcquired("Simple Outfit");
                GameObject instantiatedGear = Instantiate(MDLloader.FetchPrefab(MDLloader.outFitList, "Simple Outfit"), outFitSlot, false);

                instantiatedGear.transform.localPosition = new Vector3(0, -2.476f, 0);
                entityAnimControllerShell.animatorGroup.Add(instantiatedGear.GetComponent<Animator>());
                entityOutfitManager.UpdateOutfitBonuses();
                break;
        }
    }

}
