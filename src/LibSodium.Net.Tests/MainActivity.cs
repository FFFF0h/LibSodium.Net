#if ANDROID
namespace LibSodium.Net.Tests.Android;

/// <summary>
/// Runs the test application inside an Android activity.
/// </summary>
[global::Android.App.Activity(
	Label = "TestRunner",
	MainLauncher = true,
	Theme = "@android:style/Theme.NoDisplay"
)]
public class MainActivity : global::Android.App.Activity
{
	/// <inheritdoc/>
	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		_ = RunTestsAsync();
	}

	private async Task RunTestsAsync()
	{
		try
		{
			var context = ApplicationContext ?? throw new InvalidOperationException("ApplicationContext is null");
			var filesDirectory = context.FilesDir ?? throw new InvalidOperationException("FilesDir is null");
			var logFilePath = Path.Combine(filesDirectory.AbsolutePath, "log.txt");
			TextFileLogger.Initialize(logFilePath);
			var testResultsPath = Path.Combine(filesDirectory.AbsolutePath, "TestResults");
			Console.WriteLine($"TUNIT: Test results path: {testResultsPath}");
			var exitCode = await MicrosoftTestingPlatformEntryPoint.Main(
				["--hide-test-output", "--disable-logo", "--diagnostic", "--report-trx", "--results-directory", testResultsPath]);
			Console.WriteLine($"TUNIT: Test runner exited with code {exitCode}.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"TUNIT: EXCEPTION: {ex}");
		}
		finally
		{
			TextFileLogger.Close();
			Finish();
		}
	}
}
#endif
