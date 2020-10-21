using System;
using ConsoleApp2.Models;

#nullable enable
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var personTable = new ConsoleApp2.Generics.V2.PersonTable();
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add Person");
                Console.WriteLine("2. Search by Id");
                Console.WriteLine("3. search by age");
                Console.WriteLine("4. search by name");
                Console.WriteLine("5. List By Id");
                Console.WriteLine("6. Exit");
                var menuValue = Console.ReadLine();
                switch (menuValue)
                {
                    case "1": // Add
                    {
                        Console.WriteLine("Enter name");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter age");
                        var age = Console.ReadLine();
                        Console.WriteLine("Enter id");
                        var id = Console.ReadLine();
                        var person = new Person((name ?? string.Empty), Convert.ToInt32(age), Convert.ToInt64(id));
                        personTable.Add(person);
                        break;
                    }
                    case "2": //Search by Id
                    {
                        Console.WriteLine("enter Id");
                        var id = Convert.ToInt64(Console.ReadLine());
                        var people = personTable.SearchById(id);
                        if (people != null)
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            foreach (var person in people)
                            {
                                Console.WriteLine(person.ToString());
                            }
                            Console.WriteLine($"---------------------- End --------------------");
                        }
                        else
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            Console.WriteLine($"not found person id : {id}");
                            Console.WriteLine($"---------------------- End --------------------");
                        }

                        break;
                    }
                    case "3": // 3. search by age
                    {
                        Console.WriteLine("enter age");
                        var age = Console.ReadLine();
                        var people = personTable.SearchByAge(Convert.ToInt32(age));
                        if (people != null)
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            foreach (var person in people)
                            {
                                Console.WriteLine(person.ToString());
                            }
                            Console.WriteLine($"---------------------- End --------------------");
                        }
                        else
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            Console.WriteLine($"not found person age : {age}");
                            Console.WriteLine($"---------------------- End --------------------");
                        }

                        break;
                    }

                    case "4": //4. search by name
                    {
                        Console.WriteLine("enter name");
                        var name = Console.ReadLine();
                        var people = personTable.SearchByName(name);
                        if (people != null)
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            foreach (var person in people)
                            {
                                Console.WriteLine(person.ToString());
                            }
                            Console.WriteLine($"---------------------- End --------------------");
                        }
                        else
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            Console.WriteLine($"not found person age : {name}");
                            Console.WriteLine($"---------------------- End --------------------");
                        }
                        break;
                    }

                    case "5": //list by id
                    {
                        Console.WriteLine("List by id");
                        var people = personTable.GetAll();
                        if (people != null)
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            foreach (var person in people)
                            {
                                Console.WriteLine(person.ToString());
                            }
                            Console.WriteLine($"---------------------- End --------------------");
                        }
                        else
                        {
                            Console.WriteLine($"---------------------- Result --------------------");
                            Console.WriteLine($"not found person");
                            Console.WriteLine($"---------------------- End --------------------");
                        }
                        break;
                    }
                    case "6":
                        Console.WriteLine("Bye bye");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Bla bla bla");
                        break;
                }
            }
        }
    }
}


// comment for backup
// personTable.Add(new Person("Mark1", 228, 1));
// personTable.Add(new Person("Mark2", 2318, 2));
// personTable.Add(new Person("Mark3", 2238, 3));
// personTable.Add(new Person("Mark4", 2238, 4));
// personTable.Add(new Person("Mark4", 22384, 5));
//
// var sss1 = personTable.SearchById(1);
// var sss2 = personTable.SearchById(2);
// var sss3 = personTable.SearchById(3);
// var sss4 = personTable.SearchById(4);
// var sss5 = personTable.SearchById(5);
//
//
// var ss1 = personTable.SearchByAge(228);
// var ss2 = personTable.SearchByAge(2318);
// var ss3 = personTable.SearchByAge(2238);
// var ss4 = personTable.SearchByAge(4);           
//
// var s1 = personTable.SearchByName("Mark1");
// var s2 = personTable.SearchByName("Mark2");
// var s3 = personTable.SearchByName("Mark3");
// var s4 = personTable.SearchByName("Mark4");

//var dddd = personTable.GetAll();
//var ssssss = "sss";
// var dddd = new Storage<Person>("testing01.mk", 1028);
// var o1 = dddd.Write(new Person("Mark", 28, 1));
// var o2 = dddd.Write(new Person("Wuu", 30, 2));
// var o3 = dddd.Write(new Person("Pee", 39, 3));
// var ssss1 = dddd.Read(o1);
// var ssss2 = dddd.Read(o2);
// var ssss3 = dddd.Read(o3);


//var maxChar = 255;
//var number = 1;
//var IdByte = BitConverter.GetBytes(1);
// remark max 255 char = 1020 byte
//var nameBytes = Encoding.UTF32.GetBytes("testtststststts").PaddingRight(maxChar, Encoding.UTF32);
//var nameBytes = PaddingRightByte(),maxChar,Encoding.UTF32);
//var ageByte = BitConverter.GetBytes(2);
//var sssss = BitConverter.GetBytes(2);

//var strinffff = PaddingRightByte(nameBytes,maxChar,Encoding.UTF32);
//var sssssssssss= Encoding.UTF32.GetString(nameBytes);
// Console.WriteLine(sssssssssss);
//var sssssss = Combine(new[] {IdByte, nameBytes, ageByte});

// var mock1 = TestMock(1, "Mark", 28);
// var mock2 = TestMock(2, "Pee", 31);
//SaveFile(new[] {mock1, mock2});
//var dd = 1;
// }
//
// private static void SaveFile(byte[][] bytes)
// {
//     throw new NotImplementedException();
// }
//
// public static byte[] TestMock(int id, string name, int age)
// {
//     var maxChar = 255;
//     var idByte = BitConverter.GetBytes(id);
//     // remark max 255 char = 1020 byte
//     var nameBytes = Encoding.UTF32.GetBytes(name).PaddingRight(maxChar, Encoding.UTF32);
//     //var nameBytes = PaddingRightByte(),maxChar,Encoding.UTF32);
//     var ageByte = BitConverter.GetBytes(age);
//     //var sssss = BitConverter.GetBytes(2);
//
//     //var strinffff = PaddingRightByte(nameBytes,maxChar,Encoding.UTF32);
//     //var sssssssssss= Encoding.UTF32.GetString(nameBytes);
//     // Console.WriteLine(sssssssssss);
//     var result = Combine(new[] {idByte, nameBytes, ageByte});
//     return result;
// }
//
// private static byte[] PaddingRightByte2(byte[] nameBytes, int maxChar, Encoding encoding)
// {
//     var byteCount = encoding.GetBytes("1").Length;
//     var paddingCount = 0;
//     for (int i = 0; i < (nameBytes.Length % byteCount); i++)
//     {
//         var count = 0;
//         for (int j = 0; j < byteCount; j++)
//         {
//             if (nameBytes[i + j] == 0)
//             {
//                 count = count + 1;
//             }
//         }
//
//         if (count == byteCount)
//         {
//             paddingCount = paddingCount + 1;
//         }
//     }
//
//     var maxpadding = (paddingCount * byteCount);
//
//
//     var newArray = new byte[maxChar * byteCount];
//     // var startIndex = newArray.Length - nameBytes.Length;
//     // Array.Copy(nameBytes, 0, newArray, startIndex, nameBytes.Length);
//     return newArray;
// }
//
// private static byte[] Combine(params byte[][] arrays)
// {
//     byte[] rv = new byte[arrays.Sum(a => a.Length)];
//     int offset = 0;
//     foreach (byte[] array in arrays)
//     {
//         System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
//         offset += array.Length;
//     }
//
//     return rv;
// }


// public static byte[] ByteArrayLeftPad(byte[] input, byte padValue, int len)
// {
//     var temp = Enumerable.Repeat(padValue, len).ToArray();
//     for (var i = 0; i < input.Length; i++)
//         temp[i] = input[i];
// }

// string test = "ABCD1234";
// byte[] LogoDataBy = ASCIIEncoding.ASCII.GetBytes(test);
// var newArray = new byte[16];
//
// var startAt = newArray.Length - LogoDataBy.Length;
// Array.Copy(LogoDataBy, 0, newArray, startAt, LogoDataBy.Length);
/*
static void Main(string[] args)
{
   var persomTable = new PersonTable();
   var exit = false;
  
   
   
   while (!exit)
   {
       Console.WriteLine("1. Add Person");
       Console.WriteLine("2. Search by Id");
       Console.WriteLine("3. search by age");
       Console.WriteLine("4. search by name");
       Console.WriteLine("5. List By Id");
       Console.WriteLine("6. Exit");
       var menuValue = Console.ReadLine();
       switch (menuValue)
       {
           case "1": // add
               Console.WriteLine("Enter name");
               var name = Console.ReadLine();
               Console.WriteLine("Enter age");
               var age = Console.ReadLine();
               Console.WriteLine("Enter id");
               var id = Console.ReadLine();
               var person = new Person(name,Convert.ToInt16(age),Convert.ToInt16(id));
               persomTable.Add(person);
               
               break;
           case "2": //Search by Id
               Console.WriteLine("enter Id");
               var id1 = Console.ReadLine();
              var person1 =persomTable.SearchById(Convert.ToInt16(id1));
               if (person1 != null)
               {
                   person1.Print();
               }
               else
               {
                   Console.WriteLine($"not found person id : {id1}");
               } ;
               break;
           case "3":// 3. search by age
               Console.WriteLine("enter age");
               var age1 = Console.ReadLine();
               var person2 =persomTable.SearchByAge(Convert.ToInt16(age1));
               if (person2 != null)
               {
                   foreach (var person23 in person2)
                   {
                       person23.Print();
                   }
                  
               }
               else
               {
                   Console.WriteLine($"not found person age : {age1}");
               } ;
               
               break;           
           case "4"://// 4. search by name
               Console.WriteLine("enter name");
               var name1 = Console.ReadLine();
               var person3 =persomTable.SearchByName(name1);
               if (person3 != null)
               {
                   foreach (var person23 in person3)
                   {
                       person23.Print();
                   }
               }
               else
               {
                   Console.WriteLine($"not found person age : {name1}");
               } ;
               
               break;        
           case "5"://list by id
               Console.WriteLine("List by id");
               var person4 =persomTable.Display();
               if (person4 != null)
               {
                   foreach (var person23 in person4)
                   {
                       person23.Print();
                   }
               }
               else
               {
                   Console.WriteLine($"not found person");
               }
               break;
           case "6":
               Console.WriteLine("Bye bye");
               exit = true;
               break;
           default:
               Console.WriteLine("Bla bla bla");
               break;
       }
   }
}
*/
// }
// class Program
// {
//     static void Main(string[] args)
//     {
//         // int CompareString(string a, string b)
//         // {
//         //     return String.Compare(a, b); // a>b => 1 // a<b=> -1 //a==b => 0
//         // }
//         //
//         // // var mylist = new MyList();
//         // // while (true)
//         // // {
//         // //     Console.WriteLine("Hello World!");
//         // //     Console.WriteLine("Enter 1 To add item");
//         // //     Console.WriteLine("Enter 2 To List item");
//         // //     var input = Console.ReadLine();
//         // //     if (input == "1")
//         // //     {
//         // //         var value = Console.ReadLine();
//         // //         mylist.AddItem(value);
//         // //         input = string.Empty;
//         // //     }
//         // //
//         // //     if (input == "2")
//         // //     {
//         // //         Console.WriteLine("22222");
//         // //         var list = mylist.ListItem();
//         // //         Console.WriteLine("list : ");
//         // //         foreach (var l in list)
//         // //         {
//         // //             Console.WriteLine($"value : {l}");
//         // //         }
//         // //
//         // //         input = string.Empty;
//         // //     }
//         // //
//         // //     if (input == "3")
//         // //     {
//         // //         return;
//         // //     }
//         // // }
//         //
//         // var mylist = new MyList();
//         // mylist.AddItem("1");
//         // mylist.AddItem("1");
//         // mylist.AddItem("1");
//         // mylist.AddItem("5");
//         // mylist.AddItem("0");
//         // mylist.AddItem("5");
//         // mylist.AddItem("3");
//         //
//         //
//         // var sss = mylist.ListItem();
//         // var edee = "sss";
//
//         // var myLinklist = new MyLinklist();
//         //
//         // myLinklist.AddPointer(new LinkList(3));
//         // myLinklist.AddPointer(new LinkList(7));
//         // myLinklist.AddPointer(new LinkList(5));
//         // myLinklist.AddPointer(new LinkList(2));
//         // var sss = myLinklist.DisplayList();
//
//
//         // var mk = new MyBinaryLink();
//         // mk.Add(new Linklist2(5));
//         // mk.Add(new Linklist2(6));
//         // mk.Add(new Linklist2(7));
//         // mk.Add(new Linklist2(8));
//         // mk.Add(new Linklist2(1));
//         // var sss = mk.Search(1);
//         var t = new Testing();
//
//
//         var dddd = "sss";
//     }
// }

/*
    public class MyLinklist
    {
        private LinkList _link = null;

        public LinkList DisplayList()
        {
            return _link;
        }

        public void AddPointer(LinkList insertData)
        {
            var parentNode = FindParentNode(_link, insertData);
            InsertNode(parentNode, insertData);
        }

        private void InsertNode(LinkList parent, LinkList insertData)
        {
            if (parent == null)
            {
                insertData.Next = _link;
                _link = insertData;
                return;
            }
            else
            {
                var parentNext = parent.Next;
                insertData.Next = parentNext;
                parent.Next = insertData;
                return;
            }
        }

        private LinkList FindParentNode(LinkList current, LinkList newNode)
        {
            if (current == null || newNode.Value < current.Value)
                return null;
            if (current.Value <= newNode.Value && (current.Next == null || current.Next.Value >= newNode.Value))
                return current;
            return FindParentNode(current.Next, newNode);
        }
    }
    
    */
// }

/*
public class MyList
{
    private string[] _list = new string[0];

    public void AddItem(string t)
    {
        var max = (_list.Length - 1 < 0 ? 0 : _list.Length - 1);
        var index = FindIndex2(_list, 0, max, t);
        var list = new string[_list.Length + 1];
        CopyAndShift(list, 0, index - 1);
        list[index] = t;
        CopyAndShift(list, index, (_list.Length - 1), 1);
        _list = list;
    }

    private string[] CopyAndShift(string[] list, int startIndex, int endIndex, int shift = 0)
    {
        for (var i = startIndex; i <= endIndex; i++)
        {
            list[i + shift] = _list[i];
        }

        return list;
    }

    private int FindIndex(string[] l, string value)
    {
        for (var i = 0; i < l.Length; i++)
        {
            if (string.CompareOrdinal(l[i], value) > 0) // -1 A>B
            {
                return i;
            }
        }

        return l.Length;
    }

    private int FindIndex2(string[] l, int startIndex, int endIndex, string value)
    {
        if (startIndex == endIndex) return endIndex;
        if (Math.Abs(endIndex - startIndex) == 1)
        {
            if (CompareString(l[endIndex], value) == 0) //==
            {
                return endIndex;
            }

            // center

            if (CompareString(l[startIndex], value) == -1 && CompareString(l[endIndex], value) == 1) // <
            {
                return endIndex;
            }

            // max

            if (CompareString(l[endIndex], value) == -1) // <
            {
                return endIndex + 1;
            }

            // min 

            if (CompareString(l[startIndex], value) == 1) // >
            {
                return startIndex;
            }
        }

        var middle = (int) (startIndex + endIndex) / 2;
        var compareValue = CompareString(l[middle], value);

        if (compareValue == 0) //middle == value
        {
            return middle;
        }

        else if (compareValue == -1) // middle < value
        {
            return FindIndex2(l, middle, endIndex, value);
        }
        else if (compareValue == 1) // middle > value
        {
            return FindIndex2(l, startIndex, middle, value);
        }

        throw new Exception("ddddd");
    }

    int CompareString(string a, string b)
    {
        return String.Compare(a, b); // a>b => 1 // a<b=> -1 //a==b => 0
    }

    public string[] ListItem() => _list;
}


public class LinkList
{
    public int Value { get; set; } = 0;
    public LinkList Next { get; set; } = null;

    public LinkList(int? value)
    {
        Value = (value ?? 0);
    }
}

public class Linklist2
{
    public int Value { get; set; } = 0;
    public Linklist2 Left { get; set; } = null;
    public Linklist2 Right { get; set; } = null;

    public Linklist2(int? value)
    {
        Value = (value ?? 0);
    }
}


public class MyBinaryLink
{
    private Linklist2 _link = null;

    #region Add

    public void Add(Linklist2 insertData)
    {
        var (parentNode, position) = FindParentNode(_link, insertData);
        InsertNode(parentNode, position, insertData);
    }

    private void InsertNode(Linklist2 parentNode, Position? position, Linklist2 insertData)
    {
        if (parentNode == null)
        {
            _link = insertData;
            return;
        }

        if (position == Position.Left)
        {
            parentNode.Left = insertData;
            return;
        }

        if (position == Position.Right)
        {
            parentNode.Right = insertData;
            return;
        }

        // no more case
        throw new Exception("no more case");
    }

    private (Linklist2, Position?) FindParentNode(Linklist2 current, Linklist2 insertData)
    {
        if (current == null) return (null, null);

        if (current.Value > insertData.Value && current.Left == null)
        {
            return (current, Position.Left); // Left less then current
        }

        if (current.Value < insertData.Value && current.Right == null)
        {
            return (current, Position.Right); // Right more then current
        }

        if (current.Value > insertData.Value && current.Left != null)
        {
            return FindParentNode(current.Left, insertData); // Left
        }

        if (current.Value < insertData.Value && current.Right != null)
        {
            return FindParentNode(current.Right, insertData); // Right
        }

        // no more case
        throw new Exception("no more case");
    }

    #endregion

    #region Search

    public bool Search(int value)
    {
        return Search(_link, value);
    }
    private bool Search(Linklist2 current, int value)
    {
        if (current == null) return false;
        if (current.Value == value) return true;
        if (current.Value > value && current.Left == null)  return false;  // L
        if (current.Value > value && current.Left != null) return Search(current.Left, value);  // L
        if (current.Value < value && current.Right == null)  return false;  // R
        if (current.Value < value && current.Right != null) return Search(current.Right, value);  // R
        // no more case
        throw new Exception("no more case");
    }
    #endregion

    #region display

    public void Display()
    {
        _Display(_link);
    }
    
    private void _Display(Linklist2 current)
    {
        if (current.Left != null) _Display(current.Left);
        Console.WriteLine(current.Value);
        if (current.Right != null) _Display(current.Right);
    }

    #endregion

    public Linklist2 GetLink() => this._link;

}

*/


/*


public class PersonLink
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public PersonLink Left { get; set; } = null;
    public PersonLink Right { get; set; } = null;

    public PersonLink(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}

public class IndexLink<T>
{
    private int PersonId { get; set; }
    private T Value { get; set; }
    public  IndexLink<T> Left { get; set; } = null;

    public  IndexLink<T> Right { get; set; } = null;
}*/
/*

public class PersonTable
{
    private PersonLink _person = null;

    public void Add(PersonLink newData)
    {
        var (parent, position) = FindParentNode(_person, newData);
    }
    
    private (PersonLink, Position?) FindParentNode(PersonLink current, PersonLink insertData)
    {
        if (current == null) return (null, null);

        if (current.Id > insertData.Id && current.Left == null)
        {
            return (current, Position.Left); // Left less then current
        }

        if (current.Id < insertData.Id && current.Right == null)
        {
            return (current, Position.Right); // Right more then current
        }

        if (current.Id > insertData.Id && current.Left != null)
        {
            return FindParentNode(current.Left, insertData); // Left
        }

        if (current.Id < insertData.Id && current.Right != null)
        {
            return FindParentNode(current.Right, insertData); // Right
        }

        // no more case
        throw new Exception("no more case");
    }
    
    private void InsertNode(PersonLink parentNode, Position? position, PersonLink insertData)
    {
        if (parentNode == null)
        {
            _person = insertData;
            return;
        }

        if (position == Position.Left)
        {
            parentNode.Left = insertData;
            return;
        }

        if (position == Position.Right)
        {
            parentNode.Right = insertData;
            return;
        }

        // no more case
        throw new Exception("no more case");
    }

    public PersonLink FindById(int id)
    {
        return Search(_person, id);
    }

    private PersonLink? Search(PersonLink current, int value)
    {
        if (current == null) return null;
        if (current.Id == value) return current;
        if (current.Id > value && current.Left == null) return null; // L
        if (current.Id > value && current.Left != null) return Search(current.Left, value); // L
        if (current.Id < value && current.Right == null) return null; // R
        if (current.Id < value && current.Right != null) return Search(current.Right, value); // R
        // no more case
        throw new Exception("no more case");
    }
}

*/

//
// public class PersonTable
// {
//     private PersonLinklist<int> _idLink = null;
//     private PersonLinklist<int> _ageLink = null;
//     private PersonLinklist<string> _nameLink = null;
//
//
//     public void Add(Person newData)
//     {
//         var parent = FindParentBy<int>(IndexBy.Id, _idLink, newData);
//     }
//
//     private (PersonLinklist<TX>, Position?) FindParentBy<TX>(IndexBy indexBy, PersonLinklist<TX> current, Person newData)
//     {
//         if (current == null) return (null, null);
//
//         var compareVale = CompareValue<TX>(indexBy, current, newData);
//         if ( compareVale&& current.Left == null) // current > new
//         {
//             return (current, Position.Left); // Left less then current
//         }
//         
//         // if (current.Value > insertData.Value && current.Left == null)
//         // {
//         //     return (current, Position.Left); // Left less then current
//         // }
//         
//         if (!compareVale && current.Right == null)
//         {
//             return (current, Position.Right); // Right more then current
//         }
//
//         // if (current.Value < insertData.Value && current.Right == null)
//         // {
//         //     return (current, Position.Right); // Right more then current
//         // }
//
//         if (compareVale && current.Left != null)
//         {
//             return FindParentBy(indexBy,current.Left, newData); // Left
//         }
//         // if (current.Value > insertData.Value && current.Left != null)
//         // {
//         //     return FindParentNode(current.Left, insertData); // Left
//         // }
//
//         if (!compareVale&& current.Right != null)
//         {
//             return FindParentBy(indexBy,current.Right, newData); // Right
//         }
//         
//         // if (current.Value < insertData.Value && current.Right != null)
//         // {
//         //     return FindParentNode(current.Right, insertData); // Right
//         // }
//         
//         
//         
//         
//         
//         // if (indexBy == IndexBy.Id)
//         // {
//         //     return (null, null);
//         // }
//         // else if (indexBy == IndexBy.Age)
//         // {
//         //     return (null, null);
//         // }
//         // else if (indexBy == IndexBy.Name)
//         // {
//         //     return (null, null);
//         // }
//         // else
//         // {
//         //     throw new Exception();
//         // }
//     }
//
//     // if current > newData return true
//     // if current < newData false
//     private bool CompareValue<TX>(IndexBy indexBy, PersonLinklist<TX> current, Person newData)
//     {
//         return false;
//     }
//
//     private (PersonLinklist<TX>, Position?) FindParentNode<TX>(PersonLinklist<TX> current, Linklist2 insertData)
//     {
//         if (current == null) return (null, null);
//
//         if (current.Value > insertData.Value && current.Left == null)
//         {
//             return (current, Position.Left); // Left less then current
//         }
//
//         if (current.Value < insertData.Value && current.Right == null)
//         {
//             return (current, Position.Right); // Right more then current
//         }
//
//         if (current.Value > insertData.Value && current.Left != null)
//         {
//             return FindParentNode(current.Left, insertData); // Left
//         }
//
//         if (current.Value < insertData.Value && current.Right != null)
//         {
//             return FindParentNode(current.Right, insertData); // Right
//         }
//
//         // no more case
//         throw new Exception("no more case");
//     }
//
//     
//     
//     
// }

// public enum IndexBy
// {
//     Id,
//     Name,
//     Age,
// }
//
//
// public class Person
// {
//     public string Name { get; set; } = string.Empty;
//     public int Age { get; set; } = 0;
//     public int Id { get; set; } = 0;
//
//     public Person(string name, int age)
//     {
//         Name = name;
//         Age = age;
//     }
// }
//
// public class Node<K, V>
// {
//     public K Key;
//     public V Value;
//     public Node<K, V> Left, Right;
// }


// class Tree<K, V>
// {
//     private readonly Func<K, K, CompareValue> _compare;
//     private Node<K, V> _link = null;
//     private K LastKey;
//
//     public Tree(Func<K, K, CompareValue> compare)
//     {
//         _compare = compare;
//     }
//
//     #region Add
//
//     public void Add(Node<K, V> insertData)
//     {
//         var (parentNode, position) = FindParentNode(_link, insertData);
//         InsertNode(parentNode, position, insertData);
//         UpdateLastIndex(insertData.Key);
//     }
//
//     private void UpdateLastIndex(K insertDataKey)
//     {
//         LastKey = insertDataKey;
//     }
//
//     private void InsertNode(Node<K, V>? parentNode, Position? position, Node<K, V> insertData)
//     {
//         if (parentNode == null)
//         {
//             _link = insertData;
//             return;
//         }
//
//         if (position == Position.Left)
//         {
//             parentNode.Left = insertData;
//             return;
//         }
//
//         if (position == Position.Right)
//         {
//             parentNode.Right = insertData;
//             return;
//         }
//
//         // no more case
//         throw new Exception("no more case");
//     }
//
//     private (Node<K, V>?, Position?) FindParentNode(Node<K, V>? current, Node<K, V> insertData)
//     {
//         if (current == null) return (null, null);
//
//         var compareValue = CompareKey(current.Key, insertData.Key) == CompareValue.More;
//         if (compareValue && current?.Left == null)
//         {
//             return (current, Position.Left); // Left less then current
//         }
//
//         if (!compareValue && current?.Right == null)
//         {
//             return (current, Position.Right); // Right more then current
//         }
//
//         if (compareValue && current?.Left != null)
//         {
//             return FindParentNode(current.Left, insertData); // Left
//         }
//
//         if (!compareValue && current?.Right != null)
//         {
//             return FindParentNode(current.Right, insertData); // Right
//         }
//
//         // no more case
//         throw new Exception("no more case");
//     }
//
//     // current > new return ture
//     // current < new return false
//
//     #endregion
//
//     #region Get
//
//     public Node<K, V> GetALL() => _link;
//
//     #endregion
//
//     #region FindByKey
//
//     public Node<K, V>? FindByKey(K key)
//     {
//         return Search(_link, key);
//     }
//
//     private Node<K, V>? Search(Node<K, V>? current, K key)
//     {
//         if (current == null) return null;
//         var compareValue = CompareKey(current.Key, key);
//         if (compareValue == CompareValue.Equal) return current;
//
//         var isMore = compareValue == CompareValue.More;
//         // if (current.Id == value) return current;
//         if (isMore && current?.Left == null) return null; // L
//         if (isMore && current?.Left != null) return Search(current.Left, key); // L
//
//         if (!isMore && current?.Right == null) return null; // R
//         if (!isMore && current?.Right != null) return Search(current.Right, key); // R
//         return null;
//         // no more case
//         // throw new Exception("no more case");
//     }
//
//     #endregion
//
//     #region Comparer
//
//     private CompareValue CompareKey(K currentKey, K insertDataKey)
//     {
//         return _compare(currentKey, insertDataKey);
//     }
//
//     #endregion
//
//     #region LastKey
//
//     public K GetLastKey() => LastKey;
//
//     #endregion
//
//     #region display
//
//     public void Display()
//     {
//         _Display(_link);
//     }
//
//     private IEnumerable<V> _Display(Node<K, V> current)
//     {
//         if (current.Left != null) _Display(current.Left);
//         yield return current.Value;
//         if (current.Right != null) _Display(current.Right);
//     }
//
//     #endregion
// }
//
// public enum CompareValue
// {
//     More,
//     Less,
//     Equal,
// }
//
// public class PersonTable
// {
//     private readonly Tree<int, Person> _personTree = new Tree<int, Person>(CompareNumber);
//     private readonly Tree<string, int[]> _indexByName = new Tree<string, int[]>(CompareString);
//     private readonly Tree<int, int[]> _indexByAge = new Tree<int, int[]>(CompareNumber);
//
//     // 1. Add person
//     public void Add(Person person)
//     {
//         //insert data 
//         if (person.Id == 0)
//         {
//             person.Id = _personTree.GetLastKey() + 1;
//         }
//         else
//         {
//             var personNode = _personTree.FindByKey(person.Id);
//             if (personNode != null)
//             {
//                 //update 
//                 personNode.Value = person;
//
//                 //case thorw 
//                 //Console.WriteLine($"cant insert Id{person.Id}");
//                 //return;
//             }
//         }
//
//         var node = new Node<int, Person>()
//         {
//             Key = person.Id,
//             Value = person
//         };
//         _personTree.Add(node);
//
//         // index by name 
//         var nameIndex = _indexByName.FindByKey(person.Name);
//
//         if (nameIndex == null)
//         {
//             //add new index
//             _indexByName.Add(new Node<string, int[]>()
//             {
//                 Key = person.Name,
//                 Value = UpdateArrayValue<int>(nameIndex?.Value, person.Id)
//             });
//         }
//         else
//         {
//             //update value
//             nameIndex.Value = UpdateArrayValue<int>(nameIndex?.Value, person.Id);
//         }
//
//         // index by age 
//         var ageIndex = _indexByAge.FindByKey(person.Age);
//         if (ageIndex == null)
//         {
//             _indexByAge.Add(new Node<int, int[]>()
//             {
//                 Key = person.Age,
//                 Value = UpdateArrayValue<int>(ageIndex?.Value, person.Id)
//             });
//         }
//         else
//         {
//             var ssss = UpdateArrayValue<int>(ageIndex?.Value, person.Id);
//
//             ageIndex.Value = ssss;
//         }
//     }
//
//     // 2. Search by Id
//     public Person? SearchById(int id)
//     {
//         return _personTree.FindByKey(id)?.Value;
//     }
//
//     // 3. search by age
//     public Person[]? SearchByAge(int age)
//     {
//         var ageIndex = _indexByAge.FindByKey(age);
//         if (ageIndex == null)
//         {
//             return null;
//         }
//
//         Person[]? list = null;
//         foreach (var i in ageIndex.Value)
//         {
//             var person = _personTree.FindByKey(i)!.Value;
//             list = UpdateArrayValue(list, person);
//         }
//
//         return list;
//     }
//
//     // 4. search by name
//     public Person[]? SearchByName(string name)
//     {
//         var nameIndex = _indexByName.FindByKey(name);
//         if (nameIndex == null)
//         {
//             return null;
//         }
//
//         Person[]? list = null;
//         foreach (var i in nameIndex.Value)
//         {
//             var person = _personTree.FindByKey(i)!.Value;
//             list = UpdateArrayValue(list, person);
//         }
//
//         return list;
//     }
//     // 5. list order by id
//
//     private T[] UpdateArrayValue<T>(T[]? array, T value)
//     {
//         if (array == null)
//         {
//             return new[] {value};
//         }
//
//         return array.Append(value);
//         //return array;
//     }
//
//     #region Compare func
//
//     private static readonly Func<string, string, CompareValue> CompareString = (currentValue, newValue) =>
//     {
//         var compareValue = string.Compare(currentValue, newValue, StringComparison.Ordinal);
//         if (compareValue > 0) return CompareValue.More;
//         if (compareValue < 0) return CompareValue.Less;
//         return CompareValue.Equal;
//     };
//
//     private static readonly Func<int, int, CompareValue> CompareNumber = (currentValue, newValue) =>
//     {
//         if (currentValue > newValue) return CompareValue.More;
//         if (currentValue < newValue) return CompareValue.Less;
//         return CompareValue.Equal;
//     };
//
//     #endregion
// }

// public class Testing
// {
//     private static Func<int, int, CompareValue> compareNumber = (currentValue, newValue) =>
//     {
//         if (currentValue > newValue) return CompareValue.More;
//         
//         if (currentValue < newValue) return CompareValue.Less;
//
//         return CompareValue.Equal;
//     }; 
//     
//     private Tree<int, Person> personTree = new Tree<int, Person>(compareNumber);
//
//     
//     public Testing()
//     {
//         var sssddd = personTree.GetLastKey();
//         personTree.Add(new Node<int, Person>()
//         {
//             Key = personTree.GetLastKey() + 1,
//             Value = new Person(personTree.GetLastKey() + 1,"Mark", 28)
//         });
//
//         personTree.Add(new Node<int, Person>()
//         {
//             Key = personTree.GetLastKey() + 1,
//             Value = new Person(personTree.GetLastKey() + 1, "Pre", 21)
//         });
//         
//         var sss = personTree.Get();
//
//         var ddd = personTree.FindByKey(2);
//         var ddsd = personTree.FindByKey(5);
//         
//         var dddd = "ddd";
//     }
//     
//     
// }


// public class Testing
// {
//     // private static Func<int, int, CompareValue> compareNumber = (currentValue, newValue) =>
//     // {
//     //     if (currentValue > newValue) return CompareValue.More;
//     //     
//     //     if (currentValue < newValue) return CompareValue.Less;
//     //
//     //     return CompareValue.Equal;
//     // }; 
//     //
//     // private Tree<int, Person> personTree = new Tree<int, Person>(compareNumber);
//
//
//     // public Testing()
//     // {
//     //     // var sssddd = personTree.GetLastKey();
//     //     // personTree.Add(new Node<int, Person>()
//     //     // {
//     //     //     Key = personTree.GetLastKey() + 1,
//     //     //     Value = new Person(personTree.GetLastKey() + 1,"Mark", 28)
//     //     // });
//     //     //
//     //     // personTree.Add(new Node<int, Person>()
//     //     // {
//     //     //     Key = personTree.GetLastKey() + 1,
//     //     //     Value = new Person(personTree.GetLastKey() + 1, "Pre", 21)
//     //     // });
//     //     //
//     //     // var sss = personTree.Get();
//     //     //
//     //     // var ddd = personTree.FindByKey(2);
//     //     // var ddsd = personTree.FindByKey(5);
//     //     //
//     //     // var dddd = "ddd";
//     //     var pp = new PersonTable();
//     //
//     //     pp.Add(new Person( "Mark",18));
//     //     pp.Add(new Person( "Kai",18));
//     //     // pp.Add(new Person("Tee",22));
//     //     // pp.Add(new Person("Tee",21));
//     //     pp.SearchById(1);
//     //     var sss = pp.SearchByName("Tee");
//     //     var rrr = pp.SearchByAge(18);
//     //     var sssss = ";s";
//     // }
// }