namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("마을에 오신 여러분 환영합니다!\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            string[] startMenu = new string[2] { "상태보기", "인벤토리" };
            Console.WriteLine();

            for (int i = 0; i < startMenu.Length; i++)
            {
                Console.WriteLine((i + 1) + "." + startMenu[i]);
            }

            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string menuInput = Console.ReadLine();

            switch (menuInput)
            {
                case "1":

                    myInfo();
                    break;
                case "2":
                    Console.WriteLine("인벤토리 입장");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다!");
                    break;
            }


            static void myInfo()
            {
                Console.Clear();
                Console.WriteLine("상태보기 입장");
            }
        }
    }
}