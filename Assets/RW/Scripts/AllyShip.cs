using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShip : MonoBehaviour
{
    public bool isDead = false;
    public float speed = 5;
    public bool canShoot = true;

    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Transform shotSpawn;

    private float maxLeft = -8;
    private float maxRight = 8;
    private float maxTop = 5;
    private float maxBottom = -3.5f;

    //Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            ShootLaser();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    boostLeft();
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    boostRight();

        //}


        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    MoveUp();
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    MoveDown();
        //}

        //checking();

    }

    public void ShootLaser()
    {
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject laserShot = SpawnLaser();
        laserShot.transform.position = shotSpawn.position;
        yield return new WaitForSeconds(0.4f);
        canShoot = true;
    }

    public GameObject SpawnLaser()
    {
        GameObject newLaser = Instantiate(laser);
        newLaser.SetActive(true);
        return newLaser;
    }

    public void MoveLeft()
    {
        transform.Translate(-Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < maxLeft)
        {
            transform.position = new Vector3(maxRight, transform.position.y, 0);
        }
    }

    public void MoveRight()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * speed);
        if (transform.position.x > maxRight)
        {
            transform.position = new Vector3(maxLeft, transform.position.y, 0);
        }
    }

    //public void MoveUp()
    //{
    //    transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    //    if (transform.position.y > maxTop)
    //    {
    //        transform.position = new Vector3(transform.position.x, maxTop, 0);
    //    }
    //}

    //public void MoveDown()
    //{
    //    transform.Translate(Vector3.forward * Time.deltaTime * speed);
    //    if (transform.position.y < maxBottom)
    //    {
    //        transform.position = new Vector3(transform.position.x, maxBottom, 0);
    //    }
    //}

    public void Explode()
    {
        mesh.enabled = false;
        explosion.SetActive(true);
        isDead = true;
    }

    public void RepairShip()
    {
        explosion.SetActive(false);
        mesh.enabled = true;
        isDead = false;
    }


    //public void boostRight()
    //{
    //    transform.Translate(-Vector3.right * Time.deltaTime * speed * 2);
    //    if (transform.position.x > maxRight)
    //    {
    //        transform.position = new Vector3(maxLeft, transform.position.y, 0);
    //    }
    //}

    //public void boostLeft()
    //{
    //    transform.Translate(-Vector3.left * Time.deltaTime * speed * 2);
    //    if (transform.position.x < maxLeft)
    //    {
    //        transform.position = new Vector3(maxRight, transform.position.y, 0);
    //    }
    //}
    public void resetPos()
    {
        transform.position = new Vector3(0, -3.22f, 0);
    }

    //void checking()
    //{
    //    if (screenPos.y < 0) Debug.Log("Below screen");
    //    else if (screenPos.y > Screen.height) Debug.Log("Above screen");

    //    if (screenPos.x < 0) Debug.Log("Left of screen");
    //    else if (screenPos.x > Screen.width) Debug.Log("Right of screen");
    //}




}
