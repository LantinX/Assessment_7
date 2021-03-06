using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.DAL;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using System;
namespace FieldAgent.DAL.Testing
{
    public class SecurityClearanceTesting
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private SecurityClearanceRepository securityRepo;

        public readonly static SecurityClearance SECURITY1 = MakeSecurityClearance1();
        public readonly static SecurityClearance SECURITY2 = MakeSecurityClearance2();
        public readonly static SecurityClearance SECURITY3 = MakeSecurityClearance3();
        public readonly static SecurityClearance SECURITY4 = MakeSecurityClearance4();
        public readonly static SecurityClearance SECURITY5 = MakeSecurityClearance5();


        [SetUp]
        public void Setup()
        {

            var options = new DbContextOptionsBuilder<FieldAgentContext>()
                  .UseInMemoryDatabase("testDatabase")
                  .Options;
            db = new FieldAgentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.SaveChanges();
            securityRepo = new SecurityClearanceRepository(db);
        }

        [Test]

        public void GetSingleAlias()
        {
            Response<SecurityClearance> response = new Response<SecurityClearance>();
            
            db.SecurityClearance.Add(SECURITY1);
            db.SaveChanges();

            response.Data = SECURITY1;
            var fromMethod = securityRepo.Get(1);

            Assert.AreEqual(fromMethod.Data, response.Data);
        }
        [Test]
        public void GetAllAliases()
        {
            Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();

            db.SecurityClearance.Add(SECURITY1);
            db.SaveChanges();

            db.SecurityClearance.Add(SECURITY2);
            db.SaveChanges();

            db.SecurityClearance.Add(SECURITY3);
            db.SaveChanges();

            db.SecurityClearance.Add(SECURITY4);
            db.SaveChanges();
            db.SecurityClearance.Add(SECURITY5);
            db.SaveChanges();

            response = securityRepo.GetAll();
            Assert.AreEqual(5, response.Data.Count);
        }
        public static SecurityClearance MakeSecurityClearance1()
        {
            SecurityClearance securityClearance1 = new SecurityClearance()
            {
                SecurityClearanceName = "none"
            };
            return securityClearance1;
        }
        public static SecurityClearance MakeSecurityClearance2()
        {
            SecurityClearance securityClearance2 = new SecurityClearance()
            {
                SecurityClearanceName = "retired"
            };
            return securityClearance2;
        }
        public static SecurityClearance MakeSecurityClearance3()
        {
            SecurityClearance securityClearance3 = new SecurityClearance()
            {
                SecurityClearanceName = "secret"
            };
            return securityClearance3;
        }
        public static SecurityClearance MakeSecurityClearance4()
        {
            SecurityClearance securityClearance3 = new SecurityClearance()
            {
                SecurityClearanceName = "topsecret"
            };
            return securityClearance3;
        }
        public static SecurityClearance MakeSecurityClearance5()
        {
            SecurityClearance securityClearance3 = new SecurityClearance()
            {
                SecurityClearanceName = "secret"
            };
            return securityClearance3;
        }
    }
}