using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    public int width;
    public int height;
    public Vector2 playerStartPosition;
    public RoomData[] rooms;
}

[System.Serializable]
public class RoomData
{
    public int roomX;
    public int roomY;
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool hasGhost;
    public bool hasKey;
    public Vector2 ghostPosition;
    public Vector2 keyPosition;
}