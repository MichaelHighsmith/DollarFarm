using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NextPlant : MonoBehaviour
{

    ShedManager shedManager;

    public GameObject[] Models;
    private int i = 0;
    public GameObject RainCloud;

    void Start() {
        shedManager = GameObject.FindObjectOfType<ShedManager>();
        //since at the start no objects are active (and calling Models[i] will activate redflower, call next and prev to set i to 0 correctly 
        Next();
        Prev();
    }

    void Update() {

    }

    public void WaterPlant() {
        StartCoroutine(makeCloud());
    }

    //Display cloud watering for 4 seconds
    public IEnumerator makeCloud() {

        string currentPlant = Models[i].name;

        //Make sure the plant is ready to be rewatered
        int waterInterval = shedManager.GetPlantFeature(currentPlant, "waterInterval");
        int lastWatered = shedManager.GetPlantFeature(currentPlant, "lastWatered");
        DateTime now = DateTime.Now;
        int currentTime = (int)now.Subtract(DateTime.MinValue).TotalMinutes;
        int lastWateredDisplay = currentTime - lastWatered;

        if (waterInterval <= lastWateredDisplay) {

            //add experience

            int experienceGained = shedManager.GetPlantFeature(currentPlant, "experiencePerWater");
            int currentExperience = shedManager.GetPlantFeature(currentPlant, "experience") + experienceGained;
            shedManager.SetPlantFeature(currentPlant, "experience", currentExperience);
            shedManager.Save();

            int experienceToGrow = shedManager.GetPlantFeature(currentPlant, "experienceToGrow");
            int experienceToComplete = shedManager.GetPlantFeature(currentPlant, "experienceToComplete");

            Models[i].SetActive(false);
            Models[i + 1].SetActive(false);
            Models[i + 2].SetActive(false);

            if (currentExperience < experienceToGrow)
            {
                //play small version animation
                Models[i + 1].SetActive(true);
            }
            else if (currentExperience >= experienceToGrow && currentExperience < experienceToComplete)
            {
                //play medium animation
                Models[i + 2].SetActive(true);
            }
            else
            {
                //play full grown plant animation
                Models[i].SetActive(true);
            }

            //Water plant
            shedManager.SetPlantFeature(currentPlant, "lastWatered", currentTime);
            shedManager.Save();

            RainCloud.SetActive(true);
            yield return new WaitForSeconds(4);
            RainCloud.SetActive(false);
        }  else {
            Debug.Log("It's too early to water this plant again!");
        }
    }

    public void CheckIfWilted()
    {
        string currentPlant = Models[i].name;

        DateTime now = DateTime.Now;
        int result = (int)now.Subtract(DateTime.MinValue).TotalMinutes;

        int lastWatered = shedManager.GetPlantFeature(currentPlant, "lastWatered");

        if(result - lastWatered > 2)
        {
            Debug.Log("It's dead its been so long");
        } else
        {
            Debug.Log("Your plant is still alive!");
        }
    }

    public void Next() {

        //check if the raincloud is already playing.  If it is, say you have to wait to finish watering and don't change plants 
        if (RainCloud.activeInHierarchy) {
            Debug.Log("You must wait to finish watering!");
            return;
        }

        if (i < 18) {

            int oldModel = i;
            Models[i].SetActive(false);
            Models[i + 1].SetActive(false);
            Models[i + 2].SetActive(false);

            //move 3 indices to the next plant and find that object's name
            i = i + 3;
            string currentPlant = Models[i].name;
            Debug.LogError("now i is " + i);
            Debug.LogError("current plant is " + currentPlant);

            //check if that object is in the shedPlants array, if not then keep moving down the object list.
            while (shedManager.shedPlants.ContainsKey(currentPlant) == false && i < 18) {
                i = i + 3;
                currentPlant = Models[i].name;
                Debug.Log(currentPlant);
            }

            
            //Either we've reached the end of the user's plants or we are at another possible option
            //If we have another option, activate the next
            if (shedManager.shedPlants.ContainsKey(currentPlant)) {
                //Check to see what level the plant is at (for which animation to play)
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                int experienceToGrow = shedManager.GetPlantFeature(currentPlant, "experienceToGrow");
                int experienceToComplete = shedManager.GetPlantFeature(currentPlant, "experienceToComplete");

                CheckIfWilted();

                if (experience < experienceToGrow) {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                } else if(experience < experienceToComplete) {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                } else {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            
                
            } else {
                //If there are no other options, reactivate the old model we have just deactivated
                i = oldModel;
                currentPlant = Models[i].name;
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                int experienceToGrow = shedManager.GetPlantFeature(currentPlant, "experienceToGrow");
                int experienceToComplete = shedManager.GetPlantFeature(currentPlant, "experienceToComplete");

                CheckIfWilted();

                if (experience < experienceToGrow) {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                }
                else if (experience < experienceToComplete) {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                }
                else {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            }

            Debug.Log("" + i);
        }
    }

    public void Prev() {

        //Same logic as with "Next()"
        if (RainCloud.activeInHierarchy) {
            Debug.Log("You must wait to finish watering!");
            return;
        }

        if (i > 0) {


            int oldModel = i;
            Models[i].SetActive(false);
            Models[i + 1].SetActive(false);
            Models[i + 2].SetActive(false);
            Debug.LogError("previous" + i);

            i = i - 3;
            string currentPlant = Models[i].name;
            Debug.Log("now its" + i);
            Debug.Log(currentPlant);

            while (shedManager.shedPlants.ContainsKey(currentPlant) == false && i > 0) {
                i = i - 3;
                currentPlant = Models[i].name;
                Debug.LogError(currentPlant);
            }
            
            if (shedManager.shedPlants.ContainsKey(currentPlant)) {
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                int experienceToGrow = shedManager.GetPlantFeature(currentPlant, "experienceToGrow");
                int experienceToComplete = shedManager.GetPlantFeature(currentPlant, "experienceToComplete");

                CheckIfWilted();

                if (experience < experienceToGrow) {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                }
                else if (experience < experienceToComplete) {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                }
                else {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            }
            else {
                //If there are no other options, reactivate the old model we have just deactivated
                i = oldModel;
                currentPlant = Models[i].name;
                int experience = shedManager.GetPlantFeature(currentPlant, "experience");
                int experienceToGrow = shedManager.GetPlantFeature(currentPlant, "experienceToGrow");
                int experienceToComplete = shedManager.GetPlantFeature(currentPlant, "experienceToComplete");

                CheckIfWilted();

                if (experience < experienceToGrow) {
                    //play small version animation
                    Models[i + 1].SetActive(true);
                }
                else if (experience < experienceToComplete) {
                    //play medium animation
                    Models[i + 2].SetActive(true);
                }
                else {
                    //play full grown plant animation
                    Models[i].SetActive(true);
                }
            }
        }
    }
}
