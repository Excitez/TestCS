using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestCS
{
    internal class TestCs
    {
        public static void Main(string[] args)
        {
            var teachers = new List<Teacher>();
            var students = new List<Student>();
            var exams = new List<Exams>();

            SetValues(teachers, students, exams);
            TeacherFewest(teachers, students);
            PhysicsExamAverage(exams);
            StudentsMore90PointsInMathematicsAndTeacherAlex(teachers, students, exams);
        }

        
        private static void SetValues(List<Teacher> teachers, List<Student> students, List<Exams> exams)
        {
            teachers.Add(new Teacher { Id = 1, Name = "Alex", LastName = "Ivanov", Age = 35, Lesson = LessonType.Mathematics });
            teachers.Add(new Teacher { Id = 2, Name = "Marina", LastName = "Petrova", Age = 41, Lesson = LessonType.Physics });
            teachers.Add(new Teacher { Id = 3, Name = "Svetlana", LastName = "Ivanova", Age = 45, Lesson = LessonType.Mathematics });
            teachers.Add(new Teacher { Id = 4, Name = "Nadya", LastName = "Karpova", Age = 39, Lesson = LessonType.Physics });

            students.Add(new Student { Id = 1, Name = "Petya", LastName = "Ivanov", Age = 14, TeacherId = 1});
            students.Add(new Student { Id = 2, Name = "Marina", LastName = "Ivanova", Age = 14, TeacherId = 1});
            students.Add(new Student { Id = 3, Name = "Vlad", LastName = "Ivanov", Age = 14, TeacherId = 2});
            students.Add(new Student { Id = 4, Name = "Andrey", LastName = "Ivanov", Age = 14, TeacherId = 3});
            students.Add(new Student { Id = 5, Name = "Alex", LastName = "Ivanov", Age = 14, TeacherId = 4});
            students.Add(new Student { Id = 6, Name = "Petr", LastName = "Ivanov", Age = 14, TeacherId = 4});
            students.Add(new Student { Id = 7, Name = "Vladimir", LastName = "Ivanov", Age = 14, TeacherId = 3});

            exams.Add(new Exams { Lesson = LessonType.Mathematics, StudentId = 1, TeacherId = 1, Score = 95, ExamDate = new DateTime(2023, 5, 10) });
            exams.Add(new Exams { Lesson = LessonType.Mathematics, StudentId = 2, TeacherId = 1, Score = 92, ExamDate = new DateTime(2023, 6, 11) });
            exams.Add(new Exams { Lesson = LessonType.Mathematics, StudentId = 3, TeacherId = 2, Score = 91, ExamDate = new DateTime(2023, 7, 11) });
            exams.Add(new Exams { Lesson = LessonType.Physics, StudentId = 3, TeacherId = 2, Score = 75, ExamDate = new DateTime(2023, 6, 15) });
        }


        private static void TeacherFewest(List<Teacher> teachers, List<Student> students)
        {
            // 1.Найти учителя у которого в классе меньше всего учеников 
            var teacherWithFewestStudents = teachers
                .OrderBy(t => students.Count(s => s.TeacherId == t.Id))
                .FirstOrDefault();
            
            if (teacherWithFewestStudents != null)
            {
                Console.WriteLine($"Учитель с наименьшим числом учеников: {teacherWithFewestStudents.Name}");
            }
            else
            {
                Console.WriteLine("Учителей не найдено.");
            }
        }

        private static void PhysicsExamAverage(List<Exams> exams)
        {
            // 2. Найти средний бал экзамена по Физики за 2023 год.
            var physicsExams2023 = exams
                .Where(e => e.Lesson == LessonType.Physics && e.ExamDate.Year == 2023);

            if (physicsExams2023.Any())
            {
                var averageScore = physicsExams2023.Average(e => e.Score);
                Console.WriteLine($"Средний балл по Физике за 2023 год: {averageScore}");
            }
            else
            {
                Console.WriteLine("Экзамены по Физике за 2023 год не найдены.");
            }
        }


        private static void StudentsMore90PointsInMathematicsAndTeacherAlex(List<Teacher> teachers, List<Student> students, List<Exams> exams)
        {
            // 3 Получить количество учиников которые по экзамену Математики получили больше 90 баллов, где учитель Alex 
            
            var alex = teachers.FirstOrDefault(t => t.Name == "Alex");

            if (alex != null)
            {
                var mathStudentsWithHighScore = students
                    .Where(s => s.TeacherId == alex.Id)
                    .Join(exams.Where(e => e.Lesson == LessonType.Mathematics && e.Score > 90),
                        student => student.Id,
                        exam => exam.StudentId,
                        (student, exam) => student);

                var count = mathStudentsWithHighScore.Count();
                Console.WriteLine($"Количество учеников по Математике с баллами > 90, учитель Alex: {count}");
            }
            else
            {
                Console.WriteLine("Учитель Alex не найден.");
            }
        }
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
        }

        public class Teacher : Person
        {
            public LessonType Lesson { get; set; }
        }

        public class Student : Person
        {
            public int TeacherId { get; set; }
        }

        public class Exams
        {
            public LessonType Lesson { get; set; }
            public int StudentId { get; set; }
            public int TeacherId { get; set; }
            public int Score { get; set; }
            public DateTime ExamDate { get; set; }
            public Student Student { get; set; }
            public Teacher Teacher { get; set; }
        }

        public enum LessonType
        {
            Mathematics = 1,
            Physics = 2
        }
        
    }
}