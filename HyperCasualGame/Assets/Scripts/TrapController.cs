using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private bool isHorizontal;
    [SerializeField] private Material idleYellowMat;
    [SerializeField] private Material activeYellowMat;
    [SerializeField] private Material idleBlackMat;
    [SerializeField] private Material activeBlackMat;
    [SerializeField] private Renderer renderer;
    [SerializeField] private BallController.ColorType colorType = BallController.ColorType.Yellow;

    private Animator animator;
    private Rigidbody rigidbody;
    private Collider collider;
    
    public BallController.ColorType ColorType { get { return colorType; } }

    // Start is called before the first frame update

    public void Init()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        renderer.material = colorType == BallController.ColorType.Yellow ? (isActive ? activeYellowMat : idleYellowMat) : (isActive ? activeBlackMat : idleBlackMat);
    }

    public void ActivateTrap()
    {
        SetActive(false);
        if (isHorizontal)
            animator.Play("push_trap_anim_horizontal");
        else
            animator.Play("push_trap_anim");
    }

    public void FallDownEvent()
    {
        gameObject.SetActive(false);
    }
}
