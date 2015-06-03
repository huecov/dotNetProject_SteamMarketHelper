using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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


namespace SteamMarketHelper
{
    //Few informations:
    // Weapon Category  : is it Rifle or Knife or Pistols etc.
    // Weapon Model     : for example if Weapon Category is Pistol than Model of this Weapon can be: Five-Seven or Glock or P2000 etc. (specific model)
    // Weapon Skin      : it is name of pattern(including colors) on the spcific model of weapon: eg. AK-47 has skins like Redline(colors: black with red line) or Vulcan(colors: mostly blue) etc.

    public partial class MainWindow : Window
    {
        // Thread for clock
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public static readonly string endpoint = "http://steamcommunity.com/market/priceoverview/";
        public int currency;

        public FileNames fileNames = new FileNames();
        public static readonly string pathToFiles = "C:\\Users\\MightySheldor\\Desktop\\dotNet-projekt\\"; // Important !!!

        public MainWindow()
        {
            InitializeComponent();
            window.ResizeMode = ResizeMode.NoResize;

            chooseWeaponType2.IsEnabled = false;
            chooseWeaponType3.IsEnabled = false;
            chooseWeaponType4.IsEnabled = false;

            chooseWeaponType1.Items.Add("Pistols");
            chooseWeaponType1.Items.Add("Rifles");
            chooseWeaponType1.Items.Add("SMGs");
            chooseWeaponType1.Items.Add("Heavy");
            //chooseWeaponType1.Items.Add("Knifes");

            foreach (Currency currency in Enum.GetValues(typeof(Currency)))
                comboBoxCurrency.Items.Add(currency.ToString());

            if (comboBoxCurrency.Items.Count>2)
                comboBoxCurrency.SelectedIndex = 2;

            //Start clock
            timer.Tick += new EventHandler(timer_click);
            timer.Interval = new TimeSpan(0,0,1);
            timer.Start();
        }

        private void timer_click(object sender,EventArgs e)
        {
            DateTime d = DateTime.Now;
            labelTimer.Content = string.Format("{0}:{1}:{2}",d.Hour,d.Minute,d.Second);
        }

        // 1st ComboBox which contains information about Weapon Category : is it Pistols, Rifles, SMGs, Heavy, Knifes
        private void chooseWeaponType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showChoosenItem.Text = "";

            // Wait until none item from 1st ComboBox is choosen
            string weaponSelectedInFirstComboBox = chooseWeaponType1.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(weaponSelectedInFirstComboBox))
            {
                // Unblock 2nd ComboBox and add there items
                chooseWeaponType2.IsEnabled = true;
                addItemsToSecondList(weaponSelectedInFirstComboBox);
            
                // Print information about selected item in Text Box
                showChoosenItem.Text = weaponSelectedInFirstComboBox + ": ";
                showImage.Source = new BitmapImage(new Uri(pathToFiles + "random2.png")); //, UriKind.Relative
            }
        }

        // Method adding items to the 2nd ComboBox based on item selected in 1st ComboBox
        private void addItemsToSecondList(string weaponCategory)
        {
            // If user is changing selected item in this (2nd) ComboBox than we need to block 3th and 4th ComboBox (they are based on recursion)
            chooseWeaponType2.Items.Clear();
            chooseWeaponType3.Items.Clear();
            chooseWeaponType3.IsEnabled = false;
            chooseWeaponType4.Items.Clear();
            chooseWeaponType4.IsEnabled = false;

            // Add to 2nd list Model of given Weapon Category 
            try
            {
                string[] weaponModelsList = fileNames.retriveFileNames(pathToFiles + weaponCategory);
                foreach (string model in weaponModelsList)
                    chooseWeaponType2.Items.Add(model);
            }
            catch(System.IO.DirectoryNotFoundException) {}
                
        }

        // 2nd ComboBox which contains information about Weapon Models : AK-47, M4A1, M4A4, AWP etc. for Rifles; Glock, Deagle, P250 etc. for Pistols ...
        private void chooseWeaponType2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showChoosenItem.Text = "";

            // Wait until none item from 2st ComboBox is choosen
            if (chooseWeaponType1.SelectedItem != null)
                if (chooseWeaponType2.SelectedItem != null)
                {
                    // Unblock 3rd ComboBox and add there items
                    chooseWeaponType3.IsEnabled = true;
                    addItemsToThirdList(chooseWeaponType2.SelectedItem.ToString());

                    // Print information about selected item in Text Box
                    showChoosenItem.Text = chooseWeaponType1.SelectedItem.ToString() + ": " + chooseWeaponType2.SelectedItem.ToString();
                    showImage.Source = new BitmapImage(new Uri(pathToFiles + chooseWeaponType1.SelectedItem.ToString() + "\\" + chooseWeaponType2.SelectedItem.ToString() + ".png"));
                }
        }

        // Method adding items to the 3rd ComboBox based on item selected in 1st and 2nd ComboBox
        private void addItemsToThirdList(String weaponModel)
        {
            // If user is changing selected item in this (3rd) ComboBox than we need to block 4th ComboBox (it is based on recursion)
            chooseWeaponType3.Items.Clear();
            chooseWeaponType4.Items.Clear();
            chooseWeaponType4.IsEnabled = false;

            // Add to 3rd list Skin of given Weapon Model 
            try
            {
                string[] weaponSkinsList = fileNames.retriveFileNames(pathToFiles + weaponModel);
                foreach (string skin in weaponSkinsList)
                    chooseWeaponType3.Items.Add(skin);
            }
            catch (System.IO.DirectoryNotFoundException) { MessageBox.Show("System.IO.DirectoryNotFoundException"); }
            
        }

        // 3rd ComboBox which contains information about Weapon Skins : for eg. if we have Weapon Category = Rifles and Weapon Model = M4A1-s than we can choose skins as: Master Piece, Atomic Alloy, Basilisk etc.
        private void chooseWeaponType3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showChoosenItem.Text = "";

            // Wait until none item from 3rd ComboBox is choosen
            if (chooseWeaponType1.SelectedItem != null)
                if (chooseWeaponType2.SelectedItem != null)
                    if (chooseWeaponType3.SelectedItem != null)
                    {
                        // Unblock 4th ComboBox and add there items from Enum
                        chooseWeaponType4.IsEnabled = true;
                        chooseWeaponType4.Items.Clear();
                        foreach (Exterior exterior in Enum.GetValues(typeof(Exterior)))
                            chooseWeaponType4.Items.Add(exterior.ToString().Replace("_", " ").Replace("0","-"));

                        // Print information about selected item in Text Box
                        showChoosenItem.Text = chooseWeaponType1.SelectedItem.ToString() + ": " + chooseWeaponType2.SelectedItem.ToString() + " | " + chooseWeaponType3.SelectedItem.ToString();
                        showImage.Source = new BitmapImage(new Uri("C:\\Users\\MightySheldor\\Desktop\\dotNet-projekt\\" + chooseWeaponType2.SelectedItem.ToString() + "\\" + chooseWeaponType3.SelectedItem.ToString() + ".png"));      
                    }
        }

        // 4th ComboBox contains information about Exterior of the Weapon which says us how good looking is our skin
        private void chooseWeaponType4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showChoosenItem.Text = "";

            volumeTextBox.Text = "";
            medianaTextBox.Text = "";
            minValueTextBox.Text = "";

            // Wait until none item from 3rd ComboBox is choosen
            if (chooseWeaponType1.SelectedItem != null)
                if (chooseWeaponType2.SelectedItem != null)
                    if (chooseWeaponType3.SelectedItem != null)
                        if (chooseWeaponType4.SelectedItem != null)
                        {
                            // Print information about selected item in Text Box
                            showChoosenItem.Text += chooseWeaponType1.SelectedItem.ToString() + ": " + chooseWeaponType2.SelectedItem.ToString() + " | " + chooseWeaponType3.SelectedItem.ToString() + "\n" + chooseWeaponType4.SelectedItem.ToString();

                            // Generete Link to REST Request
                            GenerateRestLink restLink = new GenerateRestLink();

                            string parameters = restLink.generateAddress(comboBoxCurrency.SelectedIndex+1, chooseWeaponType2.SelectedItem.ToString(), chooseWeaponType3.SelectedItem.ToString(), chooseWeaponType4.SelectedItem.ToString());

                            // Print genereted link in TextBox just to check is it right (it is hidden, we need to resize window to find it)
                            linkTextBox.Text = endpoint + parameters;

                            // Make Rest Connection
                            try
                            {
                                RestClient restClient = new RestClient();
                                var jsonResult = restClient.MakeRequest(endpoint + parameters);

                                JsonObject newJsonObject = JsonConvert.DeserializeObject<JsonObject>(jsonResult);

                                // TEST convert costs -> median and minimal price
                                Weapon weapon = new Weapon();
                                try
                                {
                                    ConvertValues convert = new ConvertValues();
                                    if(!string.IsNullOrEmpty(newJsonObject.lowest_price))
                                        weapon.MinimunPrice = convert.convertCosts(comboBoxCurrency.SelectedValue.ToString(), newJsonObject.lowest_price);
                                    if(!string.IsNullOrEmpty(newJsonObject.median_price))
                                        weapon.MedianaPrice = convert.convertCosts(comboBoxCurrency.SelectedValue.ToString(), newJsonObject.median_price);
                                    if(!string.IsNullOrEmpty(newJsonObject.volume))
                                        weapon.Volume = convert.convertAmount(newJsonObject.volume);

                                    // Add other information about weapon
                                    weapon.WeaponCategory = chooseWeaponType1.SelectedItem.ToString();
                                    weapon.WeaponModel = chooseWeaponType2.SelectedItem.ToString();
                                    weapon.WeaponSkin = chooseWeaponType3.SelectedItem.ToString();
                                    weapon.WeaponExterior = chooseWeaponType4.SelectedItem.ToString();
                                    weapon.Currency = comboBoxCurrency.SelectedItem.ToString();

                                    try
                                    {
                                        ReadAndWriteToFile.writeToFile(weapon.weaponType() + " " + weapon.ToString());
                                    }
                                    catch (System.IO.IOException) { }

                                }
                                catch (System.FormatException) 
                                {
                                }
                                // ---------------- END OF TEST --------------------------

                                if (newJsonObject.lowest_price != "")
                                    minValueTextBox.Text = weapon.MinimunPrice.ToString() + " " + comboBoxCurrency.SelectedValue.ToString(); //newJsonObject.lowest_price;
                                if (newJsonObject.volume != "")
                                    volumeTextBox.Text = weapon.Volume.ToString();//newJsonObject.volume;
                                if (newJsonObject.median_price != "")
                                    medianaTextBox.Text = weapon.MedianaPrice.ToString() + " " + comboBoxCurrency.SelectedValue.ToString();//newJsonObject.median_price;

                                if (string.IsNullOrEmpty(newJsonObject.lowest_price))
                                {
                                    volumeTextBox.Text = "Podany stan broni";
                                    medianaTextBox.Text = "dla zadanego skinu";
                                    minValueTextBox.Text = "nie jest wystawiony na rynku steam.";
                                }
                            }
                            catch (System.Net.WebException)
                            {
                                volumeTextBox.Text = "Podany stan broni";
                                medianaTextBox.Text = "dla zadanego skinu";
                                minValueTextBox.Text = "nie istnieje.";
                            }

                        }
        }

        private void comboBoxCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chooseWeaponType4_SelectionChanged(sender, e);
        }
    }
}
