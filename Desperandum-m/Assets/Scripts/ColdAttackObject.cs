using UnityEngine;
using UnityEngine.Animations;

public class ColdAttackObject : MonoBehaviour
{
    public float duration = 30f;

    private float timer;
    private Animator animator;

    private void Start()
    {

        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        animator.SetFloat("despawnTime", timer);

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }


}

