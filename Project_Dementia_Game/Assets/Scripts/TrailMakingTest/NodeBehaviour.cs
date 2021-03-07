using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private float timer = 0;
    private float blinkSpeed = 0.5f;

    [SerializeField]
    private AudioClip blinkSound;
    private AudioSource audioSource;

    public bool isBlinking = false;

    private bool isBlinked = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = blinkSound;
    }

    public void StartBlinking()
    {
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking) {
            timer += Time.deltaTime;
            if (isBlinked && timer > blinkSpeed)
            {
                timer = 0;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
                audioSource.Play();
                isBlinked = false;
            }
            else if(timer > blinkSpeed)
            {
                timer = 0;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                isBlinked = true;
            }
        }
            
    }
}
