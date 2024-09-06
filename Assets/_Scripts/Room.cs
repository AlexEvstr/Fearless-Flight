public class Room
{
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    // Конструктор для указания возможных направлений движения
    public Room(bool up, bool down, bool left, bool right)
    {
        canMoveUp = up;
        canMoveDown = down;
        canMoveLeft = left;
        canMoveRight = right;
    }
}

public class GameMap
{
    public Room[,] rooms;

    public GameMap()
    {
        rooms = new Room[7, 5];


        // Первый ряд (верхний)
        rooms[0, 2] = new Room(false, true, false, false); // C1 - только вниз

        // Второй ряд
        rooms[1, 2] = new Room(true, true, false, false); // C2 - вниз и вверх

        // Третий ряд (центральный, содержит 5 комнат)
        rooms[2, 0] = new Room(false, false, false, true); // C3 - только вправо
        rooms[2, 1] = new Room(false, false, true, true);  // C4 - влево и вправо
        rooms[2, 2] = new Room(true, true, true, true);    // C5 - вверх, вниз, влево, вправо
        rooms[2, 3] = new Room(false, false, true, true);  // C6 - влево и вправо
        rooms[2, 4] = new Room(false, false, true, false); // C7 - только влево

        // Четвертый ряд
        rooms[3, 2] = new Room(true, true, false, false);  // C8 - вверх и вниз

        // Пятый ряд (нижний)
        rooms[4, 2] = new Room(true, true, false, true);  // C9 - вверх, вниз и вправо
        rooms[4, 3] = new Room(false, false, true, true);  // C10 - влево и вправо
        rooms[4, 4] = new Room(false, false, true, false); // C11 - только влево

        // Шестой ряд
        rooms[5, 1] = new Room(false, true, false, true);  // C12 - вниз и вправо
        rooms[5, 2] = new Room(true, false, true, false);   // C13 - вверх и влево

        // Седьмой ряд
        rooms[6, 1] = new Room(true, false, false, false); // C14 - только вверх
    }

    // Метод для получения комнаты
    public Room GetRoom(int x, int y)
    {
        return rooms[x, y];
    }
}
