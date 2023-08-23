using ConsoleTables;
using System.Numerics;

internal class GameManager
{
    private static Character player;
    private static Inventory inventory;
    private static Shop shop;


    static void Main(string[] args)
    {
        GameDataSetting(); //게임 데이터 세팅
        DisplayGameIntro(); //게임 인트로 표시

    }

    static void GameDataSetting()
    {
        // 캐릭터 정보와 인벤토리 초기화
        player = new Character("김전사", "전사", 1, 10, 5, 100, 1500);
        inventory = new Inventory();
        shop = new Shop();

    }

    //게임 인트로 창 표시
    static void DisplayGameIntro()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[스파르타 마을]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine("1.상태보기");
        Console.WriteLine("2.인벤토리");
        Console.WriteLine("3.상점");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                Console.Beep(300,200);
                DisplayMyInfo(); //상태보기 창 표시
                break;

            case 2:
                Console.Beep(300, 200);
                DisplayInventory(); //인벤토리 창 표시
                break;
            case 3:
                Console.Beep(300, 200);
                DisplayShop();
                break;
        }
    }

    //상태보기 창 표시
    static void DisplayMyInfo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[상태보기]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("캐릭터의 상태 정보가 표시됩니다.");
        Console.WriteLine();
        Console.ResetColor();

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
                Console.Beep(300, 200);
                DisplayGameIntro(); //게임 인트로창으로 돌아감
                break;
        }
    }

    //인벤토리 창 표시
    static void DisplayInventory()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[인벤토리]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.ResetColor();

        inventory.DisplayItemsEquip(false); //아이템 목록 표시

        Console.WriteLine("1.장착 관리");
        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                Console.Beep(300, 200);
                DisplayGameIntro(); //게임 인트로창으로 돌아감
                break;

            case 1:
                Console.Beep(300, 200);
                DisplayEquipInventory(); //인벤토리 - 장착 관리창 표시
                break;
        }
    }

    //인벤토리 - 장착 관리창 표시
    static void DisplayEquipInventory()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[인벤토리 - 장착관리]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.ResetColor();

        inventory.DisplayItemsEquip(true); //장착 관리용 아이템 목록 표시

        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, inventory.items.Count);
        if (input == 0)
        {
            Console.Beep(300, 200);
            DisplayInventory();
            return;
        }

        inventory.ChangeEquip(input - 1, player); // 장착 상태 변경
        DisplayEquipInventory(); // 장착 상태에 따라 새로 업데이트된 창 표시
    }

    //상점 창 표시
    static void DisplayShop()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[상점]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("골드로 아이템을 구매할 수 있습니다.\n");
        Console.ResetColor();

        Shop shop = new Shop();
        shop.DisplayShopItems(); // 상점에서 판매 중인 아이템 목록 표시

        Console.WriteLine("0.나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, shop.shopItems.Count);
        if (input == 0)
        {
            Console.Beep(300, 200);
            DisplayGameIntro();
            return;
        }

        shop.BuyItem(input - 1, player, inventory); // 아이템 구매
        Thread.Sleep(1000);
        DisplayShop();
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
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 2; i++)
                Console.Beep();
            Console.WriteLine("잘못된 입력입니다!\n");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }
    }
}


public class Character
{
    public string Name { get; set; }
    public string Job { get; set; }
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

        return $"{Name}(Lv.{Level})\n" +
                $"직업    : {Job}\n" +
                $"공격력  : {Atk}{bonusAtkStr}\n" +
                $"방어력  : {Def}{bonusDefStr}\n" +
                $"체력    : {Hp}\n" +
                $"골드    : {Gold} G";
    }
}

public class Item
{
    //아이템 속성
    public string Name { get; set; }
    public string EffectDescription { get; set; }
    public string Description { get; set; }
    public int BonusAtk { get; set; }
    public int BonusDef { get; set; }
    public bool IsEquipped { get; set; }
    public int Price { get; set; }

    public Item(string name, string effectDescription, string description, int bonusAtk, int bonusDef, int price)
    {
        Name = name;
        EffectDescription = effectDescription;
        Description = description;
        BonusAtk = bonusAtk;
        BonusDef = bonusDef;
        IsEquipped = false;
        Price = price;
    }
}


public class Inventory
{
    public List<Item> items; // 아이템 리스트
    
    public Inventory()
    {
        items = new List<Item>();
        items.Add(new Item("Wood Sword", "ATK+2", "A normal wood sword.", 2, 0, 0));
        items.Add(new Item("Wood Shield", "DEF+5", "A normal wood armor.", 0, 5, 0));
    }

    //인벤토리 아이템 리스트 출력
    public void DisplayItemsEquip(bool showItemNumbers)
    {
        var table = new ConsoleTable("Name", "Effects", "Description");

        for (int i = 0; i < items.Count; i++)
        {
            string equippedSign = "";
            if (items[i].IsEquipped)
            {
                equippedSign = "[E]";
            }

            string itemNumber = "";
            if (showItemNumbers) //아이템 번호가 표시되는 경우
            {
                itemNumber = $"{i + 1}.";
            }

            string itemName = $"{itemNumber}{equippedSign}{items[i].Name}";
            string itemEffect = items[i].EffectDescription;
            string itemDescription = items[i].Description;

            table.AddRow(itemName, itemEffect, itemDescription);
        }
        table.Write(Format.Alternative);
    }


    //특정 인덱스의 아이템의 장착 상태 변경
    public void ChangeEquip(int index, Character character)
    {
        Item item = items[index]; // 사용자가 선택한 아이템 가져오기

        // 아이템의 장착 상태 변경
        item.IsEquipped = !item.IsEquipped;

        if (item.IsEquipped) //장착 시 스탯 증가
        {
            character.Atk += item.BonusAtk;
            character.Def += item.BonusDef;
        }
        else //장착 해제 시 스탯 감소
        {
            character.Atk -= item.BonusAtk;
            character.Def -= item.BonusDef;
        }
    }

    //아이템 삭제
    //public void ItemRemove(int index)
    //{
    //    items.RemoveAt(index);
    //}
}

public class Shop
{
    public List<Item> shopItems; //상점 아이템 리스트

    public Shop()
    {
        shopItems = new List<Item>();
        shopItems.Add(new Item("Wood Spear", "ATK+3", "A normal wood spear.", 3, 0, 50));
        shopItems.Add(new Item("Iron Sword", "ATK+5", "A normal iron sword.", 5, 0, 1000));
        shopItems.Add(new Item("Iron Shield", "DEF+10", "A normal iron shield.", 0, 10, 150));
    }

    public void DisplayShopItems()
    {
        var table = new ConsoleTable("Name", "Effects", "Description", "Price");

        for (int i = 0; i < shopItems.Count; i++)
        {
            string itemName = $"{i + 1}.{shopItems[i].Name}";
            string itemEffect = shopItems[i].EffectDescription;
            string itemDescription = shopItems[i].Description;
            int itemPrice = shopItems[i].Price;

            table.AddRow(itemName, itemEffect, itemDescription, itemPrice + "G");
        }
        table.Write(Format.Alternative);
    }

    public void BuyItem(int index, Character player, Inventory inventory)
    {
        Item item = shopItems[index]; // 사용자가 선택한 상점 아이템 가져오기

        if (player.Gold >= item.Price) // 골드가 충분한지 확인
        {
            player.Gold -= item.Price; // 골드 차감
            inventory.items.Add(item); // 인벤토리에 아이템 추가

            Console.Beep(300, 200);
            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
            Console.WriteLine($"남은 골드: {player.Gold} G");
        }
        else // 골드가 불충분한 경우
        {
            Console.Beep();
            Console.WriteLine("골드가 부족합니다.");
        }
    }
}
