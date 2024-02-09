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
        private string category_name;
        private string species_name;
        // Variables for mammals and their species
        private string gestation_period;
        private string eye_color;
        private string energy_level;
        // Variables for reptiles and their species
        private string size;
        private string species_variation;
        private string place_of_origin;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Responsible for listening to changes in text for input fields generated as a result
        // of selecting a certain category or animal species
        private void OnChange(Object Sender, TextChangedEventArgs e)
        {
            string boxName = ((TextBox)Sender).Name;
            string boxValue = ((TextBox)Sender).Text;

            switch (boxName)
            {
                case "GestationPeriod":
                    gestation_period = boxValue;
                    break;
                case "EyeColor":
                    eye_color = boxValue;
                    break;
                case "EnergyLevel":
                    energy_level = boxValue;
                    break;
                case "Size":
                    size = boxValue;
                    break;
                case "SpeciesVariation":
                    species_variation = boxValue;
                    break;
                case "PlaceOfOrigin":
                    place_of_origin = boxValue;
                    break;
            }
        }

        // Pick Category from which species list shall be determined
        private void HandleCategory(Object Sender, RoutedEventArgs e)
        {
            Button clickedButton = Sender as Button;
            if (clickedButton != null)
            {
                category_specific_fields.Children.Clear();
                species_specific_fields.Children.Clear();

                string category = clickedButton.Name;

                category_name = category;

                List<string> species = FetchSpecies(category);

                if (species.Count() > 0)
                {
                    species_container.Children.Clear();
                }

                // Create button elements and assign them to fetched species
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
                        Button button = (Button)element;

                        button.Background = Brushes.Transparent;
                    }
                }
                clickedButton.Background = Brushes.Gray;
            }
        }

        // Determine which speices has been selected then create the appropriate fields
        private void HandleSpecies(Object Sender, RoutedEventArgs e)
        {
            Button clickedButton = Sender as Button;
            if (clickedButton != null)
            {
                species_specific_fields.Children.Clear();

                string species = clickedButton.Name;

                species_name = species;

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
                textBox.TextChanged += OnChange;

                switch (species)
                {
                    case "Cat":
                        textBlock.Text = "Eye Color";
                        textBlock.Name = "EyeColor";
                        textBox.Name = "EyeColor";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                    case "Dog":
                        textBlock.Text = "Energy Level";
                        textBlock.Name = "EnergyLevel";
                        textBox.Name = "EnergyLevel";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                    case "Snake":
                        textBlock.Text = "Place Of Origin";
                        textBlock.Name = "PlaceOfOrigin";
                        textBox.Name = "PlaceOfOrigin";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                    case "Crocodile":
                        textBlock.Text = "Species Variation";
                        textBlock.Name = "SpeciesVariation";
                        textBox.Name = "SpeciesVariation";

                        dockPanel.Children.Add(textBlock);
                        dockPanel.Children.Add(textBox);

                        species_specific_fields.Children.Add(dockPanel);
                        break;
                }

                foreach (UIElement element in species_container.Children)
                {
                    if (element is Button)
                    {
                        Button button = (Button)element;

                        button.Background = Brushes.Transparent;
                    }
                }
                clickedButton.Background = Brushes.Gray;
            }

        }

        // Responsible for creating an animal and listing its details
        private void SaveAnimal(Object Sender, RoutedEventArgs e)
        {
            // Save at animal level
            // Save at category level
            // Save at species level

            // Category classes

            Animal animal = new Animal();
            
            animal.ID = category_name == "Mammal" ? "M001" : "R001";
            animal.Name = name.Text;
            animal.Age = age.Text;
            animal.Gender = gender.SelectedItem == null ? "" : ((ComboBoxItem)gender.SelectedItem).Content.ToString();
            animal.Domesticated = domesticated.IsChecked == null ? false : true;

            // Set animal detail field values
            detail_ID.Text = animal.ID;
            detail_name.Text = animal.Name;
            detail_age.Text = animal.Age;
            detail_gender.Text = animal.Gender;
            detail_domesticated.Text = animal.Domesticated ? "Yes":"No";

            switch (category_name)
            {
                case "Mammal":
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.HorizontalAlignment = HorizontalAlignment.Stretch;

                    TextBlock textBlock1 = new TextBlock();
                    TextBlock textBlock2 = new TextBlock();

                    textBlock1.Text = "Gestation Period";
                    textBlock1.Margin = new Thickness(5);
                    textBlock2.Text = gestation_period;
                    textBlock2.Margin = new Thickness(5);
                    textBlock2.Width = 200;
                    textBlock2.HorizontalAlignment = HorizontalAlignment.Right;

                    dockPanel.Children.Add(textBlock1);
                    dockPanel.Children.Add(textBlock2);
                    animal_details.Children.Add(dockPanel);

                    if(species_name == "Dog")
                    {
                        DockPanel dockPanel1 = new DockPanel();
                        dockPanel1.HorizontalAlignment = HorizontalAlignment.Stretch;
                        TextBlock textBlock3 = new TextBlock();
                        TextBlock textBlock4 = new TextBlock();

                        textBlock3.Text = "Energy Level";
                        textBlock3.Margin = new Thickness(5);
                        textBlock4.Text = energy_level;
                        textBlock4.Margin = new Thickness(5);
                        textBlock4.Width = 200;
                        textBlock4.HorizontalAlignment = HorizontalAlignment.Right;

                        dockPanel1.Children.Add(textBlock3);
                        dockPanel1.Children.Add(textBlock4);
                        animal_details.Children.Add(dockPanel1);
                    }
                    else
                    {
                        DockPanel dockPanel3 = new DockPanel();
                        dockPanel3.HorizontalAlignment = HorizontalAlignment.Stretch;
                        TextBlock textBlock5 = new TextBlock();
                        TextBlock textBlock6 = new TextBlock();

                        textBlock5.Text = "Eye Color";
                        textBlock5.Margin = new Thickness(5);
                        textBlock6.Text = eye_color;
                        textBlock6.Margin = new Thickness(5);
                        textBlock6.Width = 200;
                        textBlock6.HorizontalAlignment = HorizontalAlignment.Right;

                        dockPanel3.Children.Add(textBlock5);
                        dockPanel3.Children.Add(textBlock6);
                        animal_details.Children.Add(dockPanel3);
                    }
                    
                    break;
                case "Reptile":
                    DockPanel dockPanel11 = new DockPanel();
                    dockPanel11.HorizontalAlignment = HorizontalAlignment.Stretch;

                    TextBlock textBlock11 = new TextBlock();
                    TextBlock textBlock22 = new TextBlock();

                    textBlock11.Text = "Size";
                    textBlock11.Margin = new Thickness(5);
                    textBlock22.Text = size;
                    textBlock22.Margin = new Thickness(5);
                    textBlock22.Width = 200;
                    textBlock22.HorizontalAlignment = HorizontalAlignment.Right;

                    dockPanel11.Children.Add(textBlock11);
                    dockPanel11.Children.Add(textBlock22);
                    animal_details.Children.Add(dockPanel11);

                    if (species_name == "Snake")
                    {
                        DockPanel dockPanel1 = new DockPanel();
                        dockPanel1.HorizontalAlignment = HorizontalAlignment.Stretch;
                        TextBlock textBlock3 = new TextBlock();
                        TextBlock textBlock4 = new TextBlock();

                        textBlock3.Text = "PlaceOfOrigin";
                        textBlock3.Margin = new Thickness(5);
                        textBlock4.Text = place_of_origin;
                        textBlock4.Margin = new Thickness(5);
                        textBlock4.Width = 200;
                        textBlock4.HorizontalAlignment = HorizontalAlignment.Right;

                        dockPanel1.Children.Add(textBlock3);
                        dockPanel1.Children.Add(textBlock4);
                        animal_details.Children.Add(dockPanel1);
                    }
                    else
                    {
                        DockPanel dockPanel3 = new DockPanel();
                        dockPanel3.HorizontalAlignment = HorizontalAlignment.Stretch;
                        TextBlock textBlock5 = new TextBlock();
                        TextBlock textBlock6 = new TextBlock();

                        textBlock5.Text = "Species Variation";
                        textBlock5.Margin = new Thickness(5);
                        textBlock6.Text = species_variation;
                        textBlock6.Margin = new Thickness(5);
                        textBlock6.Width = 200;
                        textBlock6.HorizontalAlignment = HorizontalAlignment.Right;

                        dockPanel3.Children.Add(textBlock5);
                        dockPanel3.Children.Add(textBlock6);
                        animal_details.Children.Add(dockPanel3);
                    }

                    break;
            }
        }

        // Responsible for uploading an image
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
            textBox.TextChanged += OnChange;

            switch (category)
            {
                case "Mammal":
                    textBlock.Text = "Gestation Period";
                    textBlock.Name = "GestationPeriod";
                    textBox.Name = "GestationPeriod";

                    dockPanel.Children.Add(textBlock);
                    dockPanel.Children.Add(textBox);

                    category_specific_fields.Children.Add(dockPanel);

                    Mammal mammals = new Mammal();
                    items = mammals.Species;
                    return items;
                case "Reptile":
                    textBlock.Text = "Size";
                    textBlock.Name = "Size";
                    textBox.Name = "Size";

                    dockPanel.Children.Add(textBlock);
                    dockPanel.Children.Add(textBox);

                    category_specific_fields.Children.Add(dockPanel);
                    Reptlie reptile = new Reptlie();
                    items = reptile.Species;
                    return items;
            }
            return items;
        }
    
        // Responsible for listing all animals when checkbox is checked and reverting to normal status once
        // checkbox is unchecked
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
    
        // Function Assigned to species when The Fetch all checkbox is checked
        private void GetCategory(Object Sender, RoutedEventArgs e)
        {
            Button clickedButton = Sender as Button;

            if(clickedButton != null)
            {
                foreach (UIElement element in animal_category.Children)
                {
                    if (element is Button)
                    {
                        Button button = (Button)element;

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
    
        // Function assigned to a category when one of the speices generated from selecting List all animals
        // is selected
        private void DoNothing(Object Sender, RoutedEventArgs e){

        }
    }
}
