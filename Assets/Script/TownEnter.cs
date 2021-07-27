using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownEnter : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            manager.Tutorial(5);
    }
}
