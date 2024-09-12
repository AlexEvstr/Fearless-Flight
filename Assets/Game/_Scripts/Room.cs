using UnityEngine;

public class Room
{
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool hasGhost;
    public bool hasKey;
    public Vector2? ghostPosition;
    public Vector2? keyPosition;

    public Room(bool up, bool down, bool left, bool right, bool ghost = false, bool key = false, Vector2? ghostPos = null, Vector2? keyPos = null)
    {
        canMoveUp = up;
        canMoveDown = down;
        canMoveLeft = left;
        canMoveRight = right;
        hasGhost = ghost;
        hasKey = key;
        ghostPosition = ghostPos;
        keyPosition = keyPos;
    }
}