using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    [Header("References")]
    public AudioSource audioSource;
    public Note notePrefab;
    public Transform lastSpawnedPoint;
    public Transform NoteContainer;

    [Header("Variables")]
    private float noteSpawnStartPosX;
    private Vector3 noteLocalScale;
    public bool lastSpawn = false;
    private int NumberOfNotes = 20;
    int prevRandomNoteIndex = -1;
    private float noteHeight;
    private float noteWidth;
    private int currNoteId = 1;

    private void Start()
    {
        SetDataForNotes();
        NoteSpawners();
    }


    private void SetDataForNotes()
    {
        var topRight = new Vector3(Screen.width, Screen.height, 0);
        var topRightWorldPoint = Camera.main.ScreenToWorldPoint(topRight);
        var bottomLeftWorldPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);

        var screenWidth = topRightWorldPoint.x - bottomLeftWorldPoint.x;
        var screenHeight = topRightWorldPoint.y - bottomLeftWorldPoint.y;
        var noteSpriteRender = notePrefab.GetComponent<SpriteRenderer>();

        noteHeight = screenHeight / 4;
        noteWidth = screenWidth / 4;

        noteLocalScale = new Vector3(
            noteWidth / noteSpriteRender.bounds.size.x * noteSpriteRender.transform.localScale.x,
            noteHeight / noteSpriteRender.bounds.size.y * noteSpriteRender.transform.localScale.y,
            1);
        var leftMostPoint = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        var leftMostPointPivot = leftMostPoint.x + noteWidth / 2;
        noteSpawnStartPosX = leftMostPointPivot;
    }

    public void NoteSpawners()
    {

        if (lastSpawn) { return; }

        float noteSpawnStartPosY = lastSpawnedPoint.position.y + noteHeight;
        Note note = null;
        float end_time = audioSource.clip.length - audioSource.time;

        int noteToSpawn = NumberOfNotes;

        if (end_time < NumberOfNotes)
        {
            noteToSpawn = Mathf.CeilToInt(end_time);
            lastSpawn = true;
        }
        for (int i = 0; i < noteToSpawn; i++)
        {
            var randomNotesIndex = GetRandomNotesIndex();
            for (int j = 0; j < 4; j++)
            {
                note = Instantiate(notePrefab, NoteContainer);
                note.transform.localScale = noteLocalScale;
                note.transform.position = new Vector2(noteSpawnStartPosX + noteWidth * j, noteSpawnStartPosY);
                note.isVisible = (j == randomNotesIndex);
                if(note.isVisible)
                {
                    note.ID = currNoteId;
                    currNoteId++;
                }
            }
            noteSpawnStartPosY += noteHeight;
            if (i == NumberOfNotes - 1) lastSpawnedPoint = note.transform;
        }
    }

    private int GetRandomNotesIndex()
    {
        var randomNoteIndex = Random.Range(0, 4);
        while (randomNoteIndex == prevRandomNoteIndex) randomNoteIndex = Random.Range(0, 4);
        prevRandomNoteIndex = randomNoteIndex;

        return randomNoteIndex;
    }
}
