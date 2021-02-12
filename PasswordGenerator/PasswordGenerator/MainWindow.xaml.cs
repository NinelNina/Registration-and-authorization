using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PasswordGenerator
{
    public partial class MainWindow : Window
    {
        string loginAuth;
        string passwordAuth;
        bool loginIsNotEmpty;
        bool passwordIsNotEmpty;

        string fileName = "accounts.txt";
        public MainWindow()
        {
            InitializeComponent();

            loginIsNotEmpty = false;
            passwordIsNotEmpty = false;
            Button1.IsEnabled = false;
        }

        private void generate_Click(object sender, RoutedEventArgs e)
        {
            GeneratePassword generatePassword = new GeneratePassword();

            generatedPassword.Content = generatePassword.Generator();
           
            registration.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter writeToFile = new StreamWriter(fileName, true))
                {
                    writeToFile.WriteLine(login.Text + ";" + generatedPassword.Content);
                }

                MessageBox.Show("Вы успешно зарегистрированы.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (login.Text != " " && login.Text.Length > 2)
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader readFromFile = new StreamReader(fileName))
                    {
                        var reader = readFromFile.ReadToEnd();
                        if (reader.Contains(login.Text + ";"))
                        {
                            isLoginFree.IsEnabled = true;
                            isLoginFree.Foreground = new SolidColorBrush(Colors.Red);
                            isLoginFree.Content = "Логин занят";
                            generate.IsEnabled = false;
                        }
                        else
                        {
                            isLoginFree.IsEnabled = true;
                            isLoginFree.Foreground = new SolidColorBrush(Colors.Green);
                            isLoginFree.Content = "Логин свободен";
                            generate.IsEnabled = true;
                        }
                    }
                }
                else
                {
                    isLoginFree.IsEnabled = true;
                    isLoginFree.Foreground = new SolidColorBrush(Colors.Green);
                    isLoginFree.Content = "Логин свободен";
                    generate.IsEnabled = true;
                }
            }
            else
            {
                isLoginFree.IsEnabled = true;
                isLoginFree.Foreground = new SolidColorBrush(Colors.Red);
                isLoginFree.Content = "Логин занят";
                generate.IsEnabled = false;
            }
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            loginAuth = InputLogin.Text;
            passwordAuth = InputPassword.Password;

            bool isRegistrated = false;

            if (File.Exists(fileName))
            {
                using (StreamReader readFromFile = new StreamReader(fileName))
                {
                    var reader = readFromFile.ReadToEnd();
                    if (reader.Contains(loginAuth + ";" + passwordAuth))
                    {
                        isRegistrated = true;
                    }
                    else
                    {
                        isRegistrated = false;
                    }
                }
            }
            else
            {
                isRegistrated = false;
            }

            if (isRegistrated)
            {
                MessageBox.Show("Вход выполнен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InputLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputLogin.Text != "")
            {
                loginIsNotEmpty = true;
            }
            else
            {
                loginIsNotEmpty = false;
            }

            if (loginIsNotEmpty && passwordIsNotEmpty)
            {
                Button1.IsEnabled = true;
            }
            else
            {
                Button1.IsEnabled = false;
            }
        }

        private void InputPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (InputPassword.Password != "")
            {
                passwordIsNotEmpty = true;
            }
            else
            {
                passwordIsNotEmpty = false;
            }

            if (loginIsNotEmpty && passwordIsNotEmpty)
            {
                Button1.IsEnabled = true;
            }
            else
            {
                Button1.IsEnabled = false;
            }
        }
    }
}
