using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShedList : MonoBehaviour {

    public GameObject plantEntryPrefab;

    ShedManager shedManager;

    int lastChangeCounter;
	
	void Start () {

        shedManager = GameObject.FindObjectOfType<ShedManager>();

        lastChangeCounter = shedManager.GetChangeCounter();
       
	}
	
	void Update () {
        if (shedManager == null) {
            Debug.LogError("Forgot to add shed manager component to a game object!");
            return;
        }
        
        if(shedManager.GetChangeCounter() == lastChangeCounter) {
            //no change since last update (so we dont need to update texts)
            return;
        }

        while(this.transform.childCount > 0)
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }

        string[] names = shedManager.GetPlants();

        foreach (string name in names)
        {
            GameObject go = (GameObject)Instantiate(plantEntryPrefab);
            go.transform.SetParent(this.transform, false);

            go.transform.Find("PlantName").GetComponent<Text>().text = name;
            go.transform.Find("WaterInterval").GetComponent<Text>().text = shedManager.GetPlantFeature(name, "waterInterval").ToString();
            go.transform.Find("Experience").GetComponent<Text>().text = shedManager.GetPlantFeature(name, "experience").ToString();

            int lastWatered = shedManager.GetPlantFeature(name, "lastWatered");
            DateTime now = DateTime.Now;
            int currentTime = (int)now.Subtract(DateTime.MinValue).TotalMinutes;
            int lastWateredDisplay = currentTime - lastWatered;
     
            go.transform.Find("LastWatered").GetComponent<Text>().text = lastWateredDisplay.ToString() + " Minutes";

        }
    }
}
