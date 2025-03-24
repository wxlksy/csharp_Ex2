using System;

namespace ChessGame
{
    /// <summary>
    /// Основной класс программы, который запускает игру.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }

    /// <summary>
    /// Класс для управления игровой логикой.
    /// </summary>
    class GameManager
    {
        public void StartGame()
        {
            Console.WriteLine("Введите исходные данные (например: ферзь d3 слон e1 d8):");
            string input = Console.ReadLine();

            string[] pieces = input.Split();
            if (pieces.Length != 5)
            {
                Console.WriteLine("Неверный формат ввода.");
                return;
            }

            string whitePiece = pieces[0];
            (int x1, int y1) = ChessBoard.ParsePosition(pieces[1]);
            string blackPiece = pieces[2];
            (int x2, int y2) = ChessBoard.ParsePosition(pieces[3]);
            (int x3, int y3) = ChessBoard.ParsePosition(pieces[4]);

            bool canMove = CanMove(whitePiece, (x1, y1), blackPiece, (x2, y2), (x3, y3));
            bool underAttack = IsUnderAttack((x3, y3), blackPiece, (x2, y2));

            if (canMove && !underAttack)
            {
                Console.WriteLine($"{whitePiece.ToUpper()} дойдет до {pieces[4]}");
            }
            else
            {
                Console.WriteLine($"{whitePiece.ToUpper()} не дойдет до {pieces[4]}");
            }

            ChessBoard.DrawChessBoard(whitePiece, (x1, y1), blackPiece, (x2, y2), (x3, y3));
        }

        private bool CanMove(string whitePiece, (int, int) whitePos, string blackPiece, (int, int) blackPos, (int, int) targetPos)
        {
            // Логика для проверки возможности хода
            // Пример для ферзя:
            if (whitePiece == "ферзь")
            {
                if (whitePos.Item1 == targetPos.Item1 || whitePos.Item2 == targetPos.Item2 ||
                    Math.Abs(whitePos.Item1 - targetPos.Item1) == Math.Abs(whitePos.Item2 - targetPos.Item2))
                {
                    return true;
                }
            }

            // Проверка для других фигур...

            return false;
        }

        private bool IsUnderAttack((int, int) targetPos, string blackPiece, (int, int) blackPos)
        {
            // Логика для проверки угрозы
            // Пример для слона:
            if (blackPiece == "слон")
            {
                if (Math.Abs(targetPos.Item1 - blackPos.Item1) == Math.Abs(targetPos.Item2 - blackPos.Item2))
                {
                    return true;
                }
            }

            // Проверка для других фигур...

            return false;
        }
    }

    /// <summary>
    /// Класс для представления шахматной доски и отрисовки фигур.
    /// </summary>
    static class ChessBoard
    {
        public static (int, int) ParsePosition(string position)
        {
            int x = position[0] - 'a';
            int y = 8 - (position[1] - '0');
            return (x, y);
        }

        public static void DrawChessBoard(string whitePiece, (int, int) whitePos, string blackPiece, (int, int) blackPos, (int, int) targetPos)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (whitePos == (x, y))
                    {
                        Console.Write("W ");
                    }
                    else if (blackPos == (x, y))
                    {
                        Console.Write("B ");
                    }
                    else if (targetPos == (x, y))
                    {
                        Console.Write("T ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

