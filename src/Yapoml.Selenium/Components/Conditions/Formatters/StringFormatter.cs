using System;
using System.Text;

namespace Yapoml.Selenium.Components.Conditions.Formatters
{
    public static class StringFormatter
    {
        public static string Format(string indentation, string first, string second)
        {
            first ??= "<null>";
            second ??= "<null>";

            var matcher = new diff_match_patch();
            var differences = matcher.diff_main(first, second);

            matcher.diff_cleanupSemantic(differences);

            StringBuilder line1 = new StringBuilder(indentation);
            StringBuilder line2 = new StringBuilder(indentation);

            Action<int> appendStartDiff = (int diffIndex) =>
            {
                if (diffIndex != 0)
                {
                    line1.Append('┐');
                    line2.Append('└');
                }
            };

            Action<int> appendEndDiff = (int diffIndex) =>
            {
                if (diffIndex != differences.Count - 1)
                {
                    line1.Append('┌');
                    line2.Append('┘');
                }
            };

            for (int i = 0; i < differences.Count; i++)
            {
                var difference = differences[i];

                if (difference.operation == Operation.EQUAL)
                {
                    line1.Append(difference.text);
                    line2.Append(' ', difference.text.Length);
                }
                else if (difference.operation == Operation.INSERT)
                {
                    appendStartDiff(i);

                    line1.Append(' ', difference.text.Length);
                    line2.Append(difference.text);

                    appendEndDiff(i);
                }
                else if (difference.operation == Operation.DELETE)
                {
                    if (i != differences.Count - 1 && differences[i + 1].operation == Operation.INSERT)
                    {
                        // replaced
                        var deletedPiece = difference.text;
                        var insertedString = differences[i + 1].text;

                        appendStartDiff(i);

                        line1.Append(deletedPiece);
                        line2.Append(insertedString);

                        if (deletedPiece.Length < insertedString.Length)
                        {
                            line1.Append(' ', insertedString.Length - deletedPiece.Length);
                        }
                        else if (insertedString.Length < deletedPiece.Length)
                        {
                            line2.Append(' ', deletedPiece.Length - insertedString.Length);
                        }

                        appendEndDiff(i + 1);

                        i++;
                    }
                    else
                    {
                        // just deleted
                        appendStartDiff(i);

                        line1.Append(difference.text);
                        line2.Append(' ', difference.text.Length);

                        appendEndDiff(i);
                    }
                }
            }

            return line1.AppendLine().Append(line2.ToString()).ToString();
        }
    }


}
