using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchBoxSampleAnimatorScript : MonoBehaviour
{
    Animator ani;
    [SerializeField]
    public bool isMove, isSummon, dead;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("isMove", isMove);
        ani.SetBool("isSummon", isSummon);

        // 注意這裡使用的是 SetTrigger
        if (dead)
        {
            ani.SetTrigger("dead");
            dead = false;
        }

    }
}
