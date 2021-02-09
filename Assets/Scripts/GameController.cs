using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject enemyPosition => GameObject.FindGameObjectWithTag("Player");

    [SerializeField] Sprite[] heartSprite;
    [SerializeField] Image[] heart;
    [SerializeField] Text finalTextScore;
    public float SpeedColors = 1.0f;
    public int IndexColors = 0;
    public float NextSpeed = 0.5f;

    public static int scoreHeart;
    public static bool isGame = true;
    public static bool isZero = false;
    public static bool isFirstScore = true;
    public static bool isTakeOneHeartAway = false;
    private Camera _camera => Camera.main;

    private bool isTraffic = true;
    private bool isInstantiate = false;
    private bool isMoveGame = false;

    private float nextPosition = 10f;
    private float target = 0;
    private float force = 0;
    public GameObject scoreText => GameObject.FindGameObjectWithTag("Score");
    public GameObject CharacterBotPrefab;
    public GameObject ColorsPrefab;
    public GameObject TargetFire;

    public static int score = 0;

    private void Awake()
    {
        for (int i = 0; i < heart.Length; i++) heart[i].sprite = heartSprite[0];
        scoreHeart = heart.Length;
    }

    private void Update()
    {
        if (isZero)
        {
            isZero = false;
            SpeedColors += NextSpeed;
            IndexColors = 0;
            BarrierScript barrier = FindObjectOfType<BarrierScript>();
            barrier.PlayAnimation(IndexColors, SpeedColors);
        }
        finalTextScore.text = score.ToString();
        scoreText.GetComponent<Text>().text = "x" + score;

        if (isMoveGame) MoveGame();

        if (Enemy.isDead)
        {
            isTraffic = true;

            if (!isInstantiate)
            {
                isInstantiate = true;
                Bow.isMouseDown = false;
                Instantiate(ColorsPrefab, transform.position = new Vector3(ColorsPrefab.transform.localPosition.x + nextPosition, 0, 0), transform.rotation);
                Instantiate(CharacterBotPrefab, transform.position = new Vector2(CharacterBotPrefab.transform.localPosition.x + nextPosition, -1.13f), transform.rotation);
            }

            if (isTraffic) 
            {
                force += Time.deltaTime;
                _camera.transform.position = new Vector3(_camera.transform.localPosition.x + force, _camera.transform.localPosition.y, -10f);
            }

            if (_camera.transform.localPosition.x >= target)
            {
                force = 0;
                nextPosition += 10f;
                isTraffic = false;
                isMoveGame = true;
                isInstantiate = false;
                Enemy.isDead = false;
            }
        }
        else
        {
            target = _camera.transform.localPosition.x + 10;
        }

        if(isTakeOneHeartAway) MinusOneHeart(scoreHeart);

        if (Enemy.isMoveToFire)
        {

            Enemy.isMoveToFire = false;
            Invoke(nameof(MoveFire), 1f);
            Invoke(nameof(ScorePlus), 2f);
            Invoke(nameof(ReturnIsDeadTrue), 2.5f);
        }
    }

    public void MinusOneHeart(int index)
    {
        isTakeOneHeartAway = false;
        isFirstScore = false;

        for (int i = 0; i < index; i++) heart[i].sprite = heartSprite[0];
        heart[index].sprite = heartSprite[1];
    }

    private void MoveGame()
    {
        isMoveGame = false;
        _camera.transform.position = new Vector3(Convert.ToInt32(_camera.transform.localPosition.x), _camera.transform.localPosition.y, -10f);
    }

    private void PlayAimationFire(bool isActive, bool isDestroy = false)
    {
        GameObject fire = GameObject.FindGameObjectWithTag("Fire");
        Animator fireAnimator = fire.GetComponent<Animator>();
        fireAnimator.SetBool("isFireMove", isActive);

        if (isDestroy) Destroy(fire);
    }

    private void ScorePlus()
    {
        PlayAimationFire(false, true);
        score++;
        IndexColors++;
    }
    private void MoveFire() => PlayAimationFire(true);
    private void ReturnIsDeadTrue() => Enemy.isDead = true;
}
