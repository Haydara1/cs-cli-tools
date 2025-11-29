using CMatrix.Classes;
using Spectre.Console;

namespace CMatrix;

internal partial class Program
{
    static List<Matrix> memory = [];
    static public void Menu()
    {
        Console.Clear();

        AnsiConsole.Write(
        new FigletText("CMatrix\n")
        .Centered()
        .Color(Color.Green));

        AnsiConsole.Write(new Align(
            new Text("Welcome!\nPlease choose a functionnality\n"),
            HorizontalAlignment.Center,
            VerticalAlignment.Top
        ));

        Console.WriteLine("Memory contains " + 
            (memory.Count == 0 ? "no" : memory.Count) 
            + " matrices");

        var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices([
            "Manage Memory",
            "Operate on Memory's Matrices",
            "Exit",
        ]));

        switch (option)
        {
            case "Manage Memory":
                ManageMemory();
                break;

            case "Operate on Memory's Matrices":
                Calculations();
                break;

            case "Exit":
                System.Environment.Exit(0);
                break;
        }
    }

    #region Calculation Functions

    static void Calculations()
    {
        Console.Clear();

        AnsiConsole.Write(new Align(
            new Text("Operate on available matrices"),
            HorizontalAlignment.Center,
            VerticalAlignment.Top
        ));

        Console.WriteLine("Please choose a funcionnality:");

        var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices([
            "Back",
            "Addition/Substraction/Multiplication",
            "Raise to Power",
            "Calculate Trace",
            "Calculate Determinant",
            "Diagonalise",
            "Invert Matrix",
        ]));

        switch(option)
        {
            case "Back":
                Menu();
                break;

            case "Addition/Substraction/Multiplication":
                Addition();
                break;

            case "Raise to Power":
                Power();
                break;

            case "Calculate Determinant":
                Determinant();
                break;

            case "Calculate Trace":
                Trace();
                break;

            case "Invert Matrix":
                Invert();
                break;
        }

    }

    static void Addition()
    {
        Console.Clear();

        Console.WriteLine("Please choose the index of the left hand matrix: ");

        int index1 = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.WriteLine("Please choose the index of the right hand matrix: ");

        int index2 = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        var operation = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Please choose an operation to perform")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices([
            "Addition",
            "Substraction",
            "Multiplication",
        ]));

        Matrix m1 = memory[index1];
        Matrix m2 = memory[index2];
        Matrix m3;

        string op = " ";

        switch (operation)
        {
            case "Addition":
                m3 = m1 + m2;
                op = " + ";
                break;

            case "Substraction":
                m3 = m1 - m2;
                op = " - ";
                break;

            case "Multiplication":
                m3 = m1 * m2;
                op = " x ";
                break;

            default:
                m3 = m2;
                break;
        }


        Console.Clear();

        var grid = new Grid();

        // Add columns 
        grid.AddColumn().Centered();
        grid.AddColumn().Centered();
        grid.AddColumn().Centered();
        grid.AddColumn().Centered(); 
        
        // Add content row 
        grid.AddRow(new Text[]{
        new Text(m1.ToString()).LeftJustified(),
        new Text(op).Centered(),
        new Text(m2.ToString()).RightJustified(),
        new Text(" = ")
        });

        // Write centered cell grid contents to Console
        AnsiConsole.Write(new Align(
            grid,
            HorizontalAlignment.Center,
            VerticalAlignment.Middle));

        Console.WriteLine("\n");

        AnsiConsole.Write(new Align(
            new Text(m3.ToString()),
            HorizontalAlignment.Center,
            VerticalAlignment.Middle));

        Console.WriteLine("\n\n\n");

        var confirmation = AnsiConsole.Prompt(
                            new TextPrompt<bool>("Do you want to add the result to memory?")
                            .AddChoice(true)
                            .AddChoice(false)
                            .DefaultValue(true)
                            .WithConverter(choice => choice ? "y" : "n"));

        if (confirmation)
        {
            Console.WriteLine("The matrix has been added to memory!");
            memory.Add(m3);
        }

        Console.ReadLine();
    }

    // To DO
    static void Power()
    {
        Console.Clear();

        Console.WriteLine("Please choose the index of the wanted matrix: ");

        int index = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.WriteLine("Please choose an integer power:");
        int power = Convert.ToInt32(Console.ReadLine());
    }

    static void Determinant()
    {
        Console.Clear();

        Console.WriteLine("Please choose the index of the wanted matrix: ");

        int index = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.Clear();
        Console.WriteLine("The determinant of the following matrix\n");
        Console.WriteLine(memory[index]);
        Console.WriteLine("is: " + Matrix.Det(memory[index]));
        Console.ReadLine();
    }

    static void Trace()
    {
        Console.Clear();

        Console.WriteLine("Please choose the index of the wanted matrix: ");

        int index = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.Clear();
        Console.WriteLine("The trace of the following matrix\n");
        Console.WriteLine(memory[index]);
        Console.WriteLine("is: " + Matrix.Tr(memory[index]));
        Console.ReadLine();
    }

    static void Invert()
    {
        Console.Clear();

        Console.WriteLine("Please choose the index of the wanted matrix: ");

        int index = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.Clear();
        Console.WriteLine("The Invert of the following matrix\n");
        Console.WriteLine(memory[index]);
        Console.WriteLine("is: ");
        Console.WriteLine(Matrix.Inverse(memory[index]));
        Console.ReadLine();
    }

    #endregion

    #region Memory Management
    static public void ManageMemory()
    {
        Console.Clear();

        AnsiConsole.Write(new Align(
            new Text("Manage memory's matrices"),
            HorizontalAlignment.Center,
            VerticalAlignment.Top
        ));

        Console.WriteLine("Please choose a funcionnality");

        var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices([
            "Back",
            "Add a Matrix to Memory",
            "Delete a Matrix from Memory",
            "Clear Memory",
            "Display a Matrix from Memory",
        ]));

        switch(option)
        {
            case "Back":
                Menu();
                break;

            case "Add a Matrix to Memory":
                AddMatrix();
                break;

            case "Delete a Matrix from Memory":
                DeleteMatrix();
                break;

            case "Clear Memory":
                memory.Clear();
                Menu();
                break;

            case "Display a Matrix from Memory":
                DisplayMatrix();
                break;
        }
    }

    static public void AddMatrix()
    {
        Console.Clear();
        Console.Write("Enter the number of rows: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the number of columns: ");
        int m = Convert.ToInt32(Console.ReadLine());

        float[] floats;

        Console.WriteLine("Please enter all the values of your matrix seperated by a space:");
        floats = Array.ConvertAll(
            Console.ReadLine().Trim().Split(' '), Convert.ToSingle);

        Matrix matrix = new(floats, n, m);

        Console.Clear();
        Console.WriteLine("Is this the wanted matrix?");
        Console.WriteLine(matrix);

        var option = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
        .AddChoices([
            "Yes.",
            "Transpose.",
            "No, retype.",
            "No, exit.",
        ]));

        switch(option)
        {
            case "Yes.":
                Console.WriteLine("Your matrix has been saved to memory.");
                memory.Add(matrix);
                break;

            case "Transpose.":
                memory.Add(!matrix);
                Console.WriteLine("The invert of your matrix has been saved to memory.");
                break;

            case "No, retype.":
                AddMatrix();
                break;

            case "No, exit.":
                Menu();
                break;
        }

        Console.ReadLine();
    }

    static void DeleteMatrix()
    {
        Console.Clear();
        Console.WriteLine("Please choose the index of the matrix you want to delete: ");

        int index = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.WriteLine("Are you sure you want to delete this matrix?");
        Console.WriteLine(memory[index]);

        var confirmation = AnsiConsole.Prompt(
                            new TextPrompt<bool>("Delete?")
                            .AddChoice(true)
                            .AddChoice(false)
                            .DefaultValue(true)
                            .WithConverter(choice => choice ? "y" : "n"));

        if (!confirmation)
        { Menu(); return; }

        Console.WriteLine("Matrix " + index.ToString() + " has been deleted successfully");
        memory.Remove(memory[index]);

        Console.ReadLine();    
    }

    static void DisplayMatrix()
    {
        Console.Clear();
        Console.WriteLine("Please choose the index of the matrix you want to display: ");

        int index = Convert.ToInt32(AnsiConsole.Prompt(
        new TextPrompt<string>("Available Indeces: ")
        .AddChoices(NumberToArray(memory.Count))));

        Console.Clear();
        Console.WriteLine("Matrix " + index.ToString() + " :");
        Console.WriteLine(memory[index]); 

        Console.ReadLine();
    }

    #endregion

    static string[] NumberToArray(int n)
    {
        string[] output = new string[n];
        for (int i = 0; i < n; i++)
        {
            output[i] = i.ToString();
        }

        return output;
    }
}
