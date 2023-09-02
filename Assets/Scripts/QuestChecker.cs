using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChecked : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().silverCoinsCollected >= questGoal)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
            }
            else
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(false);
                unfinishedText.SetActive(true);  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
        }
    }
}
