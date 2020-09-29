using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCut : MonoBehaviour
{
    public Material[] materialHardTimber;
    public Material[] materialSoftTimber;
    public Material[] paintedTimber;

    public static int woodColor;
    MeshRenderer meshRenderer;

    void OnCollisionStay(Collision col)
    {
        //İsmi RectangleTool veya RoundTool veya SharpTool olan bir objeye çarptığında onun ağacın merkezine olan uzaklığını ağacın çapı yapar.
        if (col.gameObject.name == "RectangleTool" || col.gameObject.name == "RoundTool" || col.gameObject.name == "SharpTool")
        {
            meshRenderer = GetComponent<MeshRenderer>();
            float radius = 2.03f - col.contacts[0].point.y; //Woodumuzu merkeziyle tool arasında mesafeyi hesaplıyoruz
            
            // 2 Scale  =  0.93 
            // Radius Scale  = radius 
            radius = 2 * radius / 0.93f;
            
            if(radius<0.3f) //Eğer çapımız 0.3f den düşükse sıfır yapıyoruz.
            {
                transform.localScale = new Vector3(0, 0, transform.localScale.z);
            }
            else // değilse woodumuzun 1 katmanı için x ve y scale lerini radiusumuz yapıyoruz z katmanı yine aynı kalıyor.
            {
                transform.localScale = new Vector3(radius, radius, transform.localScale.z);
            }
            
            
            if (radius < 2 && radius > 1.5f) // eğer radiusumuz 2 ile 1.5 arasında ise hard materiali kullanıyoruz
            {
                meshRenderer.materials = materialHardTimber;
            }
            if (radius < 1.5f) //eğer radiusumuz 1.5 un altındaysa soft materialimizi kullanıyoruz
            {
                meshRenderer.materials = materialSoftTimber;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //Eğer Ağacımıza spray particle ları çarparsa UIManager Scriptinden aldığımız woodcolor değerine göre ağacın dış kabuğunun materialini renk materialimiz yapıyoruz ve ağacımıza atıyoruz.
        if (other.gameObject.name == "paintArea") 
        {
            switch(woodColor)
            {
                case 0:
                    materialHardTimber[1] = paintedTimber[0];
                    meshRenderer.materials = materialHardTimber;
                    break;
                case 1:
                    materialHardTimber[1] = paintedTimber[1];
                    meshRenderer.materials = materialHardTimber;
                    break;
                case 2:
                    materialHardTimber[1] = paintedTimber[2];
                    meshRenderer.materials = materialHardTimber;
                    break;
                case 3:
                    materialHardTimber[1] = paintedTimber[3];
                    meshRenderer.materials = materialHardTimber;
                    break;
                case 4:
                    materialHardTimber[1] = paintedTimber[4];
                    meshRenderer.materials = materialHardTimber;
                    break;
                case 5:
                    materialHardTimber[1] = paintedTimber[5];
                    meshRenderer.materials = materialHardTimber;
                    break;
                case 6:
                    materialHardTimber[1] = paintedTimber[6];
                    meshRenderer.materials = materialHardTimber;
                    break;
            }
            
        }
    }
}
