using System.Collections.Generic;
using System.Linq;

public static class InputUtils
{
    public static string[] ProcessInput(string input, string prefix)
    {
        input = input.Remove(0, prefix.Length);
        string[] currentWords = input.Contains('"') ? input.Split('"') : input.Split(' ');
        List<string> cleanedWords = new List<string>();

        for (int i = 0; i < currentWords.Length; i++)
        {
            if (string.IsNullOrEmpty(currentWords[i]))
                continue;

            if (currentWords[i][0].Equals(' ') || currentWords[i][currentWords[i].Length - 1].Equals(' ')) currentWords[i] = currentWords[i].Trim(' ');

            cleanedWords.Add(currentWords[i]);
        }

        return cleanedWords.ToArray();
    }
}