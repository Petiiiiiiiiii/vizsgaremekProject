using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int width = 10; //"grid weight"
    public int height = 10; //"grid height"
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public int numberOfRooms = 5;
    public int maxLootPerRoom = 5;

    private int[,] dungeonMap;       // "grid"
    private List<Vector2> roomCenters = new List<Vector2>(); // szobak kozepenek pozicioja

    void Start()
    {
        dungeonMap = new int[width, height];
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // 1 = fal, 0 = padló
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                dungeonMap[x, y] = 1; //alapbol minden fal
            }
        }

        // rooms dolgok
        for (int i = 0; i < numberOfRooms; i++)
        {
            int roomWidth = Random.Range(3, 6);
            int roomHeight = Random.Range(3, 6);
            int xPos = Random.Range(1, width - roomWidth - 1);
            int yPos = Random.Range(1, height - roomHeight - 1);

            // szoba kozepenek pozicioja
            roomCenters.Add(new Vector2(xPos + roomWidth / 2, yPos + roomHeight / 2));

            // szobak bejegyzese a gridbe (0 = padlo)
            for (int x = xPos; x < xPos + roomWidth; x++)
            {
                for (int y = yPos; y < yPos + roomHeight; y++)
                {
                    dungeonMap[x, y] = 0;
                }
            }
        }

        for (int i = 0; i < roomCenters.Count - 1; i++)
        {
            Vector2 roomA = roomCenters[i];
            Vector2 roomB = roomCenters[i + 1];

            CreateTunnel(roomA, roomB);
        }
        DrawDungeon();
    }


    // szobakat osszekoto folyoso
    void CreateTunnel(Vector2 roomA, Vector2 roomB)
    {
        int x1 = (int)roomA.x;
        int y1 = (int)roomA.y;
        int x2 = (int)roomB.x;
        int y2 = (int)roomB.y;


        for (int x = Mathf.Min(x1, x2); x <= Mathf.Max(x1, x2); x++)
        {
            dungeonMap[x, y1] = 0; //vizszintesen
        }
        // Majd függõlegesen
        for (int y = Mathf.Min(y1, y2); y <= Mathf.Max(y1, y2); y++)
        {
            dungeonMap[x2, y] = 0; //fuggolegesen
        }
    }
    void DrawDungeon()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * 2, 0, y * 2);

                if (dungeonMap[x, y] == 1)
                {
                    if (IsAdjacentToFloor(x, y))
                    {
                        //fal
                        Instantiate(wallPrefab, position, GetWallRotation(x, y));
                    }
                }
                else
                {
                    //padlo
                    Instantiate(floorPrefab, position, Quaternion.identity);
                    position.y += 2;
                    //teto
                    Instantiate(floorPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    bool IsAdjacentToFloor(int x, int y)
    {
        // Vizsgáljuk meg az aktuális cella körüli nyolc szomszédos cellát
        for (int offsetX = -1; offsetX <= 1; offsetX++)
        {
            for (int offsetY = -1; offsetY <= 1; offsetY++)
            {
                // aktualis cella kihagyasa
                if (offsetX == 0 && offsetY == 0) continue;

                //szomszed cella koordinatai
                int neighborX = x + offsetX;
                int neighborY = y + offsetY; 

                // rajta van e a terkepen
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    if (dungeonMap[neighborX, neighborY] == 0) //padlo-e
                    {
                        return true; //van padlo a szomszedba
                    }
                }
            }
        }

        return false; //nincs padlo a szomszedba
    }


    Quaternion GetWallRotation(int x, int y)
    {
        //padlok
        bool hasLeftFloor = IsAdjacentToFloor(x - 1, y);
        bool hasRightFloor = IsAdjacentToFloor(x + 1, y);
        bool hasTopFloor = IsAdjacentToFloor(x, y + 1);
        bool hasBottomFloor = IsAdjacentToFloor(x, y - 1);

        if (hasLeftFloor && hasRightFloor)
        {
            return Quaternion.Euler(0, 90, 0); // Vízszintes fal
        }

        if (hasTopFloor && hasBottomFloor)
        {
            return Quaternion.Euler(0, 0, 0); // Függõleges fal
        }

        return Quaternion.identity;
    }
}
