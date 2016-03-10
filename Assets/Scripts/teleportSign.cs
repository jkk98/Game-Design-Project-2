using UnityEngine;
using System.Collections;

public class teleportSign : MonoBehaviour
{

    public int teleportX = 0;
    public int teleportY = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Teleports player to a certain spot if nearby
        if (col.gameObject.name.Contains("Character"))
        {
            col.transform.position = new Vector3(teleportX, teleportY, 0);
        }
    }
}
