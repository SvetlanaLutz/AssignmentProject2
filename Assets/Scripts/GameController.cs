using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Sprite[] heartSprite;
    [SerializeField] Image[] heart;
    [SerializeField] Text finalTextScore;
//im tierd of this 
    public float SpeedColors = 0.5f;
    public int IndexColors = 0;
    public float NextSpeed = 0.5f;
// i dot know if i need describe this but i guess its okay to understand 
    public static int scoreHeart;
    public static bool isGame = true;
    public static bool isZero = false;
    public static bool isFirstScore = true;
    //this is to solve a problem with bug when one arrow take two hearth because he tuch enemy to times or three 
    public static bool isTakeOneHeartAway = false;
    private Camera _camera => Camera.main;

    private bool isTraffic = true;
//creating charaters behind camera i think its here 
    private bool isInstantiate = false;
//here is about how camera will turn back 
    private bool isRound = false;

    private float nextPosition = 10f;
    private float target = 0;
    private float force = 0;
    public GameObject scoreText => GameObject.FindGameObjectWithTag("Score");
    public GameObject CharacterBotPrefab;
    public GameObject ColorsPrefab;

    public static int score = 0;

    private void Awake()
    {
        for (int i = 0; i < heart.Length; i++) heart[i].sprite = heartSprite[0];
        scoreHeart = heart.Length;
    }

    private void Update()
    // oh i dont remember i think when all lvels get finish u have the same one but different speed 
    {
        if (isZero)
        {
            isZero = false;
            SpeedColors += NextSpeed;
            IndexColors = 0;
            ColorBarrier barrier = FindObjectOfType<ColorBarrier>();
            barrier.PlayAnimation(IndexColors, SpeedColors);
        }
//our score in lose icon 
        finalTextScore.text = "x" + score;
        scoreText.GetComponent<Text>().text = "x" + score;
//we move camera and respond to creating characters and circles that behind camera
        if (isRound) RoundPositionCamera();

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
                isRound = true;
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

    private void RoundPositionCamera()
    {
        isRound = false;
        _camera.transform.position = new Vector3(Convert.ToInt32(_camera.transform.localPosition.x), _camera.transform.localPosition.y, -10f);
    }

    private void PlayAimationFire(bool isActive, bool isDestroy = false)
    {
        //start the animation of the fire and destruction of it 
        GameObject fire = GameObject.FindGameObjectWithTag("Fire");
        if (fire == null) return;
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

    public void ReturnIsDeadTrue() => Enemy.isDead = true;
}
