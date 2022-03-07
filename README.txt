```
Author:     Abhiveer Sharma and Greyson Mitra
Partner:    Greyson Mitra
Date:       25-Feb-2022
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  abhiveersharma
Repo:       https://github.com/Utah-School-of-Computing-de-St-Germain/assignment-six---spreadsheet-and-gui-team_debuggers
Commit #:   cb4c6868644e8130d1d03829e86896444f1cc0d3
Solution:   Spreadsheet
Copyright:  CS 3500, Abhiveer Sharma, and Greyson Mitra - This work may not be copied for use in Academic Coursework.
```

# Overview of the Spreadsheet functionality

The Spreadsheet program is currently capable of evaluating integer arithmetic expressions written using standard infix notation. It should respect the usual precedence rules and integer arithmetic.

The functionality of the spreadsheet has been updated after implementation of Assignment Two.
The purpose of this assignment is to keep track of dependencies between cells in a spreadsheet. 
We use a Dependency Graph to represent this relation.

The functionality of spreadsheet has been updated after implementation of Assignment Three.
In this assignment we will be focusing on how to take a more specific solution and make it more general.
Part of this process is turning a standalone static implementation into a more general reusable solution.
FormulaEvaluator's code has been made more general after the implementation of Formula and has added functionality.

The functionality of our Spreadsheet program has been updated after the implementation of Assignment Four.
Now, we can get the contents of cells, set the contents of cells(For example, cell "A4" contains value 5.0).
There are many added features and our spreadsheet is also capable of detecting a circular dependency.

The functionality of our Spreadsheet program has been updated after the implementation of Assignment Five.
We have enhanced and extended functionality of our previous version by implementing methods like  SetContentsOfCell,
GetCellValue, Save & GetSavedVersion.
We can also detect errors like SpreadsheetReadWriteException if a file doesn't exist, etc.

With Assignment Six done, our spreadsheet now has a simple GUI. The GUI shows cells from A-Z with numbers 1-99. It shows the cell's formulas, values,
and can update when dependee cells are changed. The GUI can open multiple new spreadsheets, can save, open previously saved spreadsheets, and can close
in multiple ways. It also has a help menu to show the user and TAs how our GUI works. There are warnings for not saving before closing the current
spreadsheet. There are a few additional features as well (see help menu).

The work in this assignment, specifically the implementation of the extension class in Evaluator.CS is inspired by Prof. Daniel Kopta's work.
The implementation of PS1 might resemble my earlier implementation, as I am retaking the class. 
I am linking my repository here: https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/FormulaEvaluator
which contains my previous work.

Some of my work in DependencyGraph.CS might resemble my earlier implementation.
I am referencing that work and linking my repository -
https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/DependencyGraph

The implementation of Assignment Three might resemble earlier implementation of the same assignment last semester.
Also, the implementation of Equals, ==, !=, GetVariables, ToString methods in Formula has been inspired by Prof. Daniel Kopta's work.
I am referncing my earlier work and linking my repository -
https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/blob/main/Spreadsheet/Formula/Formula.cs

The implementation of Assignment Four may resemble earlier implementation of a similar assignment as I am
retaking this class. I am referencing my last semester's work and linking my repository-
https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/Spreadsheet

The implementation of Assignment Five may resemble earlier implementation of a similar assignment as I am
retaking this class. I am referencing my last semester's work and linking my repository-
https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/Spreadsheet


# Time Expenditures:

    1. Assignment One:   Predicted Hours:          20       Actual Hours:       15
    2. Assignment Two:   Predicted Hours:          20       Actual Hours:       13
    3. Assignment Three: Predicted Hours:          25       Actual Hours:       20
    4. Assignment Four:  Predicted Hours:          20       Actual Hours:       16
    5. Assignment Five:  Predicted Hours:          25       Actual Hours:       25
    6. Assignment Six:   Predicted Hours:          18       Actual Hours:       27  (pair programming the entire time)

    We feel that although our estimate was off, we have both been pretty good as estimating how much time we spent. This assignment
    took more time than we expected since there was a lot to get used to with the GUI designer as well as new things to learn to work
    together as partners.

# References
1. https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/FormulaEvaluator
2. https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/DependencyGraph
3. https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/Formula
4. https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/Spreadsheet
5. https://github.com/uofu-cs3500-fall21/spreadsheet-abhiveersharma/tree/main/Spreadsheet/Spreadsheet