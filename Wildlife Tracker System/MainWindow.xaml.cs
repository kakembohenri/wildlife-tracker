using Microsoft.SqlServer.Server;
using Microsoft.Win32;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Wildlife_Tracker_System.Animals;
using Wildlife_Tracker_System.Animals.Mammal;
using Wildlife_Tracker_System.Animals.Mammal.Dog;
using Wildlife_Tracker_System.Animals.Reptile;

namespace Wildlife_Tracker_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Pick Category from which species list shall be determined
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void HandleCategory(Object Sender, RoutedEventArgs e)
        {
            Button clickedButton = Sender as Button;
            if (clickedButton != null)
            {
                category_specific_fields.Children.Clear();
                species_specific_fields.Children.Clear();

                string category = clickedButton.Name;

                List<string> species = FetchSpecies(category);

                if (species.Count() > 0)
                {
                    species_container.Children.Clear();
                }

                foreach(var specie in species)
                {
                    Button button = new Button();
                    
                    button.Content = specie;
                    button.Name = specie;
                    button.Margin = new Thickness(5, 5, 0, 5);
                    button.Background = Brushes.Transparent;
                    button.BorderThickness = new Thickness(0);
                    button.HorizontalContentAlignment = HorizontalAlignment.Left;
                    button.Click += HandleSpecies;
                    species_container.Children.Add(button);
                }
                
                foreach(UIElement element in animal_category.Children)
                {
                    if (element is Button)
                    {
                        // Cast the UIElement to Button
                        Button button = (Button)element;

                        // Assign the desired background color
                        button.Background = Brushes.Transparent;
                    }
                }
                clickedButton.Background = Brushes.Gray;
            }
        
        }

        private void HandleSpecies(Object Sender, RoutedEventArgs e)
        {
            Button clickedButton = Sender as Button;
            if (clickedButton != null)
            {
                species_specific_fields.Children.Clear();

                string species = clickedButton.Name;

                // Create elements which shall form necessary input fields
                /* Elements to be added include;
                 - DockPanel
                 - TextBox
                 - TextBlock
                 */

                DockPanel dockPanel = new DockPanel();
                TextBlock textBlock = new TextBlock();
                TextBox textBox = new TextBox();

                textBlock.Margin = new Thickness(5);
                textBox.Margin = new Thickness(5);
                textBox.Width = 200;
                textBox.HorizontalAlignment = HorizontalAlignment.Right;

                switch (species)
                {
                    case "Cat":
                        textBlock.Text = "Eye Color";
                        textBlock.Name = "EyeColor";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                    case "Dog":
                        textBlock.Text = "Energy Level";
                        textBlock.Name = "EnergyLevel";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                    case "Snake":
                        textBlock.Text = "Place Of Origin";
                        textBlock.Name = "PlaceOfOrigin";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                    case "Crocodile":
                        textBlock.Text = "Species Variation";
                        textBlock.Name = "SpeciesVariation";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                }

                foreach (UIElement element in species_container.Children)
                {
                    if (element is Button)
                    {
                        // Cast the UIElement to Button
                        Button button = (Button)element;

                        // Assign the desired background color
                        button.Background = Brushes.Transparent;
                    }
                }
                clickedButton.Background = Brushes.Gray;
            }

        }
        private void Submit(Object Sender, RoutedEventArgs e)
        {
            Dog dog = new Dog();

            dog.Name = name.Text;
            dog.Age = age.Text;
            // Send the data (for example, display in a message box)
            MessageBox.Show($"Name: {dog.Name}\nEmail: {dog.Age}");
        }

        private void UploadImage(object sender, RoutedEventArgs e)
        {
            // Open file dialog to select image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.png;*.jpeg;*.gif)|*.jpg;*.png;*.jpeg;*.gif";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Load selected image file and display in the Image control
                    BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                    imgPreview.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    
        private List<string> FetchSpecies(string category)
        {
            List <string> items = new List<string>();
            category_specific_fields.Children.Clear();

            // Create elements which shall form necessary input fields
            /* Elements to be added include;
             - DockPanel
             - TextBox
             - TextBlock
             */

            DockPanel dockPanel = new DockPanel();
            TextBlock textBlock = new TextBlock();
            TextBox textBox = new TextBox();

            textBlock.Margin = new Thickness(5);
            textBox.Margin = new Thickness(5);
            textBox.Width = 200;
            textBox.HorizontalAlignment = HorizontalAlignment.Right;

            switch (category)
            {
                case "Mammal":
                    textBlock.Text = "Gestation Period";
                    textBlock.Name = "GestationPeriod";

                    dockPanel.Children.Add(textBlock);
                    dockPanel.Children.Add(textBox);

                    category_specific_fields.Children.Add(dockPanel);

                    Mammal mammals = new Mammal();
                    items = mammals.Species;
                    return items;
                case "Reptile":
                    textBlock.Text = "Size";
                    textBlock.Name = "Size";

                    dockPanel.Children.Add(textBlock);
                    dockPanel.Children.Add(textBox);

                    category_specific_fields.Children.Add(dockPanel);
                    Reptlie reptile = new Reptlie();
                    items = reptile.Species;
                    return items;
            }
            return items;
        }
    
        private void FetchAllAnimals(Object Sender, RoutedEventArgs e)
        {
            CheckBox checkBox = Sender as CheckBox;

            List<string> animals = new List<string>();

            Mammal mammals = new Mammal();
            Reptlie repitles = new Reptlie();

            // Clear any new fields created as a result of selecting a category or specie
            category_specific_fields.Children.Clear();
            species_specific_fields.Children.Clear();

            // Clear all spieces
            species_container.Children.Clear();

            if (checkBox.IsChecked == true)
            {
                foreach (string mammal in mammals.Species)
                {
                    animals.Add(mammal);
                }

                foreach (string repitle in repitles.Species)
                {
                    animals.Add(repitle);
                }

                foreach (UIElement element in animal_category.Children)
                {
                    if (element is Button)
                    {
                        // Cast the UIElement to Button
                        Button button = (Button)element;

                        // Disable button
                        button.IsEnabled = false;
                    }
                }

                // Gray out container housing categories
                border_animal_category.Background = Brushes.LightGray;

                // List all animals in species container
                foreach (string item in animals)
                {
                    Button button = new Button();

                    button.Content = item;
                    button.Name = item;
                    button.Margin = new Thickness(5, 5, 0, 5);
                    button.Background = Brushes.Transparent;
                    button.BorderThickness = new Thickness(0);
                    button.HorizontalContentAlignment = HorizontalAlignment.Left;
                    button.Click += GetCategory;
                    species_container.Children.Add(button);
                }
            }
            else
            {
                // Revert to normal input status

                // Remove all animals from spieces container
                species_container.Children.Clear();

                // Remove gray background from categories container
                border_animal_category.Background = Brushes.Transparent;

                // Reset button on click function, isEnabled and background
                foreach (UIElement element in animal_category.Children)
                {
                    if (element is Button)
                    {
                        // Cast the UIElement to Button
                        Button button = (Button)element;

                        // Disable button
                        button.IsEnabled = true;
                        button.Background = Brushes.Transparent;
                        button.Click += HandleCategory;
                    }
                }
            }

        }
    
        private void GetCategory(Object Sender, RoutedEventArgs e)
        {
            Button clickedButton = Sender as Button;

            if(clickedButton != null)
            {
                foreach (UIElement element in animal_category.Children)
                {
                    if (element is Button)
                    {
                        // Cast the UIElement to Button
                        Button button = (Button)element;

                        // Disable button
                        button.IsEnabled = false;
                    }
                }

                string species = clickedButton.Name;

                switch (species)
                {
                    case "Cat":
                        Mammal.IsEnabled = true;
                        Mammal.Background = Brushes.Yellow;
                        Mammal.Click += DoNothing;
                        break;
                    case "Dog":
                        Mammal.IsEnabled = true;
                        Mammal.Background = Brushes.Yellow;
                        Mammal.Click += DoNothing;
                        break;
                    case "Snake":
                        Reptile.IsEnabled = true;
                        Reptile.Background = Brushes.Yellow;
                        Reptile.Click += DoNothing;
                        break;
                    case "Crocodile":
                        Reptile.IsEnabled = true;
                        Reptile.Background = Brushes.Yellow;
                        Reptile.Click += DoNothing;
                        break;
                }

            }
        }
    
        private void DoNothing(Object Sender, RoutedEventArgs e){

        }
    }
}
