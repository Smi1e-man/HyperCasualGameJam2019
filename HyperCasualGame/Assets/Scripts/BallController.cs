using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //private visual values
    [SerializeField] private Animator animator;
    [SerializeField] private Material yellowMat;
    [SerializeField] private Material blackMat;
    [SerializeField] public ColorType colorType = ColorType.Yellow;

    [SerializeField] private SwitchNode currentGoal;

    //public values
    public enum VectorType
    {
        Left,
        Right,
        Forward,
        Backward,
        Finish,
        SwitchScreen
    }

    public enum ColorType
    {
        Yellow,
        Black
    }

    public Action FallDown;
    public Action GameOverEvent;
    public BallLineController ballLineController;
    public SwitchNode Goal { get { return currentGoal; } }
    public bool Wait { get { return !move; } }

    //private values
    private const float SPEED = 5f;
    private bool move;
    private Transform targetBall;
    private Vector3 currentMoveVector;
    private VectorType currentVectorType;

    /// <summary>
    /// Private Methods.
    /// </summary>
    private void Start()
    {
        StartMove();
        currentMoveVector = new Vector3(0, 0, 1);
        currentVectorType = VectorType.Forward;
    }

    private bool CheckTakeGoal()
    {
        switch (currentVectorType)
        {
            case VectorType.Forward:
                return transform.position.z > currentGoal.GoalPos.z;
                break;
            case VectorType.Backward:
                return transform.position.z < currentGoal.GoalPos.z;
                break;
            case VectorType.Right:
                return transform.position.x > currentGoal.GoalPos.x;
                break;
            case VectorType.Left:
                return transform.position.x < currentGoal.GoalPos.x;
                break;
        }
        return false;
    }

    private void UpdateGoal(SwitchNode rotate)
    {
        currentMoveVector = (rotate.NextGoal.GoalPos - rotate.GoalPos).normalized;
        currentGoal = rotate.NextGoal;
    }

    private void OnTriggerEnter(Collider other)
    {
        DropDown(other);
    }

    private void Finish()
    {
        if (GameOverEvent != null)
            GameOverEvent();
        MessageController.Instance.GameOverEvent();
        ballLineController.Stop();
    }

    private void DropDown(Collider other)
    {
        if (colorType == other.GetComponent<TrapController>().ColorType)
        {
            StopMove();
            ballLineController.Death(this);
            if (currentMoveVector.z == 0)
                animator.Play("ball_fall_horizontal");
            else
                animator.Play("ball_fall");
        }
    }

    private void UpdatePosition(BallController previousBall = null)
    {
        if (move)
        {
            if (previousBall != null)
            {
                if (previousBall.Goal == currentGoal)
                {
                    if (Vector3.Distance(previousBall.transform.position, transform.position) < 1.25f)
                        switch (currentVectorType)
                        {
                            case VectorType.Forward:
                                transform.position = previousBall.transform.position + Vector3.forward * 1.25f;
                                break;
                            case VectorType.Backward:
                                transform.position = previousBall.transform.position + Vector3.back * 1.25f;
                                break;
                            case VectorType.Right:
                                transform.position = previousBall.transform.position + Vector3.right * 1.25f;
                                break;
                            case VectorType.Left:
                                transform.position = previousBall.transform.position + Vector3.left * 1.25f;
                                break;
                        }
                    else
                        return;
                }
                else
                {
                    if (Vector3.Distance(previousBall.transform.position, transform.position) < 1.25f)
                        transform.position += currentMoveVector * SPEED * Time.deltaTime;
                }
            }
            else
            {
                transform.position += currentMoveVector * SPEED * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Public Methods.
    /// </summary>
    public void PaintBall()
    {
        GetComponent<Renderer>().material = colorType == ColorType.Black ? blackMat : yellowMat;
    }

    public void StartMove()
    {
        move = true;
    }

    public void UpdateBall( BallController previousBall = null)
    {
        if (!isActiveAndEnabled) return;

        UpdatePosition(previousBall);

        if (CheckTakeGoal())
        {
            Switch(currentGoal);
        }
    }

    public void StopMove()
    {
        move = false;
    }

    public void Switch(SwitchNode switcher)
    {
        //RotateVector
        transform.position = switcher.GoalPos;
        switch (switcher.VectorType)
        {
            case VectorType.Finish: Finish(); break;
            case VectorType.SwitchScreen: switcher.SwitchCam(); UpdateGoal(switcher);
                currentVectorType = VectorType.Forward;
                break;
            case VectorType.Backward:
            case VectorType.Forward:
            case VectorType.Left:
            case VectorType.Right:
                currentVectorType = switcher.VectorType;
                UpdateGoal(switcher);
                break;
        }
    }

    public void FallDownEvent()
    {
        gameObject.SetActive(false);
        if (FallDown != null)
            FallDown();
    }
}
