using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFormsApp
{
    public static class PageBehavior
    {
        public static readonly BindableProperty AppearingProperty =
            BindableProperty.CreateAttached("Appearing", typeof(ICommand), typeof(PageBehavior), null, propertyChanged: OnAppearingChanged);

        public static ICommand GetAppearing(BindableObject bindableObject)
        {
            return (ICommand)bindableObject.GetValue(AppearingProperty);
        }

        private static void OnAppearingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Page target)
            {
                if (oldValue == null && newValue != null)
                {
                    target.Appearing += OnAppearing;
                }
                else if (oldValue != null && newValue == null)
                {
                    target.Appearing -= OnAppearing;
                }
            }
        }

        private static void OnAppearing(object o, EventArgs eventArgs)
        {
            var command = GetAppearing((BindableObject)o);
            if (command.CanExecute(eventArgs))
                command.Execute(eventArgs);
        }
    }
}
