using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlant : MonoBehaviour
{

    public GameObject[] Models;
    private int i = 0;
    public GameObject RainCloud;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void Next()
    {
        if (Models[i].activeInHierarchy == true && i < 3)
        {
            Models[i].SetActive(false);
            Models[i + 1].SetActive(true);
            i++;
        }
    }

    public void Prev()
    {
        if (Models[i].activeInHierarchy == true && i > 0)
        {
            Models[i].SetActive(false);
            Models[i - 1].SetActive(true);
            i--;
        }
    }
}
