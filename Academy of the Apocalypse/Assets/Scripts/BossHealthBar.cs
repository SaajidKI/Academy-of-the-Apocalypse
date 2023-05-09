using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public GameObject Enemy;
    private BossMeleeDamage h_script;
    // public Script EnemyMeleeDamage;
    // public GameObject E_Health;

    public Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {   
        localScale = transform.localScale;
        h_script = Enemy.GetComponent<BossMeleeDamage>();
        // enemy_health = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(h_script.currentHealth/h_script.maxHealth);
        // Debug.Log(h_script.currentHealth);
        // Debug.Log(h_script.maxHealth);
        localScale.x = (h_script.currentHealth/h_script.maxHealth);
        transform.localScale = localScale;
    }
}
