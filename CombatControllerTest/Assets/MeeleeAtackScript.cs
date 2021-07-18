using UnityEngine;

public class MeeleeAtackScript : MonoBehaviour
{
    [SerializeField] private LayerMask meeleeAttack;
    [SerializeField] private GameObject gun, sword, meeleeText;
    [SerializeField] private float power;


    private Rigidbody rigibody;
    private Animator animator;
    private Test testScript;

    private float meeleeAttackTime;
    private float maxTimeForMeeleeAttack;

    private void Awake()
    {
        testScript = GetComponent<Test>();
        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody>();

        maxTimeForMeeleeAttack = 1.667f;
        meeleeAttackTime = maxTimeForMeeleeAttack;
    }

    private void Update()
    {

        Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;


        if (Physics.Linecast(transform.position, enemy.transform.position, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.CompareTag("Enemy") && Vector3.Distance(transform.position, hitInfo.transform.position) < 4)
            {
                meeleeText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space) && testScript.enabled == true)
                {
                    testScript.enabled = false;
                    MeeleeAttack(enemy);
                }
            }
            else
            {
                meeleeText.SetActive(false);
            }

        }

        if (testScript.enabled == false)
        {
            meeleeAttackTime -= Time.deltaTime;

            if(meeleeAttackTime < 0f)
            {
                sword.gameObject.SetActive(false);
                gun.gameObject.SetActive(true);
                testScript.enabled = true;
                meeleeAttackTime = maxTimeForMeeleeAttack;
            }
        }

    }

    private void MeeleeAttack(Transform enemy)
    {
        gun.gameObject.SetActive(false);
        sword.gameObject.SetActive(true);
        transform.LookAt(enemy);
        rigibody.AddForce(transform.forward * power);

        Animator enemyAnimator = enemy.GetComponentInChildren<Animator>();
        Rigidbody[] enemyRb = enemy.GetComponentsInChildren<Rigidbody>();

        enemyAnimator.enabled = false;

        foreach (var item in enemyRb)
        {
            item.isKinematic = false;
            item.AddForce(this.transform.forward * 2000);
        }
        enemy.GetComponent<EnemyControllerScript>().TakeDamage(200);
        animator.SetTrigger("Meelee_Attack");       

    }
}
