using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public GameObject BallPrefab;
    public GameObject LinePrefab;

    public Transform Tip;

    GameObject ball;
    GameObject LineDrawer;
    //public Transform nozzle;

    public void Start()
    {
        Instance = this;
        ball = Instantiate(BallPrefab, Tip.position, Quaternion.identity);
        ball.transform.rotation = Tip.rotation;
        LineDrawer = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
        LineDrawer.GetComponent<LineFollow>().Ball = ball.transform;


        //ball.GetComponent<Throwable>().throwVector = -this.gameObject.transform.localPosition.normalized * 100;
        //ball.GetComponent<Throwable>().Throw();
    }


    public void StartLaser()
    {
        Vector3 direction = -this.gameObject.transform.localPosition.normalized;
        Quaternion rotation = this.gameObject.transform.rotation;
        Vector3 rotatedDirection = rotation * direction;
        // Is it because the Z value here is still be considered??
        ball.GetComponent<Throwable>().throwVector = rotatedDirection * ball.GetComponent<Throwable>().speed;
        ball.GetComponent<Throwable>().Throw();
        LineDrawer.GetComponent<LineRenderer>().startWidth = 0.05f;
        LineDrawer.GetComponent<LineRenderer>().endWidth = 0.05f;
    }

}
