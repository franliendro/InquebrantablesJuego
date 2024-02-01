using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public bool funcion;
    public int segundos;
    // Start is called before the first frame update
    void Start()
    {
        if (funcion) {
            StartCoroutine(pasarCinematica());
        }
    }

    IEnumerator pasarCinematica()
    {
        yield return new WaitForSeconds(segundos);
        IniciarJuego();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void IniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void volverEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
