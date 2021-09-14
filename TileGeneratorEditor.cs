using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TileGenerator))]
public class TileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Generate Board"))
        {
            GenerateBoard();
        }
        if (GUILayout.Button("Clear Board"))
        {
            ClearBoard((TileGenerator)target);
        }

    }

    void GenerateBoard()
    {
        TileGenerator tg = (TileGenerator)target;

        ClearBoard(tg);

        for (int i = 0; i < tg.size.x; i++)
        {
            for (int j = 0; j < tg.size.y; j++)
            {
                var newTile = PrefabUtility.InstantiatePrefab(tg.tile) as GameObject;
                newTile.transform.SetParent(tg.transform);
                newTile.transform.localPosition = new Vector3((i-j)*tg.offset.x, -(i+j)*tg.offset.y, 1-(i+j)/(tg.size.x+tg.size.y));
                newTile.name += " " + i + "-" + j;
            }
        }

    }

    void ClearBoard(TileGenerator tg)
    {
        for (int i = tg.transform.childCount-1; i >= 0; i--)
        {
            DestroyImmediate(tg.transform.GetChild(i).gameObject);
        }
    }
}
