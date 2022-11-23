using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;


    public void Play()
    {
        SceneManager.LoadScene("Barcos Locos");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void Volume(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void Graphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }
}
