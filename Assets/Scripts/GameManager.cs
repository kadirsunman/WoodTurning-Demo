using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject woodObj; 
    public GameObject woodPoint;
    GameObject createWood;

    public static bool  rotationState = true;
    void Start()
    {
        //Ağacın 0.001 genişliğinde silindirden 300 tane oluşturuyoruz.
        for (int i = 0; i < 300; i++)
        {
            createWood = Instantiate(woodObj, new Vector3(i / 100f, 0, 0), Quaternion.Euler(0, 85,90)) as GameObject;
            createWood.transform.parent = woodPoint.transform;
            createWood.transform.localPosition = new Vector3(i / 100f, 0, 0);
        }
    }

    void Update()
    {
        if(rotationState == true)
        {
            woodPoint.transform.Rotate(Time.deltaTime * 150, 0, 0); //Ağacımızı kendi ekseninde torna amacıyla döndürüyoruz.
        }

    }
   
}
