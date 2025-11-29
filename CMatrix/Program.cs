using CMatrix.Classes;
using CMatrix.MatrixClasses;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CMatrix;

internal partial class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Menu();
        }

    }



}
