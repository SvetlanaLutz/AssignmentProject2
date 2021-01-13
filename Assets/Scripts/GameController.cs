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

    public static int scoreHeart;
    public static bool isFirstScore = true;
    public static bool isTakeOneHeartAway = false;
    private Camera _camera => Camera.main;

    private bool isTraffic = true;
    private bool isInstantiate = false;
    private bool isNormalize = false;

    private float nextPosition = 10f;
    private float target = 0;
    public float force = 0;
    public GameObject scoreText => GameObject.FindGameObjectWithTag("Score");
    public GameObject CharacterBotPrefab;
    public GameObject ColorsPrefab;

    public static int score = 0;

    private void Awake()
    {  
        GameObject[] heartObject = GameObject.FindGameObjectsWithTag("Heart");
        for (int i = 0; i < heartObject.Length; i++) heart[i] = heartObject[i].GetComponent<Image>();

        for (int i = 0; i < heart.Length; i++) heart[i].sprite = heartSprite[0];
        scoreHeart = heart.Length;
    }

    private void Update()
    {
        finalTextScore.text = "Score: " + score;

        scoreText.GetComponent<Text>().text = "x" + score;

        if (isNormalize) Normalize();

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
                isNormalize = true;
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
            Invoke(nameof(MoveToFire), 1f);
            Invoke(nameof(DestroyFire), 1.5f);
            Invoke(nameof(ScorePlus), 2f);
            Invoke(nameof(MoveToFireBack), 3f);
            Invoke(nameof(ReturnIsDeadTrue), 5f);
        }
    }

    public void MinusOneHeart(int index)
    {
        isTakeOneHeartAway = false;
        isFirstScore = false;

        for (int i = index; i > 1; i--) heart[i].sprite = heartSprite[0];
        heart[index].sprite = heartSprite[1];
    }

    private void Normalize()
    {
        isNormalize = false;
        _camera.transform.position = new Vector3(Convert.ToInt32(_camera.transform.localPosition.x), _camera.transform.localPosition.y, -10f);
    }

    private void PlayAimationCharacter(string animationName)
    {
        GameObject character = GameObject.FindGameObjectWithTag("Character");
        Animation characterAnimation = character.GetComponent<Animation>();
        characterAnimation.clip = characterAnimation[animationName].clip;
        characterAnimation.Play();
    }

    private void ScorePlus() => score++;
    private void MoveToFire() => PlayAimationCharacter("MoveToFire");
    private void MoveToFireBack() => PlayAimationCharacter("MoveToFireBack");
    private void DestroyFire() => Destroy(GameObject.FindGameObjectWithTag("Fire"), 1.5f);
    private void ReturnIsDeadTrue() => Enemy.isDead = true;
}
