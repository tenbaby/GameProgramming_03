using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField]
    Transform tfNoteAppear = null;

    TimingManager thetimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;

    private void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        thetimingManager = GetComponent<TimingManager>();
        theComboManager = FindObjectOfType<ComboManager>();
    }

    void Update()
    {
        if (GameManager.Instance.isStartGame)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / bpm)
            {
                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                t_note.transform.position = tfNoteAppear.position;
                t_note.SetActive(true);
                thetimingManager.boxNoteList.Add(t_note);
                currentTime -= 60d / bpm;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                thetimingManager.MissRecord();
                theEffectManager.JudgementEffect(4);
                theComboManager.ResetCombo();
            }

            thetimingManager.boxNoteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);

        }
    }

    public void RemoveNote()
    {
        GameManager.Instance.isStartGame = false;

        for (int i = 0; i < thetimingManager.boxNoteList.Count; i++)
        {
            thetimingManager.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(thetimingManager.boxNoteList[i]);
        }

        thetimingManager.boxNoteList.Clear();
    }
}
