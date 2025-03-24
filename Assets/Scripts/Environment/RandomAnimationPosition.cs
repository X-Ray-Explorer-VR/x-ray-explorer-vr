using UnityEngine;
using Random = UnityEngine.Random;

public class RandomAnimationPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Animator starAnimation;

    private void Start()
    {
        starAnimation = GetComponent<Animator>();

        if (starAnimation)
        {
            starAnimation.Play("Floating Star", 0, Random.Range(0.0f, 1.0f));
        }
    }
}
