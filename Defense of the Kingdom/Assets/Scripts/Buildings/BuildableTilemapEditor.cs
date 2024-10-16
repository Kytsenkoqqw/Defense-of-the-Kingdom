using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildableTilemap))]
public class BuildableTilemapEditor : Editor
{
    private bool isPaintMode = true; // true - можно строить (зелёная кисть), false - нельзя строить (красная кисть)

    private void OnSceneGUI()
    {
        BuildableTilemap buildableTilemap = (BuildableTilemap)target;
        Event e = Event.current;

        // Обрабатываем клик мышью
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            // Получаем позицию мыши в мире
            Vector3 mousePosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;

            // Конвертируем позицию в координаты Tilemap
            Vector3Int cellPosition = buildableTilemap.tilemap.WorldToCell(mousePosition);

            // В зависимости от режима кисти, устанавливаем зелёную или красную клетку
            buildableTilemap.SetBuildable(cellPosition, isPaintMode);

            // Обновляем сцену
            e.Use();
            SceneView.RepaintAll();
        }
    }

    public override void OnInspectorGUI()
    {
        // Кнопки для выбора кисти
        GUILayout.Label("Выбор кисти");
        
        if (GUILayout.Button("Зелёная кисть (Можно строить)"))
        {
            isPaintMode = true;
        }

        if (GUILayout.Button("Красная кисть (Нельзя строить)"))
        {
            isPaintMode = false;
        }

        // Обновляем GUI
        DrawDefaultInspector();
    }
}