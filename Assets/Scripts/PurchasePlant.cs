using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PurchasePlant : MonoBehaviour {

    ShedManager shedManager;

    //Dictionary storing all the plants stats inside the store
    public Dictionary<string, Dictionary<string, int>> storePlants;

    void Start () {
        shedManager = GameObject.FindObjectOfType<ShedManager>();

        //Create the dictionary of data for the Shop
        if(storePlants == null) {
            storePlants = new Dictionary<string, Dictionary<string, int>>();

            if (storePlants.ContainsKey("Red Flower") == false) {
                storePlants["Red Flower"] = new Dictionary<string, int>();
                storePlants["Red Flower"]["waterInterval"] = 1;
                storePlants["Red Flower"]["experience"] = 0;
            }
            if (storePlants.ContainsKey("Blue Flower") == false) {
                storePlants["Blue Flower"] = new Dictionary<string, int>();
                storePlants["Blue Flower"]["waterInterval"] = 2;
                storePlants["Blue Flower"]["experience"] = 0;
            }
            if (storePlants.ContainsKey("Green Flower") == false) {
                storePlants["Green Flower"] = new Dictionary<string, int>();
                storePlants["Green Flower"]["waterInterval"] = 3;
                storePlants["Green Flower"]["experience"] = 0;
            }
            if (storePlants.ContainsKey("Purple Flower") == false) {
                storePlants["Purple Flower"] = new Dictionary<string, int>();
                storePlants["Purple Flower"]["waterInterval"] = 5;
                storePlants["Purple Flower"]["experience"] = 0;
            }
        }
    }

    public void buyPlant() {
        //get the name of the button currently pressed (should match it's inner text so this is fine).
        string selectedPlantString = EventSystem.current.currentSelectedGameObject.name;
        string selectedPlant = selectedPlantString.Substring(4); //Cut off the word "buy "

        //Get the current plants stats
        Dictionary<string, int> currentPlantStats = storePlants[selectedPlant];
        int waterInterval = currentPlantStats["waterInterval"];
        int experience = currentPlantStats["experience"];

        //Make sure the plant doesn't already exist in the shed
        if (shedManager.shedPlants.ContainsKey(selectedPlant))
        {
            return;
        } else
        {
            //Upon purchase, add this plants features to the shed. 
            shedManager.SetPlantFeature(selectedPlant, "waterInterval", waterInterval);
            shedManager.SetPlantFeature(selectedPlant, "experience", experience);
            DateTime now = DateTime.Now;
            shedManager.SetPlantFeature(selectedPlant, "lastWatered", (int)now.Subtract(DateTime.MinValue).TotalMinutes);
            shedManager.Save();
        }

        
    }

    

}
