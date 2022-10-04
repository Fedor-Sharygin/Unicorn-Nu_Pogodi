using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private string[] animationNames;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.speed = .5f;
    }

    public void ChangeAnimation(int animationIdx)
    {
        animator.Play(animationNames[animationIdx]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
