using UnityEngine;
using System.Collections;

public class EmailSender : MonoBehaviour {

	public void SendEmail()
	{
		string email = "keithpullout@gmail.com";
		string subject = MyEscapeURL("My Subject");
		string body = MyEscapeURL("My Body\r\nFull of non-escaped chars");
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}
		
}