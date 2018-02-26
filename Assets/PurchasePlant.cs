using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasePlant : MonoBehaviour {

    ShedManager shedManager;

    void Start () {
        shedManager = GameObject.FindObjectOfType<ShedManager>();
    }

	void Update () {
		
	}

    public void BuyRedFlower()
    {
        shedManager.SetPlantFeature("REDFLOWER", "waterInterval", 15);
        shedManager.SetPlantFeature("REDFLOWER", "experience", 15);
        shedManager.SetPlantFeature("REDFLOWER", "lastWatered", 0);
        shedManager.Save();
        Debug.LogError("BuyRedFlower called");
    }

    public void BuyBlueFlower()
    {
        shedManager.SetPlantFeature("BLUEFLOWER", "waterInterval", 30);
        shedManager.SetPlantFeature("BLUEFLOWER", "experience", 30);
        shedManager.SetPlantFeature("BLUEFLOWER", "lastWatered", 0);
        shedManager.Save();
        Debug.LogError("BuyBlueFlower called");
    }
}
