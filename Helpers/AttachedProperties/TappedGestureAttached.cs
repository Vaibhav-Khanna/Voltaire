using System.Windows.Input;
using Xamarin.Forms;

namespace voltaire.Helpers.AttachedProperties
{

    public class TappedGestureAttached
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(View), null, BindingMode.OneWay, null, OnItemTappedChanged);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(View), null, BindingMode.OneWay, null);


        public static object GetCommandParameter(BindableObject bindable)
        {
            return bindable.GetValue(CommandProperty);
        }

        public static void SetCommandParameter(BindableObject bindable, object value)
        {
            bindable.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(BindableObject bindable)
        {
            return (ICommand)bindable.GetValue(CommandProperty);
        }

        public static void SetCommand(BindableObject bindable, ICommand value)
        {
            bindable.SetValue(CommandProperty, value);
        }

        public static void OnItemTappedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as View;

            if (control != null)
            {
                control.GestureRecognizers.Clear();
                control.GestureRecognizers.Add(
                    new TapGestureRecognizer
                    {
                        Command = new Command(o =>
                        {

                            var command = GetCommand(control);

                            if (command != null && command.CanExecute(control.GetValue(CommandParameterProperty)))
                                command.Execute(control.GetValue(CommandParameterProperty));
                        })
                    }
                );
            }
        }
    }
}

