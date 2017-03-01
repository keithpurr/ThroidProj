using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class mono_gmail : MonoBehaviour {

	public string fromAddress;
	public string toAddress;
	public string pass;

	public void SetFromAddress(string fa){
		fromAddress = fa;
		Debug.Log ("The fromAddess is: " + fromAddress);
	}

	public void SetToAddress(string ta){
		toAddress = ta;
		Debug.Log ("The ToAddess is: " + toAddress);
	}

	public void SetPassWord(string pass){
		this.pass = pass;
		Debug.Log ("The pass is: " + pass);
	}


	public void SendEmail ()
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress(fromAddress);
		mail.To.Add(toAddress);
		mail.Subject = "Test Mail";
		mail.Body = "This is for testing SMTP mail from GMAIL";

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential(fromAddress, pass) as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };

		// sending screen capture/ report
//		string attachmentPath = @"/Users/Keith/Library/Application Support/Qi Liu/Thyroid/report.png";

		string attachmentPath = @"/Users/Keith/Library/Application Support/Qi Liu/Thyroid/reportInfo.dat";
		System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
		mail.Attachments.Add(attachment);

		attachmentPath = @"/Users/Keith/Library/Application Support/Qi Liu/Thyroid/report.png";
		attachment = new System.Net.Mail.Attachment(attachmentPath);
		mail.Attachments.Add(attachment);


//		smtpServer.Send(mail);
		smtpServer.SendAsync (mail, "");
//		Debug.Log("success");

	}
}

