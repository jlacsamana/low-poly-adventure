using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitButtonData : MonoBehaviour
{
    //stores outfit data 
    public ItemData outfitData;

    //outfit name tex label
    public Text outfitNameTxt;

    //player outfit manager
    public OutfitManager playerOutfitManager;

    //reference to the Character Sub UI manager
    public CharacterSubUIManager globalCharSubUIManager;

    // Start is called before the first frame update
    void Start()
    {
        playerOutfitManager = GameObject.Find("Player").GetComponent<OutfitManager>();
        globalCharSubUIManager = GameObject.Find("Systems").transform.Find("Player Journal UI Manager").gameObject.GetComponent<CharacterSubUIManager>();
        //outfitNameTxt = gameObject.transform.Find("Outfit Name").gameObject.GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //initializes button
    public void InitButton()
    {
        outfitNameTxt.text = outfitData.ItemName;
    }

    //equip an item
    public void EquipOutfit()
    {
        playerOutfitManager.entityEquipHandler.EquipItem(outfitData);
        globalCharSubUIManager.UpdateOutfitViewport();
        globalCharSubUIManager.UpdateSelectedOutfitView();

    }
}
