using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlant : MonoBehaviour
{

    ShedManager shedManager;

    public GameObject[] Models;
    private int i = 0;
    public GameObject RainCloud;


    // Use this for initialization
    void Start() {
        shedManager = GameObject.FindObjectOfType<ShedManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void WaterPlant()
    {
        StartCoroutine(makeCloud());

    }

    public IEnumerator makeCloud()
    {
        RainCloud.SetActive(true);
        yield return new WaitForSeconds(4);
        RainCloud.SetActive(false);
    }

    public void Next() {
        if (Models[i].activeInHierarchy == true && i < 3) {

            int oldModel = i;
            Models[i].SetActive(false);

            //move to the next index and find that object's name
            i++;
            string currentPlant = Models[i].name;

            //check if that object is in the shedPlants array, if not then keep moving down the object list.
            while(shedManager.shedPlants.ContainsKey(currentPlant) == false && i < 3) {
                i++;
                currentPlant = Models[i].name;
                Debug.LogError(currentPlant);
            }

            //Either we've reached the end of the user's plants or we are at another possible option
            //If we have another option, activate the next
            if (shedManager.shedPlants.ContainsKey(currentPlant)) {
                Models[i].SetActive(true);
            } else {
                //If there are no other options, reactivate the old model we have just deactivated
                i = oldModel;
                Models[i].SetActive(true);
            }


           
        }
    }

    public void Prev() {
        if (Models[i].activeInHierarchy == true && i > 0) {

            //Same logic as with "Next()"
            int oldModel = i;
            Models[i].SetActive(false);

            i--;
            string currentPlant = Models[i].name;

            while (shedManager.shedPlants.ContainsKey(currentPlant) == false && i > 0) {
                i--;
                currentPlant = Models[i].name;
                Debug.LogError(currentPlant);
            }
            
            if (shedManager.shedPlants.ContainsKey(currentPlant)) {
                Models[i].SetActive(true);
            }
            else {
                i = oldModel;
                Models[i].SetActive(true);
            }
        }
    }
}
