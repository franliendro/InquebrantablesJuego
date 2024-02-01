using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dmgGolpe = 2f;
    [SerializeField] private float tiempoEspera=1f;
    [SerializeField] private float tiempoRestante;
    public string convertir;
    public TMP_Text vidaText;
    private Rigidbody2D rigidBody;
    private Animator animator;
    public float vida=20f;
    public bool perderMostrar;
    public GameObject cartelPerder;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        convertir = Convert.ToString(vida);
        vidaText.text = convertir;
    }

    // Update is called once per frame
    void Update()
    {
        convertir = Convert.ToString(Convert.ToInt32(vida));
        vidaText.text = convertir;
        tiempoRestante -= Time.deltaTime;
        
            if (Input.GetMouseButtonDown(0))
            {
                if (tiempoRestante <= 0)
                {
                    Golpe();
                    Debug.Log("Golpe");
                    tiempoRestante = tiempoEspera;
                    StartCoroutine(golpeEspera());
                }
                
            }
        if (vida <= 0&&!perderMostrar)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            perderMostrar = true;
            StartCoroutine(mostrarCartel());
        }

    }
    IEnumerator mostrarCartel()
    {
        
        yield return new WaitForSeconds(2);
        cartelPerder.SetActive(true);

    }
    IEnumerator golpeEspera()
    {
        rigidBody.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1); 
        

    }
    private void Golpe()
    {
        if (rigidBody.velocity.x == 0 && rigidBody.velocity.y == 0)
        {
            animator.SetTrigger("Golpe");
            attackSound.Play();
        }
        else
        {
            animator.SetTrigger("GolpeCorriendo");
        }
            
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Enemigo>().RecibirGolpe(dmgGolpe);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position,radioGolpe);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            vida -= Time.deltaTime;
            Debug.Log(vida);
            if (vida <= 0)
            {
                animator.SetBool("death", true);
            }
        }
        
    }
}
