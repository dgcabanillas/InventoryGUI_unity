using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Database))]
public class DatabaseEditor : Editor {

    private Database database;
    private Item itemToDelete = null;
    private string searchString = "";
    private bool shouldSearch;

    private void OnEnable() {
        database = (Database)target;
    }

    public override void OnInspectorGUI() {
        // base.DrawDefaultInspector();
        
        if( database ) {

            EditorGUILayout.BeginHorizontal("Box");
                GUILayout.Label("Items in database: " + database.items.Count );
            EditorGUILayout.EndHorizontal();

            if( database.items.Count > 0 ) {
                GUILayout.BeginHorizontal("Box");
                    GUILayout.Label("Search: ");
                    searchString = GUILayout.TextField( searchString );
                EditorGUILayout.EndHorizontal();
            }

            if( GUILayout.Button("Add Item")) {
                AddItemWindow.ShowEmptyWindow( database );
            }

            shouldSearch = !System.String.IsNullOrEmpty( searchString );
            foreach (Item item in database.items) {
                if( shouldSearch ) {
                    if( item.name == searchString || 
                        item.name.Contains( searchString ) || 
                        item.id.ToString() == searchString ) {
                        DisplayItem( item );
                    }
                } else {
                    DisplayItem( item );
                }
            }
            if( itemToDelete != null ) {
                database.items.Remove( itemToDelete );
                itemToDelete = null;
            }
        }
        
    }

    private void DisplayItem(Item item) {
        GUIStyle labelStyle = new GUIStyle( GUI.skin.label );
        labelStyle.wordWrap = true;

        GUIStyle valueStyle = new GUIStyle( GUI.skin.label );
        valueStyle.wordWrap = true;
        valueStyle.alignment = TextAnchor.MiddleLeft;
        valueStyle.fixedWidth = 150;
        valueStyle.margin = new RectOffset(0, 10, 0, 0);

        GUIStyle idStyle = new GUIStyle( GUI.skin.label );
        idStyle.wordWrap = true;
        idStyle.alignment = TextAnchor.MiddleLeft;
        idStyle.fixedWidth = 155;
        idStyle.margin = new RectOffset(0, 10, 0, 0);

        EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal();
                GUILayout.Label("ID: ");
                EditorGUILayout.BeginHorizontal( idStyle );
                    GUILayout.Label( item.id.ToString() , GUILayout.MinWidth(80), GUILayout.MaxWidth(80));
                    GUILayout.Label("show ", GUILayout.ExpandWidth( false ) );
                    item.showData = EditorGUILayout.Toggle( item.showData , GUILayout.ExpandWidth( false ) );
                EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();

            if( item.showData ) {
                
                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Image: ");
                    GUILayout.Label( item.itemImage != null ? item.itemImage.ToString() : "null", valueStyle);
                EditorGUILayout.EndHorizontal();  

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Name: ");
                    GUILayout.Label(item.name, valueStyle);
                EditorGUILayout.EndHorizontal();    

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Type: ");
                    GUILayout.Label(item.itemType.ToString(), valueStyle);
                EditorGUILayout.EndHorizontal();   

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Stackeable: ");
                    GUILayout.Label(item.isStackeable ? "yes":"no", valueStyle);
                EditorGUILayout.EndHorizontal();   

                GUILayout.Label("Description: ");
                item.scrollPos = GUILayout.BeginScrollView(item.scrollPos, GUILayout.MinHeight(3), GUILayout.MaxHeight(70));
                    GUILayout.Label(item.description, labelStyle);
                GUILayout.EndScrollView();

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Damage: ");
                    GUILayout.Label(item.stats.damage.ToString(), valueStyle);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Defense: ");
                    GUILayout.Label(item.stats.defense.ToString(), valueStyle);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    if( GUILayout.Button("Delete") ) {
                        itemToDelete = item;
                    }

                    if( GUILayout.Button("Modify") ) {
                        ModifyItemWindow.viewWindow( database, item );
                    }
                EditorGUILayout.EndHorizontal();
            }

        EditorGUILayout.EndVertical();
    }
}