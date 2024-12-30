namespace ChartGenerator
{
    internal class TemplatedItemView : ContentView
    {
        public bool HideOnNullContent { get; set; } = false;

        #region Binable properties

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(TemplatedItemView), propertyChanged: ItemTemplateChanged);

        public static readonly BindableProperty ItemProperty = BindableProperty.Create(nameof(Item), typeof(object), typeof(TemplatedItemView), null, propertyChanged: SourceChanged);

        #endregion

        #region Public properties

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        public object Item
        {
            get => (object)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }
        #endregion

        private readonly ContentView contentView;
        public TemplatedItemView()
        {
            var grid = new Grid();
            contentView = new ContentView();
            grid.Children.Add(contentView);

            this.Content = grid;
        }

        #region Methods

        private static void ItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as TemplatedItemView;
            if (control != null && control.ItemTemplate != null)
                control.GenerateItem();
        }

        private static void SourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as TemplatedItemView;
            if (control != null && control.ItemTemplate != null)
                control.GenerateItem();
        }

        public static View CreateTemplateForItem(object item, DataTemplate itemTemplate, bool createDefaultIfNoTemplate = true)
        {
            //Check to see if we have a template selector or just a template
            var templateToUse = itemTemplate is DataTemplateSelector templateSelector ? templateSelector.SelectTemplate(item, null) : itemTemplate;

            //If we still don't have a template, create a label
            if (templateToUse == null)
                return createDefaultIfNoTemplate ? new Label() { Text = item.ToString() } : new Label() { Text = string.Empty };

            //Create the content
            //If a view wasn't created, we can't use it, exit
            if (!(templateToUse.CreateContent() is View view)) return new Label() { Text = item.ToString() };

            //Set the binding
            view.BindingContext = item;

            return view;
        }

        protected internal void GenerateItem()
        {
            if (Item == null)
            {
                // null! is used here to suppress the nullable warning because Content being null.
                contentView.Content = null!;
                return;
            }

            //Create the content
            try
            {
                contentView.Content = CreateTemplateForItem(Item, ItemTemplate);
            }
            catch
            {
                // null! is used here to suppress the nullable warning because Content being null.
                contentView.Content = null!;
            }
            finally
            {
                if (HideOnNullContent)
                    IsVisible = contentView.Content != null;
            }
        }

        #endregion
    }
}
