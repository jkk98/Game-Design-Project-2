using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class healthMethods : MonoBehaviour {

    UnityEngine.UI.Slider hpSlider;
    public int hp = 10;
    playerProgress otherMethods;

    //Subtract hp and add knock back
    public void damagePlayer()
    {
        hp -= 1;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 300));
        hpSlider.value = hp;
        if (hp == 0)
        {
            otherMethods.bringBackToLife(SceneManager.GetActiveScene().name);
            //Destroy(gameObject);
        }

    }

    //Subtract hp and add knock back
    public void healPlayer()
    {
        hp = 10;
        hpSlider.value = hp;

    }

    // Use this for initialization
    void Start () {
        hpSlider = GameObject.Find("hp_slider").GetComponent<Slider>();
        otherMethods = gameObject.GetComponent<playerProgress>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
