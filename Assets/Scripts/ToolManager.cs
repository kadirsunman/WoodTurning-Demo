using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public ParticleSystem sawdustEffect;
    void OnCollisionStay(Collision col)
    {
        //Tagi wood olan bir objeye çarptığında talaş efektinin konumunu ayarlar ve başlatır.
        if (col.gameObject.tag == "wood")
        {
            if(!sawdustEffect.isPlaying)
            {
                sawdustEffect.transform.position = col.contacts[0].point;
                sawdustEffect.Play();
            }
            
        }
    }
}
