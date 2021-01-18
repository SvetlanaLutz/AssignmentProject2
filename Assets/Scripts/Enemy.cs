using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] DescriptionColor[] descriptionColors;
    [SerializeField] AnimationClip[] animationClip;

    private SpriteRenderer _spriteRenderer => transform.GetChild(0).GetComponent<SpriteRenderer>();
    private Animation _animation => GetComponent<Animation>();

    public AnimationClip deadColors;
    public GameObject Lose => GameObject.FindGameObjectWithTag("Lose");
    public GameObject Colors;
    public GameObject FirePrefab;

    public static List<string> descriptionName = new List<string>();
    public static List<Color> descriptionColor = new List<Color>();

    public static bool isMoveToFire = false;
    public static bool isAlpha = true;
    public static bool isDead = false;
    public static string nameColor;
    public static Color color;

    [Space(5)] public float ReturnTime = 1f;

    private void Awake()
    {
        Colors = GameObject.FindGameObjectWithTag("Colors");

        Time.timeScale = 1f;
        Random rand = new Random();

        for (int i = 0; i < descriptionColors.Length; i++)
        {
            descriptionName.Add(descriptionColors[i].name);
            descriptionColor.Add(descriptionColors[i].color);

            int index = rand.Next(0, descriptionColors.Length);

            if (nameColor == null)
            {
                _spriteRenderer.color = descriptionColors[index].color;
                nameColor = descriptionColors[index].name;
                color = descriptionColors[index].color;
            }
        }
    }

    private void Update() { if (GameController.scoreHeart == 0) Invoke(nameof(ViewLose), 2f); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (Arrow.isColor == false)
            {
                Arrow.isColor = true;
                Bow.isArrow = true;
                isAlpha = false;

                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

                switch (nameColor)
                {
                    case "Green":
                        PlayAnimation(1);
                        break;
                    case "Purple":
                        PlayAnimation(2);
                        break;
                    case "Blue":
                        PlayAnimation(3);
                        break;
                }
                StartCoroutine(PlayDie(collision, ReturnTime));
            }
            else
            {
                if (GameController.isFirstScore)
                {
                    if (GameController.scoreHeart >= 0)
                    {
                        GameController.isTakeOneHeartAway = true;
                        GameController.scoreHeart--;
                    }
                }
            }
        }
    }

    private void ViewLose()
    {
        GameController.isGame = false;
        nameColor = null;
        Time.timeScale = 0;
        Colors.SetActive(false);
        Lose.transform.GetChild(0).transform.gameObject.SetActive(true);
    }

    private void PlayAnimation(int index)
    {
        if (index == 0)
        {
            Instantiate(FirePrefab, transform);
            GameObject fire = GameObject.FindGameObjectWithTag("Fire");
            fire.transform.parent = GameController.targetPosition.transform;
            fire.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        nameColor = null;
        _animation.clip = animationClip[index];
        _animation.Play();
    }

    private IEnumerator PlayDie(Collision2D collision, float returnTime)
    {
        yield return new WaitForSeconds(returnTime);

        GameObject colorsAnimation = GameObject.FindGameObjectWithTag("Colors");
        Animation _animation = colorsAnimation.GetComponent<Animation>();

        _animation.clip = deadColors;
        _animation.Play();

        isMoveToFire = true;

        PlayAnimation(0);
        collision.gameObject.GetComponent<Animation>().Play();
        Destroy(gameObject, 2f);
        Destroy(collision.gameObject, 1f);
        Destroy(_animation.gameObject, 1.5f);
    }

    public static void ReturnIsDeadTrue() => isDead = true;
}
