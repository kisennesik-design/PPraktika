using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129
{
    // Класс, представляющий базового персонажа на карте, наследует MapObject. 
    internal class Person : MapObject
    {
        // Координаты персонажа на карте. 
        int pointX;
        int pointY;

        // Конструктор, инициализирующий координаты персонажа. 
        // Параметры X и Y определяют позицию на карте. 
        public Person(int X, int Y)
        {
            pointX = X; pointY = Y;
        }
        // Переопределяет абстрактный метод для отображения персонажа на карте. 
        // В базовом классе персонаж отображается как символ '!'. 
        public override char Rendering_on_the_map()
        {
            return '!'; // Символ, представляющий персонажа на карте. 
        }
    }

    // Класс, представляющий героя на карте, наследует Person. 
    internal class Hero : Person
    {
        // Конструктор, который передает координаты в конструктор базового класса. 
        // Герой будет размещен на карте на позиции X, Y. 
        public Hero(int X, int Y) : base(X, Y)
        {

        }
        // Переопределяет абстрактный метод для отображения героя на карте. 
        // Герой отображается как символ '@' с желтым цветом. 

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // Устанавливаем желтый цвет для героя. 
            return '@'; ;  // Символ для отображения героя. 
        }
    }
    // Класс, представляющий врага на карте, наследует Person. 
    internal class Enemy : Person
    {
        // Конструктор, который передает координаты в конструктор базового класса. 
        // Враг будет размещен на карте на позиции X, Y. 
        public Enemy(int X, int Y) : base(X, Y)
        {
        }
        // Переопределяет абстрактный метод для отображения врага на карте. 
        // Враг отображается как символ '!' с красным цветом. 
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;// Устанавливаем красный цвет для врага. 
            return base.Rendering_on_the_map(); // Используем символ из базового класса для отображения 
            врага.
        }
    }
}
