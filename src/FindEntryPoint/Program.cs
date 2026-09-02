namespace FindEntryPoint;

	internal class Program
	{
#pragma warning disable TUnit0034 // Do not declare a main method
		static void Main(string[] args)
#pragma warning restore TUnit0034 // Do not declare a main method
		{
			Console.WriteLine("Hello, World!");

			var entryPoint = typeof(LibSodium.Tests.AssertLite).Assembly.EntryPoint
				?? throw new InvalidOperationException("The test assembly has no entry point.");
			var task = entryPoint.Invoke(null, [args]) as Task<int>
				?? throw new InvalidOperationException("The test assembly entry point did not return Task<int>.");

			Environment.ExitCode = task.GetAwaiter().GetResult();
		}
	}
