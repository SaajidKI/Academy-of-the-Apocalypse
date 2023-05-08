using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    public GameObject opened_1;
    public GameObject opened_2;
    public GameObject closed_1;
    public GameObject closed_2;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open() {
        closed_1.SetActive(false);
        closed_2.SetActive(false);
        opened_1.SetActive(true);
        opened_2.SetActive(true);
        portal.SetActive(true);
    }
}
