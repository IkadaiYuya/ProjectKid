using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject camera3;

    //test
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enterPos;

    [SerializeField] ParticleSystem wallLock;

    private bool trigger;
    private bool enter;
    private float time;
    // Use this for initialization
    void Start()
    {
        trigger = false;
        enter = true;
        camera3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            time += Time.deltaTime;

            if (time > 0.016f * 40)
            {
                camera3.SetActive(true);
            }
            if (time > 0.016f * 60)
            {
                if (enter)
                {
                    Instantiate(enemy, enterPos.transform.position, enterPos.transform.rotation);
                    enter = false;
                }
            }
            if (time > 0.016f * 240)
            {
                camera3.SetActive(false);
                trigger = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			wall.SetActive(true);
			wallLock.Play();
			trigger = true;
		}
    }
}
