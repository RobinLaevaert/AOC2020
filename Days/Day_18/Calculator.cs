using System.Linq;
using System.Text.RegularExpressions;

namespace Days
{
    public static class Calculator
    {
        public static string QuickMaffs(string expression, int version)
        {
            while (true)
            {
                if (expression.All(x => x != '('))
                {
                    bool isMatch;
                    switch (version)
                    {
                        case 1:
                            expression = Order_of_operations_1(expression, out isMatch);
                            break;
                        case 2:
                            expression = Order_of_operations_2(expression, out isMatch);
                            break;
                        default: isMatch = false;
                            break;
                    }
                    
                    if(!isMatch) break;
                }
                else
                {
                    var level = 0;
                    var left_parenthesis = expression.IndexOf('(');
                    var right_matching_parenthesis = -1;
                    
                    for (int i = left_parenthesis; i < expression.Length; i++)
                    {
                        if (expression[i] == '(') level++;
                        if (expression[i] == ')') level--;

                        if (level == 0)
                        {
                            right_matching_parenthesis = i;
                            break;
                        }
                    }

                    if (right_matching_parenthesis != -1 && level == 0)
                    {
                        // Yuck
                        expression = expression.Substring(0, left_parenthesis) +
                                     QuickMaffs(expression.Substring(left_parenthesis + 1, right_matching_parenthesis - left_parenthesis - 1), version) +
                                     expression.Substring(right_matching_parenthesis + 1);
                    }
                }
            }

            return expression;
        }
        
        private static string Order_of_operations_1(string expression, out bool isMatch)
        {
            const string regex_string = @"(\d+) *([\+\*]) *(\d+)";
            var regex = new Regex(regex_string, RegexOptions.Compiled);
            var regex_match = regex.Match(expression);
            isMatch = regex_match.Success;
            if (regex_match.Success)
            {
                var result = regex_match.Groups[2].Value switch
                {
                    "+" => long.Parse(regex_match.Groups[1].Value) + long.Parse(regex_match.Groups[3].Value),
                    "*" => long.Parse(regex_match.Groups[1].Value) * long.Parse(regex_match.Groups[3].Value),
                    _ => 0
                };
                expression = regex.Replace(expression, result.ToString(), 1);
            }

            return expression;
        }
        
        private static string Order_of_operations_2(string expression, out bool isMatch)
        {
            const string regex_string_1 = @"(\d+) *([\+]) *(\d+)";
            const string regex_string_2 = @"(\d+) *([\*]) *(\d+)";
            var regex_1 = new Regex(regex_string_1, RegexOptions.Compiled);
            var regex_2 = new Regex(regex_string_2, RegexOptions.Compiled);

            var regex_match_1 = regex_1.Match(expression);
            var regex_match_2 = regex_2.Match(expression);
            if (regex_match_1.Success)
            {
                var result = long.Parse(regex_match_1.Groups[1].Value) + long.Parse(regex_match_1.Groups[3].Value);
                expression = regex_1.Replace(expression, result.ToString(), 1);
            }
            else if (regex_match_2.Success)
            {
                var result = long.Parse(regex_match_2.Groups[1].Value) * long.Parse(regex_match_2.Groups[3].Value);
                expression = regex_2.Replace(expression, result.ToString(), 1);
            }

            isMatch = regex_match_1.Success || regex_match_2.Success;
            return expression;
        }
    }
}