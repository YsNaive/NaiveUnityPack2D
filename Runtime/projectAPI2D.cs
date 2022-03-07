using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------- Made by YsNaive -------------------*/


/// <summary>
/// 呼叫快捷函數
/// </summary>
public abstract class projectAPI2D : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public abstract void localAwake();



    // 取值相關
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// 取得鼠標在世界上的座標
    /// </summary>
    public Vector2 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    /// <summary>
    /// <para>取得自身父物件 </para>
    /// </summary>
    public GameObject myParent()
    {
        return this.transform.parent.gameObject;
    }


    /// <summary>
    /// <para>取得自身所有子物件，或是按特定條件搜尋 </para>
    /// <para>可用條件 int / tag</para>
    /// </summary>
    public GameObject[] myChild() { return myChild(-1, "\0", false); }
    public GameObject myChild(int number) { return myChild(number, "\0", false)[0]; }
    public GameObject[] myChild(string searchTag) { return myChild(-1, searchTag, false); }
    public GameObject myChild(string searchTag, bool isOnlyFirst) { return myChild(-1, searchTag, isOnlyFirst)[0]; }
    public GameObject[] myChild(int number, string searchTag, bool isOnlyFirst)
    {
        // number      預設為 -1        輸出所有
        // searchTag   預設為 "\0"      依照Tag搜尋
        // isOnlyFirst 預設為 false     search標頭的修飾子，是否只輸出一個項目 ?

        List<GameObject> output = new List<GameObject>();

        if (number != -1)
            output.Add(this.transform.GetChild(number).gameObject);
        else
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (searchTag == "\0" || searchTag == this.transform.GetChild(i).tag)
                {
                    output.Add(this.transform.GetChild(i).gameObject);
                    if (isOnlyFirst)
                        break;
                }
            }
        }
        if (output.Count == 0) output.Add(null);
        return output.ToArray();
    }



    // 數學相關
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>A 物件之於 B 物件的角度 ( 180 ~ -180 )</para>
    /// <para>向右為零 逆時針為正</para>
    /// </summary>
    public float obj2angle2D(GameObject a, GameObject b) { return obj2angle2D(a.transform.position, b.transform.position); }
    public float obj2angle2D(GameObject a, Vector2 b) { return obj2angle2D(a.transform.position, b); }
    public float obj2angle2D(Vector2 a, GameObject b) { return obj2angle2D(a, b.transform.position); }
    public float obj2angle2D(Vector2 a, Vector2 b)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(a.y - b.y, a.x - b.x);
    }



    // 快捷參數設定
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>設定 Color 參數</para>
    /// </summary>
    public void setColor(float R,float G,float B, float A) { setColor(new Color(R, G, B, A)); }
    public void setColor(float A) { Color temp = spriteRenderer.color; setColor(new Color(temp.r, temp.g, temp.b, A)); }
    public void setColor(float R, float G, float B) { setColor(new Color(R, G, B, spriteRenderer.color.a)); }
    public void setColor(Color color) { spriteRenderer.color = color; }

    /// <summary>
    /// <para>設定 Animator 參數，將會自動判斷型別</para>
    /// <para>#注意 float 型別要加 "f"  ex( 0.5f , 1f ,...)</para>
    /// </summary>
    public void setAnimator(string triggerName) { animator.SetTrigger(triggerName); }
    public void setAnimator(string targetName,bool isTrue) { animator.SetBool(targetName,isTrue); }
    public void setAnimator(string targetName, int intValue) { animator.SetInteger(targetName, intValue); }
    public void setAnimator(string targetName, float floatValue) { animator.SetFloat(targetName, floatValue); }
}