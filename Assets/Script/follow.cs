using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    MeshRenderer ObstacleRenderer;
    GameObject[] arrayObject;
    int arraySize=5;
    int index;
    void Start()
    {
        arrayObject = new GameObject[arraySize];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        float Distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 Direction = (target.transform.position - transform.position).normalized;
        RaycastHit hit;
        Debug.DrawRay(transform.position, Direction*Distance, Color.red);
        if(Physics.Raycast(transform.position, Direction ,out hit, Distance))
        {
            if (hit.collider.gameObject.name == "Player")
            {
                for (int i = 0; i < index; i++)
                {

                    arrayObject[i].SetActive(true);
                    arrayObject[i] = arrayObject[i + 1];
                    arrayObject[i + 1] = null;
                    index = i - 1;
                }
            }
            else if (hit.collider.gameObject.tag == "Luna")
            {

            }
            else if (hit.collider.gameObject.tag == "Oldman")
            {

            }
            else if (hit.collider.gameObject.tag == "Oldfemale")
            {

            }
            else if (hit.collider.gameObject.tag == "Worrior")
            {

            }
            else if (hit.collider.gameObject.tag == "Floor")
            {

            }
            else if (hit.collider.gameObject.tag == "Enemy")
            {

            }
            else if (hit.collider.gameObject.tag == "Heart")
            {

            }
            else if (hit.collider.gameObject.tag == "Trigger")
            {

            }
            else if (hit.collider.gameObject.tag == "EnemyBullet")
            {

            }
            else if (hit.collider.gameObject.tag == "Weapon") {
            }
            else if (hit.collider.gameObject.tag == "GreenKey")
            {
            }
            else if (hit.collider.gameObject.tag == "Return")
            {
            }
            else if (hit.collider.gameObject.tag == "Dunenter")
            {
            }
            else if (hit.collider.gameObject.tag == "BlueKey")
            {
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (arrayObject[i] == null)
                    {
                        arrayObject[i] = hit.collider.gameObject;
                        index = i;
                        arrayObject[index].SetActive(false);

                        break;

                    }
                }

            }
        }
    }
}
