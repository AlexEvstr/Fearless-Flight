using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    public int width;   // Ширина уровня
    public int height;  // Высота уровня
    public Vector2 playerStartPosition;  // Начальная позиция игрока
    public RoomData[] rooms;  // Массив комнат, каждая комната хранит свою конфигурацию
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
