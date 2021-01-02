﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlotMachineApi.Models;
using MongoDB.Driver;

namespace SlotMachineApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(ISlotMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UserCollectionName);
        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User Get(string UserName) =>
            _user.Find<User>(user => user.UserName == UserName).FirstOrDefault();

        public User Create(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Update(string UserName, User userIn) =>
            _user.ReplaceOne(user => user.UserName == UserName, userIn);

        /*        public void Remove(Book bookIn) =>
                    _books.DeleteOne(book => book.Id == bookIn.Id);

                public void Remove(string id) =>
                    _books.DeleteOne(book => book.Id == id);*/
    }
}
