using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour  
{

    public enum MonsterState { idle = 0, trace, attack, die }; // ���� ���������� �ִ� Enumerable ���� ����
    public MonsterState monsterState = MonsterState.idle; // ������ ���� ���� ������ ���� �� Enum ����

    private Transform monsterTr; // ���� ��ġ
    private Transform playerTr; // ������ �÷��̾� ��ġ
    private NavMeshAgent nvAgent; // ���� AI�� ���� �׺�޽�
    private Animator animator; // ���� �ִϸ��̼� ������Ʈ�� ���� ����

    public float traceDist = 10.0f; // ���� �����Ÿ�
    public float attackDist = 2.5f; // ���� �����Ÿ�
    private bool isDie = false; // ���� ��� ����
    private int hp = 100; // ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();      
    }

    // Update is called once per frame
    void Update()
    {
        nvAgent.destination = playerTr.position; // ���� ��ġ�� ��ġ�� ������Ʈ �Ѵ�.
        animator.SetBool("IsTrace", true); // ���� �ִϸ��̼��� Ȱ��ȭ �Ѵ�.

        StartCoroutine(CheckMonsterState()); // ���� �������� ������ ���¸� üũ�ϴ� �ڷ�ƾ
        StartCoroutine(MonsterAction()); // ������ ���¿� ���� ������ �����ϴ� �ڷ�ƾ

        this.transform.LookAt(new Vector3(playerTr.position.x, this.transform.position.y, playerTr.position.z));
    }

    // ������ �������� ������ �ൿ ���¸� üũ�ϰ� monsterState�� �� ����
    IEnumerator CheckMonsterState()
    {

        while(!isDie)
        {
            // ������ ���� 0.2�� ���� ��ٷȴٰ� ó���� ����
            yield return new WaitForSeconds(0.2f);

            // ���Ϳ� �÷��̾� ������ �Ÿ� ����
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if(dist <= attackDist && !GameManager.Instance.isGameOver)
            {
                
                monsterState = MonsterState.attack;
            }
            else if(dist <= traceDist && !GameManager.Instance.isGameOver)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }

    }

    // ������ ���°��忡 ���� ������ ������ �����ϴ� �Լ�
    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch(monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true; // ������ �ߴ��Ѵ�.
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position; // ���� ����� ��ġ�� �Ѱ��ش�.
                    nvAgent.isStopped = false;// ������ �����Ѵ�
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;
                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
       
    }

    public void GetDamage(float amount)
    {
        hp -= (int)(amount / 2.0f); // �ܰ����� �������� �������� ���̴� Ư�� �ɷ�
        animator.SetTrigger("IsHit");

        if(hp<=0)
        {
            MonsterDie();
        }
    }

    // ���� ��� �� ó�� ��ƾ
    void MonsterDie() 
    {
        // �ߺ� ���� ����
        if(isDie == true)
        {
            return;
        }
        StopAllCoroutines(); // ��� �ڷ�ƾ�� �����Ѵ�.
        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.isStopped = true;
        animator.SetTrigger("IsDie");

        // �ڽ��� Collider�� ��Ȱ��ȭ
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        // �ڽ�(Child)�� Collider�� ��Ȱ��ȭ
        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

        GameManager.Instance.GetScored(2);
    }
}
