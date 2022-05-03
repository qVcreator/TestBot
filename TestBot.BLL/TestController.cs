﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.BLL
{
    public class TestController
    {
        public Test UsersTest { get; private set; }

        public List<Group> TestingGroups { get; private set; }

        private static TestController _testController ;

        private TestController()
        {
            
        }

        public static TestController GetTestController()
        {
            if (_testController == null)
            {
                _testController = new TestController();
            }
            return _testController;
        }

        public void SetGroup(List<Group> groups)
        {
            _testController.TestingGroups = groups;
        }

        public void SetTest(Test test)
        {
            _testController.UsersTest = test;
        }

        public Dictionary<User, Test> GetDictionary()
        {
            Dictionary<User, Test> newDictionary = new Dictionary<User, Test>();
            for (int i = 0; i < _testController.TestingGroups.Count; i++)
            {
                for (int j = 0; j < _testController.TestingGroups[i].Users.Count; j++)
                {
                    User user = _testController.TestingGroups[i].Users[j];
                    newDictionary.Add(user, _testController.UsersTest);
                }
            }
            return newDictionary;
        }
    }
}