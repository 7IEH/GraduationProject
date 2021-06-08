using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLock5 : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        int gate = 5;
        if (other.gameObject.tag == "Player")
            manager.StageStart(gate);
    }
}
