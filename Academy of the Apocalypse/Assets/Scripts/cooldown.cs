using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    public Image abilityImage_1;
    public Image abilityImage_2;
    public float cooldown1 = 5;
    public GameObject coolTime;
    public bool starterFire;
    public bool starterIce;
    public Shooting shootScript;


    // Start is called before the first frame update
    void Start()
    {
        starterFire = false;
        starterIce = false;
        abilityImage_1.fillAmount = 1;
        abilityImage_2.fillAmount = 1;
        shootScript = coolTime.GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterFire == true) {
            ability1();
        }

        if (starterIce == true) {
            ability2();
        }
        
    }

    public void ability1() {
        abilityImage_1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

        if (abilityImage_1.fillAmount <= 0) {
            abilityImage_1.fillAmount = 1;
            shootScript.boolSwitchFire();
            starterFire = false;
        }
    }

    public void ability2() {
        abilityImage_2.fillAmount -= 1 / cooldown1 * Time.deltaTime;

        if (abilityImage_2.fillAmount <= 0) {
            abilityImage_2.fillAmount = 1;
            shootScript.boolSwitchIce();
            starterIce = false;
            //change
        }
    }

    public void startingFire() {
        starterFire = true;
    }

    public void startingIce() {
        starterIce = true;
    }
    
}
