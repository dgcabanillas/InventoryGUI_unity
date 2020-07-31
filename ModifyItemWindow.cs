using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModifyItemWindow : EditorWindow {
    private static Database database;
    private static EditorWindow window;
    private static Item newItem;
    private static Item itemToModify;

    private GUILayoutOption[] options = { GUILayout.MinWidth(20f), GUILayout.MaxWidth(200f) };

    public static void viewWindow( Database db, Item item ) {
        database = db;
        window = GetWindow<ModifyItemWindow>();
        window.minSize = new Vector2(300, 270);
        window.maxSize = new Vector2(300, 270);
        window.titleContent = new GUIContent("Modify Item");

        // Copy the values of the selected item
        newItem = new Item();
        newItem.name = item.name;
        newItem.itemImage = item.itemImage;
        newItem.itemType = item.itemType;
        newItem.isStackeable = item.isStackeable;
        newItem.description = item.description;
        newItem.stats = item.stats;

        // Make a reference for the item that will be modified
        itemToModify = item;
    }

    private void OnGUI() {
        DisplayItem( newItem );
    }

    private void DisplayItem(Item item) {
        GUIStyle textAreaStyle = new GUIStyle( GUI.skin.textArea );
        textAreaStyle.wordWrap = true;

        EditorGUILayout.BeginVertical("Box");

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

            if( GUILayout.Button("Edit") ) {
                EditItem();
            }

        EditorGUILayout.EndVertical();
    }

    private void EditItem() {
        Undo.RecordObject( database, "Item Modified" );
        itemToModify.name = newItem.name;
        itemToModify.itemImage = newItem.itemImage;
        itemToModify.itemType = newItem.itemType;
        itemToModify.isStackeable = newItem.isStackeable;
        itemToModify.description = newItem.description;
        itemToModify.stats = newItem.stats;
        
        EditorUtility.SetDirty( database );
        window.Close();
    }
}