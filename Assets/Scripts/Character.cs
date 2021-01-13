using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Character : MonoBehaviour
{
    [SerializeField] DescriptionColor[] descriptionColors;

    [SerializeField] AnimationClip[] animationClip;
    private Animation _animation => GetComponent<Animation>();

    public static List<string> descriptionName = new List<string>();
    public static List<Color> descriptionColor = new List<Color>();

    public static bool isAlpha = true;
    public static string nameColor;
    public static Color color;

    [Space(5)] public float ReturnTime = 1f;

    private void Awake()
    {
        SpriteRenderer _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
        }
    }

    private void PlayAnimation(int index)
    {
        _animation.clip = animationClip[index];
        _animation.Play();
    }

    private IEnumerator PlayDie(Collision2D collision, float returnTime)
    {
        yield return new WaitForSeconds(returnTime);
        PlayAnimation(0);
        collision.gameObject.GetComponent<Animation>().Play();
    }
}
