using UnityEngine;
using System.Collections;

public class EmailSender : MonoBehaviour {


	string emailAddress = "keithpullout@gmail.com";

//	public void SetEmailAddress(string fa){
//		emailAddress = fa;
//		//Debug.Log ("The fromAddess is: " + fromAddress);
//	}

	public void SendEmail()
	{
		string subject = MyEscapeURL("My Subject");
		string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
		Application.OpenURL("mailto:" + emailAddress + "?subject=" + subject + "&body=" + body);

		// no "attachment" for the mailto protocol
//		Application.OpenURL("mailto:" + emailAddress + "?subject=" + subject + "&body=" + body + "&attachment=" + "/Users/Keith/Library/Application Support/Qi Liu/Thyroid/report.png");
	}
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
		
}