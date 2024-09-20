using Student_DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Student_DataBase
{
    internal class Node
    {
        private int Group;
        public int group { get => Group; set => Group = value; }

        private string Name;
        public string name { get => Name; set => Name = value; }
        private double Avg_mark;
        public double avg_mark { get => Avg_mark; set => Avg_mark = value; }
        private Node Next;
        public Node next { get => Next; set => Next = value; }

        public Node GetCopy()                // конструктор,для того, чтобы не поломался id
        {
            return new Node() //новый элемент со значениями из этого (this)                    // используется узел для другого списка
            {
                next = null, //будет перезаписано при добавлении элемента на позицию после этого
                name = this.name,
                group = this.group,
                avg_mark = this.avg_mark
            };
        }
    }
}

namespace Final_Task
{
    internal class Student
    {

        private Node head; //узел, создание начала списка
        int count = 0;

        public Student(string Link) //применяется почти в конце при запуске в строке 515
        {
            int d = 0;
            string cleapboard = " ";
            using (
                StreamReader reader = new StreamReader(Link, Encoding.Default))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Node node = new Node();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '|')
                        {
                            switch (d)
                            {
                                case 0:
                                    node.group = Convert.ToInt32(cleapboard);//номер группы(ввод)
                                    cleapboard = " ";
                                    d++;
                                    break;
                                case 1:
                                    node.name = cleapboard;//ввод фио
                                    cleapboard = " ";
                                    d++;
                                    break;
                                case 2:
                                    node.avg_mark = Convert.ToDouble(cleapboard);//ввод средней оценки
                                    cleapboard = " ";
                                    d = 0;
                                    break;
                                default:
                                    break;
                            }
                            continue;
                        }
                        cleapboard += line[i];
                    }
                    if (head == null)
                        head = node;
                    else
                    {
                        Node current = head;
                        while (current.next != null)
                            current = current.next;
                        current.next = node;
                    }

                    count++;
                }
            }
        }

        private void AddLastNode(Node toAdd)
        {

            if (head == null)
            {
                head = toAdd;
            }
            else
            {
                Node current = head;
                while (current.next != null)
                {
                    current = current.next;
                }

                current.next = toAdd;
            }
        }

        public void AddFromOtherLinkedList(Student toAdd)
        {
            Node currentOther = toAdd.head;
            while (currentOther != null)
            {
                Node t = head;
                while (t != null)
                {
                    if (t.name == currentOther.name)
                    {
                        goto here;
                    }
                    t = t.next;
                }
                var copy = currentOther.GetCopy();
                AddLastNode(copy);
            here:;
                currentOther = currentOther.next;
            }
        }
        public void Dob_BD()
        {
            Node node = new Node();
            Console.Write("Введите номер группы студента: ");
            node.group = int.Parse(Console.ReadLine());
            Console.Write("Введите ФИО студента: ");
            node.name = Console.ReadLine();
            Console.Write("Введите средний балл студента: ");
            node.avg_mark = double.Parse(Console.ReadLine());

            if (head == null)
                head = node;
            else
            {
                Node current = head;
                while (current.next != null)
                    current = current.next;
                current.next = node;
            }

            count++;
        }
        public void Ud_BD(string m_name)
        {
            Node current = head;
            Node preprevious = null;
            while (current != null)
            {
                if (current.name.Equals(m_name))
                {
                    if (preprevious != null)
                    {
                        preprevious.next = current.next;
                    }
                    else
                    {
                        head = head.next;
                    }
                    count--;
                    return;
                }
                preprevious = current;
                current = current.next;
            }
        }
        public void Sort_ub_mark()
        {

            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;

            while (next != null)
            {
                if (current.avg_mark > next.avg_mark)
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_ub_mark();
        }
        public void Sort_vosvr_mark()
        {
            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;

            while (next != null)
            {
                if (current.avg_mark < next.avg_mark)
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_vosvr_mark();
        }
        public void Sort_ub_group()
        {

            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;

            while (next != null)
            {
                if (current.group > next.group)
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_ub_group();
        }
        public void Sort_vosvr_group()
        {

            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;

            while (next != null)
            {
                if (current.group < next.group)
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_vosvr_group();
        }
        public void Sort_ub_name()
        {

            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;

            while (next != null)
            {
                if (!(string_comparison(current.name, next.name)))
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_ub_name();
        }
        public void Sort_vosvr_name()
        {

            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;

            while (next != null)
            {
                if (string_comparison(current.name, next.name))
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_vosvr_name();
        }
        public void Sort_vosvr_name_test()
        {

            if (Count() == 0)
                return;

            Node previous = null;
            Node current = head;
            Node next = head.next;
            bool swapOccurred = false;


            while (next != null)
            {
                if (string_comparison(current.name, next.name))
                {
                    Swap(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.next;
            }

            if (swapOccurred)
                Sort_vosvr_name();
        }

        public bool string_comparison(string a, string b)
        {
            for (int i = 0; i < (a.Length < b.Length ? a.Length : b.Length); i++)
            {
                if ((int)a[i] == (int)b[i])
                    continue;
                else if ((int)a[i] < (int)b[i])
                    return true;
                else
                    return false;
            }
            return false;
        }
        public void Nayti_po_fio(string m_name)
        {
            Node current = head;
            while (current != null)
            {
                if (current.name.Equals(m_name))
                {
                    Console.WriteLine($"{current.group}\t{current.name}\t{current.avg_mark}");
                }
                current = current.next;
            }
        }
        public void Nayti_po_lastname(string m_name)
        {
            Node current = head;
            string buffer = "";
            while (current != null)
            {
                for (int i = 0; i < current.name.Length; i++)
                {
                    if (current.name[i] == ' ')
                        break;
                    buffer += current.name[i];
                }

                if (buffer.Equals(m_name))
                {
                    Console.WriteLine($"{current.group}\t{current.name}\t{current.avg_mark}");
                }

                buffer = "";
                current = current.next;
            }

        }
        public void Edit_element(int number)
        {
            if (number >= count)
                return;

            Node current = head;

            for (int i = 0; i < number; i++)
                current = current.next;

            int field;
            Console.WriteLine("Изменить поле : ");
            Console.WriteLine("1. Группа");
            Console.WriteLine("2. Фамилия");
            Console.WriteLine("3. Средний балл");
            Console.WriteLine("4. Все поля");
            Console.Write("Введите число: ");

            field = int.Parse(Console.ReadLine());

            switch (field)
            {
                case 1:
                    Console.Write("Введите группу: ");
                    current.group = int.Parse(Console.ReadLine());
                    Console.Clear();
                    break;
                case 2:
                    Console.Write("Введите ФИО: ");
                    current.name = Console.ReadLine();
                    Console.Clear();
                    break;
                case 3:
                    Console.Write("Введите средний балл: ");
                    current.avg_mark = double.Parse(Console.ReadLine());
                    Console.Clear();
                    break;
                case 4:
                    Console.Write("Введите группу: ");
                    current.group = int.Parse(Console.ReadLine());
                    Console.Write("Введите фио: ");
                    current.name = Console.ReadLine();
                    Console.Write("Введите средний балл: ");
                    current.avg_mark = double.Parse(Console.ReadLine());
                    Console.Clear();
                    break;
                default:
                    break;
            }

        }
        public void _out()
        {
            Node node = head;
            while (node != null)
            {
                Console.WriteLine($"{node.group} \t {node.name} \t {node.avg_mark}");
                node = node.next;
            }
        }
        public void Swap(Node previous, Node swap)
        {
            Node swaper = swap.next;

            if (previous == null)
                head = swaper;
            else
                previous.next = swaper;

            swap.next = swaper.next;
            swaper.next = swap;
        }
        public int Count()
        {
            return count;
        }
        public void Clear()
        {
            head = null;
            count = 0;
        }
        public void writing_to_file(string Link)
        {
            string text = "";
            Node current = head;
            while (current != null)
            {
                text += (Convert.ToString(current.group) + "\t" +
                    current.name + "\t\t" + Convert.ToString(current.avg_mark) + "\n");
                current = current.next;
            }
            using (FileStream fs = File.Create(Link))
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}

namespace Final_Task
{
    internal class Program
    {
        static void Decor()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        static void Interface()
        {
            Console.Write("Путь к базе данных: ");
            string adres1 = Console.ReadLine();
            string adres2 = adres1;

            adres1 += "Base.txt";
            adres2 += "Base_edit.txt";

            Console.Clear();
            Student student = new Student(adres1);
            int move;
            for (; ; )
            {
                Console.WriteLine("Выполнить: ");
                Decor();
                Console.WriteLine("0. Выйти");
                Console.WriteLine("1. Добавить элемент в базу .");
                Console.WriteLine("2. Удалить элемент из базы .");
                Console.WriteLine("3. Вывести на экран базу.");
                Console.WriteLine("4. Сортировка в порядке убывания по группе.");
                Console.WriteLine("5. Сортировка в порядке возрастания по группе.");
                Console.WriteLine("6. Сортировка в порядке убывания по фамилии.");
                Console.WriteLine("7. Сортировка в порядке возрастания по фамилии.");
                Console.WriteLine("8. Сортировка в порядке убывания по среднему баллу.");
                Console.WriteLine("9. Сортировка в порядке возрастания по среднему баллу.");
                Console.WriteLine("10. Найти студента по ФИО.");
                Console.WriteLine("11. Найти студента по фамилии.");
                Console.WriteLine("12. Кол-во студентов в базе.");
                Console.WriteLine("13. Измененить поле базы.");
                Console.WriteLine("14. Выгрузить данные в новый файл.");
                Console.WriteLine("15. Объединение нескольких БД. ");
                Decor();
                Console.Write("Введите номер действия : ");
                move = int.Parse(Console.ReadLine());
                switch (move)
                {
                    case 1:
                        Console.Clear();
                        student.Dob_BD();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Введите фио студента: ");
                        student.Ud_BD(Console.ReadLine());
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.Clear();
                        student.Sort_vosvr_group();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 5:
                        Console.Clear();
                        student.Sort_ub_group();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.Clear();
                        student.Sort_vosvr_name();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 7:
                        Console.Clear();
                        student.Sort_ub_name();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 8:
                        Console.Clear();
                        student.Sort_vosvr_mark();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 9:
                        Console.Clear();
                        student.Sort_ub_mark();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 10:
                        Console.Clear();
                        Console.Write("Введите фио студента: ");
                        student.Nayti_po_fio(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 11:
                        Console.Clear();
                        Console.Write("Введите фамилию студента: ");
                        student.Nayti_po_lastname(Console.ReadLine());
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 12:
                        Console.Clear();
                        student._out();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine($"Кол-во студентов в базе: {student.Count()}");
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case 13:
                        Console.Clear();
                        Console.Write("Введите номер объекта, который хотите изменить: ");
                        int num = int.Parse(Console.ReadLine());
                        student.Edit_element(num - 1);
                        break;
                    case 14:
                        Console.Clear();
                        student.writing_to_file(adres2);
                        break;
                    case 15: //индивидуальное задание
                        Console.Clear();
                        {
                            Console.WriteLine("Введите путь к файлу: ");
                            string adres3 = Console.ReadLine();
                            while (!File.Exists(adres3))
                            {
                                Console.WriteLine("Файла не существует, повторите попытку: ");
                                adres3 = Console.ReadLine();
                            }
                            Student otherStudents = new Student(adres3);
                            student.AddFromOtherLinkedList(otherStudents);
                            student.writing_to_file(adres2);
                        }
                        break;
                    default:
                        return;
                }
            }
        }
        static void Main(string[] args)
        {
            Interface();
            Console.ReadLine();
        }
    }
}


