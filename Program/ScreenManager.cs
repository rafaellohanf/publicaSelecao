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
        /// <summary>
        /// Enumerator with the screens, making easy the acces and change of screens
        /// </summary>
        enum Screens
        {
            MAIN_SCREEN,
            ADD_SCORE,
            VIEW_SCORES,
            WELCOME_SCREEN,
            SAVE_SCREEN,
            RESET_DATA,
            EXIT,
            VIEW_STATS
        }

        /// <summary>
        /// Simgleton reference
        /// </summary>
        private static ScreenManager singInstance;

        /// <summary>
        /// The score table the screen will have to work with
        /// </summary>
        private ScoreTable scoreTable;
       
        /// <summary>
        /// Controls the screen showing loop, once is false the application will end
        /// </summary>
        bool isRunning;

        /// <summary>
        /// The current Screen it's being displayed
        /// </summary>
        Screens curScreen;

        /// <summary>
        /// The screen will be displayed next by the start of next cycle
        /// </summary>
        Screens nextScreen;

        /// <summary>
        /// Private empty constructor, Singleton's purpose
        /// </summary>
        private ScreenManager()
        {
        }

        /// <summary>
        /// Instantiate the single instance of this class
        /// </summary>
        /// <returns>A reference to the single instance</returns>
        public static ScreenManager Instance()
        {
            if (singInstance == null)
            {
                singInstance = new ScreenManager();
            }
            return singInstance;
        }

        /// <summary>
        /// Starts the application receiving the score table this class will work with
        /// </summary>
        /// <param name="scoreTable">The score table this class will work with</param>
        public void Start(ScoreTable scoreTable)
        {
            isRunning = true;
            curScreen = Screens.WELCOME_SCREEN;
            this.scoreTable = scoreTable;
        }

        /// <summary>
        /// The loop of screen displaying, it will call other displays
        /// </summary>
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

                    case Screens.RESET_DATA:
                        ResetScores();
                        break;

                    case Screens.SAVE_SCREEN:
                        SavingScreen();
                        break;

                    case Screens.EXIT:
                        ExitScreen();
                        break;

                }
                curScreen = nextScreen;
                Console.Clear();
            }
        }

        /// <summary>
        /// Displays the welcome message
        /// </summary>
        public void DisplayWelcome()
        {
            Console.WriteLine("Bem vindo ao Score Tracker! Uma simples aplicação de gerenciamento de placares.");
            Console.WriteLine("Este programa foi desenvolvido por Rafael Lohan.\n");

            Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
            Console.ReadKey();
            nextScreen = Screens.MAIN_SCREEN;
        }

        /// <summary>
        /// Displays the main screen
        /// </summary>
        public void DisplayMain()
        {
            Console.WriteLine("Digite a opção escolhida:\n");
            Console.WriteLine("1 - Adicionar Placar.");
            Console.WriteLine("2 - Ver Lista de Placares.");
            Console.WriteLine("3 - Ver Estatísticas");
            Console.WriteLine("4 - Reiniciar Placares");
            Console.WriteLine("5 - Sair\n");
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
                nextScreen = Screens.RESET_DATA;
            }
            else if(option == '5' | option == '\u001b')
            {
                nextScreen = Screens.SAVE_SCREEN;
            }
        }

        /// <summary>
        /// Displays the add score screen
        /// </summary>
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

            if (!isdigital || option == "")
            {
                if (option.ToUpper() == "V")
                {
                    nextScreen = Screens.MAIN_SCREEN;
                }
                else
                {
                    Console.Write(CenterString("Você precisa digitar um número inteiro.", 72));
                    Console.WriteLine("");
                    Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                    Console.ReadKey();
                }
            }
            else
            {
                int scorePoints = Int16.Parse(option);
                bool isValid = scoreTable.AddScore(scorePoints);
                if (isValid)
                {
                    Console.WriteLine(CenterString("Placar Adicionado com sucesso!", 72));
                    Console.WriteLine("");
                    Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine(CenterString("O placar ultrapassa a faixa de 1 > 999", 72));
                    Console.WriteLine("");
                    Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                    Console.ReadKey();
                }
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Display the view scores screen
        /// </summary>
        private void DisplayViewScores()
        {
            if (scoreTable.GetScoreList().Count == 0)
            {
                Console.WriteLine(CenterString("- A lista de placares está vazia -\n",72));
                Console.WriteLine(CenterString("<Pressione qualquer tecla para voltar>", 72));
                Console.ReadKey();
                nextScreen = Screens.MAIN_SCREEN;
            }
            else
            {

                Console.WriteLine("VISUALIZADOR DE PLACARES\n");
                Console.WriteLine("   ID   |   Placar   |   Recorde  ");
                String dispId, dispScore, dispRecorde = "";
                foreach (Score scr in scoreTable.GetScoreList())
                {
                    Console.WriteLine("----------------------------------");
                    dispId = CenterString(scr.GetId().ToString(), 8);
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
                Console.WriteLine(CenterString("\n<Pressione qualquer tecla para voltar>", 72));
                Console.ReadKey();
                nextScreen = Screens.MAIN_SCREEN;
            }
        }

        /// <summary>
        /// Display the view stats screen(Number of scores, high scores and low scores)
        /// </summary>
        public void DisplayViewStats()
        {
            String nJogos = CenterString(scoreTable.GetScoreList().Count.ToString(), 10);
            String nHighScore = CenterString(scoreTable.getNHighScore().ToString(), 13);
            String nLowScore = CenterString(scoreTable.getNLowScore().ToString(), 13);

            Console.WriteLine(CenterString("<Estatísticas da tabela>", 72));
            Console.WriteLine("\n N. Jogos | High Scores | Low Scores  ");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(nJogos + "|" + nHighScore + "|" + nLowScore);
            Console.WriteLine("");

            Console.WriteLine(CenterString("<Pressione qualquer tecla para voltar>", 72));
            Console.ReadKey();
            nextScreen = Screens.MAIN_SCREEN;
        }

        /// <summary>
        /// Display the options to reset the current score table
        /// </summary>
        private void ResetScores()
        {
            Console.WriteLine("Você deseja mesmo reinciar os placares?\n");
            Console.WriteLine("S - Sim");
            Console.WriteLine("V - Voltar\n");

            char option = Console.ReadKey().KeyChar;
            if (char.ToUpper(option) == 'S')
            {
                scoreTable.Restart();

                Console.WriteLine(CenterString("Tabela reiniciada com sucesso!", 72));
                Console.WriteLine("");
                Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                Console.ReadKey();
                nextScreen = Screens.MAIN_SCREEN;
            }
            else if (char.ToUpper(option) == 'V')
            {
                nextScreen = Screens.MAIN_SCREEN;
            }
            else
            {
                Console.WriteLine(CenterString("<Valor Inválido>", 72));
                Console.WriteLine("");
                Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Display the saving screen
        /// </summary>
        private void SavingScreen()
        {
            Console.WriteLine("Você deseja salvar sua tabela antes de sair?\n");
            Console.WriteLine("S - SIM");
            Console.WriteLine("N - NÃO");
            Console.WriteLine("V - Voltar para o menu principal\n");

            char option = Console.ReadKey().KeyChar;
            if (char.ToUpper(option) == 'S')
            {
                try
                {
                    FileManager.Save<ScoreTable>(scoreTable.getFileName(), scoreTable);
                    Console.WriteLine(CenterString("Dados Salvos com sucesso!", 72));
                    Console.WriteLine("");
                    Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                    Console.ReadKey();
                    nextScreen = Screens.EXIT;
                }
                catch
                {
                    Console.WriteLine(CenterString("Não foi possível Salvar os placares.", 72));
                    Console.WriteLine("");
                    Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                    Console.ReadKey();
                    nextScreen = Screens.MAIN_SCREEN;
                }
                
            }
            else if (char.ToUpper(option) == 'N')
            {
                nextScreen = Screens.EXIT;
            }
            else if (char.ToUpper(option) == 'V')
            {
                nextScreen = Screens.MAIN_SCREEN;
            }
            else
            {
                Console.WriteLine(CenterString("<Valor Inválido>", 72));
                Console.WriteLine("");
                Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Displays the exit screen
        /// </summary>
        private void ExitScreen()
        {
            Console.WriteLine("");
            Console.WriteLine(CenterString("A aplicação será fechada",72));
            Console.WriteLine("");
            Console.WriteLine(CenterString("<Pressione qualquer tecla para continuar>", 72));
            Console.ReadKey();
            isRunning = false;
        }

        /// <summary>
        /// Helper method to centralize a string in a range of spaces
        /// </summary>
        /// <param name="s">the string to be centralized</param>
        /// <param name="spaces">number of spaces you want this string to be centralized in</param>
        /// <returns>centralized string</returns>
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