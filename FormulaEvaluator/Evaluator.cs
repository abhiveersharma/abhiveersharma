using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace FormulaEvaluator
{
    /// <summary>
    /// Author: Abhiveer Sharma
    /// Partner: None
    /// Date of Creation: January 21, 2022
    /// Course: CS 3500, University of Utah, School of Computing
    /// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
    /// I, Abhiveer Sharma, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source. All references used in the completion of the assignment are cited in my README file. 
    /// </summary>
    public static class Evaluator
    {
        public delegate int Lookup(String v);



        /// <summary>
        /// Evaluates the value of integer arithmetic expressions written using standard infix notation
        /// Stricly follows the precedence rules and integer arithmetic
        /// Takes in two parameters - a string expression and a lookup delegate
        /// "2+5+7" should evaluate to 14
        /// <param name="expression"></param> The expression to be evaluated, for example, "2+5"
        /// <param name="variableEvaluator"></param> The delegate used to look up the value of a variable
        ///  Given a variable name as its parameter, the delegate will either return an int (the value of the variable)
        ///  or throw an ArgumentException (if the variable has no value)
        /// <returns></returns> The integer value of the expression
        /// <exception cref="ArgumentException"></exception>
        /// </summary>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            Stack<int> values = new Stack<int>();
            Stack<string> operators = new Stack<string>();
            //This splits expression into an array of string tokens
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            foreach (string token in substrings)
            {
                //This removes the white space.
                String _token = token.Trim();
                int numericValue;
                bool isNumber = int.TryParse(_token, out numericValue);
                if (isNumber)
                {
                    if (StackExtension.IsOnTop(operators, "*") || StackExtension.IsOnTop(operators, "/"))
                    {

                        int operand = values.Pop();
                        String _operator = operators.Pop();
                        int result = Calculate(operand, _operator, numericValue);
                        values.Push(result);
                    }

                    else
                    {
                        values.Push(numericValue);
                    }
                }

                if (isVariable(_token))
                {
                    int variableValue = variableEvaluator(_token);
                    if (StackExtension.IsOnTop(operators, "*") || StackExtension.IsOnTop(operators, "/"))
                    {

                        int operand = values.Pop();
                        String _operator = operators.Pop();

                        int result = Calculate(operand, _operator, variableValue);
                        values.Push(result);
                    }

                    else
                    {
                        values.Push(variableValue);
                    }
                }
                
                if (((_token.Equals("+") || _token.Equals("-"))))
                {
                    if (StackExtension.IsOnTop(operators, "+") || StackExtension.IsOnTop(operators, "-"))
                    {
                        if (values.Count < 2)
                        {
                            throw new ArgumentException();
                        }
                        int operand1 = values.Pop();
                        int operand2 = values.Pop();
                        string _operator = operators.Pop();
                        int result = Calculate(operand1, _operator, operand2);
                        values.Push(result);

                    }
                    operators.Push(_token);
                }

                if (_token.Equals("*") || _token.Equals("/"))
                {
                    operators.Push(_token);
                }

                if (_token.Equals("("))
                {
                    operators.Push(_token);
                }

                if (_token.Equals(")"))
                {
                    if (values.Count >= 2 && (StackExtension.IsOnTop(operators, "+") || StackExtension.IsOnTop(operators, "-")))
                    {
                        int operand1 = values.Pop();
                        int operand2 = values.Pop();
                        string _operator = operators.Pop();
                        int result = Calculate(operand1, _operator, operand2);
                        values.Push(result);
                    }

                    if (StackExtension.IsOnTop(operators, "("))
                    {
                        operators.Pop();
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                    if (values.Count >= 2 && (StackExtension.IsOnTop(operators, "*") || StackExtension.IsOnTop(operators, "/")))
                    {
                        int operand1 = values.Pop();
                        int operand2 = values.Pop();
                        string _operator = operators.Pop();
                        int result = Calculate(operand2, _operator, operand1);
                        values.Push(result);
                    }
                }



            }
            // After the last token has been processed
            if (operators.Count == 0 && values.Count == 1)
            {
                return values.Pop();
            }

            else
            {
                if (values.Count == 2 && StackExtension.IsOnTop(operators, "+") || StackExtension.IsOnTop(operators, "-"))
                {
                    int operand1 = values.Pop();
                    int operand2 = values.Pop();
                    string _operator = operators.Pop();
                    int result = Calculate(operand1, _operator, operand2);
                    return result;
                }
                else
                {
                    throw new ArgumentException();
                }
            }

        }
        /// <summary>
        /// This is a helper method made for calculating the value of arithmetic expression
        /// involving integers and one of the following operators - '+', '-', '*' & '/'.
        /// Throws an exception if division by zero occurs
        /// <param name="value1"></param> The first value popped off the value stack
        /// <param name="exp"></param> The operator popped off the operator stack
        /// <param name="value2"></param> The second value popped off the value stack
        /// <returns></returns> The final result after performing the mathematical operation
        /// <exception cref="ArgumentException"></exception>
        /// </summary>
        private static int Calculate(int value1, String exp, int value2)
        {
            if (exp.Equals("*"))
            {
                return value1 * value2;
            }
            if (exp.Equals("+"))
            {
                return value1 + value2;
            }
            if (exp.Equals("-"))
            {
                return value2 - value1;
            }

            if (exp.Equals("/"))
            {
                if(value2 == 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    return value1 / value2;
                }
            }

            return 0;

        }

        /// <summary> This method evaluates if the String token is legal and
        /// it begins with one or more letters and ends with one or more digits
        /// For example, "A5" should be legal and "5A" should be invalid
        /// <param name="token"></param> The string token whose legality needs to be checked
        /// <returns></returns> A boolean telling the legality of the String token
        /// </summary>
        private static Boolean isVariable(String token)
        {

            return Regex.IsMatch(token, @"^[a-zA-Z]+[0-9]+$");
        }

    }
    /// <summary>
    /// This is an extension class written to help in the Evaluate method above in the Evaluator class
    /// This class implements the IsOnTop method
    /// The implementation of this class is inspired by Prof. Daniel Kopta's work.
    /// </summary>
    public static class StackExtension
    {
        /// <summary>
        /// This method checks the count of a stack (In our case Operator stack) and also finds out
        /// if the top of the stack matches object x
        /// <typeparam name="T"></typeparam> The type of the stack (In our case string)
        /// <param name="stack"></param> The stack whose count wi
        /// <param name="x"></param> The item at the top of the stack (In our case one of: +,-,/,*,(,)
        /// <returns></returns>
        /// </summary>
        public static Boolean IsOnTop<T>(this Stack<T> stack, T x)
        {
            if (stack.Count >= 1 && stack.Peek().Equals(x))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}

    
