using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildableTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase buildableTile;  // Зелёная плитка (можно строить)
    public TileBase unbuildableTile;  // Красная плитка (нельзя строить)

    private bool[,] buildableZones;

    // Размеры сетки
    public int gridWidth = 10;
    public int gridHeight = 10;

    private void Start()
    {
        // Инициализация массива
        buildableZones = new bool[gridWidth, gridHeight];
    }

    // Метод для обновления статуса зоны (в зависимости от выбранной кисти)
    public void SetBuildable(Vector3Int position, bool isBuildable)
    {
        if (isBuildable)
        {
            tilemap.SetTile(position, buildableTile);  // Устанавливаем зелёную плитку
        }
        else
        {
            tilemap.SetTile(position, unbuildableTile);  // Устанавливаем красную плитку
        }
    }
}
