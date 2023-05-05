using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorCheck : MonoBehaviour
{
    public bool blue;
    public bool green;
    public bool red;
    public bool table;
    public bool boss;
    public string color;

    // Start is called before the first frame update
    void Start()
    {
        if (blue == true) {
            color = "blue";
        }   

        if (green == true) {
            color = "green";
        }

        if (red == true) {
            color = "red";
        }

        if (table == true) {
            color = "table";
        }

        if (boss == true) {
            color = "boss";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string enemyCheck() {
        return color;
    }
}
