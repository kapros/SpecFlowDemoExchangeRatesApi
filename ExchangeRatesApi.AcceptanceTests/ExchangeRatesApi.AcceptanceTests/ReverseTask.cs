// Given an input string, reverse the string word by word.
// A word is defined as a sequence of non-space characters.
// The input string does not contain leading or trailing spaces and
//  the words are always separated by a single space.
//  For example,
//      Utils.Reverse("the sky is blue") // will return "blue is sky the"
using System;
using System.Linq;
using System.Text;

public static class Text
{
    public static string Reverse(string text)
    {
        return string.Join(" ", text.Split(" ").Reverse());
        /*
        var words = text.Split(" ");
        var newText = new StringBuilder();
        for (int i = words.Length; i > 1; i--)
        {
            newText.Append(words[i] + " ");
        }
        newText.Append(words[0]);
        return newText.ToString();
        */
        /*
        var words = text.Split(" ");
        var newOrder = new string[words.Length];
        for (int i = words.Length; i > 0; i--)
        {
            newOrder[words.Length - i] = words[i];
        }
        return string.Join(" ", newOrder);
        */
        /*
        var split = text.Split(" ");
        Array.Reverse(split);
        return string.Join(" ", split);
        */
    }
}
