using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncDemoForm
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public string[] UserActions = {"write something", "open report", "view movie", "create report", "send an email", "play game", "hack Pentahon", "hack Nasa"};

		public MainWindow()
		{
			InitializeComponent();
			LFormThread.Content += $" {getCurrentThreadInfo()}";
		}
		#region Helpers
		private string getCurrentThreadInfo()
		{
			return $"{Thread.CurrentThread.ManagedThreadId}";
		}

		private void logCurThreadInfo()
		{
			LBActionsLog.Items.Add($"Current Thread: {getCurrentThreadInfo()}");
		}

		private void log(string str)
		{
			LBActionsLog.Items.Add(str);
		}
		#endregion
		// form secondary actions
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			LBLog.Items.Insert(0,$"[{DateTime.Now}]: {UserActions[new Random().Next(UserActions.Length)]}");
		}

		private void BClearLog_Click(object sender, RoutedEventArgs e)
		{
			LBLog.Items.Clear();
		}

		private void BClearActionsLog_Click(object sender, RoutedEventArgs e)
		{
			LBActionsLog.Items.Clear();
		}

		// primary actions
		private void DoSync_Click(object sender, RoutedEventArgs e)
		{
			LBActionsLog.Items.Clear();
			log("DoSync");
			logCurThreadInfo();
			var externalCall = new ExternalCall(3000);
			log("start ExternalCall");
			var result = externalCall.LongAction().Result;
			log("ExternalCall result received");
			logCurThreadInfo();
		}

		private async void DoAsync_Click(object sender, RoutedEventArgs e)
		{
			LBActionsLog.Items.Clear();
			log($"DoAsync (Thread: {getCurrentThreadInfo()})");
			log("start ProcessResult");
			var result = await processResult().ConfigureAwait(true);
			// ConfigureAwait(true) the Main Thread will process it after await
			log($"{result} (DoAsync Thread: { getCurrentThreadInfo()})");
		}

		private async Task<string> processResult()
		{
			var externalCall = new ExternalCall(3000);
			log($"start ExternalCall (processresult Thread: {getCurrentThreadInfo()})");
			var result = await externalCall.LongAction().ConfigureAwait(false);
			// it will throw error, because a secondary Thread will process remaining code (in this function)
			//log($"ExternalCall returns: {result}. (processresult Thread: {getCurrentThreadInfo()})");
			return result + " (Processed by processresult(). Thread after await externalCall: {getCurrentThreadInfo()})";
		}

		private async void BDoAsyncError_Click(object sender, RoutedEventArgs e)
		{
			LBActionsLog.Items.Clear();
			log($"DoAsync (Thread: {getCurrentThreadInfo()})");
			log("start ProcessResult");
			try { 
				var result = await processResultWithError().ConfigureAwait(true);
			}
			catch (Exception ex)
			{
				log($"ExternalCallError catched (DoAsyncError Thread: {getCurrentThreadInfo()})");
				log($"Exception{ex.ToString()}");
			}
			// ConfigureAwait(true) the Main Thread will process it after await
			log($"(DoAsync Thread: { getCurrentThreadInfo()})");
		}

		private async Task<string> processResultWithError()
		{
			var externalCall = new ExternalCall(3000);
			log($"start ExternalCall (processresult Thread: {getCurrentThreadInfo()})");
			var result = await externalCall.LongActionWithError().ConfigureAwait(false);
			// it will throw error, because a secondary Thread will process remaining code (in this function)
			//log($"ExternalCall returns: {result}. (processresult Thread: {getCurrentThreadInfo()})");
			return result + " (Processed by processresult(). Thread after await externalCall: {getCurrentThreadInfo()})";
		}
		#region Dialogs
		private void BSyncDialog_Click(object sender, RoutedEventArgs e)
		{
			PasswordWindow passwordWindow = new PasswordWindow();
			passwordWindow.Show();
			//it prints empty, because it must do it after form closing... (see async version below to reslove this)
			log(passwordWindow.Password);
		}

		private async void BAsyncDialog_Click(object sender, RoutedEventArgs e)
		{
			var dialogData = await getPasswordFormResult();
			log(dialogData.Password);
		}

		private Task<PasswordWindow> getPasswordFormResult()
		{
			var tcs = new TaskCompletionSource<PasswordWindow>();

			PasswordWindow passwordWindow = new PasswordWindow();
			passwordWindow.Closed += (o,e) => tcs.SetResult(passwordWindow);
			passwordWindow.Show();
			return tcs.Task;
		}
		#endregion

		private void BAsyncVoid_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				log($"start OnClick (processresult Thread: {getCurrentThreadInfo()})");
				DoSomething();// UI thread will leave the method after first await meeting inside the method
				log($"Continue OnClick (processresult Thread: {getCurrentThreadInfo()})");
			}
			catch(Exception ex)
			{
				//will never catch it
				log($"Error was thrown: {ex.Message}");
			}
		}

		private async void DoSomething()
		{
			log($"start DoSomething (processresult Thread: {getCurrentThreadInfo()})");
			var externalCall = new ExternalCall(3000);
			var result = await externalCall.LongActionWithError().ConfigureAwait(false);
			// error will be thrown in Secondary Thread + the Main UI will silently shut down
			//log($"continue DoSomething (processresult Thread: {getCurrentThreadInfo()})");
		}
	}
}
