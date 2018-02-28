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
        shedManager.SetPlantFeature("redflower", "experience", 0);
        shedManager.SetPlantFeature("redflower", "lastWatered", 0);
        shedManager.Save();
        Debug.Log("BuyRedFlower called");
    }

    public void BuyBlueFlower()
    {
        shedManager.SetPlantFeature("blueflower", "waterInterval", 30);
        shedManager.SetPlantFeature("blueflower", "experience", 0);
        shedManager.SetPlantFeature("blueflower", "lastWatered", 0);
        shedManager.Save();
        Debug.Log("BuyBlueFlower called");
    }

    public void BuyGreenFlower()
    {
        shedManager.SetPlantFeature("greenflower", "waterInterval", 30);
        shedManager.SetPlantFeature("greenflower", "experience", 0);
        shedManager.SetPlantFeature("greenflower", "lastWatered", 0);
        shedManager.Save();
        Debug.Log("BuyGreenFlower called");
    }

    public void BuyPurpleFlower()
    {
        shedManager.SetPlantFeature("purpleflower", "waterInterval", 30);
        shedManager.SetPlantFeature("purpleflower", "experience", 0);
        shedManager.SetPlantFeature("purpleflower", "lastWatered", 0);
        shedManager.Save();
        Debug.Log("BuyPurpleFlower called");
    }
}
