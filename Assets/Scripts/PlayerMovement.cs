using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{
    public float multiplier = 1;
    private float targetspeedX = 10f;
    private float targetspeedY = 10f;
    private float speedConstant;
    public float acceleration = 1f;
    public float deceleration = 1f;
    private float currentSpeedx = 0f;
    private float currentSpeedy = 0f;
    private float speedx, speedy;
    public float scale = 1;
    public float speedbonus = 1;
    public float fuelmod = 1;
    public float Speedtolerance = 0.1f;
    private Vector2 bouncepoint = Vector2.zero;
    private Vector2 playerposition = Vector2.zero;
    private Vector2 bounceboxposition = Vector2.zero;
    private bool FuelEmpty = false;
    public Transform bouncebox;
    public Transform PlayerSprite;
    public Transform DrillSprite;
    public GameObject playerpos;
    public GameObject Drill;
    public AudioManager audioManager;
    public Fuel fuel;

    Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        speedConstant = 10;

    }

    void Update()
    {
        if (Mathf.Abs(speedx) > 0 || Mathf.Abs(speedy) > 0)
        {
            fuel.takedamage(0.01f/fuelmod);
        }

        if (fuel.currenthealth == 0)
            FuelEmpty = true;
        else
            FuelEmpty = false;

        if (Input.GetKey(KeyCode.LeftShift) && fuel.currenthealth > 0)
        {
            fuel.takedamage(0.05f/ fuelmod);
            targetspeedX = 2 * speedConstant * multiplier * speedbonus;
            targetspeedY = 2 * speedConstant * multiplier * speedbonus;

            acceleration = 3 * speedbonus;
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            targetspeedX = speedConstant * multiplier;
            targetspeedY = speedConstant * multiplier;
            acceleration = 1 * speedbonus;
        }


        speedx = Input.GetAxisRaw("Horizontal");
        speedy = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(speedx) > 0)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                targetspeedX = 10 * speedbonus;
            
        }
        else
            targetspeedX = 0;


        if (Mathf.Abs(speedy) > 0)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                targetspeedY = 10 * speedbonus;
           
        }
        else
            targetspeedY = 0;


        if (Mathf.Abs(currentSpeedx) <= targetspeedX && speedx > 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX, acceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedx) <= targetspeedX && speedx < 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX * -1, acceleration * Time.deltaTime);
        }
        //X axis acceleration
        if (Mathf.Abs(currentSpeedx) >= targetspeedX && speedx >= 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX, deceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedx) >= targetspeedX && speedx < 0)
        {
            currentSpeedx = Mathf.Lerp(currentSpeedx, targetspeedX * -1, deceleration * Time.deltaTime);
        }
        //X axis deceleration

        if (Mathf.Abs(currentSpeedy) <= targetspeedY && speedy > 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY, acceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedy) <= targetspeedY && speedy < 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY * -1, acceleration * Time.deltaTime);
        }
        //Y axis acceleration
        if (Mathf.Abs(currentSpeedy) >= targetspeedY && speedy >= 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY, deceleration * Time.deltaTime);
        }
        else if (Mathf.Abs(currentSpeedy) >= targetspeedY && speedy < 0)
        {
            currentSpeedy = Mathf.Lerp(currentSpeedy, targetspeedY * -1, deceleration * Time.deltaTime);
        }
        //Y axis deceleration

        if (FuelEmpty)
        {
            if (Mathf.Abs(speedx) > 0 && Mathf.Abs(speedy) > 0)
                rb.velocity = new Vector2(currentSpeedx / 10*Mathf.Sqrt(2), currentSpeedy / 10*Mathf.Sqrt(2));
            else
                rb.velocity = new Vector2(currentSpeedx/10, currentSpeedy/10);
        }
        else
        {
            if (Mathf.Abs(speedx) > 0 && Mathf.Abs(speedy) > 0)
                rb.velocity = new Vector2(currentSpeedx / Mathf.Sqrt(2), currentSpeedy / Mathf.Sqrt(2));
            else
                rb.velocity = new Vector2(currentSpeedx, currentSpeedy);
        }

        if (currentSpeedx < -Speedtolerance) 
        {
            PlayerSprite.localScale = new Vector3 (-1,1,1);
            DrillSprite.localScale = new Vector3(-1, 1, 1);
        }
        else if(currentSpeedx > Speedtolerance)
        {
            PlayerSprite.localScale = new Vector3(1, 1, 1);
            DrillSprite.localScale = new Vector3(1, 1, 1);
        }

        if (Speedtolerance >= currentSpeedx && currentSpeedx >= -Speedtolerance) 
        {
            float Drillrotation = Drill.transform.rotation.z;
            if(0.9 >= Drillrotation &&  Drillrotation >= -0.9)
            {
                PlayerSprite.localScale = new Vector3(1, 1, 1);
                DrillSprite.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                PlayerSprite.localScale = new Vector3(-1, 1, 1);
                DrillSprite.localScale = new Vector3(-1, 1, 1);
            }
        }


    }
    void OnCollisionEnter2D(Collision2D col)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[col.contactCount];
        col.GetContacts(contacts);
        bouncepoint = new Vector2(currentSpeedx,currentSpeedy);
        bouncebox.localPosition = bouncepoint;
        playerposition = playerpos.transform.position;
        bounceboxposition = bouncebox.transform.position;
        Vector2 directionVector = bounceboxposition - playerposition;

        if (contacts.Length > 0)
        {
            Vector2 normal = contacts[0].normal;

          
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                directionVector.x = -1*directionVector.x;
                currentSpeedx = directionVector.x / 5;
            }
            else if (Mathf.Abs(normal.x) < Mathf.Abs(normal.y))
            {
                directionVector.y = -1*directionVector.y;
                currentSpeedy = directionVector.y / 5;
            }
     
        }
        
        
    }
}