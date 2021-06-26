[TestCase(@"text", new[] {@"text"})]
[TestCase(@"hello world", new[] {@"hello", @"world"})]
[TestCase(@"''", new[] {@""})]
[TestCase(@"'\''", new[] {@"'"})]
[TestCase(@"'\\\''", new[] {@"\'"})]
[TestCase(@"'", new[] {@""})]
[TestCase(@"'h'   'h", new[] {@"h", @"h"})]
[TestCase(@"h 'h'", new[] {@"h", @"h"})]
[TestCase(@"'h\\'", new[] {@"h\"})]
[TestCase(@"'h \'h'", new[] {@"h 'h"})]
[TestCase(@"'h' h", new[] {@"h", @"h"})]
[TestCase(@"' '' '", new[] {@" ", @" "})]
[TestCase("' \" '", new[] {" \" "})]
[TestCase("\"\\\"\"", new[] {"\""})]
[TestCase("' ", new[] {" "})]
[TestCase(" ''  ''", new[] {"", ""})]
[TestCase("\"''\"", new[] {"''"})]
[TestCase("", new string[0] )]
public static void RunTests(string input, string[] expectedOutput)
{
    Test(input, expectedOutput);
}