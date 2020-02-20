using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    public partial class DashBoard : Form
    {
        private Data data;
        private Map map;
        private readonly MakeGraphics graphics;
        private CustomListBox districtList, nHoodList, propList, hostsList;
        private EnterData enteredData;
        private CreateNewDistrict newDist;
        private CreateNewNbhood newNbhood;
        private CreateNewProperty newProp;
        private BarGraph districtBar, nbBar, propBar, avBar;
        private string filePath;
        public DashBoard()
        {
            InitializeComponent();
            //Hide the map whilst processing
            mapPannel.Visible = false;
            graphics = new MakeGraphics();
            data = new Data();
            //No top bar as I've got my own
            FormBorderStyle = FormBorderStyle.None;
            //Start the application maximised
            WindowState = FormWindowState.Maximized;
            //Allow the pannels to scroll that need it
            districtBarPanel.AutoScroll = true;
            nbBarPanel.AutoScroll = true;
            propBarPanel.AutoScroll = true;
            avBarPanel.AutoScroll = true;
            //Allows for pressing enter to search aswell as the button
            this.searchTextBox.KeyPress += new KeyPressEventHandler(CheckKeys);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Telling the graphics class to draw and re-draw the top bar every time OnPaint is called
            graphics.DrawTopBar(e);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void UpdateInfoBox()
        {
            //Goes through every item in the info box and sets it
            District selectedDistrict = data.GetDistrict(districtList.GetIndex());
            Neighbourhood selectedNeighbourhood = selectedDistrict.GetNeighbourhood(nHoodList.GetIndex());
            Property selectedProperty = selectedNeighbourhood.GetProperty(propList.GetIndex());

            //district info
            districtNameText.Text = selectedDistrict.GetDistrictName();
            numNbHoodText.Text = Convert.ToString(selectedDistrict.GetNumNeighbourhoods());
            distAvgPriceText.Text = String.Format("{0:$0.00}", selectedDistrict.GetAvgPrice());

            //nbhood info
            nbHoodNameText.Text = selectedNeighbourhood.GetNeighbourhoodName();
            numPropsText.Text = Convert.ToString(selectedNeighbourhood.GetNumProperties());
            nbHoodAvgPriceText.Text = String.Format("{0:$0.00}", selectedNeighbourhood.GetAvgPrice());

            //property and host info
            propNameText.Text = selectedProperty.GetPropertyName();
            propIdText.Text = Convert.ToString(selectedProperty.GetPropertyID());
            hostNameText.Text = selectedProperty.GetHostName();
            hostIdText.Text = Convert.ToString(selectedProperty.GetHostID());
            numHostPropText.Text = Convert.ToString(selectedProperty.GetNumHostProperties());
            latText.Text = Convert.ToString(selectedProperty.GetLatitude());
            lonText.Text = Convert.ToString(selectedProperty.GetLongitude());
            roomTypeText.Text = selectedProperty.GetRoomType();
            propPriceText.Text = String.Format("{0:$0.00}", selectedProperty.GetPrice());
            minNumNightsText.Text = Convert.ToString(selectedProperty.GetMinNumNights());
            avalibilityText.Text = Convert.ToString(selectedProperty.GetAvailability());

        }
        private void PopulateListBox()
        {
            //Fills the list boxes for the first time
            districtList = new CustomListBox("Name", "No. of Nb.Hoods");
            nHoodList = new CustomListBox("Name", "No. of Propty.");
            propList = new CustomListBox("Name", "Price");
            hostsList = new CustomListBox("Name", "No. Properties");

            //Creating event listners for when the index changes 
            districtList.IndexChanged += new EventHandler(ListBoxIndexChanged);
            nHoodList.IndexChanged += new EventHandler(ListBoxIndexChanged);
            propList.IndexChanged += new EventHandler(ListBoxIndexChanged);
            hostsList.IndexChanged += new EventHandler(ListBoxIndexChanged);
            districtList.doubleClick += new EventHandler(ListBoxDoubleClick);
            nHoodList.doubleClick += new EventHandler(ListBoxDoubleClick);
            propList.doubleClick += new EventHandler(ListBoxDoubleClick);
            hostsList.doubleClick += new EventHandler(ListBoxDoubleClick);
            //Adding the lists to the pannels
            nHoodPanel.Controls.Add(nHoodList);
            districtsPanel.Controls.Add(districtList);
            propertyPanel.Controls.Add(propList);
            hostsPanel.Controls.Add(hostsList);

            Util.UpdateDistrictList(data, districtList, nHoodList, propList, hostsList);
        }
        private void ListBoxIndexChanged(object sender, EventArgs e)
        {
            //Updating the list boxes depending on what one was changed
            if (sender.Equals(districtList))
            {
                Util.UpdateNbList(data.GetDistrict(districtList.GetIndex()), nHoodList, propList, hostsList);
                LoadNbBar();
            }
            if (sender.Equals(nHoodList))
            {
                Util.UpdatePropLst(data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()), propList, hostsList);
                LoadPropBar();
            }
            if (sender.Equals(propList))
            {
                Util.SetHostSelected(data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetProperty(propList.GetIndex()), hostsList);
            }
            UpdateInfoBox();
            // Updating the info box with selected host
            if (sender.Equals(hostsList))
            {
                Property selectedProperty = data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).SearchHost(hostsList.GetLeftText());
                hostNameText.Text = selectedProperty.GetHostName();
                hostIdText.Text = Convert.ToString(selectedProperty.GetHostID());
                numHostPropText.Text = Convert.ToString(selectedProperty.GetNumHostProperties());
            }
        }
        private void ListBoxDoubleClick(object sender, EventArgs e)
        {
            //Opens a data entry window on a listbox double click
            if (sender.Equals(districtList))
            {
                CreateDataEntry(districtList);
                enteredData.dataSubmit += new EventHandler(DistrictInput);
            }
            else if (sender.Equals(nHoodList))
            {
                CreateDataEntry(nHoodList);
                enteredData.dataSubmit += new EventHandler(NbHoodInput);
            }
            else if (sender.Equals(propList))
            {
                CreateDataEntry(propList);
                enteredData.dataSubmit += new EventHandler(PropInput);
            }

            else if (sender.Equals(hostsList))
            {
                CreateDataEntry(hostsList);
                enteredData.dataSubmit += new EventHandler(HostInput);
            }
        }
        private void CreateDataEntry(CustomListBox list)
        {
            //Shows a data entry box when an intem is double clicked on
            enteredData = new EnterData(list.GetSelectedText());
            enteredData.Show();
        }
        private void DistrictInput(object sender, EventArgs e)
        {
            //Gets the information that's changed and updates it
            data.GetDistrict(districtList.GetIndex()).SetDistrictName(enteredData.GetText());
            enteredData.Dispose();
            Util.UpdateDistrictList(data, districtList, nHoodList, propList, hostsList);
            FileManager.SaveDataOut(filePath, data);
        }
        private void NbHoodInput(object sender, EventArgs e)
        {
            //Gets the information that's changed and updates it
            data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).SetNeighbourhoodName(enteredData.GetText());
            enteredData.Dispose();
            Util.UpdateNbList(data.GetDistrict(districtList.GetIndex()),nHoodList, propList, hostsList);
            FileManager.SaveDataOut(filePath, data);
        }
        private void PropInput(object sender, EventArgs e)
        {
            //Gets the information that's changed and updates it
            //Left side means the name has been edited
            if (propList.GetSide() == "left")
            {
                data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetProperty(propList.GetIndex()).SetPropertyName(enteredData.GetText());
            }
            //Right side means price has changed
            else
            {
                data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetProperty(propList.GetIndex()).SetPrice(enteredData.GetText());
                Util.SetControlPropertyThreadSafe(mapPannel, "Visible", false);
                Util.UpdateAverage(data);
                map.Dispose();
                LoadMap();
                UpdateInfoBox();
            }
            //Save the data
            enteredData.Dispose();
            Util.UpdatePropLst(data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()), propList, hostsList);
            FileManager.SaveDataOut(filePath, data);
        }
        private void HostInput(object sender, EventArgs e)
        {
            //Updates all host names from the changed one
            string origName = data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetProperty(hostsList.GetIndex()).GetHostName();
            data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).UpdateAllHosts(origName, enteredData.GetText());
            enteredData.Dispose();
            Util.UpdateHostList(data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()), hostsList);
            FileManager.SaveDataOut(filePath, data);
        }
        private void DashBoard_Load(object sender, EventArgs e)
        {
            //Gets the filepath, opens it and loads the graphs
            filePath = Util.GetFile(ref data);
            PopulateListBox();
            UpdateInfoBox();
            LoadMap();
            LoadDistBar();
            LoadNbBar();
            LoadPropBar();
        }
        private void CreateBarGraph(ref BarGraph bg, ref Panel p)
        {
            //Creates the barcharts with corresponding titles to the panels
            string name = "";
            if(bg != null)
            {
                bg.Dispose();
            }
            if (p.Equals(districtBarPanel))
            {
                name = "Average Price Per District";
            }
            if (p.Equals(nbBarPanel))
            {
                name = "Average Price Per  Neighbourhood";
            }
            if (p.Equals(propBarPanel))
            {
                name = "Price Per Property";
            }
            if (p.Equals(avBarPanel))
            {
                name = "Avalibility Per Property";
            }
            bg = new BarGraph(p.Height, name);
            p.Visible = false;
            bg.processed += ShowBar;
        }
        private void LoadDistBar()
        {
            //Loads or reloads the district bar chart with the data from the array
            CreateBarGraph(ref districtBar, ref districtBarPanel);
            foreach (District district in data.GetAllDistricts())
            {
                if(district.GetNumNeighbourhoods() != 0)
                {
                    districtBar.Add(Convert.ToInt32(district.GetAvgPrice()), district.GetDistrictName());
                }
            }
            districtBar.CalculateBars();
        }
        private void LoadNbBar()
        {
            //Loads or reloads the nbhood bar chart with the data from the array
            CreateBarGraph(ref nbBar, ref nbBarPanel);
            foreach (Neighbourhood nbHood in data.GetDistrict(districtList.GetIndex()).GetAllNeighbourhoods())
            {
                nbBar.Add(Convert.ToInt32(nbHood.GetAvgPrice()), nbHood.GetNeighbourhoodName());
            }
            nbBar.CalculateBars();
        }
        private void LoadPropBar()
        {
            //Loads or reloads both the property bar charts with the data from the array
            CreateBarGraph(ref propBar, ref propBarPanel);
            CreateBarGraph(ref avBar, ref avBarPanel);
            foreach (Property prop in data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetAllProperties())
            {
                propBar.Add(prop.GetPrice(), Convert.ToString(prop.GetPropertyID()));
                avBar.Add(prop.GetAvailability(), Convert.ToString(prop.GetPropertyID()));
            }
            propBar.CalculateBars();
            avBar.CalculateBars();
        }

        private void NewDistBtn_Click(object sender, EventArgs e)
        {
            //Opens the create new district form and creates an event handler to listen for when the data is ready to update
            if(newDist != null) newDist.Dispose();
            newDist = new CreateNewDistrict();
            newDist.Show();
            newDist.submit += new EventHandler(CreateNewDist);
        }
        private void CreateNewDist(object sender, EventArgs e)
        {
            //Updates the data in the array, saves it and reloads the information o the form
            string[] newDistData = newDist.GetText();
            data.AddDistrict(new District(newDistData[0], "1", new Neighbourhood(newDistData[1], new Property(
                newDistData[3], newDistData[2], newDistData[5], newDistData[4], "1", newDistData[6], newDistData[7], newDistData[8], newDistData[9], newDistData[10], newDistData[11]
                ))));
            FileManager.SaveDataOut(filePath, data);
            Util.UpdateDistrictList(data, districtList, nHoodList, propList, hostsList);
            LoadDistBar();
            districtList.SetIndex(data.GetAllDistricts().Length);
        }

        private void NewNbhoodBtn_Click(object sender, EventArgs e)
        {
            //Opens the create new nbhood form and creates an event handler to listen for when the data is ready to update
            if (newNbhood != null) newNbhood.Dispose();
            newNbhood = new CreateNewNbhood();
            newNbhood.Show();
            newNbhood.submit += new EventHandler(CreateNewNbhood);
        }
        private void CreateNewNbhood(object sender, EventArgs e)
        {
            //Updates the data in the array, saves it and reloads the information o the form
            string[] newNbhoodData = newNbhood.GetText();
            data.GetDistrict(districtList.GetIndex()).AppendNeighbourhood(new Neighbourhood(newNbhoodData[0], new Property(
                newNbhoodData[2], newNbhoodData[1], newNbhoodData[4], newNbhoodData[3], "1", newNbhoodData[5], newNbhoodData[6], newNbhoodData[7], newNbhoodData[8], newNbhoodData[9], newNbhoodData[10]
                )));
            //Increment num neighbourhoods by 1
            data.GetDistrict(districtList.GetIndex()).SetNumNeighbourhoods(data.GetDistrict(districtList.GetIndex()).GetNumNeighbourhoods() + 1);
            FileManager.SaveDataOut(filePath, data);
            Util.UpdateDistrictList(data, districtList, nHoodList, propList, hostsList);
            LoadNbBar();
        }

        private void NewPropBtn_Click(object sender, EventArgs e)
        {
            //Opens the create new property form and creates an event handler to listen for when the data is ready to update
            if (newProp != null) newProp.Dispose();
            newProp = new CreateNewProperty();
            newProp.Show();
            newProp.submit += new EventHandler(CreateNewProperty);
        }
        private void CreateNewProperty(object sender, EventArgs e)
        {
            //Updates the data in the array, saves it and reloads the information o the form
            string[] newPropData = newProp.GetText();
            data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).AppendProperty(new Property(
                newPropData[1], newPropData[0], newPropData[3], newPropData[2], "1", newPropData[4], newPropData[5], newPropData[6], newPropData[7], newPropData[8], newPropData[9]
                ));
            //Increment the num properties by 1
            data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).SetNumProperties(data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetNumProperties() + 1);
            FileManager.SaveDataOut(filePath, data);
            Util.UpdateNbList(data.GetDistrict(districtList.GetIndex()), nHoodList, propList, hostsList);
            LoadNbBar();
            LoadNbBar();

        }

        private void DistSaveChanges_Click(object sender, EventArgs e)
        {
            //Sets the selected district's name to what ever is in the text box
            int distIndex = districtList.GetIndex();
            data.GetDistrict(districtList.GetIndex()).SetDistrictName(districtNameText.Text);
            Util.UpdateDistrictList(data, districtList, nHoodList, propList, hostsList);
            LoadDistBar();
            FileManager.SaveDataOut(filePath, data);
            districtList.SetIndex(distIndex + 1);
        }


        private void NbhoodSaveChanges_Click(object sender, EventArgs e)
        {
            //Sets the selected nbhood's name to what ever is in the text box
            int districtIndex = districtList.GetIndex();
            data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).SetNeighbourhoodName(nbHoodNameText.Text);
            Util.UpdateNbList(data.GetDistrict(districtList.GetIndex()), nHoodList, propList, hostsList);
            LoadNbBar();
            FileManager.SaveDataOut(filePath, data);
            districtList.SetIndex(districtIndex + 1);
        }

        private void PropSaveChanges_Click(object sender, EventArgs e)
        { 
            Property tempProp = data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetProperty(propList.GetIndex());
            //Checking all of the information inside the text boxes is valid before saving
            if (!Util.IsNumeric(propIdText.Text))
            {
                MessageBox.Show("Property ID MUST be numeric, your changes have not been saved.");
            }
            else if (!Util.IsDouble(latText.Text))
            {
                MessageBox.Show("Lattitude MUST be numeric, your change have not been saved.");
            }
            else if (!Util.IsDouble(lonText.Text))
            {
                MessageBox.Show("Longitude MUST be numeric, your changes have note been saved.");
            }
            else if (!Util.IsDouble(propPriceText.Text.Replace('$', ' ')))
            {
                MessageBox.Show("Price MUST be numeric, your changes have not been saved.");
            }
            else if (!Util.IsNumeric(avalibilityText.Text))
            {
                MessageBox.Show("Avalibility MUST be numeric, your changes have not been saved.");
            }
            //366 as could be leap year
            else if(Convert.ToInt32(avalibilityText.Text) > 366)
            {
                MessageBox.Show("Avalibility MUST be less than 366");
            }
            else if (!Util.IsNumeric(minNumNightsText.Text))
            {
                MessageBox.Show("Minimum number of nights MUST be numeric, your chnages have not been saved.");
            }
            else
            {
                //Saving the information in the textboxes to the selected property if all checks are okay
                int districtIndex = districtList.GetIndex();
                tempProp.SetPropertyName(propNameText.Text);
                tempProp.SetPropertyID(propIdText.Text);
                tempProp.SetLatitude(latText.Text);
                tempProp.SetLongitude(lonText.Text);
                tempProp.SetPrice(propPriceText.Text.Replace('$', ' '));
                tempProp.SetMinNumNights(minNumNightsText.Text);
                tempProp.SetAvailability(avalibilityText.Text);
                tempProp.SetRoomType(roomTypeText.Text);
                LoadPropBar();
                Util.UpdatePropLst(data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()), propList, hostsList);
                FileManager.SaveDataOut(filePath, data);
                districtList.SetIndex(districtIndex + 1);
            }
        }

        private void delProp_Click(object sender, EventArgs e)
        {
            //Making sure the user didn't accidentally press the button
            DialogResult dialogResult = MessageBox.Show(String.Format("Are you sure you want to remove property {0}",
                data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).GetProperty(propList.GetIndex()).GetPropertyName()), "", MessageBoxButtons.OKCancel);
            //If they did mean to, remove the property, update the data and save
            if (dialogResult == DialogResult.OK)
            {
                data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).RemoveProperty(propList.GetIndex());
                Util.UpdateNbList(data.GetDistrict(districtList.GetIndex()), nHoodList, propList, hostsList);
                FileManager.SaveDataOut(filePath, data);
            }
        }

        private void hostSaveChanges_Click(object sender, EventArgs e)
        {
            try //Need to try as some times a race error comes if the user does something before it can save the data
            {
                string origName = data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).SearchHost(hostsList.GetLeftText()).GetHostName();
                if (origName == null)
                {
                    MessageBox.Show("There was an error saving changes to host list");
                }
                else
                {
                    data.GetDistrict(districtList.GetIndex()).GetNeighbourhood(nHoodList.GetIndex()).UpdateAllHosts(origName, hostNameText.Text);
                    FileManager.SaveDataOut(filePath, data);
                }
            }
            catch
            {
                MessageBox.Show("An error occurred whilst saving host data");
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            //Result will be an array of numbers saying where it was found
            int[] result = data.SearchByName(searchTextBox.Text.ToString().ToLower());
            //Results -1 is if nothing is found
            if (result[0] == -1)
            {
                MessageBox.Show(String.Format("Could not find {0}", searchTextBox.Text.ToString()));
            }
            else
            {
                //Different lenghts of results are returned depending on what was found
                // 1 means only a district was found, but will set every time
                if(result.Length >= 1)
                {
                    districtList.SetIndex(result[0] + 1);
                }
                if(result.Length >= 2)
                {
                    nHoodList.SetIndex(result[1] + 1);
                }
                if(result.Length >= 3)
                {
                    propList.SetIndex(result[2] + 1);
                }
            }
        }

        private void ShowBar(object sender, EventArgs e)
        {
            //Shows the bars once they are loaded
            if (sender.Equals(districtBar))
            {
                districtBarPanel.Controls.Add(districtBar);
                districtBarPanel.Visible = true;
            }
            if (sender.Equals(nbBar))
            {
                nbBarPanel.Controls.Add(nbBar);
                Util.SetControlPropertyThreadSafe(nbBarPanel, "Visible", true);
            }
            if (sender.Equals(propBar))
            {
                propBarPanel.Controls.Add(propBar);
                Util.SetControlPropertyThreadSafe(propBarPanel, "Visible", true);
            }
            if (sender.Equals(avBar))
            {
                avBarPanel.Controls.Add(avBar);
                Util.SetControlPropertyThreadSafe(avBarPanel, "Visible", true);
            }
        }
        private void LoadMap()
        {
            //Creats the map and awaits it to be completed loading
            map = new Map(
                data.GetDistrict(1).GetAvgPrice(),
                data.GetDistrict(2).GetAvgPrice(),
                data.GetDistrict(4).GetAvgPrice(),
                data.GetDistrict(0).GetAvgPrice(),
                data.GetDistrict(3).GetAvgPrice()
            );
            mapPannel.Controls.Add(map);
            map.mapLoaded += ShowMap;
            map.Process();
        }
        private void ShowMap(object sender, EventArgs e)
        {
            //The event that is triggered once the map is loaded, so it is set to visable thread safe
            Util.SetControlPropertyThreadSafe(mapPannel, "Visible", true);
        }
        private void CheckKeys(object sender, KeyPressEventArgs e)
        {
            //If the key pressed is enter, search
            if (e.KeyChar == (char)13)
            {
                searchBtn.PerformClick();
            }
        }
    }
}
