using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    public Text TimerTxt;
    public Text LevelText;
    public Text finalScore;

    public GameObject GameCompPanel;
    public GameObject GamePausePanel;


    public GameObject[] Levels;

    public GameObject[] Spawners;
    public GameObject[] Mirrors;
    public GameObject[] Absorbers;
    public Transform[] RandomPos;
    public Sprite[] SimpleMirrorSprites;



    public GameObject MidCircleBoundary;
    public GameObject MidCircleBoundaryTwo;
    public GameObject MidCircleBoundaryThree;
    public GameObject MidCircleBoundaryFour;
    public GameObject MidCircleBoundaryFive;
    public GameObject MidCircle;

    


    public float targetTime;
    [HideInInspector] public bool startTimer = false;
    int TotalMA;
    int TotalMirrors;
    int TotalAbsorbers;
    [HideInInspector] public bool isGameStart = false;

 
    // int[] spvalues= new int[10];
    int[] spvalues= new int[10];

    private void Start()
    {
        PlayFabSettings.staticSettings.TitleId = "B1634";
    PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
    {
        CustomId = "TomtheBomb",
        CreateAccount = true
    }, OnLoginSuccess, OnLoginFailure);

        Instance = this;
        //TurnOnLevel();

        Application.targetFrameRate = -1;
        //Spawners = GameObject.FindGameObjectsWithTag("Spawner");
        //Mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        //Absorbers = GameObject.FindGameObjectsWithTag("Absorber");

        SetMirrors();
        // SetAbsorbers();



        TotalMA = TotalMirrors + TotalAbsorbers;
        for(int i = 0; i < 10; i++){
            spvalues[i] = -10;
        }
        SetSpawners();

    }

    void SetSpawners()
    {
        // int TotalSpawners = Random.Range(0, Spawners.Length);
        int TotalSpawners = 5;

        // if(TotalSpawners > TotalMA)
        // {
        //     SetSpawners();
        //     return;
        // }
        
        int SpawnerCount = 0;
        for (int i = 0; i < TotalSpawners; i++)
        {
            while (SpawnerCount < i)
            {
                int num = Random.Range(0, Spawners.Length);
                if (Spawners[num].activeInHierarchy == false)
                {
                    Spawners[num].SetActive(true);
                    float randomAngle = Random.Range(-20f, 20f);

        // Rotate the component around the Z-axis (for 2D rotations)
            Spawners[num].transform.Rotate(Vector3.forward, randomAngle);
                    SpawnerCount++;
                }
                // int checker = 0;
                // foreach(int obj in spvalues){
                //     if(obj == num){
                //         checker = 1;
                //         print("HIT HERE");
                //          // Exit the loop once the value is found
                //     }
                // }
                // if (Spawners[num].activeInHierarchy == false && checker == 0)
                // {
                //     Spawners[num].SetActive(true);
                //     spvalues[SpawnerCount] = num;
                //     SpawnerCount++;
                    
                // }
            }
        }
    }

    void SetMirrors()
    {
        targetTime = 0;
        float x;
        float y;
        float sizeX;
        float sizeY;

        TotalMirrors = 5;
        int MirrorCount = 0;
        foreach (GameObject mirror in Mirrors)
        {   
            
            x = Random.Range(2.8f, -2.8f);
            y = Random.Range(2.8f, -2.8f);
            while(Mathf.Abs(x) < 1f || Mathf.Abs(y)  < 1f){
                x = Random.Range(3f, -3f);
            y = Random.Range(3f, -3f);
            }
            
            sizeX = Random.Range(.37f, .75f);
            sizeY = Random.Range(.37f, .75f);
            Vector3 newScale = new Vector3(sizeX, sizeY, 0);
            mirror.transform.localScale = newScale;
            mirror.transform.position = new Vector3(x, y, 0);
        }

        for (int i = 0; i <= TotalMirrors; i++)
        {
            while (MirrorCount <= i)
            {
                int num = Random.Range(0, Mirrors.Length);
                if (Mirrors[num].activeInHierarchy == false)
                {
                    int randomIndex = Random.Range(0, 8);

                    // Define the possible angles in 45-degree increments
                    int[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };

                    // Select the random angle from the array
                    int randomAngle = angles[randomIndex];

                    // Rotate the component around the Z-axis
                    Mirrors[num].transform.Rotate(Vector3.forward, randomAngle);
                    Mirrors[num].SetActive(true);
                    MirrorCount++;
                }
            }
        }

        foreach (GameObject mirror in Mirrors) //bounds.Intersects(collider2.bounds) IsTouching(MidCircleBoundary.GetComponent<PolygonCollider2D>()
        {
            if (mirror.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircleBoundary.GetComponent<PolygonCollider2D>().bounds) || mirror.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircle.GetComponent<CircleCollider2D>().bounds))
            {
                mirror.transform.position = RandomPos[Random.Range(0, RandomPos.Length)].position;
            }
        }

    }

    

    void SetAbsorbers()
    {
        float x;
        float y;


        TotalAbsorbers = 0;
    
        int AbsorberCount = 0;
        foreach (GameObject absorber in Absorbers)
        {
            x = Random.Range(3.63f, -3.63f);
            y = Random.Range(3.95f, -3.95f);
            absorber.transform.position = new Vector3(x, y, 0);
        }

        for (int i = 0; i < Absorbers.Length; i++)
        {
            if (AbsorberCount <= TotalAbsorbers)
            {
                int num = Random.Range(0, Absorbers.Length);
                if (Absorbers[num].activeInHierarchy == false)
                {
                    Absorbers[num].SetActive(true);
                    AbsorberCount++;
                }
            }
        }

        foreach (GameObject absorber in Absorbers)
        {
            if (absorber.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircleBoundary.GetComponent<PolygonCollider2D>().bounds) || absorber.GetComponent<PolygonCollider2D>().bounds.Intersects(MidCircle.GetComponent<CircleCollider2D>().bounds))
            {
                absorber.transform.position = RandomPos[Random.Range(0, RandomPos.Length)].position;
            }
        }
    }



    private void Update()
    {
        
        if (startTimer)
        {
            targetTime += Time.deltaTime;
            TimerTxt.text = ((int)targetTime).ToString();
            finalScore.text = TimerTxt.text;
            if (targetTime >=100)
            {
                GameCompPanel.SetActive(true);
                GameObject[] Lasers = GameObject.FindGameObjectsWithTag("Laser");
                foreach (GameObject laser in Lasers)
                {
                    laser.GetComponent<Rigidbody2D>().freezeRotation = true;
                    laser.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
                startTimer = false;

            }
            
        }
        else
            return;
    }

    public void StartGame()
    {
        foreach (GameObject Spawner in Spawners)
        {
            if (Spawner.activeInHierarchy == true)
            {
                Spawner.GetComponent<Spawner>().StartLaser();
            }
        }

        foreach (GameObject mirror in Mirrors)
        {   
             
    
            mirror.GetComponent<PolygonCollider2D>().isTrigger = false;
            mirror.GetComponent<Drag>().enabled = false;

        }

        foreach (GameObject absorber in Absorbers)
        {
            absorber.GetComponent<PolygonCollider2D>().isTrigger = true;
            absorber.AddComponent<Absorber>();
        }


        for (int i = 0; i < Mirrors.Length; i++)
        {
            Mirrors[i].GetComponent<SpriteRenderer>().sprite = SimpleMirrorSprites[i];
        }


        MidCircleBoundary.SetActive(false);
        MidCircleBoundaryTwo.SetActive(false);
        MidCircleBoundaryThree.SetActive(false);
        MidCircleBoundaryFour.SetActive(false);
        MidCircleBoundaryFive.SetActive(false);


        startTimer = true;
        isGameStart = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Pause()
    {
        GamePausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void Resume()
    {
        GamePausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }


}
