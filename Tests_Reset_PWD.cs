using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace DB
{
	[TestFixture]
	public class TestsPWD
	{
		AndroidApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			// TODO: If the Android app being tested is included in the solution then open
			// the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			app = ConfigureApp.Android.ApkFile("/Users/administrator/Downloads/app-internal-debug (15).apk").StartApp();
			//.Android
			// TODO: Update this path to point to your Android app and uncomment the
			// code if the app is not included in the solution.
			//.ApkFile ("../../../Android/bin/Debug/UITestsAndroid.apk")
			//.StartApp();
		}



		[Test]
		public void ForgotPWD_InvalidEmailInSystem()
		{
			app.Tap(x => x.Id("cc_forgot_password"));
			app.Screenshot("Tapped on view with class: AppCompatTextView with id: cc_forgot_password with text: Passwort vergessen?");
			app.Tap(x => x.Id("rp_email"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: rp_email");
			app.EnterText(x => x.Id("rp_email"), "juvve@hotmail.com");
			app.DismissKeyboard();
			app.Tap(x => x.Id("rp_new_password"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: rp_new_password");
			app.EnterText(x => x.Id("rp_new_password"), "qwer1234");
			app.DismissKeyboard();
			app.Tap(x => x.Id("rp_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: rp_button with text: Passwort ändern");
			app.Tap(x => x.Id("message"));
			app.Screenshot("Tapped on view with class: AppCompatTextView with id: message with text: Die Kombination aus E-Mail-Adresse und Passwort ist ungültig. Bitte prüfen Sie die Daten.\n[SIC003]");
			var invalidEmail = app.Query("message").Any();
			Assert.IsTrue(invalidEmail);
		}

		[Test]
		public void ForgotPWD_Errornotifications()
		{
			
			app.Tap(x => x.Id("cc_forgot_password"));
			app.Screenshot("Tapped on view with class: AppCompatTextView with id: cc_forgot_password with text: Passwort vergessen?");
			app.Tap(x => x.Id("rp_email"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: rp_email");
			app.EnterText(x => x.Id("rp_email"), "gio@.");
			app.WaitForElement(x => x.Text("Ungültige E-Mail-Adresse"));
			app.Screenshot("Waited for view with class: AppCompatTextView with text: Ungültige E-Mail-Adresse");
			var errorNoteExists = app.Query("Ungültige E-Mail-Adresse").Any();
			Assert.IsTrue(errorNoteExists);
			app.DismissKeyboard();
			app.Tap(x => x.Id("rp_new_password"));
			app.EnterText(x => x.Id("rp_new_password"), "qwertyi");
			app.EnterText(x => x.Id("rp_new_password"), "u");
			app.WaitForElement(x => x.Text("Bitte Passwortrichtlinie beachten"));
			app.Screenshot("Waited for view with class: AppCompatTextView with text: Bitte Passwortrichtlinie beachten");
			var pwd = app.Query("Bitte Passwortrichtlinie beachten").Any();
			Assert.IsTrue(pwd);
		}


		[Test]
		public void ResetPWD_LogedIn()
		{
			app.Tap(x => x.Id("cc_forgot_password"));
			app.Screenshot("Tapped on view with class: AppCompatTextView with id: cc_forgot_password with text: Passwort vergessen?");
			app.Tap(x => x.Id("rp_email"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: rp_email");
			app.EnterText(x => x.Id("rp_email"), "giovanni.hanselius+dev28@tretton37.com");
			app.DismissKeyboard();
			app.Tap(x => x.Id("rp_new_password"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: rp_new_password");
			app.EnterText(x => x.Id("rp_new_password"), "qwer1234");
			app.DismissKeyboard();
			app.ScrollDownTo("rp_button");
			app.Screenshot("Scrolled to Class: '[Class: Name=android.support.v7.widget.AppCompatButton]'; Id: 'rp_button'; Text: 'Passwort ändern'; ");
			app.Tap(x => x.Id("rp_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: rp_button with text: Passwort ändern");
			app.Tap(x => x.Id("rp_pin"));
			app.EnterText(x => x.Id("rp_pin"), "0");
			app.EnterText(x => x.Id("rp_pin"), "1");
			app.EnterText(x => x.Id("rp_pin"), "3");
			app.EnterText(x => x.Id("rp_pin"), "3");
			app.EnterText(x => x.Id("rp_pin"), "7");
			app.EnterText(x => x.Id("rp_pin"), "0");
			app.DismissKeyboard();
			app.Tap(x => x.Id("rp_activate"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: rp_activate with text: Neues Passwort aktivieren");
			app.Tap(x => x.Id("tint_toolbar_title"));
			app.Screenshot("Tapped on view with class: AppCompatTextView with id: tint_toolbar_title with text: Fahrt beginnen");
			var headerExists = app.Query("tint_toolbar_title").Any();
			Assert.IsTrue(headerExists);
		}




		[Test]
		public void InvalidPWD()
		{
			app.Tap(x => x.Id("cc_password_edittext"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: cc_password_edittext");
			app.EnterText(x => x.Id("cc_password_edittext"), "qweftyui");
			//app.Tap(x => x.Id("cc_emailaddress_edittext"));
			//app.Screenshot("Tapped on view with class: EditTextValidating with id: cc_emailaddress_edittext");
			app.DismissKeyboard();
			//app.Tap(x => x.Text("Bitte Passwortrichtlinie beachten"));
			app.Screenshot("Tapped on view with class: AppCompatTextView with text: Bitte Passwortrichtlinie beachten");
			var ExistINPWD = app.Query("Bitte Passwortrichtlinie beachten").Any();
			Assert.IsTrue(ExistINPWD);
		}

		[Test]
		public void InvalidAccountPWD ()
		{
			app.Tap(x => x.Id("cc_emailaddress_edittext"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: cc_emailaddress_edittext");
			app.EnterText(x => x.Id("cc_emailaddress_edittext"), "giovanni.hanselius+dev20@tretton37.com");
			app.DismissKeyboard();
			app.Tap(x => x.Id("cc_password_edittext"));
			app.Screenshot("Tapped on view with class: EditTextValidating with id: cc_password_edittext");
			app.EnterText(x => x.Id("cc_password_edittext"), "qwertyuu1");
			app.DismissKeyboard();

			app.Tap(x => x.Id("cc_submit_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: cc_submit_button with text: Anmelden");
			app.Tap(x => x.Id("alertTitle"));
			app.Screenshot("Tapped on view with class: DialogTitle with id: alertTitle with text: Fehlerhafte Eingabe");
			var Alert1 = app.Query("alertTitle").Any();
			Assert.IsTrue(Alert1);

			app.Tap(x => x.Id("button1"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: button1 with text: Erneut eingeben");
			app.Tap(x => x.Id("cc_submit_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: cc_submit_button with text: Anmelden");
			app.Tap(x => x.Id("alertTitle"));
			app.Screenshot("Tapped on view with class: DialogTitle with id: alertTitle with text: Fehlerhafte Eingabe");
			var Alert2 = app.Query("alertTitle").Any();
			Assert.IsTrue(Alert2);

			app.Tap(x => x.Id("button1"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: button1 with text: Erneut eingeben");
			app.Tap(x => x.Id("cc_submit_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: cc_submit_button with text: Anmelden");
			app.Tap(x => x.Id("alertTitle"));
			app.Screenshot("Tapped on view with class: DialogTitle with id: alertTitle with text: Fehlerhafte Eingabe");
			var Alert3 = app.Query("alertTitle").Any();
			Assert.IsTrue(Alert3);
			

			app.Tap(x => x.Id("button1"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: button1 with text: Erneut eingeben");
			app.Tap(x => x.Id("cc_submit_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: cc_submit_button with text: Anmelden");
			app.Tap(x => x.Id("alertTitle"));
			app.Screenshot("Tapped on view with class: DialogTitle with id: alertTitle with text: Fehlerhafte Eingabe");
			var Alert4 = app.Query("alertTitle").Any();
			Assert.IsTrue(Alert4);

			app.Tap(x => x.Id("button1"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: button1 with text: Erneut eingeben");
			app.Tap(x => x.Id("cc_submit_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: cc_submit_button with text: Anmelden");
			app.Tap(x => x.Id("alertTitle"));
			app.Screenshot("Tapped on view with class: DialogTitle with id: alertTitle with text: Account gesperrt");
			var Alert5 = app.Query("alertTitle").Any();
			Assert.IsTrue(Alert5);

			app.Tap(x => x.Id("button1"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: button1 with text: OK");
			app.Tap(x => x.Id("cc_submit_button"));
			app.Screenshot("Tapped on view with class: AppCompatButton with id: cc_submit_button with text: Anmelden");
			app.Tap(x => x.Id("alertTitle"));
			app.Screenshot("Tapped on view with class: DialogTitle with id: alertTitle with text: Account gesperrt");
			var Alert6 = app.Query("Account gesperrt").Any();
			Assert.IsTrue(Alert6);

		}

	}
}
