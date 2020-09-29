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
            SAVE_SCREEN,
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
                Console.WriteLine(CenterString("==================< SCORE TRACKER >==================\n", 72));
                switch (curScreen)
                {
                    case Screens.WELCOME_SCREEN:
                        DisplayWelcome();
                        break;

                    case Screens.MAIN_SCREEN:
                        DisplayMain();
                        break;

                    case Screens.ADD_SCORE:
                        DisplayAddScore();
                        break;

                    case Screens.VIEW_SCORES:
                        DisplayViewScores();
                        break;

                    case Screens.VIEW_STATS:
                        DisplayViewStats();
                        break;

                    case Screens.EXIT:
                        SavingScreen();
                        break;

                }
                curScreen = nextScreen;
                Console.Clear();
            }
        }

        public void DisplayWelcome()
        {
            Console.WriteLine("Bem vindo ao Score Tracker! Uma simples aplicação de gerenciamento de placares.");
            Console.WriteLine("Este programa foi desenvolvido por Rafael Lohan.\n");

            Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
            Console.ReadKey();
            nextScreen = Screens.MAIN_SCREEN;
        }

        public void DisplayMain()
        {
            Console.WriteLine("Digite a opção escolhida:\n");
            Console.WriteLine("1 - Adicionar placar.");
            Console.WriteLine("2 - Ver lista de placares.");
            Console.WriteLine("3 - Ver estatísticas");
            Console.WriteLine("4 - Sair\n");
            char option = Console.ReadKey().KeyChar;

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
        }

        public void DisplayAddScore()
        {

            Console.WriteLine("ADICIONAR UM PLACAR\n");
            Console.WriteLine("O placar deverá ser um número entre 1 e 999.");
            Console.WriteLine("V - voltar\n");
            Console.Write("Digite o número do placar que você deseja adicionar:");
            String option = Console.ReadLine().ToString();
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
                if (option.ToUpper() == "V")
                {
                    nextScreen = Screens.MAIN_SCREEN;
                }
                else
                {
                    Console.Write("\nVocê precisa digitar um número inteiro.");
                    Console.ReadKey();
                }
            }
            else
            {
                int scorePoints = Int16.Parse(option);
                bool isValid = scoreTable.AddScore(scorePoints);
                if (isValid)
                {
                    Console.WriteLine("Placar Adcionado com Sucesso!");
                }
                else
                {
                    Console.WriteLine("O placar ultrapassa a faixa de 1 > 999");
                }
                Console.ReadKey();
            }
        }

        private void DisplayViewScores()
        {
            Console.WriteLine("VISUALIZADOR DE PLACARES\n");
            if (scoreTable.GetScoreList().Count == 0)
            {
                Console.WriteLine(CenterString("- A lista de placares está vazia -\n",72));
                Console.WriteLine(CenterString("<Pressione qualquer tecla para voltar>", 72));
                Console.ReadKey();
                nextScreen = Screens.MAIN_SCREEN;
            }
            else
            {
                Console.WriteLine("     ID     |   Placar   |   Recorde  ");
                String dispId, dispScore, dispRecorde = "";
                foreach (Score scr in scoreTable.GetScoreList())
                {
                    Console.WriteLine("--------------------------------");
                    dispId = CenterString(scr.GetId().ToString(), 12);
                    dispScore = CenterString(scr.GetPoints().ToString(), 12);
                    switch (scr.GetSCoreType())
                    {
                        case Score.ScoreType.STANDARD:
                            dispRecorde = CenterString("-Nenhum-", 12);
                            break;

                        case Score.ScoreType.HIGH_SCORE:
                            dispRecorde = CenterString("High Score", 12);
                            break;

                        case Score.ScoreType.LOW_SCORE:
                            dispRecorde = CenterString("Low Score", 12);
                            break;
                    }

                    Console.WriteLine(dispId + "|" + dispScore + "|" + dispRecorde);
                }
                Console.ReadKey();
            }
        }

        public void DisplayViewStats()
        {

        }

        private void SavingScreen()
        {
            Console.WriteLine("Você deseja salvar sua tabela antes de sair?");
            Console.WriteLine("S - SIM");
            Console.WriteLine("N - NÃO");
            char option = Console.ReadKey().KeyChar;
            Console.Clear();
            if (char.ToUpper(option) == 'S')
            {
                try
                {
                    FileManager.Save<ScoreTable>(scoreTable.getFileName(), scoreTable);
                }
                catch
                {
                    Console.WriteLine(CenterString("Não foi possível Salvar os placares.", 72));
                    Console.ReadKey();
                }
            }
            else if (char.ToUpper(option) == 'N')
            {
                nextScreen = Screens.EXIT;
            }
            else
            {
                Console.WriteLine(CenterString("-Valor Inválido-", 72));
            }
        }

        private void ExitScreen()
        {
            Console.WriteLine("\n<Pressione qualquer tecla para continuar>");
            Console.ReadKey();
            isRunning = false;
        }

        private String CenterString(String s, int spaces)
        {
            int leeway1 = (int)((spaces - s.Length) / 2);
            int leeway2 = spaces - s.Length - leeway1;
            String spaces1 = "";
            String spaces2 = "";

            for (int i = 0; i < leeway1; i++)
            {
                spaces1 += " ";
            }

            for (int i = 0; i < leeway2; i++)
            {
                spaces2 += " ";
            }

            return spaces1 + s + spaces2;
        }
    }
}