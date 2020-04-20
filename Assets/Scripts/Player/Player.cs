using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool bPlayerMove;
    public bool bPlayerControl;
    private UIManager mUIManager;
    public GameObject ArrowBoard = null;
    public Text BombText;
    public static int BoomCount = 3;
    public GameObject BombEffect;
    public GameObject UmbrellaBtn;
    public GameObject UmbrellaEffectPref;

    public GameObject Umbrella;

    public GameObject HPBar;

    public float fSpeed = 5f;
    public float fRadius = 1.5f;
    public float fRunningTime = 0f;

    public bool bLeft = false;
    public bool bRight = false;

    private Vector3 vNewPos = new Vector3();
    private float x = 0f;
    private float y = 1f;

    private float fAnimationAngle = 0;

    public static float fHp;
    public float fMaxHp = 3f;
    public bool IsTracking = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
     
        if (col.gameObject.tag == "Arrow")
        {
            if (fHp > 0)
            {
                HPBar.transform.GetChild((int)fHp).gameObject.active = false;
            }
            fHp--;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag.Equals("Item"))
        {
            BoomCount++;
            //GameObject instance = (GameObject)Instantiate(UmbrellaEffectPref, new Vector3(UmbrellaBtn.transform.position.x, UmbrellaBtn.transform.position.y, 10f), UmbrellaBtn.transform.rotation);
            //Destroy(instance, 4f);
            Destroy(col.gameObject);
        }
        if (col.gameObject.name.Equals("Heal"))
        {
            fHp++;
            Destroy(col.gameObject);
        }
    }

    public void PlayerResult()
    {
        if (ScoreManager.CScore >= PlayerPrefs.GetFloat("BEST", 0))
        {
            PlayerPrefs.SetFloat("BEST", ScoreManager.CScore);
            Social.Active.ReportScore(ScoreManager.CScore * 1, GPGSIds.leaderboard_readerboard, bSuccess =>
            {
                if (bSuccess)
                {
                    Debug.Log($"{ScoreManager.CScore} reported");
                }
                else
                {
                    Debug.Log("Authentication is required");
                }
            });
        }
        mUIManager.Result();
        AdManager.Instance.IsShowAd();
    }

    public void MoveLeft()
    {
        bRight = true;
    }

    public void MoveRight()
    {
            bLeft = true;
    }

    public void MoveLeftDown()
    {
            bRight = false;
    }

    public void MoveRightDown()
    {
            bLeft = false;
    }

    private void UmbrellaFalse()
    {
        Umbrella.SetActive(false);
    }
    
    private void UpdateHp()
    {
        if(fHp < 0)
        {
            PlayerResult();
            fHp = 0;
        }
        if(fHp > fMaxHp)
        {
            fHp = fMaxHp;
        }
    }

    public void ActiveBoom()
    {
        if (BoomCount > 0)
        {
            BoomCount--;
            Umbrella.active = true;
            Invoke("UmbrellaFalse", 5f);
            Umbrella.GetComponent<Unbrella>().UmbrellaOpen();
            AudioManager.Instance.RandomSoundEffect(AudioManager.Instance.Bomb);
        }
    }

    public float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private void ChangeAnimationAngle(float val)
    {
        fAnimationAngle += val * Time.deltaTime;
    }

    private void PlayerIdle()
    {
        float angle = GetAngle(transform.position, new Vector3(0f, 0f, 0f));
        Vector3 euler = new Vector3(0f, 0f, angle + 90f + fAnimationAngle);
        transform.rotation = Quaternion.Euler(euler);
    }

    private void MoveIdleAnimation()
    {
        if ((int)fAnimationAngle == 0f) return;
        if (fAnimationAngle < 5 && fAnimationAngle > -5)
            fAnimationAngle = 0;
        else if (fAnimationAngle > 5)
            ChangeAnimationAngle(-200);
        else if (fAnimationAngle < 5)
            ChangeAnimationAngle(200);
    }

    private void MovePositionRight()
    {
        fRunningTime += Time.deltaTime * fSpeed;
        x = fRadius * Mathf.Cos(fRunningTime);
        y = fRadius * Mathf.Sin(fRunningTime);
        vNewPos = new Vector3(x, y, 0f);
        this.transform.position = vNewPos;
    }

    private void MovePositionLeft()
    {
        fRunningTime -= Time.deltaTime * fSpeed;
        x = fRadius * Mathf.Cos(fRunningTime);
        y = fRadius * Mathf.Sin(fRunningTime);
        vNewPos = new Vector3(x, y, 0f);
        this.transform.position = vNewPos;
    }

    private void MoveAnimationRight()
    {
        if (fAnimationAngle >= -80 && fAnimationAngle <= 80) ChangeAnimationAngle(-500);
        CheckMoveAnimation();
    }

    private void MoveAnimationLeft()
    {
        if (fAnimationAngle >= -80 && fAnimationAngle <= 80) ChangeAnimationAngle(500);
        CheckMoveAnimation();
    }

    private void CheckMoveAnimation()
    {
        if (fAnimationAngle < -80) fAnimationAngle = -80;
        else if (fAnimationAngle > 80) fAnimationAngle = 80;
    }

    public bool GetbPlayerMove()
    {
        return bPlayerMove;
    }

    //tutorial
    public void GoToMove()
    {
        fSpeed = 2f;
        if (transform.position.y <= 1.49f)
        {
            MovePositionRight();
            MoveAnimationRight();
        }
        else MoveIdleAnimation();
        fSpeed = 5f;
    }

    public void PlayerReset()
    {
        BoomCount = 3;
        fHp = fMaxHp;
        HPBar.transform.GetChild((int)1).gameObject.active = true;
        HPBar.transform.GetChild((int)2).gameObject.active = true;
    }

    private void Start()
    {
        mUIManager = FindObjectOfType<UIManager>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        BoomCount = 3;
        fHp = fMaxHp;
    }

    private void Update()
    {
        UpdateHp();
        PlayerIdle();
        BombText.text = BoomCount.ToString();

        if (GameManager.Instance.IsStart)
        {
            MoveAnimationRight();
            MovePositionRight();
            return;
        }

        if (!GameManager.Instance.IsInGame && !GameManager.Instance.IsTutorial)
            return;

        if (bRight)
        {
            MoveAnimationRight();
            MovePositionRight();
        }
        else if (bLeft)
        {
            MoveAnimationLeft();
            MovePositionLeft();
        }
        else MoveIdleAnimation();
    }
}
