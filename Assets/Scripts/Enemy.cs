using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    //to make a character lose color and dissaper with arrow
    [SerializeField] DescriptionColor[] descriptionColors;
    [SerializeField] AnimationClip[] animationClip;
//we take a component and put it to the rebder sprite
    private SpriteRenderer _spriteRenderer => transform.GetChild(0).GetComponent<SpriteRenderer>();
    private Animation _animation => GetComponent<Animation>();
// we animate the dead colors to make it dissaper
    public AnimationClip deadColors;
    //we are looking for tag 
    public GameObject Lose => GameObject.FindGameObjectWithTag("Lose");
    public GameObject Colors;
    //we create prefab that will create a fire
    public GameObject FirePrefab;

    public static bool isMoveToFire = false;
    public static bool isDead = false;
    //herewe say about color data and with we refering to
    public static string nameColor;
//points
    [Space(5)] public float ReturnTime = 1f;

    private void Awake()
    //start animation and timeout
    {
        Colors = GameObject.FindGameObjectWithTag("Colors");
        Time.timeScale = 1f;
        Random rand = new Random();

        for (int i = 0; i < descriptionColors.Length; i++)
        {
            int index = rand.Next(0, descriptionColors.Length);

            if (nameColor == null)
            {
                _spriteRenderer.color = descriptionColors[index].color;
                nameColor = descriptionColors[index].name;
            }
        }
    }

//we check what will happen if we died we run the function after two seconds and call the lose function 
    private void Update() { if (GameController.scoreHeart == 0) Invoke(nameof(ViewLose), 2f); }

    private void OnCollisionEnter2D(Collision2D collision)
    //we check here if the colour of arrow its right or no 
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (Arrow.isColor == false)
            {
                Arrow.isColor = true;
                Bow.isArrow = true;

                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
// options of colour with can change
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
                        GameController.scoreHeart--;
                        GameController.isTakeOneHeartAway = true;
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
            GameObject target = GameObject.FindGameObjectWithTag("Target");
            fire.transform.parent = target.transform;
            fire.transform.position = new Vector3(0, 0, 0);
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
//clor fanding animation
        _animation.clip = deadColors;
        _animation.Play();

        isMoveToFire = true;
        //camera movement 
        PlayAnimation(0);
        collision.gameObject.GetComponent<Animation>().Play();
        Destroy(gameObject, 2f);
        Destroy(collision.gameObject, 1f);
        Destroy(_animation.gameObject, 1.5f);
    }

    public static void ReturnIsDeadTrue() => isDead = true;
}
