using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Ayn� GameObject �zerindeki Animator bile�enini al
        animator = GetComponent<Animator>();
    }

    public void SetRunning()
    {
        animator.SetBool("isRun", true);
        animator.SetBool("isIdle", false);
    }

    public void SetIdle()
    {
        animator.SetBool("isRun", false);
        animator.SetBool("isIdle", true);
    }
}
