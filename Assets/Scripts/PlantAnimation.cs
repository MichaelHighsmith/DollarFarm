using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantAnimation : MonoBehaviour {

    private Animation anim;
    string plantDisplayName;
    string plantAnimationName;
    public Text plantText;

    void Start()
    {
        anim = GetComponent<Animation>();
        plantDisplayName = anim.name;
        plantAnimationName = anim.name;

        //Make display name neater for user
        if (plantDisplayName.Contains("small")) {
            plantDisplayName = plantDisplayName.Substring(0, plantDisplayName.Length - 5);
            plantDisplayName = plantDisplayName + " (baby)";
        } else if (plantDisplayName.Contains("medium")) {
            plantDisplayName = plantDisplayName.Substring(0, plantDisplayName.Length - 6);
            plantDisplayName = plantDisplayName + " (growing)";
        }
        
    }

    void Update()
    {
        anim.Play(plantAnimationName);
        plantText.text = plantDisplayName;
    }
}
