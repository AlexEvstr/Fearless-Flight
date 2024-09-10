using UnityEngine;

public class Room
{
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;
    public bool hasGhost;    // Призрак в комнате
    public bool hasKey;      // Ключ в комнате
    public Vector2? ghostPosition; // Позиция призрака (null, если нет призрака)
    public Vector2? keyPosition;   // Позиция ключа (null, если нет ключа)

    // Конструктор для комнаты с возможностью задания позиции ключа и призрака
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

public class GameMap
{
    public Room[,] rooms;

    public GameMap()
    {
        rooms = new Room[7, 5];

        // Первый ряд (верхний)
        rooms[0, 2] = new Room(false, true, false, false, false, true, null, new Vector2(0,-2)); // C1 - только вниз

        // Второй ряд
        rooms[1, 2] = new Room(true, true, false, false); // C2 - вниз и вверх

        // Третий ряд (центральный, содержит 5 комнат)
        rooms[2, 0] = new Room(false, false, false, true, true, false, new Vector2(-2,0), null); // C3 - только вправо
        rooms[2, 1] = new Room(false, false, true, true);  // C4 - влево и вправо
        rooms[2, 2] = new Room(true, true, true, true);    // C5 - вверх, вниз, влево, вправо + ключ
        rooms[2, 3] = new Room(false, false, true, true);  // C6 - влево и вправо
        rooms[2, 4] = new Room(false, false, true, false); // C7 - только влево

        // Четвертый ряд
        rooms[3, 2] = new Room(true, true, false, false);  // C8 - вверх и вниз

        // Пятый ряд (нижний)
        rooms[4, 2] = new Room(true, true, false, true);  // C9 - вверх, вниз и вправо + призрак
        rooms[4, 3] = new Room(false, false, true, true);  // C10 - влево и вправо
        rooms[4, 4] = new Room(false, false, true, false); // C11 - только влево

        // Шестой ряд
        rooms[5, 1] = new Room(false, true, false, true);  // C12 - вниз и вправо
        rooms[5, 2] = new Room(true, false, true, false);   // C13 - вверх и влево

        // Седьмой ряд
        rooms[6, 1] = new Room(true, false, false, false, true, false, new Vector2(0, -2), null); // C14 - только вверх
    }

    // Метод для получения комнаты
    public Room GetRoom(int x, int y)
    {
        return rooms[x, y];
    }
}