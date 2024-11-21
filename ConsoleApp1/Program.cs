using System;

// Обобщенный интерфейс для клонирования объектов
public interface IClonable<T> where T : IClonable<T>
{
    T Clone();
}

// Класс Point, реализующий IClonable<Point>
public class Point : IClonable<Point>
{
    public int X { get; set; }
    public int Y { get; set; }

    // Параметрический конструктор (конструктор копирования)
    public Point(Point other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        X = other.X;
        Y = other.Y;
    }

    // Реализация метода Clone
    public Point Clone()
    {
        return new Point(this);
    }

    // Дополнительный конструктор для удобства создания объектов
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Переопределение метода ToString для удобного вывода
    public override string ToString()
    {
        return $"Point(X: {X}, Y: {Y})";
    }
}

// Класс Rectangle, реализующий IClonable<Rectangle>
public class Rectangle : IClonable<Rectangle>
{
    public Point TopLeft { get; set; }
    public Point BottomRight { get; set; }

    // Параметрический конструктор (конструктор копирования)
    public Rectangle(Rectangle other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        TopLeft = other.TopLeft.Clone();
        BottomRight = other.BottomRight.Clone();
    }

    // Реализация метода Clone
    public Rectangle Clone()
    {
        return new Rectangle(this);
    }

    // Дополнительный конструктор для удобства создания объектов
    public Rectangle(Point topLeft, Point bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }

    // Переопределение метода ToString для удобного вывода
    public override string ToString()
    {
        return $"Rectangle(TopLeft: {TopLeft}, BottomRight: {BottomRight})";
    }
}

public class Program
{
    // Метод для клонирования объекта, реализующего IClonable<T>
    public static T CloneObject<T>(T obj) where T : IClonable<T>
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        return obj.Clone();
    }

    public static void Main()
    {
        // Пример клонирования точки
        Point originalPoint = new Point(10, 20);
        Point clonedPoint = CloneObject(originalPoint);

        Console.WriteLine("Оригинальная точка: " + originalPoint);
        Console.WriteLine("Клонированная точка: " + clonedPoint);

        // Изменение клона, чтобы убедиться, что это отдельный объект
        clonedPoint.X = 30;
        clonedPoint.Y = 40;

        Console.WriteLine("\nПосле изменения клона:");
        Console.WriteLine("Оригинальная точка: " + originalPoint);
        Console.WriteLine("Клонированная точка: " + clonedPoint);

        // Пример клонирования прямоугольника
        Rectangle originalRectangle = new Rectangle(
            new Point(0, 0),
            new Point(100, 50)
        );
        Rectangle clonedRectangle = CloneObject(originalRectangle);

        Console.WriteLine("\nОригинальный прямоугольник: " + originalRectangle);
        Console.WriteLine("Клонированный прямоугольник: " + clonedRectangle);

        // Изменение клона, чтобы убедиться, что это отдельный объект
        clonedRectangle.TopLeft.X = -10;
        clonedRectangle.TopLeft.Y = -20;

        Console.WriteLine("\nПосле изменения клона прямоугольника:");
        Console.WriteLine("Оригинальный прямоугольник: " + originalRectangle);
        Console.WriteLine("Клонированный прямоугольник: " + clonedRectangle);
    }
}
