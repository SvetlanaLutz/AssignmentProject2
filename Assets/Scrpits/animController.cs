using System.Collections;
using UnityEngine;

public class animController : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0));
        {
            anim.Play("PauseMenu_anim");
        } 
    }
}
