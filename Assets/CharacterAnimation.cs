using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;


    void Start()
    {
        animator = GameObject.Find("MainCharacter").GetComponent<Animator>();
    }

    public void SetRunning(bool running)
    {

        if (running == true)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", true);
        }
    }
}
