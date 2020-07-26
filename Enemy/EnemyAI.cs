using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //general variables
    public Transform target;
    public GameObject[] allTargets;
    public EnumsScript.Player_State playerStateReference;
    public EnemyFSM enemyBrain;
    public EnumsScript.Enemy_State previousState;
    public EnumsScript.Enemy_State currentState;
    public Animator myAnimator;
    public bool brainAction;
    public float noActionTime;
    public float noActionTimeCount;

    public float checkDistance;

    //seek variables
    public float distance;
    public float velocity;
    public float seekOponentTimer;
    public float seekOponentCounter;

    //attack variables
    private float attackAbilityProb;
    public bool hasAbility;
    public int attackDesition;
    public bool castingAbility;
    public float attackTime;
    public float attackTimeCounter;

    //To-Do
    //public int attackComboLengh;
    //public int attackComboCounter;

    //defend variables
    public float defendTime;
    public float defendTimeCounter;
    public float defenceDistance;
    public bool defenceLock;

    //hurt variables
    public bool interrupted;

    // Start is called before the first frame update
    void Start()
    {
        enemyBrain = new EnemyFSM();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        allTargets= GameObject.FindGameObjectsWithTag("Player");
        //attackAbilityProb = (float)abilityProbSlider.value;
        Mathf.Clamp(attackAbilityProb, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance = Vector3.Distance(transform.position, target.position);
        currentState = enemyBrain.GetCurrentState();
        if (brainAction)
        {
            TakeAction();
            enemyBrain.GetCurrentState();
        }
    }

    public void TakeAction()
    {
        switch (currentState)
        {
            case EnumsScript.Enemy_State.INITIAL:
                AtStartFight();
                break;

            case EnumsScript.Enemy_State.ATTACK:
                Attack();
                break;

            case EnumsScript.Enemy_State.DEFEND:
                Defend();
                break;

            case EnumsScript.Enemy_State.SEEK:
                SeekTarget();
                break;

            case EnumsScript.Enemy_State.NO_ACTION:
                NoAction();
                break;

            case EnumsScript.Enemy_State.AVOID:
                break;

            case EnumsScript.Enemy_State.STRUGGLE:

                break;

            case EnumsScript.Enemy_State.ABILITY:
                UseAbility();
                break;

            case EnumsScript.Enemy_State.DEATH:
                brainAction = false;
                break;

            case EnumsScript.Enemy_State.NONE:
                break;
        }
    }

    public void Attack()
    {
        //To-Do
        Debug.Log("attacking");
        defenceLock = false;
        attackTimeCounter += Time.deltaTime;
        if (attackTimeCounter > attackTime)
        {
            previousState = enemyBrain.GetCurrentState();
            enemyBrain.PopState();
            enemyBrain.PushState(EnumsScript.Enemy_State.NO_ACTION);
            attackTimeCounter = 0;
        }

        /*
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            //transform.Translate(target.position);
        }
        else if (Vector3.Distance(transform.position, target.position) > distance)
        {
            previousState = enemyBrain.GetCurrentState();
            enemyBrain.PopState();
            enemyBrain.PushState(EnumsScript.Enemy_State.SEEK);
        }
        */
    }

    public void UseAbility()
    {
        Debug.Log("Use ability");
        //To-Do
        previousState = enemyBrain.GetCurrentState();
        enemyBrain.PopState();
        enemyBrain.PushState(EnumsScript.Enemy_State.NO_ACTION);
    }

    public void Defend()
    {
        Debug.Log("Defend");
        defenceLock = false;
        defendTimeCounter += Time.deltaTime;
        if (defendTimeCounter > defendTime)
        {
            defendTimeCounter = 0;
            previousState = enemyBrain.GetCurrentState();
            enemyBrain.PopState();
            if (currentState != EnumsScript.Enemy_State.SEEK)
            {
                enemyBrain.PushState(EnumsScript.Enemy_State.SEEK);
            }
        }
    }

    public void NoAction()
    {
        Debug.Log("No action");
        noActionTimeCount += Time.deltaTime;
        if (noActionTimeCount > noActionTime)
        {
            noActionTimeCount = 0;
            previousState = enemyBrain.GetCurrentState();
            enemyBrain.PopState();
            if (playerStateReference == EnumsScript.Player_State.ATTACK)
            {
                enemyBrain.PushState(EnumsScript.Enemy_State.DEFEND);
            }
            else
            {
                enemyBrain.PushState(EnumsScript.Enemy_State.SEEK);
            }
        }
    }

    //first execution when the fight starts
    public void AtStartFight()
    {
        Debug.Log("Initial State");
        //To-Do
        //target = allTargets[Random.Range(0, allTargets.Length)].transform;
        //enable animations when the animator is implemented
        /*if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("StartAnimation"))
        {
            enemyBrain.PopState();
            enemyBrain.PushState(EnumsScript.Enemy_State.SEEK);
        }*/
        previousState = enemyBrain.GetCurrentState();
        enemyBrain.PopState();
        enemyBrain.PushState(EnumsScript.Enemy_State.SEEK);
    }

    //Seek the target for pushing other states like attacking
    public void SeekTarget()
    {
        Debug.Log("seeking target");

        //To-Do
        //add animations
        if (hasAbility)
        {
            attackTimeCounter += Time.deltaTime;
            if (attackTimeCounter > attackTime)
            {
                attackTimeCounter =0;
                previousState = enemyBrain.GetCurrentState();
                enemyBrain.PopState();
                enemyBrain.PushState(EnumsScript.Enemy_State.ABILITY);
            }
        }
        
        //if(the enemy is far, then approach
        if ((Vector3.Distance(transform.position, target.position) > distance&&playerStateReference!=EnumsScript.Player_State.ATTACK)||
            (Vector3.Distance(transform.position, target.position) > distance && defenceLock))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);
        }
        //if the enemy isn't in defend distance and the player is attacking, then block defend condition and just ram to the player
        else if (Vector3.Distance(transform.position, target.position) > defenceDistance
            && playerStateReference == EnumsScript.Player_State.ATTACK&&!defenceLock)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);
            defenceLock = true;
        }
        //if the enemy isn't close enough for attacking and the player is attacking and the enemy is in the determined defend range, then start defending
        else if (Vector3.Distance(transform.position, target.position) > distance &&
            Vector3.Distance(transform.position, target.position) < defenceDistance
            && playerStateReference == EnumsScript.Player_State.ATTACK)
        {
            enemyBrain.PushState(EnumsScript.Enemy_State.DEFEND);
        }
        //if the player is in attack range, then attack
        else if(Vector3.Distance(transform.position, target.position) < distance)
        {
            previousState = enemyBrain.GetCurrentState();
            enemyBrain.PopState();
            enemyBrain.PushState(EnumsScript.Enemy_State.ATTACK);
        }
    }

    public void Hurt()
    {

    }

    //push the struggle state if the enemy isn't defending
    public void TryPushHurtState()
    {
        //To-Do
        //check all states for the hurt condition to be applied
        //just defend state applied
        if (enemyBrain.GetCurrentState() != EnumsScript.Enemy_State.DEFEND)
        {
            enemyBrain.PushState(EnumsScript.Enemy_State.STRUGGLE);
        }
    }
}