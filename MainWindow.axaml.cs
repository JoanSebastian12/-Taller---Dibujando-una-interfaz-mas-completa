using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace MiPresentacion;

public partial class MainWindow : Window
{
    private const string MensajeInicial = "Aquí se mostrará el resultado.";
    private bool _saludoAlternativo = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnPresentarme_Click(object? sender, RoutedEventArgs e)
    {
        if (ValidarCampos(out string nombre, out string apellido, out int edad))
        {
            txtResultado.Foreground = new SolidColorBrush(Color.Parse("#1E293B"));
            txtResultado.Text = ObtenerMensajePresentacion(nombre, apellido, edad);
        }
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        txtNombre.Text = string.Empty;
        txtApellido.Text = string.Empty;
        txtEdad.Text = string.Empty;

        txtResultado.Foreground = new SolidColorBrush(Color.Parse("#64748B"));
        txtResultado.Text = MensajeInicial;

        txtNombre.Focus();
    }

    private void BtnCambiarSaludo_Click(object? sender, RoutedEventArgs e)
    {
        _saludoAlternativo = !_saludoAlternativo;

        if (ValidarCamposSilencioso(out string nombre, out string apellido, out int edad))
        {
            txtResultado.Foreground = new SolidColorBrush(Color.Parse("#1E293B"));
            txtResultado.Text = ObtenerMensajePresentacion(nombre, apellido, edad);
        }
        else
        {
            txtResultado.Foreground = new SolidColorBrush(Color.Parse("#0D9488"));
            txtResultado.Text = _saludoAlternativo
                ? "Saludo alternativo seleccionado. Presiona 'Presentarme' para ver el mensaje."
                : "Saludo predeterminado seleccionado. Presiona 'Presentarme' para ver el mensaje.";
        }
    }

    private bool ValidarCampos(out string nombre, out string apellido, out int edad)
    {
        nombre = txtNombre.Text?.Trim() ?? string.Empty;
        apellido = txtApellido.Text?.Trim() ?? string.Empty;
        string edadTexto = txtEdad.Text?.Trim() ?? string.Empty;
        edad = 0;

        if (string.IsNullOrWhiteSpace(nombre))
        {
            MostrarAdvertencia("Advertencia: El nombre no puede estar vacío ni contener únicamente espacios.");
            txtNombre.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(apellido))
        {
            MostrarAdvertencia("Advertencia: El apellido no puede estar vacío ni contener únicamente espacios.");
            txtApellido.Focus();
            return false;
        }

        if (!int.TryParse(edadTexto, out edad))
        {
            MostrarAdvertencia("Advertencia: Por favor ingresa una edad numérica válida. No se permiten letras ni símbolos.");
            txtEdad.Focus();
            return false;
        }

        if (edad <= 0)
        {
            MostrarAdvertencia("Advertencia: Edad inválida. La edad debe ser un número entero positivo mayor a cero.");
            txtEdad.Focus();
            return false;
        }

        return true;
    }

    private bool ValidarCamposSilencioso(out string nombre, out string apellido, out int edad)
    {
        nombre = txtNombre.Text?.Trim() ?? string.Empty;
        apellido = txtApellido.Text?.Trim() ?? string.Empty;
        string edadTexto = txtEdad.Text?.Trim() ?? string.Empty;
        edad = 0;

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
        {
            return false;
        }

        if (!int.TryParse(edadTexto, out edad) || edad <= 0)
        {
            return false;
        }

        return true;
    }

    private void MostrarAdvertencia(string mensaje)
    {
        txtResultado.Foreground = new SolidColorBrush(Color.Parse("#DC2626"));
        txtResultado.Text = mensaje;
    }

    private string ObtenerMensajePresentacion(string nombre, string apellido, int edad)
    {
        if (_saludoAlternativo)
        {
            return $"¡Mucho gusto! Soy {nombre} {apellido},\n" +
                   $"cuento con {edad} años de edad y me apasiona\n" +
                   $"el diseño y desarrollo de software con C# y Avalonia UI.";
        }

        return $"¡Hola! Mi nombre es {nombre} {apellido},\n" +
               $"tengo {edad} años y estoy aprendiendo\n" +
               $"a desarrollar interfaces gráficas con C#.";
    }
}
