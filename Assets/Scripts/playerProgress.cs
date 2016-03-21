using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerProgress : MonoBehaviour {
    public GameObject raptorForm;
    public GameObject triceraForm;
    public GameObject tRexForm;
    public GameObject eggForm;
    public static int hp = 10;
    //Just an arbitrary amount, don't know if we'll even use lives
    public static int lives = 3;
    public static bool hasRaptorForm = true;
    public static bool hasTriceraForm = true;
    public static bool hasTRexForm;
    string currentLevel;

    void Start()
    {

    }

    public void switchForms (Vector3 position, Quaternion rotation, string to, int currentHp)
    {
        if (to.Contains("raptor") && hasRaptorForm)
        {
            Destroy(gameObject);
            GameObject raptor = Instantiate(raptorForm, position, rotation) as GameObject;
            raptor.gameObject.GetComponent<healthMethods>().hp = currentHp;
        }
        if (to.Contains("egg"))
        {
            Destroy(gameObject);
            GameObject eggboy = Instantiate(eggForm, position, rotation) as GameObject;
            eggboy.gameObject.GetComponent<healthMethods>().hp = currentHp;

        }
        if (to.Contains("tricera"))
        {
            Destroy(gameObject);
            GameObject tricera = Instantiate(triceraForm, position, rotation) as GameObject;
            tricera.gameObject.GetComponent<healthMethods>().hp = currentHp;

        }

    }
    public void bringBackToLife (string level)
    {
        Debug.Log(lives);
        currentLevel = level;
        if (lives > 0)
        {
            Debug.Log("RESURRECT");
            lives -= 1;
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            SceneManager.LoadScene("gameOver");
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
