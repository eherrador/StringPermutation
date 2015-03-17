using System;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace StringPermutation
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			if (args == null || args.Length == 0) {
				Console.WriteLine("args is null"); // Check for null array
			}
			else
			{
				if (args.Length > 1) {
					Console.WriteLine("Too many params"); // Check many params
				}
				else {
					if (File.Exists (args[0])) {
						string line;
						char[] inputSet;

						//Stopwatch timer = Stopwatch.StartNew();

						using (StreamReader reader = new StreamReader (args[0])) {
							while ((line = reader.ReadLine ()) != null) {
								inputSet = line.ToCharArray ();
								Array.Sort (inputSet);
								StringBuilder resultPermutation = new StringBuilder ();
								resultPermutation.Append (inputSet).Append (',');

								while (KnuthPermutation (inputSet)) {
									resultPermutation.Append (inputSet).Append (',');
								}
								resultPermutation.Remove (resultPermutation.Length - 1, 1);
								Console.WriteLine (resultPermutation);
							}
						}

						//timer.Stop();  
						//TimeSpan timespan = timer.Elapsed;
						//Console.WriteLine (String.Format("KnuthPermutation: {0}:{1}", timespan.Minutes, timespan.TotalSeconds));
					} else
						Console.WriteLine ("File does not exist...");
				}
			}
			return 0; //or exit code of your choice
		}

		//private static bool KnuthPermutation(char[] inputData)
		private static bool KnuthPermutation<T>(T[] inputData) where T : IComparable<T>
		{
		 	/*
         	Knuths
         	1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
         	2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
         	3. Swap a[j] with a[l].
         	4. Reverse the sequence from a[j + 1] up to and including the final element a[n].
         	*/

			var largestIndex = -1;
			for (var i = inputData.Length - 2; i >= 0; i--)
			{
				if (inputData[i].CompareTo(inputData[i + 1]) < 0) {  //if (inputData[i] < inputData[i + 1]) {
					largestIndex = i;
					break;
				}
			}

			if (largestIndex < 0) return false;

			var largestIndex2 = -1;
			for (var i = inputData.Length - 1 ; i >= 0; i--) {
				if (inputData[largestIndex].CompareTo(inputData[i]) < 0) {  //if (inputData[largestIndex] < inputData[i]) {
					largestIndex2 = i;
					break;
				}
			}

			var tmp = inputData[largestIndex];
			inputData[largestIndex] = inputData[largestIndex2];
			inputData[largestIndex2] = tmp;

			for (int i = largestIndex + 1, j = inputData.Length - 1; i < j; i++, j--) {
				tmp = inputData[i];
				inputData[i] = inputData[j];
				inputData[j] = tmp;
			}

			return true;
		}
	}
}