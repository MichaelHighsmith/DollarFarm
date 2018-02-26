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
        shedManager.SetPlantFeature("redflower", "waterInterval", 15);
        shedManager.SetPlantFeature("redflower", "experience", 15);
        shedManager.SetPlantFeature("redflower", "lastWatered", 0);
        shedManager.Save();
        Debug.LogError("BuyRedFlower called");
    }

    public void BuyBlueFlower()
    {
        shedManager.SetPlantFeature("blueflower", "waterInterval", 30);
        shedManager.SetPlantFeature("blueflower", "experience", 30);
        shedManager.SetPlantFeature("blueflower", "lastWatered", 0);
        shedManager.Save();
        Debug.LogError("BuyBlueFlower called");
    }
}
