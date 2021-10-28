using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tu_trigger2 : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            manager.Tutorial(10);
        this.gameObject.SetActive(false);
    }
}
