using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace practica_24
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Класс сущности Студент
        public class Student
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
        }

        // Пример данных (можно заменить реальными данными из базы)
        private List<Student> students = new List<Student>
        {
            new Student { StudentId = 1, Name = "Иванов" },
            new Student { StudentId = 2, Name = "Петров" },
            new Student { StudentId = 3, Name = "Сидоров" }
        };

        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = students;
        }

        // Класс окна для добавления студента
        public class AddStudentWindow : Window
        {
            private List<Student> students;

            public Student NewStudent { get; private set; }

            public AddStudentWindow(List<Student> students)
            {
                this.students = students;
                InitializeWindow();
            }

            private void InitializeWindow()
            {
                Title = "Добавление студента";
                Width = 300;
                Height = 200;

                StackPanel stackPanel = new StackPanel();

                TextBox textBoxName = new TextBox();
                textBoxName.Margin = new Thickness(10);
                Label labelName = new Label();
                labelName.Content = "Имя студента:";
                stackPanel.Children.Add(labelName);
                stackPanel.Children.Add(textBoxName);

                Button addButton = new Button();
                addButton.Content = "Добавить";
                addButton.Margin = new Thickness(10);
                addButton.Click += (sender, e) =>
                {
                    NewStudent = new Student
                    {
                        StudentId = GetNextStudentId(),
                        Name = textBoxName.Text
                    };
                    Close();
                };
                stackPanel.Children.Add(addButton);

                Content = stackPanel;
            }

            private int GetNextStudentId()
            {
                return students.Count + 1;
            }
        }

        // Класс окна для редактирования студента
        public class EditStudentWindow : Window
        {
            private Student _student;

            public EditStudentWindow(Student student)
            {
                _student = student;
                InitializeWindow();
            }

            private void InitializeWindow()
            {
                Title = "Редактирование студента";
                Width = 300;
                Height = 200;

                StackPanel stackPanel = new StackPanel();

                TextBox textBoxName = new TextBox();
                textBoxName.Margin = new Thickness(10);
                textBoxName.Text = _student.Name; // Заполнение поля именем студента для редактирования
                Label labelName = new Label();
                labelName.Content = "Имя студента:";
                stackPanel.Children.Add(labelName);
                stackPanel.Children.Add(textBoxName);

                Button saveButton = new Button();
                saveButton.Content = "Сохранить";
                saveButton.Margin = new Thickness(10);
                saveButton.Click += (sender, e) =>
                {
                    _student.Name = textBoxName.Text; // Обновление имени студента
                    Close();
                };
                stackPanel.Children.Add(saveButton);

                Content = stackPanel;
            }
        }

        // Обработчик события клика по кнопке "Добавить"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow = new AddStudentWindow(students);
            addStudentWindow.Owner = this;

            if (addStudentWindow.ShowDialog() == true)
            {
                Student newStudent = addStudentWindow.NewStudent;
                students.Add(newStudent);
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = students;
            }
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = students;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = students;
            }
        }
        // Обработчик события клика по кнопке "Редактировать"
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is Student selectedStudent)
            {
                EditStudentWindow editStudentWindow = new EditStudentWindow(selectedStudent);
                editStudentWindow.Owner = this;

                if (editStudentWindow.ShowDialog() == true)
                {
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = students;
                }
            }
        }
    }
}
