using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tu_trigger : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        int gate = 0;
        if (other.gameObject.tag == "Player")
        {
            manager.StageStart(gate);
            manager.Tutorial(9);
        }
            
        this.gameObject.SetActive(false);
    }
}
