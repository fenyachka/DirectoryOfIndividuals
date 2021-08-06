using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Persons.Any()) return;
            
            var persons = new List<Person>
            {
                new Person
                {
                   FirstNameGeo = "ფენია",
                   FirstNameEn = "Fenya",
                   LastNameGeo = "მურადოვა",
                   LastNameEn = "Muradova",
                   PrivateNumber = "01010192444",
                   Birthdate = DateTime.Now.AddMonths(-2),
                   Address = "კოსტავა 77ა",
                   Phone = "579140411",
                   Email = "fenyamuradova@gmail.com"
                },
                new Person
                {
                   FirstNameGeo = "მარიამ",
                   FirstNameEn = "Mariam",
                   LastNameGeo = "ლეჟავა",
                   LastNameEn = "Lezjava",
                   PrivateNumber = "01010192555",
                   Birthdate = DateTime.Now.AddMonths(-1),
                   Address = "ონაშცილის ქუჩა",
                   Phone = "578152531",
                   Email = "mariamlezjava@gmail.com"
                },
                new Person
                {
                   FirstNameGeo = "გიორგი",
                   FirstNameEn = "Giorgi",
                   LastNameGeo = "ლაგვილავა",
                   LastNameEn = "Lagvilava",
                   PrivateNumber = "01010192666",
                   Birthdate = DateTime.Now.AddMonths(0),
                   Address = "ონაშცილის ქუჩა",
                   Phone = "579151525",
                   Email = "glagvilava@gmail.com"
                },
                new Person
                {
                   FirstNameGeo = "test3",
                   FirstNameEn = "Fenya",
                   LastNameGeo = "მურადოვა",
                   LastNameEn = "Muradova",
                   PrivateNumber = "01010192444",
                   Birthdate = DateTime.Now.AddMonths(-2),
                   Address = "კოსტავა 77ა",
                   Phone = "579140411",
                   Email = "fenyamuradova@gmail.com"
                },
                new Person
                {
                   FirstNameGeo = "test2",
                   FirstNameEn = "Mariam",
                   LastNameGeo = "ლეჟავა",
                   LastNameEn = "Lezjava",
                   PrivateNumber = "01010192555",
                   Birthdate = DateTime.Now.AddMonths(-1),
                   Address = "ონაშცილის ქუჩა",
                   Phone = "578152531",
                   Email = "mariamlezjava@gmail.com"
                },
                new Person
                {
                   FirstNameGeo = "test1",
                   FirstNameEn = "Giorgi",
                   LastNameGeo = "ლაგვილავა",
                   LastNameEn = "Lagvilava",
                   PrivateNumber = "01010192666",
                   Birthdate = DateTime.Now.AddMonths(0),
                   Address = "ონაშცილის ქუჩა",
                   Phone = "579151525",
                   Email = "glagvilava@gmail.com"
                },
            };

            await context.Persons.AddRangeAsync(persons);
            await context.SaveChangesAsync();


            if(context.Relationships.Any()) return;

            var relat = new List<Relationship>
            {
               new Relationship{
                  Id=1,
                  Definition="თანამშრომელი"
               },
               new Relationship{
                  Id=2,
                  Definition="ნათესავი"
               },
               new Relationship{
                  Id=3,
                  Definition="ნაცნობი"
               },
               new Relationship{
                  Id=4,
                  Definition="სხვა"
               }
            };

            await context.Relationships.AddRangeAsync(relat);
            await context.SaveChangesAsync();
        }
    }
}