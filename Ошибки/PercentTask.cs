public static double Calculate(string userInput)
{
	var splittedUserInput  = userInput.Split(' ');
	var contributionSize = double.Parse(splittedUserInput[0]);
	var interestRate = 1.0 + (double.Parse(splittedUserInput[1]) / (12 * 100));
	var depositTerm = int.Parse(splittedUserInput[2]);
	return contributionSize * Math.Pow(interestRate, depositTerm);
}