using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using System.Collections.Generic;

namespace HelloWorldService.Tests
{

    public class ContactTests
    {
        HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:43953/api/");
        }

        //Refactored this according to what Dan did
        private HttpResponseMessage CreateNewContact(string name)
        {
            var newContact = new Contact
            {
                Name = name,
                Phones = new[] {
                    new Phone {
                        Number = "425-111-2222",
                        PhoneType = PhoneType.Mobile
                    }
                }
            };
            var newJson = JsonConvert.SerializeObject(newContact);
            var postContent = new StringContent(newJson, Encoding.UTF8, "application/json");
            var postResult = client.PostAsync("contacts", postContent).Result;
            
            return postResult;
        }




        [Test]
        public void TestGetSpecific_Bad()
        {
            //Arrange
            
            
            //Act
            var result = client.GetAsync("contacts/10211").Result;

            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void TestAddNewContact()
        {
            HttpResponseMessage postResult = CreateNewContact("test");

            //NUnit assertion version
            Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);

            //FluentAssertion version
            postResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }



        [Test]
        public void TestGetAll()
        {
            //Arrange


            //Act
            var getResult = client.GetAsync("contacts").Result;

            //Assert that get result was OK
            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);


        }

        [Test]
        public void TestDelete_ValidContactName()
        {
            //Arrange - create a contact and get the contact
            var postResult = CreateNewContact("TestDelete_ValidContactName");
            var json = postResult.Content.ReadAsStringAsync().Result;
            var contact = JsonConvert.DeserializeObject<Contact>(json);

            //Act - Perform the deletion that we are testing
            var result = client.DeleteAsync("contacts/" + contact.Id).Result;

            //Assert that deleting contact was OK
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void TestDelete_InvalidContactStringId()
        {
            //Arrange
            

            //Act
            var result = client.DeleteAsync("contacts/abc").Result;


            //Assert that invalid string ID was a bad request
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void TestDelete_InvalidContactId()
        {
            //Arrange


            //Act
            var result = client.DeleteAsync("contacts/1").Result;

            //Assert that this was a bad request
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

    }
}