using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public AudioSource audioSource;
    private float noteHeight;
    private float noteWidth;
    public Note notePrefab;
    private Vector3 noteLocalScale;
    public Transform lastSpawnedPoint;

    private void SetDataForNotes()
    {
        Vector3 topRight = new Vector3(Screen.width, Screen.height, 0);
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
    }

    public void NoteSpawners()
    {
        float noteSpawnStartPosY = lastSpawnedPoint.position.y + noteHeight;
        Note note = null;
        float end_time = audioSource.clip.length - audioSource.time;
        int noteToSpawn = 
    }
}
