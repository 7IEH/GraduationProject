using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tu_trigger3 : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            manager.Tutorial(11);
        this.gameObject.SetActive(false);
    }
}
