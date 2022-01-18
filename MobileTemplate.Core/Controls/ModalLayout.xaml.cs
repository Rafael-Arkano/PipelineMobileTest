namespace MobileTemplate.Controls
{    
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Modal frame content control
    /// </summary>
    [ContentProperty("ModalContent")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalLayout
    {

        /// <summary>
        /// Manages the binding of the <see cref="ModalContent"/> property
        /// </summary>
        public static readonly BindableProperty ModalContentProperty = BindableProperty.Create(
            nameof(ModalContent),
            typeof(View),
            typeof(ModalLayout),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: Content_PropertyChanged
        );


        /// <summary>
        /// Manages the binding of the <see cref="Command"/> property
        /// </summary>
        public static readonly BindableProperty CloseCommandProperty =
            BindableProperty.Create(nameof(CloseCommand), typeof(ICommand), typeof(ICommand));


        /// <summary>
        /// Manages the binding of the <see cref="CommandParameter"/> property
        /// </summary>
        public static readonly BindableProperty CloseCommandParameterProperty =
            BindableProperty.Create(nameof(CloseCommandParameter), typeof(bool), typeof(bool));


        /// <summary>
        /// Manages the binding of the <see cref="CommandParameter"/> property
        /// </summary>
        public static readonly BindableProperty ModalHeightProperty =
            BindableProperty.Create(nameof(ModalHeight), 
                typeof(int), 
                typeof(int), 
                defaultValue: 300,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: ModalHeight_PropertyChanged);


        /// <summary>
        /// Cancel command to close the modal
        /// </summary>
        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }


        /// <summary>
        /// Cancel parameter to close the modal
        /// </summary>
        public bool CloseCommandParameter
        {
            get => (bool)GetValue(CloseCommandParameterProperty);
            set => SetValue(CloseCommandParameterProperty, value);
        }


        /// <summary>
        /// Modal height
        /// </summary>
        public int ModalHeight
        {
            get => (int)GetValue(ModalHeightProperty);
            set => SetValue(ModalHeightProperty, value);
        }


        ///// <summary>
        /// Custom content
        /// </summary>
        public View ModalContent
        {
            get => (View)GetValue(ModalContentProperty);
            set => SetValue(ModalContentProperty, value);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public ModalLayout()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the property change
        /// </summary>        
        private static void Content_PropertyChanged(BindableObject bind, object oldValue, object newValue)
        {
            var control = (ModalLayout)bind;
            if (control.ViewContent == null) return;
            if (ReferenceEquals(newValue, control)) return;
            control.ViewContent.Content = (View)newValue;
        }


        /// <summary>
        /// Handles the property change
        /// </summary>        
        private static void ModalHeight_PropertyChanged(BindableObject bind, object oldValue, object newValue)
        {
            var control = (ModalLayout)bind;
            if (ReferenceEquals(newValue, control)) return;
            control.ModalHeight = (int)newValue;
            control.ModalLayoutHeight.HeightRequest = control.ModalHeight;
        }
        


        /// <summary>
        /// Handles the tab outside the frame
        /// </summary>        
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            this.IsVisible = false;          
        }

    }
}