using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ShedManager : MonoBehaviour {

    public Dictionary<string, Dictionary<string, int>> shedPlants;

    int changeCounter = 0;
    string[] plantArray;

    //Struct for the plants to be serialized in Save()
    [Serializable]
    public struct Plants
    {
        public static Dictionary<string, Dictionary<string, int>> plants;
    }
    


    void Start () {
        //These dummy features are called the first time to set data, otherwise data is already being saved and loaded so just increment the changeCounter 1.
        //With the changeCounter +1, ShedList's update method will be called once to update the plant list.  
        if(shedPlants == null)
        {
            SetPlantFeature("Red Flower", "waterInterval", 1);
            SetPlantFeature("Red Flower", "experience", 0);
            SetPlantFeature("Red Flower", "experiencePerWater", 6);

            DateTime now = DateTime.Now;
            int result = (int)now.Subtract(DateTime.MinValue).TotalMinutes;

            SetPlantFeature("Red Flower", "lastWatered", result);
            SetPlantFeature("Red Flower", "experienceToGrow", 10);
            SetPlantFeature("Red Flower", "experienceToComplete", 20);
        }
        
        Load();
    }

    void Init() {
        if (shedPlants != null)
            return;
        shedPlants = new Dictionary<string, Dictionary<string, int>>();
        plantArray = shedPlants.Keys.ToArray();
    }
	
    //Search plant dictionary for stats on plants
    public int GetPlantFeature(string plantName, string feature) {
        Init();

        if(shedPlants.ContainsKey(plantName) == false) {
            //We have no record for this plant
            return 0;
        }

        if(shedPlants[plantName].ContainsKey(feature) == false) {
            return 0;
        }

        return shedPlants[plantName][feature];
        
    }

    //Add plant stats into dictionary (will be used in the shop)
    public void SetPlantFeature(string plantName, string feature, int value) {
        Init();

        changeCounter++;

        if(shedPlants.ContainsKey(plantName) == false) {
            shedPlants[plantName] = new Dictionary<string, int>();
        }

        shedPlants[plantName][feature] = value;
        
    }

    public string[] GetPlants() {
        Init();
        return shedPlants.Keys.ToArray();
    }

    public void WaterAllPlants()
    {
        
        plantArray = shedPlants.Keys.ToArray();
        foreach (string plant in plantArray)
        {
            //Water plant
            DateTime now = DateTime.Now;
            int result = (int)now.Subtract(DateTime.MinValue).TotalMinutes;
            SetPlantFeature(plant, "lastWatered", result);
        }
        Save();
    }

    public int GetChangeCounter() {
        return changeCounter;
    }

    //Save the user's shed data
    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/shedInfo.dat");
        Plants.plants = shedPlants;
        
        bf.Serialize(file, Plants.plants);
        file.Close();
        Debug.Log("Save called");
        Debug.Log(Application.persistentDataPath + "/shedInfo.dat");
    }

    //Load the user's shed data
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/shedInfo.dat")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/shedInfo.dat", FileMode.Open);
            
            shedPlants = (Dictionary<string, Dictionary<string, int>>) bf.Deserialize(file);
            file.Close();
            Debug.Log("LoadCalled");
        }
    }
	
}
