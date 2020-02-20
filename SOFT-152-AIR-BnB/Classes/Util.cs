using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    static class Util
    {
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
        public static string GetFile(ref Data data)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                //Opens the file dialog in the user's 'my documents' folder
                fileDialog.InitialDirectory = System.IO.Path.GetFullPath(String.Format("C:\\Users\\{0}\\Documents",Environment.UserName));
                //Only allows the user to select .txt files
                fileDialog.Filter = "Text files (*.txt)|*.txt";
                fileDialog.FilterIndex = 2;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileManager.ReadDataIn(fileDialog.FileName, ref data);
                    return fileDialog.FileName;
                }
                else
                {
                    //If the user closes the file diolog, the program closes
                    System.Environment.Exit(1);
                    return null;
                }
            }
        }
        public static bool IsNumeric(string input)
        {
            //returns true if the input can be converted to an int
            return int.TryParse(input, out _);
        }
        public static bool IsDouble(string input)
        {
            //returns true if the input can be converted to a doulbe
            return double.TryParse(input, out _);
        }
        public static void UpdateDistrictList(Data inData, CustomListBox districtList, CustomListBox nHoodList, CustomListBox propList, CustomListBox hostsList)
        {
            //Clears all of the lists
            districtList.Clear();
            nHoodList.Clear();
            propList.Clear();
            foreach (District district in inData.GetAllDistricts())
            {
                //loop through all districts and add them to the district list box
                districtList.AddLeft(district.GetDistrictName());
                districtList.AddRight(Convert.ToString(district.GetNumNeighbourhoods()));
            }
            districtList.SetIndex(1);
            UpdateNbList(inData.GetDistrict(0), nHoodList, propList, hostsList);
            districtList.AllowChange();
        }
        public static void UpdateNbList(District district, CustomListBox nHoodList, CustomListBox propList, CustomListBox hostsList)
        {
            //Clear each list box
            nHoodList.Clear();
            propList.Clear();
            hostsList.Clear();
            foreach (Neighbourhood neighbourhood in district.GetAllNeighbourhoods())
            {
                //Loop through all nbhoods in the district and add their data to the list box
                nHoodList.AddLeft(neighbourhood.GetNeighbourhoodName());
                nHoodList.AddRight(Convert.ToString(neighbourhood.GetNumProperties()));
            }
            nHoodList.SetIndex(1);
            UpdatePropLst(district.GetNeighbourhood(0), propList, hostsList);
            nHoodList.AllowChange();
        }
        public static void UpdatePropLst(Neighbourhood neighbourhood,  CustomListBox propList, CustomListBox hostsList)
        {
            //Clear the list boxes
            propList.Clear();
            hostsList.Clear();

            foreach (Property property in neighbourhood.GetAllProperties())
            {
                //Loop through each property in the neighbourhood and add their data to the list box
                propList.AddLeft(property.GetPropertyName());
                propList.AddRight(Convert.ToString(property.GetPrice()));

                // Getting all unique hosts in nbHood
                if (!hostsList.Has(property.GetHostName()))
                {
                    hostsList.AddLeft(property.GetHostName());
                    hostsList.AddRight(property.GetFormattedHostProperties());
                }
            }
            propList.SetIndex(1);
            hostsList.SetIndex(1);
            propList.AllowChange();
            hostsList.AllowChange();
        }
        public static void SetHostSelected(Property selectedProp, CustomListBox hostsList)
        {
            //Sets the selected item in the hosts list from the selected property
            hostsList.SetIndex(hostsList.SearchIndex(selectedProp.GetHostName()));
        }
        public static void UpdateHostList(Neighbourhood nbHood, CustomListBox hostsList)
        {
            hostsList.Clear();
            foreach (Property property in nbHood.GetAllProperties())
            {
                if (!hostsList.Has(property.GetHostName()))
                {
                    hostsList.AddLeft(property.GetHostName());
                    hostsList.AddRight(property.GetFormattedHostProperties());
                }
            }
        }
        public static void UpdateAverage(Data data)
        {
            foreach (District district in data.GetAllDistricts())
            {
                foreach (Neighbourhood nbHood in district.GetAllNeighbourhoods())
                {
                    nbHood.CalculateAverage();
                }
                district.CalculateAverage();
            }
        }
        //From https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
        //Needed to safely show the map pannel from a different thread
        //Map takes a while to load, so threadding is required to not lock GUI
        public static void SetControlPropertyThreadSafe(Control control, string name, object value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, name, value });
            }
            else
            {
                control.GetType().InvokeMember(
                    name,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { value });
            }
        }
    }
}
