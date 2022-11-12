using System.Reflection.Metadata;

using StoreLib;
using static System.Console;
using static System.Console;

WriteLine("Menu with Delegate");
int x = 10;
object GetParam2()
{
    return x;
}
object GetParam3()
{
    return Menu.GetString("Enter word: ");
}
object GetParam4()
{
    string name = Menu.GetString("Enter name: ");
    int age = Menu.GetInt("Enter age: ");
    return new Person(name, age);
}

Func<Menu.Command[]> initMenu = () => new Menu.Command[]
{
    new Menu.Command("CmdNull", (int)AppCommand.Command1, Method1, null),
    new Menu.Command("CommandInt", (int)AppCommand.Command2, Method2, GetParam2),
    new Menu.Command("CommandStr", (int)AppCommand.Command3, Method3, GetParam3),
    new Menu.Command("CommandPerson", (int)AppCommand.Command4, Method4, GetParam4),
    new Menu.Command("Exit", (int)AppCommand.Exit, null, null)
};
Menu menu = new Menu(initMenu);
menu.Run();

void Method1(object par1 = null)
{
    WriteLine("Method1");
    string some = "Enter some: ".Read();
    Console.WriteLine(some);
}
void Method2(object number)
{
    WriteLine("Method2");
    Console.WriteLine((int)number);
}
void Method3(object word)
{
    WriteLine("Method3");
    Console.WriteLine((string)word);
}
void Method4(object person)
{
    WriteLine("Method4");
    Person p = person as Person;
    Console.WriteLine($"{p.name} : {p.age}");
}

enum AppCommand 
{ 
    Command1 = 1, 
    Command2,
    Command3,
    Command4,
    Exit = 0
};

class Person
{
    public string name;
    public int age;
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }   
}


