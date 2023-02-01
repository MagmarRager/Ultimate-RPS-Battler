using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevTools : MonoBehaviour
{

    public int tier = 0;
    public List<int> savedUnits = new List<int>();

    public List<UnitInfo> loadedUnitinfo = new List<UnitInfo>();
    private void Update()
    {

        //Key Compinations are:
        // RES = Restart Scene
        // DEL = Clear PlayerPrefs
        // SAV = Save Units To firebase
        // LOD = Load Units from firebase
        //

        if(Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.L))
        {
            Debug.LogWarning("Player prefs Cleared!");
            PlayerPrefs.DeleteAll();
        }

        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.V))
        {
            UnitSaver.Instance.SaveUnitsToDatabase(savedUnits, tier);
            Debug.Log("Try Save");
        }

        if(Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.O) && Input.GetKeyDown(KeyCode.D))
        {
            FirebaseManager.Instance.LoadUnitData<UnitInfo>(0, LoadUnitToList);
            Debug.Log("Try Load");

        }
    }


    public void LoadUnitToList(List<UnitInfo> userUnits)
    {
        loadedUnitinfo = userUnits;
    }
}
