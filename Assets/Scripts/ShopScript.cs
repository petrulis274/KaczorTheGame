using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ShopScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    [SerializeField]  GameObject player;
    private bool playerInTrigger = false;
    public GameObject shopScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.B))
        {
            shopScreen.SetActive(!shopScreen.activeSelf);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    public void BuyDash()
    {
        if (ScoreManager.score >= 1)
        {
            ScoreManager.score -= 1;
            CharacterController2D.dashUnlocked = true;
            audioSource.PlayOneShot(audioClip);
            
        }
    }

    public void BuyDamage()
    {
        
    }

    public void BuyRange()
    {
        
    }
}
