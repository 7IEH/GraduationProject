using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLock3 : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        int gate = 3;
        if (other.gameObject.tag == "Player")
            manager.StageStart(gate);
    }
}
