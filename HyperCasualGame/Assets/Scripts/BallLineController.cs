using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BallLineController : MonoBehaviour
{
    public Action WinAction;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private float distanceBetween;
    [SerializeField] private int numbers;

    [ContextMenu("CreateBalls")]
    void SetUpBalls()
    {
#if UNITY_EDITOR
        ballsLine.Clear();
        for (int j = 0; j < numbers; j++)
        {
            GameObject newBall = PrefabUtility.InstantiatePrefab(ballPrefab as GameObject) as GameObject;
            newBall.transform.position = startPoint - new Vector3(0, 0, 1) * distanceBetween * j;
            newBall.transform.SetParent(transform);
            newBall.GetComponent<BallController>().colorType = UnityEngine.Random.Range(0, 2) == 0 ? BallController.ColorType.Yellow : BallController.ColorType.Black;
            ballsLine.Add(newBall.GetComponent<BallController>());
        }
#endif
    }

    [ContextMenu("PaintBalls")]
    private void PaintBalls()
    {
        for (int i = 0; i < ballsLine.Count; i++)
        {
            BallController ball = ballsLine[i];
            ball.PaintBall();
        }
    }


    [SerializeField] private List<BallController> ballsLine = new List<BallController>();

    [SerializeField] private List<BallController> waitBalls;

    private int currentNumbers;

    public void Death(BallController ball)
    {
        currentNumbers--;
        ballsLine.Remove(ball);
        ProgressController.Instance.ChangeValue(1 - (float)(currentNumbers) / (float)(numbers));
    }

    private void Awake()
    {
        for (int i = 0; i < ballsLine.Count; i++)
        {
            BallController ball = ballsLine[i];
            ball.ballLineController = this;
            ball.FallDown += OnBallFall;
        }
        currentNumbers = numbers;
    }

    public void Stop()
    {
        foreach (var ball in ballsLine) { ball.StopMove(); }
    }
    
    public void StartPartLine()
    {
        foreach (var ball in waitBalls)
        {
            ball.StartMove();
        }
    }

    private void OnBallFall()
    {
        foreach (var ball in ballsLine)
        {
            if (ball.gameObject.activeInHierarchy) { return; }
        }

        if (WinAction != null)
        {
            WinAction();
        }
        MessageController.Instance.GameWinEvent();
    }

    private void Update()
    {
        if (ballsLine.Count > 0)
        {
            ballsLine[ballsLine.Count - 1].UpdateBall();
            if (ballsLine.Count > 1)
                for (int i = ballsLine.Count - 2; i > -1; i--)
                {
                    ballsLine[i].UpdateBall(ballsLine[i + 1]);
                }
        }
    }
}
