using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace ScoreTracker.Program
{
    class ScreenManager
    {
        enum Screens
        {
            MAIN_SCREEN,
            ADD_SCORE,
            VIEW_SCORES,
            WELCOME_SCREEN,
            EXIT,
            VIEW_STATS
        }

        private static ScreenManager singInstance;
        private ScoreTable scoreTable;
        bool isRunning;

        Screens curScreen;
        Screens nextScreen;

        private ScreenManager()
        {
        }

        public static ScreenManager Instance()
        {
            if (singInstance == null)
            {
                singInstance = new ScreenManager();
            }
            return singInstance;
        }

        public void Start(ScoreTable scoreTable)
        {
            isRunning = true;
            curScreen = Screens.WELCOME_SCREEN;
            this.scoreTable = scoreTable;
        }

        public void Display()
        {
            while (isRunning)
            {
                Console.WriteLine("====================< SCORE TRACKER >====================\n");

                if (curScreen == Screens.WELCOME_SCREEN)
                {
                    Console.WriteLine("Bem vindo ao Score Tracker! Uma simples aplicação de gerenciamento de placares.");
                    Console.WriteLine("Este programa foi desenvolvido por Rafael Lohan.\n");
                    Console.WriteLine("<Pressione qualquer tecla para continuar>");
                    Console.ReadKey();
                    nextScreen = Screens.MAIN_SCREEN;
                }
                else if (curScreen == Screens.MAIN_SCREEN)
                {
                    Console.WriteLine("Digite a opção escolhida:\n");
                    Console.WriteLine("1 - Adicionar placar.");
                    Console.WriteLine("2 - Ver lista de placares.");
                    Console.WriteLine("3 - Ver estatísticas");
                    Console.WriteLine("4 - Sair\n");
                    char option = Console.ReadKey().KeyChar;
                    Console.Clear();
                    if (option == '1')
                    {
                        nextScreen = Screens.ADD_SCORE;
                    }
                    else if (option == '2')
                    {
                        nextScreen = Screens.VIEW_SCORES;
                    }
                    else if (option == '3')
                    {
                        nextScreen = Screens.VIEW_STATS;
                    }
                    else if (option == '4')
                    {
                        nextScreen = Screens.EXIT;
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida.");
                        Console.WriteLine("\n<Pressione qualquer tecla para continuar>");
                        Console.ReadKey();
                    }
                }
                else if (curScreen == Screens.ADD_SCORE)
                {
                    Console.WriteLine("ADICIONAR UM PLACAR\n");
                    Console.WriteLine("O placar deverá ser um número superior a 0 e menor do que 1000.");
                    Console.Write("Digite o número do placar que você deseja adicionar:");
                    char[] option = Console.ReadLine().ToCharArray();
                    Console.Clear();

                    bool isdigital = true;
                    foreach (char c in option)
                    {
                        if (!Char.IsDigit(c))
                        {
                            isdigital = false;
                            break;
                        }
                    }
                    if (!isdigital)
                    {
                        Console.Write("\nVocê precisa digitar um número inteiro.");
                        Console.ReadKey();
                    }
                    else
                    {
                        bool isValid = scoreTable.AddScore(Convert.ToInt32(option));
                        if (isValid)
                        {

                        }
                        else
                        {

                        }
                    }
                
                }
                else if (curScreen == Screens.VIEW_SCORES)
                {
                    Console.WriteLine("VISUALIZADOR DE PLACARES\n");
                    if (scoreTable.GetScoreList().Count == 0)
                    {
                        Console.WriteLine(" - A lista de placares está vazia - ");
                        Console.ReadKey();
                        nextScreen = Screens.MAIN_SCREEN;
                    }
                    else
                    {
                        Console.WriteLine("    ID     |   Placar   |   Recorde  ");
                        foreach (Score scr in scoreTable.GetScoreList())
                        {
                            Console.WriteLine("--------------------------------");

                        }
                    }
                }
                else if (curScreen == Screens.VIEW_STATS)
                {

                }
                else if (curScreen == Screens.EXIT)
                {
                    Console.WriteLine("O programa está sendo finalizado.");
                    Console.WriteLine("\n<Pressione qualquer tecla para continuar>");
                    Console.ReadKey();
                    isRunning = false;
                }
                curScreen = nextScreen;
                Console.Clear();
            }
        }

        public void DisplayWelcomeScreen()
        {

        }

        public void DisplayMainSCreen()
        {

        }
        public void DisplayAddScore()
        {

        }
        public void DisplayViewScores()
        {

        }
      
        public void DisplayViewStats()
        {

        }
    }
}
