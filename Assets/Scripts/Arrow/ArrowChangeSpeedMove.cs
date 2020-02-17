using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class ArrowChangeSpeedMove : MonoBehaviour
{
    public GameObject DropEffectPref;
    public Vector3 target = new Vector3(0f, 0f, 0f);
    public float fArrowSpeed;
    private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            ScoreManager.CScore += 25;
            GameObject Instance = Instantiate(DropEffectPref, gameObject.transform.position, transform.rotation) as GameObject;
            AudioManager.Instance.RandomSoundEffect(AudioManager.Instance.RainCol);
            Destroy(Instance, 0.7f);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Active")
        {
            ChangeSpeed();
            animator.speed = 1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            if (fArrowSpeed >= 5)
                AudioManager.Instance.RandomSoundEffect(AudioManager.Instance.RainCol);
            Destroy(gameObject);
        }
    }

    private void RandomArrowSpeed(int val)
    {
        fArrowSpeed = Random.Range(2, val);
    }

    private void MoveArrow()
    {
        if (!GameManager.Instance.IsInGame && !GameManager.Instance.IsTutorial)
            return;

        float step = fArrowSpeed * GameManager.Instance.fGameSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        float angle = GetAngle(transform.position, target);
        Vector3 euler = new Vector3(0f, 0f, angle);
        transform.rotation = Quaternion.Euler(euler);
    }

    private void ChangeSpeed()
    {
        fArrowSpeed = 7.5f;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        RandomArrowSpeed(5);
        animator.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();
        if (transform.position.y == 0 && transform.position.x == 0) Destroy(this.gameObject);
    }
    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
}
