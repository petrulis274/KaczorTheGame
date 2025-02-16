using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxPull : MonoBehaviour
{

    public bool beingPushed;

    private float xPos;
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;        
    }

    // Update is called once per frame
    void Update()
    {
        if (beingPushed == false)
        {
            transform.position = new Vector2(xPos, transform.position.y);
        }
        else
        {
            xPos = transform.position.x;
        }
    }
}
