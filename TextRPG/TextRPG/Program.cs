
internal class Program
{
    private static Character player; //캐릭터 객체
    private static Inventory inventory; //인벤토리 객체

    static void Main(string[] args)
    {
        GameDataSetting(); //게임 데이터 세팅
        DisplayGameIntro(); //게임 인트로 표시
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
        
        // 아이템 정보 세팅
        inventory = new Inventory();

    }

    //게임 인트로 창 표시
    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1.상태보기");
        Console.WriteLine("2.인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                // 작업해보기
                DisplayInventory();
                break;
        }
    }

    //상태보기 창 표시
    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    //인벤토리 창 표시
    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        inventory.DisplayItems();

        Console.WriteLine("1.장착 관리");
        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                DisplayEquipInventory();
                break;
        }
    }

    //장착 관리 창 표시
    static void DisplayEquipInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        inventory.DisplayItemsEquip();

        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, inventory.items.Count);
        if (input == 0)
        {
            DisplayInventory();
            return;
        }

        inventory.ChangeEquip(input - 1); // 장착 상태 변경
        DisplayEquipInventory(); // 장착 상태에 따라 새로 업데이트된 창 표시
    }

    //입력값 검사 메서드, min과 max는 유효한 입력값의 범위
    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            //string 변수인 input을 int형으로 변환하려고 시도, 성공시, 변환된 int값을 ret에 할당.
            bool parseSuccess = int.TryParse(input, out var ret);
            
            //지정된 범위 내에 변환된 int값(ret)이 있는지 확인 후, 반환
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            //입력이 정수가 아니거나 범위를 벗어난 경우 실행
            Console.WriteLine("잘못된 입력입니다.\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }
    }
}


public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}



public class Inventory
{
    public List<string> items; // 아이템 리스트
    private List<bool> equip; // 아이템 장착 상태 리스트

    public Inventory()
    {
        items = new List<string>();
        items.Add("검 | 공격력 +2 | 그냥 검이다.");
        items.Add("갑옷 | 방어력 +5 | 그냥 갑옷이다");

        equip = new List<bool>();
        equip.Add(false);
        equip.Add(false);
    }

    //인벤토리 아이템 리스트 출력
    public void DisplayItems()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < items.Count; i++)
        {
            string equippedSign = equip[i] ? "[E]" : "";
            Console.WriteLine($"- {equippedSign}{items[i]}");
        }

        Console.WriteLine();
    }

    //인벤토리-장착관리 아이템 리스트 출력
    public void DisplayItemsEquip()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < items.Count; i++)
        {
            string equippedSign = equip[i] ? "[E]" : "";
            Console.WriteLine($"- {i + 1}.{equippedSign}{items[i]}");
        }

        Console.WriteLine();
    }

    public void ChangeEquip(int index)
    {
        equip[index] = !equip[index];
    }
}