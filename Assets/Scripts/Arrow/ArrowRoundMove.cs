using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowRoundMove : MonoBehaviour
{
    public GameObject DropEffectPref;
    public Vector3 vTarget = new Vector3(0f, 0f, 0f);
    public Animator animator;
    public float fArrowSpeed = 2;
    public float RoundSpeed = 0.5f;
    public float fNowAngle = 0;
    public float fRandius = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager.CScore += 15;
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            if (fArrowSpeed >= 5)
                AudioManager.Instance.RandomSoundEffect(AudioManager.Instance.RainCol);
            GameObject Instance = Instantiate(DropEffectPref, gameObject.transform.position, transform.rotation) as GameObject;
            AudioManager.Instance.RandomSoundEffect(AudioManager.Instance.RainCol);
            Destroy(Instance, 0.7f);
            Destroy(gameObject);
        }
    }

    private void RandomArrowSpeed(int val)
    {
        fArrowSpeed = Random.Range(2, val);
        fRandius = 10f;
    }

    private void CheckAngle()
    {
        fNowAngle = Random.Range(0f, 100f);
        //fNowAngle = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
    }

    public float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private void MoveArrow()
    {
        if (!GameManager.Instance.IsInGame && !GameManager.Instance.IsTutorial)
            return;

        fNowAngle += RoundSpeed * Time.deltaTime;
        fRandius -= fArrowSpeed * Time.deltaTime;
        float x = fRandius * Mathf.Cos(fNowAngle);
        float y = fRandius * Mathf.Sin(fNowAngle);
        transform.position = new Vector3(x, y, 0f);
        float angle = GetAngle(transform.position, vTarget);
        Vector3 euler = new Vector3(0f, 0f, angle);
        transform.rotation = Quaternion.Euler(euler);
        transform.Rotate(new Vector3(0f, 0f, 90f));
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        RandomArrowSpeed(5);
        CheckAngle();
        animator.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();
    }
}
