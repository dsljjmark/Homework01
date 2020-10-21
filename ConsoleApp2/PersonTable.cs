using System;
using System.Collections.Generic;
using ConsoleApp2.Enums;
using ConsoleApp2.Extensions;
using ConsoleApp2.Generics;
using ConsoleApp2.Models;

namespace ConsoleApp2
{
    public class PersonTable
    {
        private readonly Tree<long, Person> _personTree = new Tree<long, Person>(CompareNumber, 0);
        private readonly Tree<string, long[]> _indexByName = new Tree<string, long[]>(CompareString, string.Empty);
        private readonly Tree<long, long[]> _indexByAge = new Tree<long, long[]>(CompareNumber, 0);

        // 1. Add person
        public void Add(Person person)
        {
            //insert data 
            if (person.Id == 0)
            {
                person.Id = _personTree.GetLastKey() + 1;
            }
            else
            {
                var personNode = _personTree.FindByKey(person.Id);
                if (personNode != null)
                {
                    //update 
                    //personNode.Value = person;

                    //case thorw 
                    Console.WriteLine($"cant insert Id{person.Id}");
                    return;
                }
            }

            var node = new Node<long, Person>(person.Id, person);
            _personTree.Add(node);

            // index by name 
            var nameIndex = _indexByName.FindByKey(person.Name);

            if (nameIndex == null)
            {
                //add new index
                _indexByName.Add(new Node<string, long[]>(person.Name, UpdateArrayValue(nameIndex?.Value, person.Id)));
            }
            else
            {
                //update value
                nameIndex.Value = UpdateArrayValue(nameIndex.Value, person.Id);
            }

            // index by age 
            var ageIndex = _indexByAge.FindByKey(person.Age);
            if (ageIndex == null)
            {
                _indexByAge.Add(new Node<long, long[]>(person.Age, UpdateArrayValue(ageIndex?.Value, person.Id)));
            }
            else
            {
                ageIndex.Value = UpdateArrayValue(ageIndex.Value, person.Id);
            }
        }

        // 2. Search by Id
        public Person? SearchById(int id)
        {
            return _personTree.FindByKey(id)?.Value;
        }

        // 3. search by age
        public Person[]? SearchByAge(int age)
        {
            var ageIndex = _indexByAge.FindByKey(age);
            if (ageIndex == null)
            {
                return null;
            }

            Person[]? list = null;
            foreach (var i in ageIndex.Value)
            {
                var person = _personTree.FindByKey(i)!.Value;
                list = UpdateArrayValue(list, person);
            }

            return list;
        }

        // 4. search by name
        public Person[]? SearchByName(string name)
        {
            var nameIndex = _indexByName.FindByKey(name);
            if (nameIndex == null)
            {
                return null;
            }

            Person[]? list = null;
            foreach (var i in nameIndex.Value)
            {
                var person = _personTree.FindByKey(i)!.Value;
                list = UpdateArrayValue(list, person);
            }

            return list;
        }
        // 5. list order by id

        public IEnumerable<Person>? Display()
        {
            return _personTree.Display();
        }

        private T[] UpdateArrayValue<T>(T[]? array, T value)
        {
            if (array == null)
            {
                return new[] {value};
            }

            return array.Append(value);
            //return array;
        }

        #region Compare func

        private static readonly Func<string, string, CompareValue> CompareString = (currentValue, newValue) =>
        {
            var compareValue = string.Compare(currentValue, newValue, StringComparison.Ordinal);
            if (compareValue > 0) return CompareValue.More;
            if (compareValue < 0) return CompareValue.Less;
            return CompareValue.Equal;
        };

        private static readonly Func<long, long, CompareValue> CompareNumber = (currentValue, newValue) =>
        {
            if (currentValue > newValue) return CompareValue.More;
            if (currentValue < newValue) return CompareValue.Less;
            return CompareValue.Equal;
        };

        #endregion
    }
}

