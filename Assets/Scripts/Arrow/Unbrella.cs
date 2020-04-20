using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unbrella : MonoBehaviour
{
    public float fAnimationAngle = 0f;
    public float fRunningTime = 0f;
    public float x = 0;
    public float y = 1;
    public float fRadius = 6;
    public Vector3 vNewPos;
    public GameObject Player;
    public Player m_CPlayer;
    private Animator animator;
    public float m_invoke_time = 5.0f;
  
    public float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private void StopAnimation()
    {
        animator.speed = 0f;
    }

    public void UmbrellaOpen()
    {
        animator.speed = 1f;
        m_invoke_time = 5.0f;
        Invoke("StopAnimation", m_invoke_time);
    }

    private void UmbrellaIdle()
    {
        float angle = GetAngle(transform.position, new Vector3(0f, 0f, 0f));
        Vector3 euler = new Vector3(0f, 0f, angle + 90f + fAnimationAngle);
        transform.rotation = Quaternion.Euler(euler);
    }

    private void MovePosition()
    {
        x = fRadius * Mathf.Cos(fRunningTime);
        y = fRadius * Mathf.Sin(fRunningTime);
        vNewPos = new Vector3(x, y, 0f);
        this.transform.position = vNewPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_CPlayer = Player.GetComponent<Player>();
        fRunningTime = m_CPlayer.fRunningTime;
        animator.speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        this.fRunningTime = m_CPlayer.fRunningTime;
        UmbrellaIdle();
        MovePosition();
    }
}
