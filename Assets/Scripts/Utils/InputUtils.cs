using System;
using System.Collections.Generic;
using System.Linq;

public static class InputUtils
{
    public static string[] ProcessInput(string input)
    {
        input = input.Remove(0, MessageUtils.CommandPrefix.Length);
        string[] currentWords;

        if (input.Contains('"'))
            currentWords = SplitHandlingQuotes(input);
        else
            currentWords = input.Split(' ');

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

    private static string[] SplitHandlingQuotes(string input)
    {
        return input
            .Split('"')
            .Select((element, i) => SplitOrGetNewElement(element, i))
            .SelectMany(element => element)
            .ToArray();
    }

    private static string[] SplitOrGetNewElement(string element, int i)
    {
        return i % 2 == 0
            ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            : new string[] { element };
    }
}