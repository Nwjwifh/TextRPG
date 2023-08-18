namespace TextRPG
{
    internal class GameManager
    {
        static void Main(string[] args)
        {
            MainMenu();
            
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("마을에 오신 여러분 환영합니다!\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            string[] startMenu = new string[2] { "상태보기", "인벤토리" };
            Console.WriteLine();

            for (int i = 0; i < startMenu.Length; i++)
            {
                Console.WriteLine((i + 1) + "." + startMenu[i]);
            }

            string menuInput = GetUserInput();
            switch (menuInput)
            {
                case "1":
                    MyInfo();
                    break;
                case "2":
                    InventoryInfo();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다!");
                    break;
            }
        }

        static void MyInfo()
        {
            Console.Clear();
            Player player = new Player("김전사", "전사");

            Console.WriteLine("\n0.나가기");

            string menuInput = GetUserInput();
            switch (menuInput)
            {
                case "0":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다!");
                    break;
            }
        }

        static void InventoryInfo()
        {
            Console.Clear();

            Console.WriteLine("\n0.나가기");

            string menuInput = GetUserInput();
            switch (menuInput)
            {
                case "0":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다!");
                    break;
            }
        }

        static string GetUserInput()
        {
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            return Console.ReadLine();
        }
    }

    class Player
    {
        public string Name;
        public string Class;
        public int Level;
        public int Attack;
        public int Defense;
        public int Health;
        public int Gold;

        public Player(string name, string playerClass)
        {
            Name = name;
            Class = playerClass;
            Level = 1;
            Attack = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
            Console.WriteLine($"이름: {Name}");
            Console.WriteLine($"직업: {Class}");
            Console.WriteLine($"레벨: {Level}");
            Console.WriteLine($"공격력: {Attack}");
            Console.WriteLine($"방어력: {Defense}");
            Console.WriteLine($"체력: {Health}");
            Console.WriteLine($"골드: {Gold}");
        }
    }

    class Item
    {

    }
}