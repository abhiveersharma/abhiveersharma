/// <summary>
/// Author: Abhiveer Sharma
/// Partner: None
/// Date of Creation: January 21, 2022
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Abhiveer Sharma - This work may not be copied for use in Academic Coursework. 
/// I, Abhiveer Sharma, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source. All references used in the completion of the assignment are cited in my README file. 
/// This is a console project created to test the Formula Evaluator project in the Spreadsheet solution.
/// We test expressions like "5+5" and also the ones with variables like "2+A1" and use lambda expressions
/// to assign a value to our variables.
/// </summary>

//This should return 10
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("5+5", s => 0));

//This should return 13
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("5+X5", s => 8));

//Testing an invalid expression
try
{
    FormulaEvaluator.Evaluator.Evaluate("5X", s => 0);
}
catch (ArgumentException)
{
    Console.WriteLine("This is a syntatctical error. Please check the formula again.");
}

//This should return 35
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(5+(6*5))", s => 0));

//This should return 53
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("6 + 7 + (5)*8", s => 0));

//Testing an invalid expression
try
{
    FormulaEvaluator.Evaluator.Evaluate(" -A- ", s => 0);
}
catch (ArgumentException)
{
    Console.WriteLine("This is a syntatctical error. Please check the formula again.");
}

//This should return 13
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(((5+X5)))", s => 8));

//This should return 10
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(50+10)/A1", s => 6));

//This should return 3
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(6*5+2+7)/B5", s => 13));

//Testing division by zero error
try
{
    FormulaEvaluator.Evaluator.Evaluate("5/0", s => 0);
}
catch (ArgumentException)
{
    Console.WriteLine("Division by zero error.");
}

// Testing division by zero error. "50/0" should throw an exception
try
{
    FormulaEvaluator.Evaluator.Evaluate("50/c1", s => 0);
}
catch (ArgumentException)
{
    Console.WriteLine("Division by zero error.");
}

//This should return 0
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("0/B5", s => 13));

//This should return 58
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(6*5+(2*7*2))", s => 0));

//This should return 242
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(6*5+2+7*5*(z5+1))", s => 5));

//This should return 2
Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("10/(X5+3)", s => 2));





