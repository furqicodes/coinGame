using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpinObject : MonoBehaviour
{
    public float spinSpeed = 50f;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;    
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Rotate(new Vector3(0,0,1), spinSpeed * Time.deltaTime);
    }
}
