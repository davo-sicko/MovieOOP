using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp
{
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public String Genre { get; set; }
        public int Year { get; set; }
        public int Rating { get; set; }

        public Movie(int id, string title, string genre, int year, int rating)
        {
            Id = id;
            Title = title;
            Genre = genre;
            Year = year;
            Rating = rating;
        }
        public override string ToString()
        {
            return "\nID: " + Id + "\nНазвание фильма: " + Title + 
                "\nЖанр фильма: " + Genre + "\nГод: " + Year + "\nРейтинг: " + Rating;
        }
    }
    internal class Program
    {
        static List<Movie> movies;
        static void Main(string[] args)
        {
            movies = new List<Movie>();
            Movie m1 = new Movie(0, "Лицо со шрамом", "Драма", 1984, 2);
            Movie m2 = new Movie(1, "Титаник", "Драма", 1999, 4);
            Movie m3 = new Movie(2, "Король Лев", "Драма", 2000, 4);
            Movie m4 = new Movie(3, "Такси 4", "Драма", 2004, 4);
            Movie m5 = new Movie(4, "Тачки", "Драма", 2008, 2);
            movies.Add(m1);
            movies.Add(m2);
            movies.Add(m3);
            movies.Add(m4);
            movies.Add(m5);
            Menu();
        }
        public static void showMovies()
        {
            foreach (Movie movie in movies)
            {
                Console.WriteLine(movie);
            }
        }
        public static void addMovie()
        {
            try
            {
                Console.WriteLine("\nВведите ID фильма: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите название фильма: ");
                String title = Console.ReadLine();
                Console.WriteLine("Введите жанр фильма: ");
                String genre = Console.ReadLine();
                Console.WriteLine("Введите год фильма: ");
                int year = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите рейтинг данного фильма: ");
                int rating = int.Parse(Console.ReadLine());

                if (id <= 0 || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(genre) || year <= 0 || rating <= 0)
                {
                    Console.WriteLine("Не все данные были введены, попробуйте еще раз!");
                    return;
                }
                else
                {
                    Movie movie = new Movie(id, title, genre, year, rating);
                    movies.Add(movie);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Что то пошло не так: " + e.ToString());
            }
        }
        public static void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nДобро пожаловать в каталог фильмов!");
                Console.WriteLine("1. Показать все фильмы");
                Console.WriteLine("2. Найти фильм по названию");
                Console.WriteLine("3. Показать фильмы, выпущенные после 2000");
                Console.WriteLine("4. Показать фильм с наивысшим рейтингом");
                Console.WriteLine("5. Добавить фильм");
                Console.WriteLine("6. Удалить фильм по ID");
                Console.WriteLine("7. Показать статистику");
                Console.WriteLine("0. Выход");
                Console.WriteLine("\n Теперь выбери подходящий пункт: ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());


                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("\nДо свидания!");
                            return;
                        case 1:
                            showMovies();
                            break;
                        case 2:
                            findMovieByTitle();
                            break;
                        case 3:
                            findMovieByYear();
                            break;
                        case 4:
                            findMovieByRating();
                            break;
                        case 5:
                            addMovie();
                            break;
                        case 6:
                            removeMovie();
                            break;
                        case 7:
                            movieStatistics();
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Ошибка: " + e);
                }
            }
            
        }

        private static void movieStatistics()
        {
            if (movies.Count == 0)
            {
                Console.WriteLine("Нет фильмов в коллекции!");
                return;
            }
            else
            {
                double sum = 0;
                Movie oldest = movies[0];
                Movie newest = movies[0];
                foreach (Movie movie in movies)
                {
                    sum += movie.Rating;
                    if (movie.Year < oldest.Year)
                    {
                        oldest = movie;
                    }
                    if (movie.Year > newest.Year)
                    {
                        newest = movie;
                    }
                }
                Console.WriteLine("\nСТАТИСТИКА:");
                Console.WriteLine("Количество фильмов: " + movies.Count);
                Console.WriteLine("Средний рейтинг: " + sum / movies.Count);
                Console.WriteLine("Самый старый фильм: " + oldest.Title);
                Console.WriteLine("Самый новый фильм: " + newest.Title);
            }

        }

        public static void removeMovie()
        {
            bool found = false;
            Console.WriteLine("Введите ID фильма, который хотите удалить: ");
            int id = int.Parse(Console.ReadLine());
            foreach (Movie movie in movies)
            {
                if (movie.Id == id)
                {
                    Console.WriteLine("Фильм найден: \n" + movie + "\n");
                    movies.Remove(movie);
                    found = true;
                    return;
                }
            }
            if (!found)
            {
                Console.WriteLine("Фильм не найден!");
                return;
            }
        }

        public static void findMovieByRating()
        {
            if (movies.Count == 0) return;
            bool found = false;
            Movie highestRating = movies[0];
            foreach (Movie movie in movies)
            {
                if (movie.Rating > highestRating.Rating)
                {
                    found = true; 
                    highestRating = movie;
                }
            }
            Console.WriteLine("\nФильм с наивысшим рейтингом: " + highestRating);
        }

        public static void findMovieByYear()
        {
            bool found = false;
            Console.Write("Все фильмы, выпущенные после 2000 года: ");
            
            foreach(Movie movie in movies)
            {
                if (movie.Year >= 2000)
                {
                    found = true;
                    Console.WriteLine("Результат: \n");
                    Console.WriteLine("\n" + movie);
                }
            }
            if (!found)
            {
                Console.WriteLine("Нет таких фильмов");
            }
        }

        public static void findMovieByTitle()
        {
            bool found = false;
            Console.Write("\nВведите название фильма: ");
            String title = Console.ReadLine();
            if (!string.IsNullOrEmpty(title))
            {
                foreach (Movie movie in movies)
                {
                    if (movie.Title == title)
                    {
                        found = true;
                        Console.WriteLine("\nНашли: \n" + movie);
                    }
                }
            }
            else
            {
                Console.WriteLine("Название не введено");
                return;
            }
            if (!found)
            {
                Console.WriteLine("Фильм не найден!\n");
            }
        }
    }
}

