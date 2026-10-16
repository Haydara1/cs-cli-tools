namespace Lissajous_curves;
using Raylib_cs;
using System.Diagnostics;

internal class Program
{
    static private int width;

    static public int Width
    {
        get { return width; }
        set { width = value * 120; }
    }

    static public int Radius = 50;

    static private int AngularVelocity = 5;

    unsafe static void Main(string[] args)
    {
        bool DrawCurves = false;

        Width = 6;

        Raylib.InitWindow(Width, Width, "Lissajous Curves");
        Raylib.SetTargetFPS(64);

        Console.Clear();

        double Time = 0;
        double* TmPtr = &Time;

        Random rnd = new Random();

        int[] AngularVelocities = new int[]
        {
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
            rnd.Next(-10, 10),
        };

        ReferenceCircle[] referenceCircles = new ReferenceCircle[]
        {
            new ReferenceCircle(Radius, 180, 60, AngularVelocity + AngularVelocities[0], TmPtr),
            new ReferenceCircle(Radius, 300, 60, AngularVelocity + AngularVelocities[1], TmPtr),
            new ReferenceCircle(Radius, 420, 60, AngularVelocity + AngularVelocities[2], TmPtr),
            new ReferenceCircle(Radius, 540, 60, AngularVelocity + AngularVelocities[3], TmPtr),
            new ReferenceCircle(Radius, 660, 60, AngularVelocity + AngularVelocities[4], TmPtr),
            new ReferenceCircle(Radius, 60, 180, AngularVelocity + AngularVelocities[5], TmPtr),
            new ReferenceCircle(Radius, 60, 300, AngularVelocity + AngularVelocities[6], TmPtr),
            new ReferenceCircle(Radius, 60, 420, AngularVelocity + AngularVelocities[7], TmPtr),
            new ReferenceCircle(Radius, 60, 540, AngularVelocity + AngularVelocities[8], TmPtr),
            new ReferenceCircle(Radius, 60, 660, AngularVelocity + AngularVelocities[9], TmPtr),
        };

        ReferenceCircle[] referenceCirclesNonRemovable = new ReferenceCircle[]
        {
            new ReferenceCircle(Radius, 180, 60, AngularVelocity + AngularVelocities[0], TmPtr, false),
            new ReferenceCircle(Radius, 300, 60, AngularVelocity + AngularVelocities[1], TmPtr, false),
            new ReferenceCircle(Radius, 420, 60, AngularVelocity + AngularVelocities[2], TmPtr, false),
            new ReferenceCircle(Radius, 540, 60, AngularVelocity + AngularVelocities[3], TmPtr, false),
            new ReferenceCircle(Radius, 660, 60, AngularVelocity + AngularVelocities[4], TmPtr, false),
            new ReferenceCircle(Radius, 60, 180, AngularVelocity + AngularVelocities[5], TmPtr, false),
            new ReferenceCircle(Radius, 60, 300, AngularVelocity + AngularVelocities[6], TmPtr, false),
            new ReferenceCircle(Radius, 60, 420, AngularVelocity + AngularVelocities[7], TmPtr, false),
            new ReferenceCircle(Radius, 60, 540, AngularVelocity + AngularVelocities[8], TmPtr, false),
            new ReferenceCircle(Radius, 60, 660, AngularVelocity + AngularVelocities[9], TmPtr, false),
        };

        int[] CurvesPointX = new int[5];
        int[] CurvesPointY = new int[5];


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            if (!DrawCurves)
            {
                Raylib.ClearBackground(Color.BLACK);

                for (int i = 0; i < 10; i++)
                {
                    (int x, int y) = referenceCircles[i].Update();

                    if (i < 5)
                        CurvesPointX[i] = x;
                    else
                        CurvesPointY[i - 5] = y;
                }

                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        Raylib.DrawCircle(CurvesPointX[i], CurvesPointY[j], 5, Color.YELLOW);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    (int x, int y) = referenceCirclesNonRemovable[i].Update();

                    if (i < 5)
                        CurvesPointX[i] = x;
                    else
                        CurvesPointY[i - 5] = y;
                }

                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        Raylib.DrawCircle(CurvesPointX[i], CurvesPointY[j], 1, Color.WHITE);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                DrawCurves = !DrawCurves;
                Raylib.ClearBackground(Color.BLACK);
            }

            Raylib.EndDrawing();

            Time += 0.01;
        }

        Raylib.CloseWindow();
    }

}
