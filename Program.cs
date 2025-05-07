using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
///	 The problem:
///	 A set of problems are presented to a contestant. He/she has to solve as many as possible within a given time.
///	 Penalty is calculated as such. When problem is solved the time from the start of the contest to the 
///	 problems completion is added to the penalty. The total penalty is then a sum of all calculated penalties.
///	 
///	 The key here (which took me some time to figure out) is that the contestant does not have to solve all problems in
///	 the order they are presented. Therefore solving the fastest problems first will result in less time being added as penalty.
///	 
/// 
///  Expected input:
///	 Line 1:	nrOfProblems = 1 <= N <= 10^4 (or 10 000)
///				totalLengthOfContest = 1 <= T <= 10^9 (or 1 000 000 000)
///				
///	 Line 2:	A B C t_0
///				Integer: A
///				Integer: B
///				Integer: C 
///				Integer: t_0: t_0 <= C	: How many minutes the first problem takes to solve
///				
///				t_i = ((At-i + B)% C) + 1, i € [1, N -1]
///				
/// Expected output:	maxProblemSolvedWithinTime (totalPenalty % 1000000007)
///						
///						Integer: maxProblemSolvedWithinTime
///						Integer: (totalPenalty % 1000000007)
/// </summary>


string input;


while ((input = Console.ReadLine()) != null)
{

	int[] firstLine = Array.ConvertAll(input.Split(' '), int.Parse);
	int[] secondLine = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

	int nrOfProblems = firstLine[0];
	int totalLengthOfContest = firstLine[1];

	int A = secondLine[0];
	int B = secondLine[1];
	int C = secondLine[2];
	int t = secondLine[3]; 

	long[] solveTimes = new long[nrOfProblems];
	
	// add the time to solve the first problem. 
	solveTimes[0] = t;


	for (int i = 1; i < nrOfProblems; i++)
	{
		// Handles the case when t is larger then the allotted time for the contest in the first round.
		if (t > totalLengthOfContest) break;

		// Checks the time for the next problem
		solveTimes[i] = ((((A * solveTimes[i - 1]) + B) % C) + 1);
	}

	solveTimes = solveTimes.OrderBy(x => x).ToArray();

	int timePassed = 0;
	int maxProblemSolvedWithinTime = 0;
	long totalPenalty = 0;
	
	foreach (int time in solveTimes)
	{
		timePassed += time;
		
		// If true, then the current problem takes longer to solve then the allotted time of the contest.
		if (timePassed > totalLengthOfContest) break;

		totalPenalty += timePassed;
		maxProblemSolvedWithinTime++;
	}

	totalPenalty %= 1000000007;

	Console.WriteLine(String.Format("{0} {1}", maxProblemSolvedWithinTime, totalPenalty));

	solveTimes = new long[nrOfProblems];
}