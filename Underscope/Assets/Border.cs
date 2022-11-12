using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Vector2 viewPortPos = new Vector2(.5f,.5f);
    // Start is called before the first frame update
    void Start()
    {
        Replace();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replace(float distance = 10)
    {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPos.x, viewPortPos.y, distance));
    }
}
