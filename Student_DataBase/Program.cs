public enum Pol
{
    Мужчина,
    Девушка
}
public enum Post
{
    Менеджер,
    Инженер,
    Руководитель
}
public struct Employee
{
    public string FIO; //ФИО сотрудника
    public DateTime Birth; //дата рождения сотрудника
    public Pol Pol; //пол сотрудника
    public Post Post; //должность сотрудника
    public int Year; //год поступления на указаную должность сотрудника
    public Employee(string fio, DateTime birth, Pol pol, Post post, int year)
    {
        FIO = fio;
        Birth = birth;
        Pol = pol;
        Post = post;
        Year = year;
    }
    public void Show()
    {
        Console.WriteLine($"Фио: {FIO}. Дата рождения: {Birth.ToShortDateString()}. Пол: {Pol}. Должность: {Post}. Год: {Year}.");
    }

}

class Program
{
    static List<Employee> employees = new List<Employee>
         {
            new Employee("Зайцева Дарья Александровна", new DateTime(2005, 6, 17), Pol.Девушка, Post.Менеджер, 2018),
            new Employee("Безгодкова Виктория Игоревна", new DateTime(2004, 12, 15), Pol.Девушка, Post.Инженер, 2019),
            new Employee("Веселов Кирилл Юрьевич", new DateTime(2004, 12, 6), Pol.Мужчина, Post.Инженер, 2019),
            new Employee("Бронин Евгений Александрович", new DateTime(2001, 6, 10), Pol.Мужчина, Post.Менеджер, 2022),
            new Employee("Слепенкова Мария Александровна", new DateTime(2005, 10, 10), Pol.Девушка, Post.Руководитель, 2018)
         };
    static void ShowList()
    {
        foreach (var e in employees)
        {
            e.Show();
            Console.WriteLine();
        }
    }
    static void NewEmployee()
    {
        Employee employ = new Employee();
        try
        {
            Console.WriteLine("Введите ФИО сотрудника: ");
            employ.FIO = Console.ReadLine();
            Console.WriteLine("Введите дату рождения сотрудника: ");
            employ.Birth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите пол сотрудника: ");
            Console.WriteLine("Введите пол:\n  1 - Мужчина\n  2 - Девушка\n");
            employ.Pol = (Pol)(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Введите должность сотрудника: ");
            Console.WriteLine("Введите должность:\n  1 - Менеджер\n  2 - Инженер\n  3 - Руководитель\n");
            employ.Post = (Post)(Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Введите год поступления сотрудника на должность: ");
            employ.Year = int.Parse(Console.ReadLine());
            employees.Add(employ);
        }
        catch
        {
            Console.WriteLine("Ошибочные данные! Введите заново.");
            NewEmployee();
        }
    }
    static void Delet()
    {
        Console.WriteLine("Введите ФИО сотрудника: ");
        string fio = Console.ReadLine();
        Employee employ = employees.Find(e => e.FIO == fio);
        if (employ.FIO == null)
        {
            Console.WriteLine("Сотрудник не найден.");
        }
        else
        {
            employees.Remove(employ);
            Console.WriteLine("Сотрудник успешно удален.");
        }
    }
    static void Decor()
    {
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    }

    static void Main(string[] args)
    {


        Post FindPost = Post.Инженер;
        //средний стаж сотрудников
        List<Employee> PeoplFindPost = employees.Where(e => e.Post == FindPost).ToList();
        int TotalYear = PeoplFindPost.Sum(e => DateTime.Now.Year - e.Year);
        double AvargeY = TotalYear / (double)PeoplFindPost.Count;
        //срудний возраст сотрудников мальчиков
        int AgeMale = employees.Where(e => e.Pol == Pol.Мужчина).Sum(e => DateTime.Now.Year - e.Birth.Year);
        int MaleCount = employees.Count(e => e.Pol == Pol.Мужчина);
        double AverageM = AgeMale / (double)MaleCount;
        //срудний возраст сотрудников девочек
        int AgeFemale = employees.Where(e => e.Pol == Pol.Девушка).Sum(e => DateTime.Now.Year - e.Birth.Year);
        int femaleCount = employees.Count(e => e.Pol == Pol.Девушка);
        double AverageF = AgeFemale / (double)femaleCount;

        Console.BackgroundColor = ConsoleColor.Cyan;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();

        int move;
        for (; ; )
        {
            Decor();
            Console.WriteLine("Выполнить: ");
            Decor();
            Console.WriteLine("1. Средний стаж сотрудников.");
            Console.WriteLine("2. Средний возраст сотрудников.");
            Console.WriteLine("3. Список сотрудников.");
            Console.WriteLine("4. Добавить нового сотрудника.");
            Console.WriteLine("5. Удалить сотрудника.");
            Decor();
            Console.Write("Введите номер действия : ");
            move = int.Parse(Console.ReadLine());
            switch (move)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Средний стаж сотрудников {FindPost}s: {AvargeY}");
                    break;
                case 2:
                    Console.Clear();
                    int morf;
                    Decor();
                    Console.WriteLine("1. Средний возраст женщин .");
                    Console.WriteLine("2. Средний возраст мужчин .");
                    Decor();
                    Console.Write("Введите номер действия : ");
                    morf = int.Parse(Console.ReadLine());
                    switch (morf)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"Средний возраст сотрудников женского пола: {AverageF}");
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine($"Средний возраст сотрудников мужского пола: {AverageM}");
                            break;
                        default:
                            {
                                Console.WriteLine("Пункт не существует.");
                            }
                            break;
                    }
                    break;
                case 3:
                    Console.Clear();
                    ShowList();
                    break;
                case 4:
                    Console.Clear();
                    NewEmployee();
                    break;
                case 5:
                    Console.Clear();
                    Delet();
                    break;
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("Пункт не существует.");
                    }
                    break;
            }
        }
    }
}
