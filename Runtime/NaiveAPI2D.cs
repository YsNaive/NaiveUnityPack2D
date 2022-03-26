using NaiveAPI;
using System.Collections.Generic;
using UnityEngine;

/*--------------------- Made by YsNaive -------------------*/


/// <summary>
/// �I�s�ֱ����
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

    

    // ���Ȭ���
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// ���o���Цb�@�ɤW���y��
    /// </summary>
    public Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    /// <summary>
    /// <para>���o�ۨ������� </para>
    /// </summary>
    public GameObject MyParent()
    {
        return this.transform.parent.gameObject;
    }


    /// <summary>
    /// <para>���o�ۨ��Ҧ��l����A�άO���S�w����j�M </para>
    /// <para>�i�α��� int / tag</para>
    /// </summary>
    public GameObject[] MyChild() { return MyChild(-1, "\0", false); }
    public GameObject MyChild(int number) { return MyChild(number, "\0", false)[0]; }
    public GameObject[] MyChild(string searchName) { return MyChild(-1, searchName, false); }
    public GameObject MyChild(string searchName, bool isOnlyFirst) { return MyChild(-1, searchName, isOnlyFirst)[0]; }
    public GameObject[] MyChild(int number, string searchName, bool isOnlyFirst)
    {
        // number      �w�]�� -1        ��X�Ҧ�
        // searchTag   �w�]�� "\0"      �̷�Tag�j�M
        // isOnlyFirst �w�]�� false     search���Y���׹��l�A�O�_�u��X�@�Ӷ��� ?

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




    // �ƾǬ���
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>A ���󤧩� B ���󪺨��� ( 180 ~ -180 )</para>
    /// <para>�V�k���s �f�ɰw����</para>
    /// </summary>
    public float Obj2angle2D(GameObject a, GameObject b) { return Obj2angle2D(a.transform.position, b.transform.position); }
    public float Obj2angle2D(GameObject a, Vector2 b) { return Obj2angle2D(a.transform.position, b); }
    public float Obj2angle2D(Vector2 a, GameObject b) { return Obj2angle2D(a, b.transform.position); }
    public float Obj2angle2D(Vector2 a, Vector2 b)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(a.y - b.y, a.x - b.x);
    }



    // �ֱ��ѼƳ]�w
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>�]�w Color �Ѽ�</para>
    /// </summary>
    public void SetColor(float R,float G,float B, float A) { SetColor(new Color(R, G, B, A)); }
    public void SetColor(float A) { Color temp = spriteRenderer.color; SetColor(new Color(temp.r, temp.g, temp.b, A)); }
    public void SetColor(float R, float G, float B) { SetColor(new Color(R, G, B, spriteRenderer.color.a)); }
    public void SetColor(Color color) { spriteRenderer.color = color; }

    /// <summary>
    /// <para>�]�w Animator �ѼơA�N�|�۰ʧP�_���O</para>
    /// <para>#�`�N float ���O�n�[ "f"  ex( 0.5f , 1f ,...)</para>
    /// </summary>
    public void SetAnimator(string triggerName) { animator.SetTrigger(triggerName); }
    public void SetAnimator(string targetName,bool isTrue) { animator.SetBool(targetName,isTrue); }
    public void SetAnimator(string targetName, int intValue) { animator.SetInteger(targetName, intValue); }
    public void SetAnimator(string targetName, float floatValue) { animator.SetFloat(targetName, floatValue); }

    /// <summary>
    /// <para>�K�[ UI_System �� UI_tooltips</para>
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
