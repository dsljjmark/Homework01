using System;
using System.Collections.Generic;
using ConsoleApp2.Enums;
using ConsoleApp2.Files;
using ConsoleApp2.Interfaces;
using ConsoleApp2.Models;

namespace ConsoleApp2.Generics.V2
{
    public class PersonTable
    {
        // storage
        private readonly IStorage<Person> _personStorage;
        private readonly IStorage<Node<long, Person>> _indexByIdStorage;
        private readonly IStorage<Node<long, Person>> _indexByAgeStorage;
        private readonly IStorage<Node<string, Person>> _indexByNameStorage;
        
        // tree
        private readonly Tree<long, Person> _idTree;
        private readonly Tree<long, Person> _ageTree;
        private readonly Tree<string, Person> _nameTree;
        
        //fn
        private static readonly Func<long, long, CompareValue> CompareNumber = (currentValue, newValue) =>
        {
            if (currentValue > newValue) return CompareValue.More;
            if (currentValue < newValue) return CompareValue.Less;
            return CompareValue.Equal;
        };
        private static readonly Func<string, string, CompareValue> CompareString = (currentValue, newValue) =>
        {
            var compareValue = string.Compare(currentValue, newValue, StringComparison.Ordinal);
            if (compareValue > 0) return CompareValue.More;
            if (compareValue < 0) return CompareValue.Less;
            return CompareValue.Equal;
        };

        public PersonTable()
        {
            // data storeage
            _personStorage = new Storage<Person>("Data_Person", 1032);
           
            // indexStorage
            _indexByIdStorage = new Storage<Node<long, Person>>("Index_Id", (43 + 8));
            _indexByAgeStorage = new Storage<Node<long, Person>>("Index_Age", (43 + 8));
            _indexByNameStorage = new Storage<Node<string, Person>>("Index_Name", (43 + 1020));
            
            // tree
            // main index
            _idTree = new Tree<long, Person>(CompareNumber, 0, _indexByIdStorage,false);
            // index by age
            _ageTree = new Tree<long, Person>(CompareNumber, 0, _indexByAgeStorage, true);
            // index by name
            _nameTree = new Tree<string, Person>(CompareString, string.Empty, _indexByNameStorage, true);
        }
        // 1. Add person
        public void Add(Person person)
        {
            // Id
            // Validate duplicate id
            var canInsert = !IsDuplicateId(person);
            if (!canInsert)
            {
                Console.WriteLine($"---------------------- Result --------------------");
                Console.WriteLine($"Can't Add Person. Id {person.Id} is duplicated.");
                Console.WriteLine($"---------------------- End --------------------");
                return;
            }
            
            //if index = 0 auto assign index 
            if (person.Id == 0)
            {
                person.Id = _idTree.MaxIndex + 1;
            }

            var dataOffset = _personStorage.Write(person);
            var idNode = new Node<long, Person>()
            {
                Key = person.Id,
                RefCurrent = dataOffset,
                Current = _indexByIdStorage.GetOffset(),
                Left = null,
                Right = null,
                Duplicate = null,
            };
            _idTree.Add(idNode);
            
            
            // age
            var ageNode = new Node<long, Person>()
            {
                Key = person.Age,
                RefCurrent = dataOffset,
                Current = _indexByAgeStorage.GetOffset(),
                Left = null,
                Right = null,
                Duplicate = null,
            };
            _ageTree.Add(ageNode);    
            
            // name
            var nameNode = new Node<string, Person>()
            {
                Key = person.Name,
                RefCurrent = dataOffset,
                Current = _indexByNameStorage.GetOffset(),
                Left = null,
                Right = null,
                Duplicate = null,
            };
            _nameTree.Add(nameNode);
        }

        private bool IsDuplicateId(Person person)
        {
            return _idTree.FindByKey(person.Id) != null;
        }
        
        // 2. Search by Id
        public IEnumerable<Person>? SearchById(long id)
        {
            var indexNodes = _idTree.FindByKey(id);
            if (indexNodes is null) return null;
            return Looping(indexNodes);

            IEnumerable<Person> Looping(IEnumerable<Node<long, Person>> nodes)
            {
                foreach (var index in indexNodes)
                {
                    yield return index.GetCurrentObject(_personStorage);
                }
            }
        }
        
        // 3. search by age
        public IEnumerable<Person>? SearchByAge(int age)
        {
            var indexNodes = _ageTree.FindByKey(age);
            if (indexNodes is null) return null;
            return Looping(indexNodes);

            IEnumerable<Person> Looping(IEnumerable<Node<long, Person>> nodes)
            {
                foreach (var index in indexNodes)
                {
                    yield return index.GetCurrentObject(_personStorage);
                }
            }
        }

        // 4. search by name
        public IEnumerable<Person>? SearchByName(string name)
        {
            var indexNodes = _nameTree.FindByKey(name);
            if (indexNodes is null) return null;
            
            return Looping(indexNodes);
            
            IEnumerable<Person> Looping(IEnumerable<Node<string, Person>> nodes)
            {
                foreach (var index in indexNodes)
                {
                    yield return index.GetCurrentObject(_personStorage);
                }
            }
        }
        
        // 5. list order by id
        public IEnumerable<Person>? GetAll()
        {
            var indexNodes = _idTree.GetAll();
            if (indexNodes is null) return null;
            return Looping(indexNodes);

            IEnumerable<Person> Looping(IEnumerable<Node<long, Person>> nodes)
            {
                foreach (var index in indexNodes)
                {
                    yield return index.GetCurrentObject(_personStorage); //_personStorage.Read(index.RefCurrent);
                }
            }
        }
    }
}