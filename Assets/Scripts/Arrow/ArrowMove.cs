using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowMove : MonoBehaviour
{
    public Vector3 target = new Vector3(0f, 0f, 0f);
    public float fArrowSpeed;
    public GameObject DropEffectPref;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Respawn"))
        {
            if (this.gameObject.tag.Equals("Item"))
            {
                Destroy(gameObject);
                return;
            }

            if (fArrowSpeed >= 5)
                AudioManager.Instance.RandomSoundEffect(AudioManager.Instance.RainCol);
            GameObject Instance = Instantiate(DropEffectPref, gameObject.transform.position, transform.rotation) as GameObject;
            Destroy(Instance, 0.7f);
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

        float step = fArrowSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        float angle = GetAngle(transform.position, target);
        Vector3 euler = new Vector3(0f, 0f, angle);
        transform.rotation = Quaternion.Euler(euler);
        transform.Rotate(new Vector3(0f, 0f, 90f));
    }

    public static float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    void Start()
    {
        RandomArrowSpeed(5);
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();
    }

}
