using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddItemWindow : EditorWindow {
    private static Database database;
    private static EditorWindow window;
    private static Item newItem;

    private GUILayoutOption[] options = { GUILayout.MinWidth(20f), GUILayout.MaxWidth(200f) };
    private bool shouldDisable = false;

    public static void ShowEmptyWindow( Database db ) {
        database = db;
        window = GetWindow<AddItemWindow>();
        window.minSize = new Vector2(300, 290);
        window.maxSize = new Vector2(300, 290);
        window.titleContent = new GUIContent("Add Item");

        newItem = new Item();
    }

    private void OnGUI() {
        DisplayItem( newItem );
    }

    private void DisplayItem(Item item) {
        GUIStyle textAreaStyle = new GUIStyle( GUI.skin.textArea );
        textAreaStyle.wordWrap = true;

        EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal();
                GUILayout.Label("ID: ");
                item.id = EditorGUILayout.IntField(item.id, options);
            EditorGUILayout.EndHorizontal();

            shouldDisable = ( database.FindItemInDatabase( item.id ) != null );

            EditorGUI.BeginDisabledGroup( shouldDisable );

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Name: ");
                    item.name = EditorGUILayout.TextField(item.name, options);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Item Image: ");
                    item.itemImage = (Sprite)EditorGUILayout.ObjectField(item.itemImage, typeof(Sprite), false);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Type: ");
                    item.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(item.itemType, options);
                EditorGUILayout.EndHorizontal();   

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Stackeable: ");
                    item.isStackeable = EditorGUILayout.Toggle(item.isStackeable, options);
                EditorGUILayout.EndHorizontal();   

                GUILayout.Label("Description: ");
                item.description = EditorGUILayout.TextArea(item.description, textAreaStyle, GUILayout.MinHeight(100));

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Damage: ");
                    item.stats.damage = EditorGUILayout.IntField(item.stats.damage, options);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Defense: ");
                    item.stats.defense = EditorGUILayout.IntField(item.stats.defense, options);
                EditorGUILayout.EndHorizontal();

                if( GUILayout.Button("Confirm") ) {
                    AddItem();
                }
            EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();
    }

    private void AddItem() {
        Undo.RecordObject( database, "Item Added" );
        database.items.Add( newItem );

        EditorUtility.SetDirty( database );
        window.Close();
    }
}