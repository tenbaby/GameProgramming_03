using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource goal;
    NoteManager theNote;

    Result theResult;

    // Start is called before the first frame update
    void Start()
    {
        goal = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goal.Play();
            PlayerController.s_canPresskey = false;
            theNote.RemoveNote();
            theResult.ShowResult();
        }
    }
}
