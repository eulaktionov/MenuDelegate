using System.Reflection.Metadata;
using System.Runtime.InteropServices;

using StoreLib;
using static System.Console;
using static System.Console;

WriteLine("Menu with Delegate");
int x = 10;
ParamInt GetParam2()
{
    return new ParamInt() { value = x };
}
ParamStr GetParam3()
{
    return new ParamStr()
    { value = Menu.GetString("Enter word: ") };
}
ParamPerson GetParam4()
{
    string name = Menu.GetString("Enter name: ");
    int age = Menu.GetInt("Enter age: ");
    return new ParamPerson
    { value = new Person(name, age) };
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

void Method1(Param par = null)
{
    WriteLine("Method1");
    string some = "Enter some: ".Read();
    Console.WriteLine(some);
}
void Method2(Param paramInt)
{
    WriteLine("Method2");
    int number = (paramInt as ParamInt).value;
    Console.WriteLine(number);
}
void Method3(Param paramStr)
{
    WriteLine("Method3");
    string word = (paramStr as ParamStr).value;
    Console.WriteLine(word);
}
void Method4(Param paramPerson)
{
    WriteLine("Method4");
    Person p = (paramPerson as ParamPerson).value;
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

class ParamInt : Param { public int value { get; set; } }
class ParamStr : Param { public string value { get; set; } }
class ParamPerson : Param { public Person value { get; set; } }

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


