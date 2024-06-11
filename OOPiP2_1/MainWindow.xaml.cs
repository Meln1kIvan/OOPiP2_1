using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.ComponentModel;
using MyLib;

namespace MedicinesDirectory
{
    public partial class MainWindow : Window
    {
        private bool isClosing = false;
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            isClosing = true; // Set the flag before closing the window
            SaveMedicinesToXml();
        }
        private void SaveMedicinesToXml()
        {
            if (isClosing)
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "G:\\2 курс\\OOPiP2_1\\OOPiP2_1\\Medicines.xml");

                XDocument doc;
                XElement rootElement;

                if (File.Exists(filePath))
                {
                    doc = XDocument.Load(filePath);
                    rootElement = doc.Root;
                }
                else
                {
                    doc = new XDocument();
                    rootElement = new XElement("Medicines");
                    doc.Add(rootElement);
                }

                // Remove existing medicine elements from the XML file
                rootElement.Elements("Medicine").Remove();

                foreach (Medicine medicine in Medicines)
                {
                    // Check for data correctness
                    if (string.IsNullOrEmpty(medicine.Name) || string.IsNullOrEmpty(medicine.Form) ||
                        medicine.Quantity <= 0 || string.IsNullOrEmpty(medicine.Unit) ||
                        string.IsNullOrEmpty(medicine.Dosage) || medicine.Price <= 0 ||
                        string.IsNullOrEmpty(medicine.Manufacturer))
                    {
                        // Data is not valid, skip saving
                        continue;
                    }

                    // Create a new medicine element
                    XElement medicineElement = new XElement("Medicine");
                    medicineElement.Add(new XElement("Name", medicine.Name));
                    medicineElement.Add(new XElement("Form", medicine.Form));
                    medicineElement.Add(new XElement("Quantity", medicine.Quantity));
                    medicineElement.Add(new XElement("Unit", medicine.Unit));
                    medicineElement.Add(new XElement("Dosage", medicine.Dosage));
                    medicineElement.Add(new XElement("Price", medicine.Price.ToString("F")));
                    medicineElement.Add(new XElement("Manufacturer", medicine.Manufacturer));

                    // Add the new medicine element to the root element
                    rootElement.Add(medicineElement);
                }

                // Remove deleted medicine elements from the XML file
                foreach (Medicine medicine in Medicines)
                {
                    if (!Medicines.Any(m => m.Name == medicine.Name))
                    {
                        // The medicine was deleted, remove it from the XML document
                        XElement deletedMedicineElement = rootElement.Elements("Medicine")
                            .FirstOrDefault(e => e.Element("Name")?.Value == medicine.Name);

                        if (deletedMedicineElement != null)
                        {
                            deletedMedicineElement.Remove();
                        }
                    }
                }

                doc.Save(filePath);
            }
        }
        public ObservableCollection<Medicine> Medicines { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Medicines = new ObservableCollection<Medicine>();
            LoadMedicinesFromXml();
            Closing += MainWindow_Closing;
            DataContext = this;
        }
        private void LoadMedicinesFromXml()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "G:\\2 курс\\OOPiP2_1\\OOPiP2_1\\Medicines.xml");

            if (!File.Exists(filePath))
            {
                return;
            }

            XDocument doc = XDocument.Load(filePath);

            IEnumerable<XElement> medicineElements = doc.Root?.Elements("Medicine");

            if (medicineElements == null)
            {
                return;
            }

            foreach (XElement medicineElement in medicineElements)
            {
                Medicine medicine = new Medicine
                {
                    Name = medicineElement.Element("Name")?.Value,
                    Form = medicineElement.Element("Form")?.Value,
                    Unit = medicineElement.Element("Unit")?.Value,
                    Dosage = medicineElement.Element("Dosage")?.Value,
                    Manufacturer = medicineElement.Element("Manufacturer")?.Value
                };

                int quantity;
                if (int.TryParse(medicineElement.Element("Quantity")?.Value, out quantity))
                {
                    medicine.Quantity = quantity;
                }
                else
                {
                    // Обработка ошибки парсинга количества
                }

                decimal price;
                if (decimal.TryParse(medicineElement.Element("Price")?.Value, out price))
                {
                    medicine.Price = price;
                }
                else
                {
                    // Обработка ошибки парсинга цены
                }

                Medicines.Add(medicine);
            }
        }
    }
}