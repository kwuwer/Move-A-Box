// OHOHO... MAGIA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Referencje do rigidbody, kamery, deklaracje zmiennych
    public Rigidbody rb;
    public GameObject playerCamera;
    // Offset służy nam do ustawienia kamery za graczem w pozycji 'fixed'
    private Vector3 offset; 
    // Siły działające na gracza
    private float forwardForce;
    private float maxForce;
    private float sideForce;

    // Czy przegraliśmy? Czy wygraliśmy?
    // Ekstra zmienne, które sprawdzają czy już coś się wykonało
    public  bool lostLife;
    public  bool finishedLevel;
    private bool soundOn;
    private bool hitTar;
    private bool addedExtraLife;
    private bool enabledGodMode;


    // Sprawdzamy czy ruszamy się w lewo czy prawo
    private bool leftSide;
    private bool rightSide;

    // INIT
    void Start () {
        offset = transform.position - playerCamera.transform.position;
        Debug.Log(GameManager.Instance.difficulty);
        forwardForce = GameManager.Instance.fwSpeed;
        maxForce = GameManager.Instance.maxforce;
        sideForce = GameManager.Instance.sideSpeed;
        lostLife = false;
        soundOn = false;
        finishedLevel = false;
        hitTar = false;
        addedExtraLife = false;
        enabledGodMode = false;
	}

    /* Primo  : sprawdzamy, czy mamy się poruszać w lewo czy w prawo
     *          dzieje się to w update, bo to jest co klatkę odpalane
     * Secundo: Sprawdzamy czy pozycja na osi Z jest ponad 200 (wygrana)
     *          jeśli na osi Y jest poniżej 0 to przyjmujemy że gracz spadł
     */
    private void Update()
    {
        if (Input.GetKey("a") || Input.GetKey("left")){
            leftSide = true;
            rightSide = false;
            return;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            leftSide = false;
            rightSide = true;
            return;
        }
        leftSide = false;
        rightSide = false;

        if (transform.position.z >= 200){
            finishedLevel = true;
        }
        if (transform.position.y <0.0f){
            lostLife = true;
        }
    }

    /*  FixedUpdate to preferowana funkcja przez Unity do wykonywania wszelkich
     *  dziwactw z fizyką obiektów. Dlatego w update zbieramy gdzie się mamy ruszać,
     *  a odpowiednia siła zostaje dodana tutaj.
     */
    void FixedUpdate () {
        if (forwardForce != maxForce) {
            if (leftSide)
            rb.AddForce(-sideForce, 0, 0, ForceMode.VelocityChange);
        else if (rightSide)
            rb.AddForce(sideForce, 0, 0, ForceMode.VelocityChange);
            rb.AddForce(0, 0, forwardForce);
        }else{
            rb.AddForce(0, 0, maxForce);
        }
            
	}

    /* Kamerę za graczem ustawiamy w LateUpdate, kiedy to już wszystkie obliczenia
     * per klatka zostały zrobione, obiekty rozmieszczone itp. itd. 
     * 
     * Na samym końcu przesuwamy kamerę tak, aby jej odległość w przestrzeni była 
     * równa offsetowi.
     */
    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position - offset;
    }

    // Collision!
    private void OnCollisionEnter(Collision collision)
    {
        if (enabledGodMode == false)
        {
            if (collision.gameObject.CompareTag("Obstacle") == true)
            {
                if (GameManager.Instance.extraLifes > 0)
                {
                    collision.gameObject.SetActive(false);
                    GameManager.Instance.extraLifes -= 1;
                }
                else
                {
                    Debug.Log("Collision!");
                    if (soundOn == false)
                    {
                        var audioSource = GetComponent<AudioSource>();
                        audioSource.Play();
                    }
                    forwardForce = 0;
                    rb.AddForce(0, 0, forwardForce);
                    lostLife = true;
                    soundOn = true;
                }
            }
        }
    }

    // Trigger - różni się o tyle, że jest aktywowany, a na obiekt nie działa
    // fizyka
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tar") == true)
        {
            if (hitTar == false)
            {
                sideForce = sideForce / 4;
                other.gameObject.SetActive(false);
                hitTar = true;
            }
        }
        if (other.gameObject.CompareTag("ExtraLife") == true)
        {
            if (addedExtraLife == false)
            {
                GameManager.Instance.extraLifes += 1;
                other.gameObject.SetActive(false);
                addedExtraLife = true;
            }
        }
        if (other.gameObject.CompareTag("Immortal") == true)
        {
            if (enabledGodMode == false)
            {
                enabledGodMode = true;
                other.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        addedExtraLife = false;
    }
}

