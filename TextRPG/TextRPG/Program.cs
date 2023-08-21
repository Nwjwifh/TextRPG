using System.Numerics;

internal class GameManager
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
        // 캐릭터 정보와 인벤토리 초기화
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
        inventory = new Inventory();

    }

    //게임 인트로 창 표시
    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
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
                DisplayMyInfo(); //상태보기 창 표시
                break;

            case 2:
                DisplayInventory(); //인벤토리 창 표시
                break;
        }
    }

    //상태보기 창 표시
    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine(player.DisplayStats()); //캐릭터 정보 표시

        Console.WriteLine();
        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro(); //게임 인트로창으로 돌아감
                break;
        }
    }

    //인벤토리 창 표시
    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        inventory.DisplayItems(); //아이템 목록 표시

        Console.WriteLine("1.장착 관리");
        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro(); //게임 인트로창으로 돌아감
                break;

            case 1:
                DisplayEquipInventory(); //인벤토리 - 장착 관리창 표시
                break;
        }
    }

    //인벤토리 - 장착 관리창 표시
    static void DisplayEquipInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        inventory.DisplayItemsEquip(); //장착 관리용 아이템 목록 표시

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

        inventory.ChangeEquip(input - 1, player); // 장착 상태 변경
        DisplayEquipInventory(); // 장착 상태에 따라 새로 업데이트된 창 표시
    }

    //입력값 검사 메서드, min과 max는 유효한 입력값의 범위
    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            //string을 int형으로 변환하려고 시도, 성공시 변환된 int값을 ret에 할당.
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
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set;  }
    public int Gold { get; set; }

    private int initialAtk;
    private int initialDef;

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        //캐릭터 속성
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
        initialAtk = atk;  // 초기 공격력
        initialDef = def;  // 초기 방어력
    }

    //캐릭터 스탯을 문자열로 변환하여 반환하는 메서드
    public string DisplayStats()
    {
        string bonusAtkStr = "";
        string bonusDefStr = "";

        //아이템을 장착하여 현재 수치와 초기 수치가 다른경우 추가 수치 표시
        if (Atk != initialAtk)
        {
            bonusAtkStr = $"(+{Atk - initialAtk})";
        }
        if (Def != initialDef)
        {
            bonusDefStr = $"(+{Def - initialDef})";
        }
   
        return ($"{Name}({Job})\n" +
               $"Lv.{Level}\n" +
               $"공격력 : {Atk}{bonusAtkStr}\n" +
               $"방어력 : {Def}{bonusDefStr}\n" +
               $"체력 : {Hp}\n" +
               $"Gold : {Gold} G");
    }
}

public class Item
{
    //아이템 속성
    public string Name { get; }
    public string Description { get; }
    public int BonusAtk { get; }
    public int BonusDef { get; }
    public bool IsEquipped { get; set; }

    public Item(string name, string description, int bonusAtk, int bonusDef)
    {
        Name = name;
        Description = description;
        BonusAtk = bonusAtk;
        BonusDef = bonusDef;
        IsEquipped = false;
    }
}


public class Inventory
{
    public List<Item> items; // 아이템 리스트
    

    public Inventory()
    {
        items = new List<Item>();
        items.Add(new Item("검", "공격력 +2 | 그냥 검이다.", 2, 0));
        items.Add(new Item("갑옷",  "방어력 +5 | 그냥 갑옷이다.", 0, 5));
    }

    //인벤토리 아이템 리스트 출력
    public void DisplayItems()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < items.Count; i++)
        {
            string equippedSign = "";
            if (items[i].IsEquipped)
            {
                equippedSign = "[E]";
            }
            Console.WriteLine($"- {equippedSign}{items[i].Name} | {items[i].Description}");
        }

        Console.WriteLine();
    }

    //인벤토리-장착관리 아이템 리스트 출력(번호만 추가한 버전)
    public void DisplayItemsEquip()
    {
        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < items.Count; i++)
        {
            string equippedSign = "";
            if (items[i].IsEquipped)
            {
                equippedSign = "[E]";
            }
            Console.WriteLine($"- {i + 1}.{equippedSign}{items[i].Name} | {items[i].Description}");
        }

        Console.WriteLine();
    }

    //특정 인덱스의 아이템의 장착 상태 변경
    public void ChangeEquip(int index, Character character)
    {
        Item item = items[index]; // 선택한 아이템 가져오기

        // 아이템의 장착 상태 변경(반전)
        item.IsEquipped = !item.IsEquipped;

        if (item.IsEquipped)
        {
            character.Atk += item.BonusAtk;
            character.Def += item.BonusDef;
        }
        else
        {
            character.Atk -= item.BonusAtk;
            character.Def -= item.BonusDef;
        }
    }
}