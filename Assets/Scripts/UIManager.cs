using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject rectangleTool;
    public GameObject roundTool;
    public GameObject sharpTool;
    public GameObject selectedTool;
    public GameObject colorButtons;
    public GameObject toolButtons;
    public GameObject spray;
    public GameObject createObjeImage;
    public ParticleSystem paintParticle;
    public Light spotLight;
    public GameObject paintArea;
    
    
    public Texture2D[] colorTexture;


    public GameObject wood;
    
    public float speed = 8f;// Kesicinin ekrana girip çıkma hızı
    private bool toolState = false; //Kesicinin Ekrana girip çıkması için gereken değişken
    private int toolOneProgress = 0; //Kesici update fonksiyonunda çalıştığından dolayı ekrana 1 defa getirmek için kullanılan değişken
    private int selectTool = 0; //Seçilen Kesici Takımını update fonksiyonunda otomatik olarak ekrana getirmek için kullanılan değişken

    private Vector3 outScreen = new Vector3(-0.45f, -4.85f, 8.1f); //Ekranın dış pozisyonu
    private Vector3 inScreen = new Vector3(-0.45f, -0.75f, 8.1f);//Ekranın iç pozisyonu



    //UI üzerindeki rectangle Butonunun click eventi
    public void rectangleToolCreate() 
    {
        toolState = false; //Kesici takımını ekrandan çıkarabilmek için değişkeni false yapıyoruz
        selectTool = 0;
    }
    //UI üzerindeki round Butonunun click eventi
    public void roundToolCreate()
    {
        toolState = false;
        selectTool = 1;
    }
    //UI üzerindeki sharp Butonunun click eventi
    public void sharpToolCreate()
    {
        toolState = false;
        selectTool = 2;
    }
    public void completedButton()
    {
        GameManager.rotationState = false; //Wood un döndürmesini diğer scriptde durduruyoruz.
        selectedTool.SetActive(false); //Toolları kapatıyoruz.
        toolButtons.SetActive(false);
        createObjeImage.SetActive(false);
        colorButtons.SetActive(true);
        spray.SetActive(true);


        //Biten wood u ortaya dik bir şekilde 10 da 3 büyüterek getiriyoruz
        wood.transform.rotation = Quaternion.Euler(0, 0, 90);
        wood.transform.position = new Vector3(-0.22f, 0.5f, 6f);
        wood.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        //Işığı öne getiriyoruz
        spotLight.transform.position = new Vector3(0, 8, 2.45f); //65  8 2.45
        spotLight.transform.rotation = Quaternion.Euler(65, 0, 0);
    }
    
    void Update()
    {
        if (toolState == false)//Ekrandan Çık
        {
            toolOneProgress = 0;
            //Parçayı ekrandan çıkarmak için kullanılan satır
            selectedTool.transform.position = Vector3.MoveTowards(selectedTool.transform.localPosition, outScreen, Time.deltaTime * speed); 
            //Parça ekrandan çıktığı zaman seçtiğimiz kesici takımını getirebilmemiz için gereken sorgu bloğu
            if (selectedTool.transform.position == outScreen)
            {
                //Seçilen Kesici Takımının SetActivini açıp diğer kesicileri kapatan sorgulama bloğu
                if (selectTool ==0)
                {
                    rectangleTool.SetActive(true);
                    roundTool.SetActive(false);
                    sharpTool.SetActive(false);
                }
                if (selectTool == 1)
                {
                    rectangleTool.SetActive(false);
                    roundTool.SetActive(true);
                    sharpTool.SetActive(false);
                }
                if (selectTool == 2)
                {
                    rectangleTool.SetActive(false);
                    roundTool.SetActive(false);
                    sharpTool.SetActive(true);
                }
                toolState = true; //Seçilen kesici takımını ekrana getirmesi için değişkeni true yapıyoruz 
            }
        }
        if(toolState ==true && toolOneProgress == 0)//Ekrana Gir
        {
            //Parçayı ekrandan çıkarmak için kullanılan satır
            selectedTool.transform.position = Vector3.MoveTowards(selectedTool.transform.position, inScreen, Time.deltaTime * speed);
            if(selectedTool.transform.position == inScreen)
            {
                toolOneProgress = 1;
            }
        }
        ToolMovement();
        SprayMovement();
    }

    void ToolMovement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Farenin veya dokunulan noktanın ekranda yerini alıyoruz.
        if (Input.GetMouseButton(0))// Sol click basıldığının kontrolünü yapıyoruz.
        {
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                //Eğer tıklanılan nesne "toolMovementArea" ise toolumuzun pozisyonunu hit ile aynı yapıyoruz.
                if (objectHit.name == "toolMovementArea") 
                {
                    selectedTool.transform.position = new Vector3(hit.point.x-0.5f, hit.point.y, selectedTool.transform.position.z);
                }
            }
        }
    }
    void SprayMovement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Farenin veya dokunulan noktanın ekranda yerini alıyoruz.
        if (Input.GetMouseButton(0))// Sol click basıldığının kontrolünü yapıyoruz.
        {
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                //Eğer tıklanılan nesne "sprayMovementArea" ise toolumuzun pozisyonunu hit ile aynı yapıyoruz.
                if (objectHit.name == "sprayMovementArea")
                {
                    spray.transform.position = new Vector3(hit.point.x - 0.5f, hit.point.y, spray.transform.position.z);
                }
            }
        }
    }
    ParticleSystem.ShapeModule shape;

    public void purple() 
    {
        //Spreyin ucundaki boya "Particle System" ın reklendirilmesi için shape ine dışardan aldığımız texturu atıyoruz.
        shape = paintParticle.shape;
        shape.texture = colorTexture[0];
        //Spreyin ucundaki boyayı başlatıyoruz. Loop olduğu için bir kere basmamız yeterli butona
        paintParticle.Play();
        //Boyanın yapılması için spray ile ağacın çarpışmasını renk butonuna tıkladığımız zaman başlatmasını sağlıyoruz.
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        //WoodCut scriptindeki wood un colorunu ui managerdeki butonlar ile kontrol ediyoruz.
        WoodCut.woodColor = 0;
    }
    public void blue() 
    {
        shape = paintParticle.shape;
        shape.texture = colorTexture[1];
        paintParticle.Play();
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        WoodCut.woodColor = 1;
    }
    public void pink() 
    {
        shape = paintParticle.shape;
        shape.texture = colorTexture[2];
        paintParticle.Play();
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        WoodCut.woodColor = 2;
    }
    public void gray() 
    {
        shape = paintParticle.shape;
        shape.texture = colorTexture[3];
        paintParticle.Play();
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        WoodCut.woodColor = 3;
    }
    public void green() 
    {
        shape = paintParticle.shape;
        shape.texture = colorTexture[4];
        paintParticle.Play();
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        WoodCut.woodColor = 4;
    }
    public void turqoise() 
    {
        shape = paintParticle.shape;
        shape.texture = colorTexture[5];
        paintParticle.Play();
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        WoodCut.woodColor = 5;
    }
    public void roundedblue() 
    {
        shape = paintParticle.shape;
        shape.texture = colorTexture[6];
        paintParticle.Play();
        paintArea.GetComponent<CapsuleCollider>().enabled = true;
        WoodCut.woodColor = 6;
    }
}
