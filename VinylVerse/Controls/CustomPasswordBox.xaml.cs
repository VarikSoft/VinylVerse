using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinylVerse.Controls
{
    public partial class CustomPasswordBox : UserControl
    {
        // Хранит реальное значение пароля
        private string _realPassword = string.Empty;

        public CustomPasswordBox()
        {
            InitializeComponent();
            IsPasswordHidden = true; // по умолчанию пароль скрыт
            UpdateTextBox();
            UpdatePlaceholderVisibility();
        }

        #region Свойства-зависимости

        /// <summary>
        /// Свойство для реального пароля (для привязки)
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(CustomPasswordBox),
                new PropertyMetadata(string.Empty, OnPasswordChanged));

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomPasswordBox cpb)
            {
                cpb._realPassword = e.NewValue as string ?? string.Empty;
                cpb.UpdateTextBox();
                cpb.UpdatePlaceholderVisibility();
            }
        }

        /// <summary>
        /// Режим отображения: true – пароль скрыт (маскируется), false – отображается в открытом виде.
        /// </summary>
        public static readonly DependencyProperty IsPasswordHiddenProperty =
            DependencyProperty.Register(nameof(IsPasswordHidden), typeof(bool), typeof(CustomPasswordBox),
                new PropertyMetadata(true, OnIsPasswordHiddenChanged));

        public bool IsPasswordHidden
        {
            get => (bool)GetValue(IsPasswordHiddenProperty);
            set => SetValue(IsPasswordHiddenProperty, value);
        }

        private static void OnIsPasswordHiddenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomPasswordBox cpb)
            {
                cpb.UpdateTextBox();
            }
        }

        /// <summary>
        /// Символ маски. По умолчанию – '●'
        /// </summary>
        public static readonly DependencyProperty MaskCharacterProperty =
            DependencyProperty.Register(nameof(MaskCharacter), typeof(char), typeof(CustomPasswordBox),
                new PropertyMetadata('●', OnMaskCharacterChanged));

        public char MaskCharacter
        {
            get => (char)GetValue(MaskCharacterProperty);
            set => SetValue(MaskCharacterProperty, value);
        }

        private static void OnMaskCharacterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomPasswordBox cpb)
            {
                cpb.UpdateTextBox();
            }
        }

        /// <summary>
        /// Текст подсказки (placeholder), отображается при пустом поле без фокуса.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CustomPasswordBox),
                new PropertyMetadata("Введите пароль"));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        #endregion

        #region Обновление отображения

        /// <summary>
        /// Обновляет текст в TextBox: если пароль скрыт – отображаются символы маски, иначе – реальный текст.
        /// </summary>
        private void UpdateTextBox()
        {
            if (IsPasswordHidden)
            {
                PART_TextBox.Text = new string(MaskCharacter, _realPassword.Length);
            }
            else
            {
                PART_TextBox.Text = _realPassword;
            }
        }

        /// <summary>
        /// Управляет видимостью placeholder'а.
        /// </summary>
        private void UpdatePlaceholderVisibility()
        {
            PlaceholderTextBlock.Visibility = (string.IsNullOrEmpty(_realPassword) && !PART_TextBox.IsFocused)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        #endregion

        #region Переключение режима (ToggleButton)

        private void PART_ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // При Checked пароль становится видимым
            IsPasswordHidden = false;
            UpdateTextBox();
        }

        private void PART_ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // При Unchecked пароль скрыт
            IsPasswordHidden = true;
            UpdateTextBox();
        }

        #endregion

        #region Обработка ввода в TextBox

        private void PART_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void PART_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        /// <summary>
        /// При изменении текста в режиме отображения (пароль виден) обновляем _realPassword.
        /// </summary>
        private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsPasswordHidden)
            {
                _realPassword = PART_TextBox.Text;
                Password = _realPassword;
            }
            UpdatePlaceholderVisibility();
        }

        /// <summary>
        /// Обработка ввода символов в скрытом режиме.
        /// Если есть выделение – удаляем его, затем вставляем новый символ и отображаем маску.
        /// </summary>
        private void PART_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsPasswordHidden)
            {
                int caretIndex = PART_TextBox.CaretIndex;
                if (PART_TextBox.SelectionLength > 0)
                {
                    int selectionStart = PART_TextBox.SelectionStart;
                    _realPassword = _realPassword.Remove(selectionStart, PART_TextBox.SelectionLength);
                    caretIndex = selectionStart;
                }
                _realPassword = _realPassword.Insert(caretIndex, e.Text);
                Password = _realPassword;
                PART_TextBox.Text = new string(MaskCharacter, _realPassword.Length);
                PART_TextBox.CaretIndex = caretIndex + e.Text.Length;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Обработка клавиш Backspace, Delete и вставки (Ctrl+V) в скрытом режиме с учётом выделения.
        /// </summary>
        private void PART_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsPasswordHidden)
            {
                int selectionStart = PART_TextBox.SelectionStart;
                int selectionLength = PART_TextBox.SelectionLength;
                int caretIndex = PART_TextBox.CaretIndex;

                if (e.Key == Key.Back)
                {
                    if (selectionLength > 0)
                    {
                        _realPassword = _realPassword.Remove(selectionStart, selectionLength);
                        caretIndex = selectionStart;
                    }
                    else if (caretIndex > 0)
                    {
                        _realPassword = _realPassword.Remove(caretIndex - 1, 1);
                        caretIndex--;
                    }
                    Password = _realPassword;
                    PART_TextBox.Text = new string(MaskCharacter, _realPassword.Length);
                    PART_TextBox.CaretIndex = caretIndex;
                    e.Handled = true;
                }
                else if (e.Key == Key.Delete)
                {
                    if (selectionLength > 0)
                    {
                        _realPassword = _realPassword.Remove(selectionStart, selectionLength);
                        caretIndex = selectionStart;
                    }
                    else if (caretIndex < _realPassword.Length)
                    {
                        _realPassword = _realPassword.Remove(caretIndex, 1);
                    }
                    Password = _realPassword;
                    PART_TextBox.Text = new string(MaskCharacter, _realPassword.Length);
                    PART_TextBox.CaretIndex = caretIndex;
                    e.Handled = true;
                }
                else if (e.Key == Key.V && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    if (Clipboard.ContainsText())
                    {
                        string pasteText = Clipboard.GetText();
                        if (selectionLength > 0)
                        {
                            _realPassword = _realPassword.Remove(selectionStart, selectionLength);
                            caretIndex = selectionStart;
                        }
                        _realPassword = _realPassword.Insert(caretIndex, pasteText);
                        Password = _realPassword;
                        PART_TextBox.Text = new string(MaskCharacter, _realPassword.Length);
                        PART_TextBox.CaretIndex = caretIndex + pasteText.Length;
                    }
                    e.Handled = true;
                }
            }
        }

        #endregion
    }
}
