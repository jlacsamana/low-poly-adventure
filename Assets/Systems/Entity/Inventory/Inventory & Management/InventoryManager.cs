using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    //attach this to any entity with an inventory

    //this entity's inventory
    public Inventory entityInventoryComponent;

    //reference to Item view manager UI
    public DisplayCurrentItemList itemViewManager;

    //reference to entity
    public Entity entityMain;
    
    //reference to journal Ui Manager
    public CurrentStatDisplay statViewManager;

    //refereence to local aniamtor controller shell
    public AnimationControllerShell localAnimatorShell;

    //reference to attack system
    public AttackSystem entityCombat;

    //reference to local gear animation handler
    public AnimEquiphandler animEquip;

    //reference to player shield system
    public ShieldSystem entityShieldSys;

    //reference to NPC combat handler


    // Use this for initialization
    void Start () {
        entityInventoryComponent = gameObject.GetComponent<Inventory>();
        localAnimatorShell = gameObject.GetComponent<AnimationControllerShell>();
        animEquip = gameObject.GetComponent<AnimEquiphandler>();
        entityCombat = gameObject.GetComponent<AttackSystem>();
        entityMain = gameObject.GetComponent<Entity>();
        entityShieldSys = gameObject.GetComponent<ShieldSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //equips an item
    public void Equip(ItemData gearItem)
    {
        if (entityCombat.isAttacking == false)
        {
            int ringSlot = 1;
            switch (gearItem.ItemType)
            {
                case "Weapon":
                    switch (gearItem.WeaponType)
                    {
                        case "Two Handed":
                            ReturnAmmoToInv();
                            if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                            }
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                            }
                            entityInventoryComponent.entityLeftHandSlot.ItemName = "Empty Item Slot";
                            entityInventoryComponent.entityInventory.Remove(gearItem);
                            entityInventoryComponent.entityRightHandSlot = gearItem;
                            animEquip.EquipItem(gearItem, ringSlot);
                            break;
                        case "One Handed":
                            ReturnAmmoToInv();
                            if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                            }
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                            }
                            entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                            entityInventoryComponent.entityInventory.Remove(gearItem);
                            entityInventoryComponent.entityRightHandSlot = gearItem;
                            animEquip.EquipItem(gearItem, ringSlot);
                            break;
                        case "Shield":
                            if (entityInventoryComponent.entityRightHandSlot.WeaponType == "One Handed")
                            {
                                if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                }
                                entityInventoryComponent.entityInventory.Remove(gearItem);
                                entityInventoryComponent.entityLeftHandSlot = gearItem;
                                animEquip.EquipItem(gearItem, ringSlot);
                                entityShieldSys.SetShield(entityInventoryComponent.entityLeftHandSlot.ShieldDurability);
                            }
                            break;
                        case "Staff":
                            ReturnAmmoToInv();
                            if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                            }
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                            }
                            entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                            entityInventoryComponent.entityInventory.Remove(gearItem);
                            entityInventoryComponent.entityRightHandSlot = gearItem;
                            animEquip.EquipItem(gearItem, ringSlot);
                            break;
                        case "Projectile Weapon":
                            ReturnAmmoToInv();
                            if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                            }
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                            }
                            entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                            entityInventoryComponent.entityInventory.Remove(gearItem);
                            entityInventoryComponent.entityRightHandSlot = gearItem;
                            animEquip.EquipItem(gearItem, ringSlot);
                            break;
                        case "Ammunition":
                            switch (entityInventoryComponent.entityRightHandSlot.CombatType)
                            {
                                case "Bow":
                                    if (gearItem.AmmunitionType == "Bow Ammo")
                                    {                                     
                                        foreach (ItemData ammo in entityInventoryComponent.AmmoQuiver)
                                        {
                                            entityInventoryComponent.entityInventory.Add(ammo);
                                        }
                                        entityInventoryComponent.AmmoQuiver = new List<ItemData>();
                                        List<ItemData> removeList = new List<ItemData>();
                                        foreach (ItemData item in entityInventoryComponent.entityInventory)
                                        {
                                            if (item.CheckIfEqual(gearItem))
                                            {
                                                entityInventoryComponent.AmmoQuiver.Add(item);
                                                removeList.Add(item);
                                            }
                                        }
                                        foreach (ItemData removeItem in removeList)
                                        {
                                            entityInventoryComponent.entityInventory.Remove(removeItem);
                                        }
                                        animEquip.EquipItem(gearItem, ringSlot);
                                    }
                                    break;
                                case "Crossbow":
                                    if (gearItem.AmmunitionType == "Crossbow Ammo")
                                    {
                                        foreach (ItemData ammo in entityInventoryComponent.AmmoQuiver)
                                        {
                                            entityInventoryComponent.entityInventory.Add(ammo);
                                        }
                                        entityInventoryComponent.AmmoQuiver = new List<ItemData>();
                                        List<ItemData> removeList = new List<ItemData>();
                                        foreach (ItemData item in entityInventoryComponent.entityInventory)
                                        {
                                            if (item.CheckIfEqual(gearItem))
                                            {
                                                entityInventoryComponent.AmmoQuiver.Add(item);
                                                removeList.Add(item);
                                            }
                                        }
                                        foreach (ItemData removeItem in removeList)
                                        {
                                            entityInventoryComponent.entityInventory.Remove(removeItem);
                                        }
                                    }
                                    animEquip.EquipItem(gearItem, ringSlot);
                                    break;
                            }
                            break;
                        case "Tome":
                            if (entityInventoryComponent.entityRightHandSlot.WeaponType == "Staff")
                            {
                                if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                }
                                entityInventoryComponent.entityInventory.Remove(gearItem);
                                entityInventoryComponent.entityLeftHandSlot = gearItem;
                                animEquip.EquipItem(gearItem, ringSlot);
                            }
                            break;
                    }
                    break;
                case "Armor":
                    switch (gearItem.ArmorType)
                    {
                        case "Head":
                            if (entityInventoryComponent.entityHeadSlot.ItemName != "No Helmet")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityHeadSlot = gearItem;
                            entityInventoryComponent.entityInventory.Remove(gearItem);
     
                            break;
                        case "Chest":
                            if (entityInventoryComponent.entityTorsoSlot.ItemName != "No ChestPlate")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityTorsoSlot = gearItem;
                            entityInventoryComponent.entityInventory.Remove(gearItem);

                            break;
                        case "Legs":
                            if (entityInventoryComponent.entityLegsSlot.ItemName != "No Greaves")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityLegsSlot = gearItem;
                            entityInventoryComponent.entityInventory.Remove(gearItem);
   
                            break;
                        case "Foot":
                            if (entityInventoryComponent.entityFeetSlot.ItemName != "No Boots")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityFeetSlot = gearItem;
                            entityInventoryComponent.entityInventory.Remove(gearItem);
 
                            break;
                        case "Necklace":
                            if (entityInventoryComponent.entityNecklaceSlot.ItemName != "No Necklace")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityNecklaceSlot = gearItem;
                            entityInventoryComponent.entityInventory.Remove(gearItem);

                            break;
                        case "Ring":
                            if (entityInventoryComponent.entityRightRingSlot.ItemName == "No Ring")
                            {
                                entityInventoryComponent.entityRightRingSlot = gearItem;
                                entityInventoryComponent.entityInventory.Remove(gearItem);
                                ringSlot = 1;
             
                            }
                            else if (entityInventoryComponent.entityLeftRingSlot.ItemName == "No Ring")
                            {
                                entityInventoryComponent.entityRightRingSlot = gearItem;
                                entityInventoryComponent.entityInventory.Remove(gearItem);
                                ringSlot = 2;
                
                            }
                            else if (entityInventoryComponent.entityLeftRingSlot.ItemName != "No Ring" && entityInventoryComponent.entityRightRingSlot.ItemName != "No Ring")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightRingSlot);
                                entityInventoryComponent.entityRightRingSlot = gearItem;
                                entityInventoryComponent.entityInventory.Remove(gearItem);
                                ringSlot = 2;
              
                            }
                            break;
                    }
                    break;
            }
            //localAnimatorShell.UpdateAnimatorList();
            entityCombat.AttackTypeListener();
            localAnimatorShell.AttackStateChecker();
            itemViewManager.UpdateItemListView();
            itemViewManager.UpdateEquipItemListView();
            statViewManager.UpdateValues();
        }

    }

    //unequips an item
    public void Unequip(ItemData gearItem)
    {
        if (entityCombat.isAttacking == false)
        {
            int ringSlot = 1;
            switch (gearItem.ItemType)
            {
                case "Weapon":
                    switch (gearItem.WeaponType)
                    {
                        case "Two Handed":
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                ReturnAmmoToInv();
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                                entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                                entityInventoryComponent.entityRightHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Bare Hands"]);
                                animEquip.UnequipItem(gearItem, ringSlot);
                            }
                            break;
                        case "One Handed":
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                ReturnAmmoToInv();
                                if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                }
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                                entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                                entityInventoryComponent.entityRightHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Bare Hands"]);
                                animEquip.UnequipItem(gearItem, ringSlot);
                            }
                            break;
                        case "Shield":
                            if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                                animEquip.UnequipItem(gearItem, ringSlot);
                                entityShieldSys.SetShield(0);
                            }
                            break;
                        case "Staff":
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                ReturnAmmoToInv();
                                if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                }
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                                entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                                entityInventoryComponent.entityRightHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Bare Hands"]);
                                animEquip.UnequipItem(gearItem, ringSlot);
                            }
                            break;
                        case "Projectile Weapon":
                            if (entityInventoryComponent.entityRightHandSlot.ItemName != "Bare Hands")
                            {
                                ReturnAmmoToInv();
                                if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                }
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightHandSlot);
                                entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                                entityInventoryComponent.entityRightHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.twoHandedWeaponsList["Bare Hands"]);
                                animEquip.UnequipItem(gearItem, ringSlot);
                            }
                            break;
                        case "Ammmunition":
                            if (entityInventoryComponent.AmmoQuiver.Count > 0)
                            {
                                foreach (ItemData ammo in entityInventoryComponent.AmmoQuiver)
                                {
                                    entityInventoryComponent.entityInventory.Add(ammo);
                                }
                                entityInventoryComponent.AmmoQuiver = new List<ItemData>();
                                animEquip.UnequipItem(gearItem, ringSlot);
                            }
                            break;
                        case "Tome":
                            if (entityInventoryComponent.entityLeftHandSlot.ItemName != "Empty Item Slot")
                            {
                                entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftHandSlot);
                                entityInventoryComponent.entityLeftHandSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.oneHandedWeaponsList["Empty Item Slot"]);
                                animEquip.UnequipItem(gearItem, ringSlot);
                            }
                            break;
                    }
                    break;
                case "Armor":
                    switch (gearItem.ArmorType)
                    {
                        case "Head":
                            if (entityInventoryComponent.entityHeadSlot.ItemName != "No Helmet")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityHeadSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.headGearList["No Helmet"]);
    
                            break;
                        case "Chest":
                            if (entityInventoryComponent.entityTorsoSlot.ItemName != "No ChestPlate")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityTorsoSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.torsoGearList["No ChestPlate"]);
              
                            break;
                        case "Legs":
                            if (entityInventoryComponent.entityLegsSlot.ItemName != "No Greaves")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityLegsSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.legGearList["No Greaves"]);
           
                            break;
                        case "Foot":
                            if (entityInventoryComponent.entityFeetSlot.ItemName != "No Boots")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityFeetSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.footGearList["No Boots"]);
               
                            break;
                        case "Necklace":
                            if (entityInventoryComponent.entityNecklaceSlot.ItemName != "No Necklace")
                            {
                                entityInventoryComponent.entityInventory.Add(gearItem);
                            }
                            entityInventoryComponent.entityNecklaceSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.necklaceList["No Necklace"]);
                
                            break;
                        case "Ring":
                            if (entityInventoryComponent.entityLeftRingSlot == gearItem)
                            {
                                ringSlot = 2;
                                if (entityInventoryComponent.entityLeftRingSlot.ItemName != "No Ring")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityLeftRingSlot);
                                }
                                entityInventoryComponent.entityLeftRingSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.ringList["No Ring"]);
                     
                            }
                            else if (entityInventoryComponent.entityRightRingSlot == gearItem)
                            {
                                ringSlot = 1;
                                if (entityInventoryComponent.entityRightRingSlot.ItemName != "No Ring")
                                {
                                    entityInventoryComponent.entityInventory.Add(entityInventoryComponent.entityRightRingSlot);
                                }
                                entityInventoryComponent.entityRightRingSlot = JsonToItemData.ParseItemFromDatabase(LoadDataBase.ringList["No Ring"]);
         
                            }
                            break;


                    }
                    break;
            }
            //localAnimatorShell.UpdateAnimatorList();
            entityCombat.AttackTypeListener();
            localAnimatorShell.AttackStateChecker();
            itemViewManager.UpdateItemListView();
            itemViewManager.UpdateEquipItemListView();
            statViewManager.UpdateValues();
        }
    }

    //consume a consumable
    public void ConsumeItem(ItemData consumableItem)
    {
        //applies instant hp/mana/stamina gain or loss
        entityMain.currentEntityHealth += consumableItem.Health;
        entityMain.currentEntityMana += consumableItem.Mana;
        entityMain.currentEntityStamina += consumableItem.Stamina;

        //creates an empty list to put into function; consumables will never target enemies, so that parameter is useless in this case
        GameObject[] dummyList = new GameObject[0];

        //applies any status effects attached to consumable
        WorldEffectSystem.BestowStatusEffect(consumableItem.EffectNameList,
        consumableItem.EffectTypeList, consumableItem.EffectChanceList,
        consumableItem.EffectDurationList,consumableItem.EffectTargetList,
        gameObject ,dummyList
        );

        entityInventoryComponent.entityInventory.Remove(consumableItem);
        itemViewManager.UpdateItemListView();
        itemViewManager.UpdateEquipItemListView();
    }

    //drops item(s)
    public void DropItems(ItemData droppableItem, int itemQuantity)
    {
        List<ItemData> itemsToRemove = new List<ItemData>();
        foreach (ItemData m in entityInventoryComponent.entityInventory)
        {
            if (m.CheckIfEqual(droppableItem))
            {
                itemsToRemove.Add(m);
            }
        }

        int counter = 0;
        while ( counter < itemQuantity)
        {
            entityInventoryComponent.entityInventory.Remove(itemsToRemove[counter]);
            counter++;
        }

        itemViewManager.UpdateItemListView();
        itemViewManager.UpdateEquipItemListView();
    }

    //returns all ammo to inventory
    public void ReturnAmmoToInv()
    {
        //move ammo back to inventory
        if (entityInventoryComponent.AmmoQuiver.Count > 0)
        {
            foreach (ItemData ammo in entityInventoryComponent.AmmoQuiver)
            {
                entityInventoryComponent.entityInventory.Add(ammo);
            }
            entityInventoryComponent.AmmoQuiver = new List<ItemData>();
        }
    }



}
