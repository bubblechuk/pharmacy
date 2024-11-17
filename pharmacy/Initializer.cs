using Microsoft.EntityFrameworkCore;
using pharmacy.DbContextPhar;
using pharmacy.models;
namespace pharmacy
{
    static internal class Initializer
    {
        public static void Init() {
            using (var context = new PharmacyDataBaseContext()) {
                context.Database.EnsureCreated();
                var entities = context.Model.GetEntityTypes();

                foreach (var entity in entities)
                {
                    var tableName = entity.GetTableName();
                    var sql = $"DELETE FROM {tableName}"; 

                    context.Database.ExecuteSqlRaw(sql);
                }

                var supplier1 = new Supplier
                {
                    Name = "Поставщик 1",
                    DrugsOnSupHands = new List<DrugsOnSupHand>
                    {
                        new DrugsOnSupHand
                        {
                            DrugName = "Аспирин",
                            Prior = 1,
                            Filling = 100,
                            Amount = 500
                        },
                        new DrugsOnSupHand
                        {
                            DrugName = "Парацетамол",
                            Prior = 2,
                            Filling = 200,
                            Amount = 300
                        }
                    }
                };

                var supplier2 = new Supplier
                {
                    Name = "Поставщик 2",
                    DrugsOnSupHands = new List<DrugsOnSupHand>
                    {
                        new DrugsOnSupHand
                        {
                            DrugName = "Ибупрофен",
                            Prior = 1,
                            Filling = 150,
                            Amount = 200
                        },
                        new DrugsOnSupHand
                        {
                            DrugName = "Цитрамон",
                            Prior = 3,
                            Filling = 50,
                            Amount = 150
                        }
                    }
                };

                var pharmacy1 = new Pharmacy
                {
                    Name = "Аптека 1",
                    Adress = "ул. Ленина, д.1",
                    Supplier = supplier1,
                    DrugsOnParHands = new List<DrugsOnParHand>
                    {
                        new DrugsOnParHand
                        {
                            DrugName = "Аспирин",
                            Price = 12.50m,
                            Filling = 100,
                            BestBeforeDate = "2025-01-01",
                            Amount = 50
                        },
                        new DrugsOnParHand
                        {
                            DrugName = "Парацетамол",
                            Price = 10.00m,
                            Filling = 200,
                            BestBeforeDate = "2024-12-01",
                            Amount = 30
                        }
                    }
                };

                var pharmacy2 = new Pharmacy
                {
                    Name = "Аптека 2",
                    Adress = "ул. Пушкина, д.2",
                    Supplier = supplier2,
                    DrugsOnParHands = new List<DrugsOnParHand>
                    {
                        new DrugsOnParHand
                        {
                            DrugName = "Ибупрофен",
                            Price = 15.00m,
                            Filling = 150,
                            BestBeforeDate = "2026-06-01",
                            Amount = 40
                        },
                        new DrugsOnParHand
                        {
                            DrugName = "Цитрамон",
                            Price = 8.00m,
                            Filling = 50,
                            BestBeforeDate = "2025-09-15",
                            Amount = 25
                        }
                    }
                };

                context.Suppliers.Add(supplier1);
                context.Suppliers.Add(supplier2);
                context.Pharmacies.Add(pharmacy1);
                context.Pharmacies.Add(pharmacy2);

                context.SaveChanges();
            }
        }
    }
}
