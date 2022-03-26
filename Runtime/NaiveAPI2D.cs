using NaiveAPI;
using System.Collections.Generic;
using UnityEngine;

/*--------------------- Made by YsNaive -------------------*/


/// <summary>
/// 呼叫快捷函數
/// </summary>
public abstract class NaiveAPI2D : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public abstract void LocalAwake();

    

    // 取值相關
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// 取得鼠標在世界上的座標
    /// </summary>
    public Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    /// <summary>
    /// <para>取得自身父物件 </para>
    /// </summary>
    public GameObject MyParent()
    {
        return this.transform.parent.gameObject;
    }


    /// <summary>
    /// <para>取得自身所有子物件，或是按特定條件搜尋 </para>
    /// <para>可用條件 int / tag</para>
    /// </summary>
    public GameObject[] MyChild() { return MyChild(-1, "\0", false); }
    public GameObject MyChild(int number) { return MyChild(number, "\0", false)[0]; }
    public GameObject[] MyChild(string searchName) { return MyChild(-1, searchName, false); }
    public GameObject MyChild(string searchName, bool isOnlyFirst) { return MyChild(-1, searchName, isOnlyFirst)[0]; }
    public GameObject[] MyChild(int number, string searchName, bool isOnlyFirst)
    {
        // number      預設為 -1        輸出所有
        // searchTag   預設為 "\0"      依照Tag搜尋
        // isOnlyFirst 預設為 false     search標頭的修飾子，是否只輸出一個項目 ?

        List<GameObject> output = new List<GameObject>();
        string searchObj;

        if (number != -1)
            output.Add(this.transform.GetChild(number).gameObject);
        else
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (searchName.IndexOf("tag=") != -1)
                    searchObj = "tag=" + this.transform.GetChild(i).tag;
                else
                    searchObj = this.transform.GetChild(i).name;

                if (searchName == "\0" || searchName == searchObj)
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
    public float Obj2angle2D(GameObject a, GameObject b) { return Obj2angle2D(a.transform.position, b.transform.position); }
    public float Obj2angle2D(GameObject a, Vector2 b) { return Obj2angle2D(a.transform.position, b); }
    public float Obj2angle2D(Vector2 a, GameObject b) { return Obj2angle2D(a, b.transform.position); }
    public float Obj2angle2D(Vector2 a, Vector2 b)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(a.y - b.y, a.x - b.x);
    }



    // 快捷參數設定
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>設定 Color 參數</para>
    /// </summary>
    public void SetColor(float R,float G,float B, float A) { SetColor(new Color(R, G, B, A)); }
    public void SetColor(float A) { Color temp = spriteRenderer.color; SetColor(new Color(temp.r, temp.g, temp.b, A)); }
    public void SetColor(float R, float G, float B) { SetColor(new Color(R, G, B, spriteRenderer.color.a)); }
    public void SetColor(Color color) { spriteRenderer.color = color; }

    /// <summary>
    /// <para>設定 Animator 參數，將會自動判斷型別</para>
    /// <para>#注意 float 型別要加 "f"  ex( 0.5f , 1f ,...)</para>
    /// </summary>
    public void SetAnimator(string triggerName) { animator.SetTrigger(triggerName); }
    public void SetAnimator(string targetName,bool isTrue) { animator.SetBool(targetName,isTrue); }
    public void SetAnimator(string targetName, int intValue) { animator.SetInteger(targetName, intValue); }
    public void SetAnimator(string targetName, float floatValue) { animator.SetFloat(targetName, floatValue); }

    /// <summary>
    /// <para>添加 UI_System 之 UI_tooltips</para>
    /// </summary>
    public UI_tooltips SetTooltips(GameObject tooltips) { return SetTooltips(tooltips, UI_tooltips.tooltipsMode.Hold, new Vector2(0, 0), false); }
    public UI_tooltips SetTooltips(GameObject tooltips, UI_tooltips.tooltipsMode mode) { return SetTooltips(tooltips, mode, new Vector2(0, 0), false); }
    public UI_tooltips SetTooltips(GameObject tooltips, UI_tooltips.tooltipsMode mode, Vector2 pointerOffset) { return SetTooltips(tooltips, mode, pointerOffset, true); }
    public UI_tooltips SetTooltips(GameObject tooltips, UI_tooltips.tooltipsMode mode , Vector2 followPointerOffset, bool isFollowPointer)
    {
        UI_tooltips UI_tooltips = null;
        if (!TryGetComponent<UI_tooltips>(out UI_tooltips)) UI_tooltips = gameObject.AddComponent<UI_tooltips>();

        UI_tooltips.toolTips = tooltips;
        UI_tooltips.mode = mode;
        UI_tooltips.isFollowPointer = isFollowPointer;
        UI_tooltips.followPointerOffset = followPointerOffset;

        return UI_tooltips;
    }
}
