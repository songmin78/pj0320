using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weaponcheck : MonoBehaviour
{
    Animator animator;
    [Header("��Ÿ ����")]
    [SerializeField] float speed = 1.0f;//���� ���� �ӵ�
    [SerializeField] float Xposition;//���� x��ǥ
    [SerializeField] float Yposition;//���� y��ǥ
    [SerializeField] float Zposition;//���� z��ǥ
    [SerializeField] float ZeulerAngles;//���� rotation z ��ǥ
    [SerializeField] float eulercheck;//�ٶ󺸰��ִ� ����

    [Header("���� ����")]
    [SerializeField] public GameObject Typeweapon;

    [Header("�������")]
    [SerializeField] bool attackcheck = true;//���� üũ
    [SerializeField] bool Counter = false;//ī���� ����
    [SerializeField] public float AttackdamageMax = 1f;//���� ������
    [SerializeField] bool arrow = false;//ȭ���ΰ��� Ȯ��
    [SerializeField] bool punch = false;//����������� Ȯ��
    [SerializeField] public bool magic = false;//�����ΰ��� Ȯ��
    [SerializeField] bool Ctmagic = false;//���� ī����(�Ϲݰ��ݰ� �ٸ� ��Ŀ�������� ������ ���ܼ� ���� ����)
    [SerializeField] bool magicway = false;//������ ������ ���� üũ
    float waymagic;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Ctmagic == true)
        {
            if (collision.gameObject.tag == "Monster")
            {
                Destroy(Typeweapon);
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        startattack();

        GameManager.Instance.Weaponcheck = this;
    }

    void Update()
    {
        shotarrow();
        weapondestory();
        magicwaychange();

    }

    public void Attackdamage(int _weaponType,int _eulercheck)
    {
        if(_weaponType == 0)//���Ÿ� ����
        {
            Debug.Log("���Ÿ�");
        }
        else if(_weaponType == 1)//���� ����
        {
            eulercheck = _eulercheck; 
            Debug.Log(eulercheck);
        }
        else if(_weaponType == 2)//��������
        {
            magicway = true;
            Debug.Log("����");
        }
    }

    public void Counterdamage(int _weaponType, int _eulercheck)
    {
        if (_weaponType == 0)//���Ÿ� ����
        {
            Debug.Log("���Ÿ� ī����");
        }
        else if (_weaponType == 1)//���� ����
        {
            eulercheck = _eulercheck;
            Debug.Log("�ٰŸ� ī����");
        }
        else if (_weaponType == 2)//��������
        {
            Debug.Log("���� ī����");
        }
    }

    private void shotarrow()
    {
        if(arrow == true)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
        else if(punch == true)
        {
            //1.������Ʈ�� �����ϸ� �� �ڸ��� ���� ���� ->��
            //2.rotation�� z���� +180 Ȥ�� -180 �� ���Ѵ�
            //3.2������ ���� ���� Time.deltatime * punchtime �ʷ� ȸ���� ���� ��Ų�� 
            eulerchange();
        }
        else if(Ctmagic == true)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

    public void startattack()
    {
        Xposition = transform.position.x;
        Yposition = transform.position.y;
        Zposition = transform.position.z;

        ZeulerAngles = transform.rotation.z;

        new Vector3(Xposition, Yposition, Zposition);
        //new Vector3(0, 0, ZeulerAngles);
    }

    private void weapondestory()
    {
        float Xchange = transform.position.x;
        float Ychange = transform.position.y;
        float Zchange = transform.position.z;

        float Zrotation = transform.eulerAngles.z;

        if (arrow == true)
        {
            transform.position = new Vector3(Xchange, Ychange, 0);

            if (Xchange >= Xposition + 5 || Xchange <= Xposition - 5)
            {
                Destroy(Typeweapon);
            }
            else if (Ychange >= Yposition + 5 || Ychange <= Yposition - 5)
            {
                Destroy(Typeweapon);
            }
        }
        else if (punch == true)
        {

        }
        else if(magic == true)
        {
            animator.SetBool("check", true);
            if (GameManager.Instance.Player.MagicCheck == false)
            {
                animator.SetBool("check", false);
                Destroy(Typeweapon);
            }
        }
        else if(Ctmagic == true)
        {
            transform.position = new Vector3(Xchange, Ychange, 0);

            if (Xchange >= Xposition + 5 || Xchange <= Xposition - 5)
            {
                Destroy(Typeweapon);
            }
            else if (Ychange >= Yposition + 5 || Ychange <= Yposition - 5)
            {
                Destroy(Typeweapon);
            }
        }
    }
  
    /// <summary>
    /// eluercheck-> 1=> ����,2 => ����, 3=> ������, 4=> �Ʒ��� 
    /// </summary>
    private void eulerchange()//�����ϴ� ���⿡ ���� ȸ���ϴ� �ڵ�//���� ���⿡�� �ش�
    {
        if(eulercheck == 1)
        {
            transform.eulerAngles += new Vector3(0, 0, -90 * Time.deltaTime * speed);
        }
        else if(eulercheck == 2)
        {
            transform.eulerAngles += new Vector3(0, 0, -180 * Time.deltaTime * speed);
        }
        else if(eulercheck == 3)
        {
            transform.eulerAngles += new Vector3(0, 0, -180 * Time.deltaTime * speed);
        }
        else if(eulercheck == 4)
        {
            transform.eulerAngles += new Vector3(0, 0, -270 * Time.deltaTime * speed);
        }
    }

    private void magicwaychange()//�������� ������ ��ȯ
    {
        if (magicway == true)
        {
            waymagic = GameManager.Instance.CheckBox.waycheck;//checkbox�� �ִ� �ٶ󺸰��ִ� ������ Ȯ��
            Vector3 vec3 = GameManager.Instance.Player.transform.position;//���� �÷��̾��� ��ġ�� Ȯ��
            Vector3 vec = GameManager.Instance.Player.transform.eulerAngles;
            if(waymagic == 0)//������ �ٶ󺸰�������
            {
                if (GameManager.Instance.Player.transform.localScale == new Vector3(-1, 1, 1))
                {
                    vec = vec + new Vector3(0, 0, 270);
                    vec3 = vec3 + new Vector3(0, 1, 0);
                }
                else
                {
                    vec = vec + new Vector3(0, 0, 90);
                    vec3 = vec3 + new Vector3(0, 1, 0);
                }
                //vec3 = vec3 + new Vector3(0, 0, 90);
            }
            else if(waymagic == 1)//�������� �ٶ󺸰�������
            {
                vec = vec + new Vector3(0, 0, 0);
                vec3 = vec3 + new Vector3(0.9f, 0, 0);
                //vec3 = vec3 + new Vector3(0, 0, 0);
            }
            else if(waymagic == 2)//�Ʒ����� �ٶ󺸰�������
            {
                if(GameManager.Instance.Player.transform.localScale == new Vector3(-1, 1, 1))
                {
                    vec = vec + new Vector3(0, 0, 90);
                    vec3 = vec3 + new Vector3(0, -1, 0);
                }
                else
                {
                    vec = vec + new Vector3(0, 0, 270);
                    vec3 = vec3 + new Vector3(0, -1, 0);
                }
                //vec3 = vec3 + new Vector3(0, 0, 270);
            }
            else if(waymagic == 3)//������ �ٶ󺸰�������
            {
                vec = vec + new Vector3(0, 0, 0);
                vec3 = vec3 + new Vector3(-0.9f, 0, 0);
                //vec3 =  vec3 +new Vector3(0, 0, 180);
            }

            transform.eulerAngles = vec;
            transform.position = vec3;
        }
    }
}
