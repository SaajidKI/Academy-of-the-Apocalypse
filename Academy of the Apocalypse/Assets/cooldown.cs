using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    public Image abilityImage_1;
    public float cooldown1 = 5;
    public GameObject coolTime;
    public bool starter;
    public Shooting shootScript;


    // Start is called before the first frame update
    void Start()
    {
        starter = false;
        abilityImage_1.fillAmount = 1;
        shootScript = coolTime.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starter == true) {
            ability1();
        }
        
    }

    public void ability1() {
        abilityImage_1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

        if (abilityImage_1.fillAmount <= 0) {
            abilityImage_1.fillAmount = 1;
            shootScript.boolSwitch();
            starter = false;
        }
    }

    public void starting() {
        starter = true;
    }
}
