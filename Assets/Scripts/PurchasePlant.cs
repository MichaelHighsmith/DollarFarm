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
            Debug.Log("storePlants created");
            storePlants = new Dictionary<string, Dictionary<string, int>>();

            if (storePlants.ContainsKey("Red Flower") == false) {
                storePlants["Red Flower"] = new Dictionary<string, int>();
                storePlants["Red Flower"]["waterInterval"] = 1;
                storePlants["Red Flower"]["experience"] = 0;
                storePlants["Red Flower"]["experiencePerWater"] = 4;
                storePlants["Red Flower"]["experienceToGrow"] = 10;
                storePlants["Red Flower"]["experienceToComplete"] = 20;
            }
            if (storePlants.ContainsKey("Blue Flower") == false) {
                storePlants["Blue Flower"] = new Dictionary<string, int>();
                storePlants["Blue Flower"]["waterInterval"] = 3;
                storePlants["Blue Flower"]["experience"] = 0;
                storePlants["Blue Flower"]["experiencePerWater"] = 6;
                storePlants["Blue Flower"]["experienceToGrow"] = 10;
                storePlants["Blue Flower"]["experienceToComplete"] = 20;
            }
            if (storePlants.ContainsKey("Green Flower") == false) {
                storePlants["Green Flower"] = new Dictionary<string, int>();
                storePlants["Green Flower"]["waterInterval"] = 10;
                storePlants["Green Flower"]["experience"] = 0;
                storePlants["Green Flower"]["experiencePerWater"] = 8;
                storePlants["Green Flower"]["experienceToGrow"] = 20;
                storePlants["Green Flower"]["experienceToComplete"] = 35;
            }
            if (storePlants.ContainsKey("Purple Flower") == false) {
                storePlants["Purple Flower"] = new Dictionary<string, int>();
                storePlants["Purple Flower"]["waterInterval"] = 15;
                storePlants["Purple Flower"]["experience"] = 0;
                storePlants["Purple Flower"]["experiencePerWater"] = 10;
                storePlants["Purple Flower"]["experienceToGrow"] = 30;
                storePlants["Purple Flower"]["experienceToComplete"] = 50;
            }
            if(storePlants.ContainsKey("Lavender Spring") == false)
            {
                storePlants["Lavender Spring"] = new Dictionary<string, int>();
                storePlants["Lavender Spring"]["waterInterval"] = 30;
                storePlants["Lavender Spring"]["experience"] = 0;
                storePlants["Lavender Spring"]["experiencePerWater"] = 20;
                storePlants["Lavender Spring"]["experienceToGrow"] = 75;
                storePlants["Lavender Spring"]["experienceToComplete"] = 150;
            }
            if (storePlants.ContainsKey("Sky Violet") == false)
            {
                storePlants["Sky Violet"] = new Dictionary<string, int>();
                storePlants["Sky Violet"]["waterInterval"] = 45;
                storePlants["Sky Violet"]["experience"] = 0;
                storePlants["Sky Violet"]["experiencePerWater"] = 45;
                storePlants["Sky Violet"]["experienceToGrow"] = 100;
                storePlants["Sky Violet"]["experienceToComplete"] = 200;
            }
            if (storePlants.ContainsKey("FireFly") == false)
            {
                storePlants["FireFly"] = new Dictionary<string, int>();
                storePlants["FireFly"]["waterInterval"] = 60;
                storePlants["FireFly"]["experience"] = 0;
                storePlants["FireFly"]["experiencePerWater"] = 100;
                storePlants["FireFly"]["experienceToGrow"] = 250;
                storePlants["FireFly"]["experienceToComplete"] = 500;
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
        int experiencePerWater = currentPlantStats["experiencePerWater"];
        int experienceToGrow = currentPlantStats["experienceToGrow"];
        int experienceToComplete = currentPlantStats["experienceToComplete"];

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
            shedManager.SetPlantFeature(selectedPlant, "experiencePerWater", experiencePerWater);
            shedManager.SetPlantFeature(selectedPlant, "experienceToGrow", experienceToGrow);
            shedManager.SetPlantFeature(selectedPlant, "experienceToComplete", experienceToComplete);
            shedManager.Save();
        }

        
    }

    

}
