using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField passwordCheckerTextBox;

    // our sounds
    public AudioSource yupSound;
    public AudioSource nopeSound;

    public void CheckInputAndContinue()
    {
        passwordCheckerTextBox = GameObject.Find("PasswordTextBox").GetComponent<TMP_InputField>();

        if (passwordCheckerTextBox.text == "COMRADE")
        {
            yupSound.Play();
            SceneManager.LoadScene("Game");
        }
        else
        {
            nopeSound.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nopeSound = GameObject.Find("MissSound").GetComponent<AudioSource>();
        yupSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}