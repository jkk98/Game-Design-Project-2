using UnityEngine;
using System.Collections;

public class carScript : MonoBehaviour {
    public GameObject electraProto;
    public GameObject sabertooth;
    public GameObject archaeo;
    public GameObject ankylo;
    public GameObject oviraptor;
    public GameObject roboRaptor;
    public GameObject tricera_truck;
    public GameObject tricera_boss;
    public GameObject popcorn;

    int ticks = 0; //50 ticks per second in FixedUpdate, used for creating dinosaurs
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        ticks += 1;
        /*if (ticks == 200)
        {
            GameObject proto1 = Instantiate(electraProto, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto2 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 400)
        {
            GameObject sabertooth1 = Instantiate(sabertooth, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject sabertooth2 = Instantiate(sabertooth, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;

        } if (ticks == 600)
        {
            GameObject oviraptor1 = Instantiate(oviraptor, transform.position + (transform.right * 2.5f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            oviraptor1.gameObject.GetComponent<oviraptor_script>().xShoot = 0.5f;
            oviraptor1.gameObject.GetComponent<oviraptor_script>().yShoot = 0.5f;
            oviraptor1.gameObject.GetComponent<oviraptor_script>().egg_speed = 3.0f;
            GameObject oviraptor2 = Instantiate(oviraptor, transform.position + (transform.right * -2.5f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            oviraptor2.gameObject.GetComponent<oviraptor_script>().xShoot = 0.5f;
            oviraptor2.gameObject.GetComponent<oviraptor_script>().yShoot = 0.5f;
            oviraptor2.gameObject.GetComponent<oviraptor_script>().egg_speed = 3.0f;
        }
        if (ticks == 800)
        {
            GameObject popcorn1 = Instantiate(popcorn, transform.position + (transform.right * -3f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject; ;

        }
        if (ticks == 1000)
        {
            GameObject archaeo1 = Instantiate(archaeo, transform.position + (transform.right * 1.5f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject archaeo2 = Instantiate(archaeo, transform.position + (transform.right * -2.5f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }*/
        /*if (ticks == 1)
        {
            tricera_boss.gameObject.GetComponent<triceraBossScript>().activateBoss();
        }*/
        if (ticks == 200)
        {
            GameObject proto1 = Instantiate(electraProto, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto2 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 400)
        {
            GameObject proto3 = Instantiate(electraProto, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto4 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 600)
        {
            GameObject proto5 = Instantiate(electraProto, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto6 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto7 = Instantiate(electraProto, transform.position + (transform.right * 1.0f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto8 = Instantiate(electraProto, transform.position + (transform.right * -1.6f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject popcorn2 = Instantiate(popcorn, transform.position + (transform.right * -3f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 1000)
        {
            GameObject sabertooth1 = Instantiate(sabertooth, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject sabertooth2 = Instantiate(sabertooth, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 1200)
        {
            GameObject proto9 = Instantiate(electraProto, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto10 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject archaeo1 = Instantiate(archaeo, transform.position + (transform.right * 1.5f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject archaeo2 = Instantiate(archaeo, transform.position + (transform.right * -2.5f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;

        }
        if (ticks == 1600)
        {
            GameObject oviraptor1 = Instantiate(oviraptor, transform.position + (transform.right * 2.5f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            oviraptor1.gameObject.GetComponent<oviraptor_script>().xShoot = 0.5f;
            oviraptor1.gameObject.GetComponent<oviraptor_script>().yShoot = 0.5f;
            oviraptor1.gameObject.GetComponent<oviraptor_script>().egg_speed = 3.0f;
            GameObject oviraptor2 = Instantiate(oviraptor, transform.position + (transform.right * -2.5f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            oviraptor2.gameObject.GetComponent<oviraptor_script>().xShoot = 0.5f;
            oviraptor2.gameObject.GetComponent<oviraptor_script>().yShoot = 0.5f;
            oviraptor2.gameObject.GetComponent<oviraptor_script>().egg_speed = 3.0f;
        }
        if (ticks == 1800)
        {
            GameObject popcorn2 = Instantiate(popcorn, transform.position + (transform.right * -3f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject; ;

        }
        if (ticks == 2000)
        {
            GameObject archaeo3 = Instantiate(archaeo, transform.position + (transform.right * 1.5f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject archaeo4 = Instantiate(archaeo, transform.position + (transform.right * -2.5f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 2200)
        {

            GameObject oviraptor3 = Instantiate(oviraptor, transform.position + (transform.right * 2.5f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            oviraptor3.gameObject.GetComponent<oviraptor_script>().xShoot = 0.5f;
            oviraptor3.gameObject.GetComponent<oviraptor_script>().yShoot = 0.5f;
            oviraptor3.gameObject.GetComponent<oviraptor_script>().egg_speed = 3.0f;
            GameObject oviraptor4 = Instantiate(oviraptor, transform.position + (transform.right * -2.5f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            oviraptor4.gameObject.GetComponent<oviraptor_script>().xShoot = 0.5f;
            oviraptor4.gameObject.GetComponent<oviraptor_script>().yShoot = 0.5f;
            oviraptor4.gameObject.GetComponent<oviraptor_script>().egg_speed = 3.0f;
            GameObject sabertooth3 = Instantiate(sabertooth, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject sabertooth4 = Instantiate(sabertooth, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 2400)
        {
            GameObject proto11 = Instantiate(electraProto, transform.position + (transform.right * 1.5f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto12 = Instantiate(electraProto, transform.position + (transform.right * -2.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto5 = Instantiate(electraProto, transform.position + (transform.right * 1.0f * 1.0f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject proto6 = Instantiate(electraProto, transform.position + (transform.right * -1.6f) + (transform.up * 1.0f), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject popcorn3 = Instantiate(popcorn, transform.position + (transform.right * -3f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (ticks == 2800)
        {
            tricera_boss.gameObject.GetComponent<triceraBossScript>().activateBoss();
        }
        if (ticks == 3600)
        {
            GameObject popcorn4 = Instantiate(popcorn, transform.position + (transform.right * -3f) + (transform.up * 1.5f), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
	

	}
}
