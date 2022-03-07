using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------- Made by YsNaive -------------------*/


/// <summary>
/// �I�s�ֱ����
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



    // ���Ȭ���
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// ���o���Цb�@�ɤW���y��
    /// </summary>
    public Vector2 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    /// <summary>
    /// <para>���o�ۨ������� </para>
    /// </summary>
    public GameObject myParent()
    {
        return this.transform.parent.gameObject;
    }


    /// <summary>
    /// <para>���o�ۨ��Ҧ��l����A�άO���S�w����j�M </para>
    /// <para>�i�α��� int / tag</para>
    /// </summary>
    public GameObject[] myChild() { return myChild(-1, "\0", false); }
    public GameObject myChild(int number) { return myChild(number, "\0", false)[0]; }
    public GameObject[] myChild(string searchTag) { return myChild(-1, searchTag, false); }
    public GameObject myChild(string searchTag, bool isOnlyFirst) { return myChild(-1, searchTag, isOnlyFirst)[0]; }
    public GameObject[] myChild(int number, string searchTag, bool isOnlyFirst)
    {
        // number      �w�]�� -1        ��X�Ҧ�
        // searchTag   �w�]�� "\0"      �̷�Tag�j�M
        // isOnlyFirst �w�]�� false     search���Y���׹��l�A�O�_�u��X�@�Ӷ��� ?

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



    // �ƾǬ���
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>A ���󤧩� B ���󪺨��� ( 180 ~ -180 )</para>
    /// <para>�V�k���s �f�ɰw����</para>
    /// </summary>
    public float obj2angle2D(GameObject a, GameObject b) { return obj2angle2D(a.transform.position, b.transform.position); }
    public float obj2angle2D(GameObject a, Vector2 b) { return obj2angle2D(a.transform.position, b); }
    public float obj2angle2D(Vector2 a, GameObject b) { return obj2angle2D(a, b.transform.position); }
    public float obj2angle2D(Vector2 a, Vector2 b)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(a.y - b.y, a.x - b.x);
    }



    // �ֱ��ѼƳ]�w
    /*---------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// <para>�]�w Color �Ѽ�</para>
    /// </summary>
    public void setColor(float R,float G,float B, float A) { setColor(new Color(R, G, B, A)); }
    public void setColor(float A) { Color temp = spriteRenderer.color; setColor(new Color(temp.r, temp.g, temp.b, A)); }
    public void setColor(float R, float G, float B) { setColor(new Color(R, G, B, spriteRenderer.color.a)); }
    public void setColor(Color color) { spriteRenderer.color = color; }

    /// <summary>
    /// <para>�]�w Animator �ѼơA�N�|�۰ʧP�_���O</para>
    /// <para>#�`�N float ���O�n�[ "f"  ex( 0.5f , 1f ,...)</para>
    /// </summary>
    public void setAnimator(string triggerName) { animator.SetTrigger(triggerName); }
    public void setAnimator(string targetName,bool isTrue) { animator.SetBool(targetName,isTrue); }
    public void setAnimator(string targetName, int intValue) { animator.SetInteger(targetName, intValue); }
    public void setAnimator(string targetName, float floatValue) { animator.SetFloat(targetName, floatValue); }
}