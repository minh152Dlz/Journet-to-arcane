using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
     Transform[] listtarget;
    public float smoothing;

    Vector3 offset;

    float lowY;

    // Start is called before the first frame update
    void Start()
    {
        //target = listtarget[0];
        offset = transform.position - target.position;
        
        lowY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // StartCoroutine(Switchcamera());
        // foreach(Transform obj in listtarget)
        //     {
        //          if(!obj.GetComponent<PlayerAbility>().check && obj.GetComponent<PlayerController>().enabled)
        //         {
        //             target = listtarget[0];
        //         }
        //     }

        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing*Time.deltaTime);
        //khoa camera truc -y
        //if(transform.position.y < lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        //khoa camera truc y
        //if(transform.position.y > lowY) transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
    }

    IEnumerator Switchcamera()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            yield return new WaitForSeconds(0.5f);
            foreach(Transform obj in listtarget)
            {
                if(obj.GetComponent<PlayerController>().enabled)
                {
                    target = obj;
                }
            }
        }
    }
}