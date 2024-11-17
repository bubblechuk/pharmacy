using Microsoft.EntityFrameworkCore;
using pharmacy.DbContextPhar;
using pharmacy.models;
using pharmacy;
using System.ComponentModel.Design;
bool menu = true;
Initializer.Init(); // на случай если надо сбросить дб
while (menu) {
    Console.WriteLine("-----:Меню:-----");
    Console.WriteLine("+ Добавление лекарства");
    Console.WriteLine("- Удаление лекарства");
    Console.WriteLine("= (Директор) закуп недостающих и спсание просроченных ");
    Console.WriteLine("1. Сведения об аптеке");
    Console.WriteLine("2. Сведения об лекарствах");
    Console.WriteLine("3. Сведения об поставщиках");
    Console.WriteLine("4. Товары в каждой аптеке");
    Console.WriteLine("5. Проверка срока годности");
    Console.WriteLine("6. Какие товары можно заказать");
    Console.WriteLine("7. Суммарная стоимость товаров");
    Console.WriteLine("8. Товар у поставщиков");
    Console.WriteLine("9. Товар с заданной фасовкой");
    Console.WriteLine("0. Выход");
    Console.WriteLine("-----------------");
    Console.WriteLine("Выберите команду: ");
    var choice = Console.ReadLine();
    switch (choice) {
        case "1":
            { 
            using (var context = new PharmacyDataBaseContext()) {
                var pharmacies = context.Pharmacies.ToList();
                Console.WriteLine($"--{"pharmacy_id"}--{"Name"}--{"supplier_id"}--{"adress"}");
                foreach (var pharmacy in pharmacies) {
                    Console.WriteLine($"--{pharmacy.PharmacyId}--{pharmacy.Name}--{pharmacy.SupplierId}--{pharmacy.Adress}");
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
            break;
            }
        case "2": {
                using (var context = new PharmacyDataBaseContext())
                { 
                    var drugs = context.DrugsOnParHands.ToList();
                    Console.WriteLine($"--{"drug_id"}--{"pharmacy_id"}--{"price"}--{"filling"}--{"best_before_date"}--{"amount"}");
                    foreach (var drug in drugs)
                    {
                        Console.WriteLine($"--{drug.DrugId}--{drug.DrugName}--{drug.PharmacyId}--{drug.Price}--{drug.Filling}--{drug.BestBeforeDate}--{drug.Amount}");
                    }
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
                    break;    
        }
        case "3":
            {
                using (var context = new PharmacyDataBaseContext())
                {
                    var suppliers = context.Suppliers.ToList();
                    foreach (var supplier in suppliers)
                    {
                        Console.WriteLine("--Id--Name--");
                        Console.WriteLine($"--{supplier.Id}--{supplier.Name}--");
                    }
                    
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case "4":
            {
                using (var context = new PharmacyDataBaseContext())
                {
                    var pharmacies = context.Pharmacies.ToList();
                    foreach (var pharmacy in pharmacies)
                    {
                        Console.WriteLine($"-----{pharmacy.Name}-----");
                        Console.WriteLine($"--{"drug_id"}--{"pharmacy_id"}--{"price"}--{"filling"}--{"best_before_date"}--{"amount"}");
                        foreach (var drug in context.DrugsOnParHands.Where(elem => elem.Pharmacy.PharmacyId == pharmacy.PharmacyId))
                        {
                            Console.WriteLine($"--{drug.DrugId}--{drug.DrugName}--{drug.PharmacyId}--{drug.Price}--{drug.Filling}--{drug.BestBeforeDate}--{drug.Amount}");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                    
                }
                break;
            }
        case "5": {
                using (var context = new PharmacyDataBaseContext()) {
                    var drugs = context.DrugsOnParHands.ToList();
                    Console.WriteLine("--drug_name--best_before_date--status");
                    foreach (var drug in drugs) {
                        Console.WriteLine(
                     $"--{drug.DrugName}--{drug.BestBeforeDate}--" +
                     (DateTime.TryParse(drug.BestBeforeDate, out DateTime bestBeforeDate) && bestBeforeDate < DateTime.Now
                         ? "Просрочено"
                         : "Не просрочено"));
                    }
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }

                break;
            }
        case "6": {
                using (var context = new PharmacyDataBaseContext())
                {
                    var suppliers = context.Suppliers.ToList();
                    foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"-----{supplier.Name}-----");
                    foreach (var drug in context.DrugsOnSupHands.Where(elem => elem.Supplier.Id == supplier.Id))
                    {
                        Console.WriteLine($"--{drug.DrugId}--{drug.DrugName}--{drug.SupplierId}--{drug.Prior}--{drug.Filling}--{drug.Amount}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
                break;
            }
        case "7": {
                var sum = 0;
                using (var context = new PharmacyDataBaseContext()) { 
                    var drugs = context.DrugsOnParHands.ToList();
                    foreach (var drug in drugs) {
                        sum += Convert.ToInt32(drug.Price);
                    }
                }
                Console.WriteLine($"Сумма всех товаров в аптеке: {sum}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case "8": {
                using (var context = new PharmacyDataBaseContext())
                {
                    Console.WriteLine("Введите название лекарства:");
                    string? name = Console.ReadLine();
                    var suppliers = context.Suppliers.ToList();
                    foreach (var supplier in suppliers)
                    {
                        Console.WriteLine($"-----{supplier.Name}-----");
                        foreach (var drug in context.DrugsOnSupHands.Where(elem => elem.Supplier.Id == supplier.Id && elem.DrugName == name))
                        {
                            Console.WriteLine($"--{drug.DrugId}--{drug.DrugName}--{drug.SupplierId}--{drug.Prior}--{drug.Filling}--{drug.Amount}");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case "9": {
                using (var context = new PharmacyDataBaseContext())
                {
                    Console.WriteLine("Введите нужное лекарство: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Введите нужную фасовку: ");
                    var filling = Convert.ToInt32(Console.ReadLine());
                    var fillingDrugs = context.DrugsOnSupHands.Where(elem => elem.Filling == filling && elem.DrugName == name);
                    var suppliers = fillingDrugs.Select(elem => elem.Supplier.Name).ToList();
                    Console.WriteLine($"Лекарство {name} в фасовке {filling} имеется у поставщиков: ");
                    foreach (var supplier in suppliers) {
                        Console.WriteLine(supplier);
                    }
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case "0": { 
                menu = false; 
                break; 
            }
        case "+": {
                Console.WriteLine("Введите название аптеки: ");
                string? pharmacy = Console.ReadLine();
                Console.WriteLine("Введите название лекарства: ");
                var drugs = Console.ReadLine();
                Console.WriteLine("Введите дозировку: ");
                var filling = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите количество: ");
                var amount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите цену: ");
                var price = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Введите название поставщика: ");
                var supplier = Console.ReadLine();
                using (var context = new PharmacyDataBaseContext())
                {
                    var drugz = context.DrugsOnSupHands
                        .AsNoTracking()
                        .Where(name => name.DrugName == drugs)
                        .OrderBy(prior => prior)
                        .ToList();

                    var drugzpar = context.DrugsOnParHands
                        .AsNoTracking()
                        .FirstOrDefault(name => name.DrugName == drugs);

                    var pharmacyid = context.Pharmacies
                        .AsNoTracking()
                        .FirstOrDefault(ph => ph.Name == pharmacy);

                    if (pharmacyid == null)
                    {
                        Console.WriteLine("Аптека с указанным названием не найдена.");
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        Console.Clear();

                    }

                    if (drugz.Any() && drugz[0].DrugName == drugzpar?.DrugName)
                    {
                        if (drugzpar != null)
                        {
                            drugzpar.Amount += amount;
                            context.DrugsOnParHands.Update(drugzpar);
                        }
                    }
                    else
                    {
                        var drug = new DrugsOnParHand
                        {
                            DrugName = drugs,
                            PharmacyId = pharmacyid.PharmacyId,
                            Price = price,
                            Filling = filling,
                            BestBeforeDate = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd"),
                            Amount = amount
                        };
                        context.DrugsOnParHands.Add(drug);
                    }

                    context.SaveChanges();
                }
                break;
            }
        case "-":
            {
                Console.WriteLine("Введите название лекарства: ");
                string? drug = Console.ReadLine();
                using (var context = new PharmacyDataBaseContext())
                {
                    var drugToDelete = context.DrugsOnParHands.FirstOrDefault(elem => elem.DrugName == drug);
                    context.DrugsOnParHands.Remove(drugToDelete);
                    context.SaveChanges();
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        case "=": {
                using (var context = new PharmacyDataBaseContext()) {
                    var drugs = context.DrugsOnParHands.ToList();
                    var drugsSup = context.DrugsOnSupHands.ToList();
                    foreach (var drug in drugs) {
                        if (drug.Amount >= 0) {
                            continue;
                        }
                        var drugSup = drugsSup.FirstOrDefault(elem => elem.DrugName == drug.DrugName);
                        if (drugSup.Amount > 10)
                        {
                            drug.Amount += 10;
                            drugSup.Amount -= 10;
                        }
                        else if (drugSup.Amount < 10)
                        {
                            drug.Amount = drugSup.Amount;
                            drugSup.Amount = 0;
                        }
                        else 
                        {
                            Console.WriteLine("Невозможно закупить товар! У поставщика закончились товары.");
                        }
                    }

                    var toDelete = context.DrugsOnParHands.ToList();
                    foreach (var element in toDelete) {
                        if (DateTime.TryParse(element.BestBeforeDate, out DateTime bestBeforeDate) && bestBeforeDate < DateTime.Now) {
                            context.DrugsOnParHands.Remove(element);
                        }
                    }
                    context.SaveChanges();
                }
                break;
            }
        default: {
                Console.WriteLine("Неверный параметр!");
                break;
            }
    }
}
