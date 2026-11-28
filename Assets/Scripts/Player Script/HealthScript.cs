using System.Collections;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    [SerializeField] private float cannibal_Health = 100f;
    [SerializeField] private float boar_Health = 150f;

    public bool is_Player, is_Boar, is_Cannibal;

    private bool is_dead;

    private EnemyAudio enemy_audio;

    private PlayerStats player_Stats;

    void Awake()
    {
        if (is_Boar || is_Cannibal)
        {
            health = is_Boar ? boar_Health : cannibal_Health;

            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            // get enemy audio
            enemy_audio = GetComponentInChildren<EnemyAudio>();
        }

        if (is_Player)
        {
            player_Stats = GetComponent<PlayerStats>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if (is_dead)
            return;

        health -= damage;

        if (is_Player)
        {
            // display the health UI
            player_Stats.Display_HealthStats(health);
        }

        if (is_Boar || is_Cannibal)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }

        if (health <= 0)
        {
            PlayerDied();
            is_dead = true;
        }
    }

    void PlayerDied()
    {
        if (is_Cannibal)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            gameObject.transform.Rotate(90f, 0f, 0f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());

            // Enemy Manager spawn more enemies
            EnemyManager.instance.EnemyDied(true);
        }

        if (is_Boar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            StartCoroutine(DeadSound());

            // Enemy Manager spawn more enemies
            EnemyManager.instance.EnemyDied(false);
        }

        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffgameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffgameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemy_audio.play_dieSound();
    }
}