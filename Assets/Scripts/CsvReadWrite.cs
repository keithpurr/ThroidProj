using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadWrite : MonoBehaviour {

	private List<string[]> rowData = new List<string[]>();

	GameObject dataManager;


	// Use this for initialization
	void Start () {
		Save();
	}

	void Save(){

		// hold reference of DataManager
		dataManager = GameObject.Find("DataManager");

		Debug.Log ("found my datamanager");

		// Creating First row of titles manually..
		string[] rowDataTemp = new string[7];
		rowDataTemp[0] = "First Name";
		rowDataTemp[1] = "Last Name";
		rowDataTemp[2] = "Gender";
		rowDataTemp[3] = "Date of Birth";
		rowDataTemp[4] = "MRN";
		rowDataTemp[5] = "Number of Nodules";
		rowDataTemp[6] = "Date of Scan";

		rowData.Add(rowDataTemp);

		// You can add up the values in as many cells as you want.
		for(int i = 0; i < 10; i++){
			rowDataTemp = new string[7];
			rowDataTemp[0] = "Sushanta"+i; // First Name
			rowDataTemp[1] = ""+i; // Last Name
			rowDataTemp[2] = "$"+UnityEngine.Random.Range(5000,10000); // Income
			rowDataTemp[3] = "Date of Birth";
			rowDataTemp[4] = "MRN";
			rowDataTemp[5] = "Number of Nodules";
			rowDataTemp[6] = "Date of Scan";

			rowData.Add(rowDataTemp);
		}

		string[][] output = new string[rowData.Count][];

		for(int i = 0; i < output.Length; i++){
			output[i] = rowData[i];
		}

		int length = output.GetLength(0);
		string delimiter = ",";

		StringBuilder sb = new StringBuilder();

		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));


		string filePath = getPath();

//		StreamWriter outStream = System.IO.File.CreateText (filePath);
		StreamWriter outStream = System.IO.File.AppendText(filePath);
		outStream.WriteLine(sb);
		outStream.Close();

		Debug.Log ("save path is: " + filePath);
	}

	// Following method is used to retrive the relative path as device platform
	private string getPath(){
		#if UNITY_EDITOR
		return Application.dataPath +"/CSV/"+"Saved_data.csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"Saved_data.csv";
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"Saved_data.csv";
		#else
		return Application.dataPath +"/"+"Saved_data.csv";
		#endif
		}
		}