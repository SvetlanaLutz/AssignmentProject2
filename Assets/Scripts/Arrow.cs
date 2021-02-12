using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//to grab the first colour with it tuch
public class Arrow : MonoBehaviour
{
    private bool isFirstColor = true;
    public static bool isColor = true;
//if we hit the barriers that we stop physics and create a new arrow 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Bow.isArrow = true;
            Bow.isMouseDown = false;
            GameController.isFirstScore = true;
            Destroy(gameObject, 0.3f);
        }

        if (collision.gameObject.CompareTag("Color"))
        {
            if(isFirstColor) ColorChange(collision);
        }
    }

    private void ColorChange(Collider2D collision)
    {
        isFirstColor = false;
        //if th color is correct or no 

        if (collision.gameObject.name == Enemy.nameColor) isColor = false;
        else isColor = true;
//colouring the color of the circle 
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = collision.GetComponent<SpriteRenderer>().color;
    }
//destroy th old arrow
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bow"))
        {
            Bow.isMouseDown = false;
            Bow.isArrow = true;
            Destroy(gameObject);
        }
    }
}
