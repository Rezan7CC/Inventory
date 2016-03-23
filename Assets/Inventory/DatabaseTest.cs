using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;

public class DatabaseTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {  
        SqliteConnection dbCon = new SqliteConnection("URI=file:test.db;");
        dbCon.Open();
        SqliteCommand selectAll = new SqliteCommand("select * from TestTable", dbCon);
        SqliteDataReader selectAllReader = selectAll.ExecuteReader();
        int bp = 1;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
