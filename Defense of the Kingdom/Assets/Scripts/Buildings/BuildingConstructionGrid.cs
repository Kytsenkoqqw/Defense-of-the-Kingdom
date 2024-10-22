using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingConstructionGrid : EditorWindow
{
    private Tilemap selectedTilemap;
    private TileBase allowedTile; // Тайл для "разрешённой" зоны
    private TileBase restrictedTile; // Тайл для "запрещённой" зоны
    private bool isDrawingAllowed = true; // Определяет, рисуем ли мы "разрешённые" или "запрещённые" зоны

    [MenuItem("Window/Building Zone Editor")]
    public static void ShowWindow()
    {
        GetWindow<BuildingConstructionGrid>("Building Zone Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Building Zone Editor", EditorStyles.boldLabel);

        // Выбор Tilemap
        selectedTilemap = (Tilemap)EditorGUILayout.ObjectField("Tilemap", selectedTilemap, typeof(Tilemap), true);

        // Выбор Tile для разрешённой зоны
        allowedTile = (TileBase)EditorGUILayout.ObjectField("Allowed Tile", allowedTile, typeof(TileBase), false);

        // Выбор Tile для запрещённой зоны
        restrictedTile = (TileBase)EditorGUILayout.ObjectField("Restricted Tile", restrictedTile, typeof(TileBase), false);

        // Кнопка переключения между режимами рисования
        isDrawingAllowed = GUILayout.Toggle(isDrawingAllowed, "Draw Allowed Zones");

        if (selectedTilemap == null || allowedTile == null || restrictedTile == null)
        {
            EditorGUILayout.HelpBox("Please assign all fields before drawing!", MessageType.Warning);
        }
        else
        {
            // Инструкции
            GUILayout.Label("Hold Shift and click on the Tilemap to paint the zones.");

            // Обновляем сцену
            SceneView.duringSceneGui += OnSceneGUI;
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        // Проверяем, если нажата левая кнопка мыши и удерживается Shift
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && Event.current.shift)
        {
            Vector2 mousePosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3Int gridPosition = selectedTilemap.WorldToCell(hit.point);

                if (isDrawingAllowed)
                {
                    // Рисуем тайл для разрешённой зоны
                    selectedTilemap.SetTile(gridPosition, allowedTile);
                }
                else
                {
                    // Рисуем тайл для запрещённой зоны
                    selectedTilemap.SetTile(gridPosition, restrictedTile);
                }

                // Обновляем Tilemap
                Event.current.Use();
                EditorUtility.SetDirty(selectedTilemap);
            }
        }

        sceneView.Repaint();
    }

    private void OnDisable()
    {
        // Отписываемся от обновления сцены
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}
