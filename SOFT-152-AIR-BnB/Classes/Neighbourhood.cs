using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFT_152_AIR_BnB
{
    class Neighbourhood
    {
        //
        //Defining private variables
        //
        private string neighbourhoodName;
        private int numProperties;
        private Property[] properties;
        private double avgPrice;
        private bool readingData;
        //
        //Defining constructors with string and integer inputs - for user-inputs
        //
        public Neighbourhood(string name, Property inProp)
        {
            SetNeighbourhoodName(name);
            numProperties = 1;
            properties = new Property[1];
            properties[0] = inProp;

        }
        public Neighbourhood(string name, string inNumProperties)
        {
            SetNeighbourhoodName(name);
            SetNumProperties(inNumProperties);
            properties = new Property[numProperties];
            //Setting reading data to true, this stops recalculating average whilst reading in data
            readingData = true;

        }
        public Property[] GetAllProperties()
        {
            return properties;
        }
        public Property GetProperty(int index)
        {
            try
            {
                return properties[index];
            }
            catch
            {
                return null;
            }
        }
        public void AppendProperty(Property inProperty)
        {
            Array.Resize(ref properties, properties.Length + 1);
            properties[properties.Length - 1] = inProperty;
            numProperties += 1;
            CalculateAverage();
        }
        public void AddProperty(Property inProperty, int index)
        {
            properties[index] = inProperty;
            if (!readingData)
            {
                //Recalculating average
                CalculateAverage();
            }
        }
        public void RemoveProperty(int index)
        {
            for(int i = index; i < properties.Length - 1; i++)
            {
                //Moving each element down one from the index, then resizing
                properties[i] = properties[i + 1];
            }
            numProperties -= 1;
            Array.Resize(ref properties, properties.Length - 1);
        }
        public void CalculateAverage()
        {
            double avg = 0;
            foreach (Property prop in properties)
            {
                avg += prop.GetPrice();
            }
            avgPrice = avg / numProperties;
        }
        public void RemoveProperty(string propertyName)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].GetPropertyName() == propertyName)
                {
                    for (int index = i; index < properties.Length - 1; index++)
                    {
                        // moving elements downwards, to fill the gap at [index]
                        properties[index] = properties[index + 1];
                    }
                    // the empty element in the array is now at the end, so we can delete it
                    Array.Resize(ref properties, properties.Length - 1);
                }
            }
            throw new Exception(String.Format("Could not delete property {0}", propertyName));
        }
        public Property SearchHost(string search)
        {
            foreach (Property property in properties)
            {
                if (property.GetHostName() == search)
                {
                    return property;
                }
            }
            return null;
        }
        public void UpdateAllHosts(string origName, string newName)
        {
            //Loops through all the properties, changing the host names if they match- used for editing host names
            foreach (Property property in properties)
            {
                if (property.GetHostName() == origName)
                {
                    property.SetHostName(newName);
                }
            }
        }
        public void ReadingDone()
        {
            readingData = false;
        }
        public double GetAvgPrice()
        {
            return avgPrice;
        }
        public void SetNeighbourhoodName(string inName)
        {
            neighbourhoodName = inName;
        }
        public string GetNeighbourhoodName()
        {
            return neighbourhoodName;
        }

        //
        //Get & set number of properties in the neighbourhood
        //
        public void SetNumProperties(int inNum)
        {
            numProperties = inNum;
        }
        public void SetNumProperties(string inNum)
        {
            try
            {
                numProperties = Convert.ToInt32(inNum);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Error in processing number of properties for neighbourhood {0}", neighbourhoodName));
            }
        }
        public int GetNumProperties()
        {
            return numProperties;
        }

        //Get & set a property
        public void SetProperty(int index, Property inProp)
        {
            try
            {
                properties[index] = inProp;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Could not set property {0} in neighbourhood {1}, out of space", inProp.GetPropertyName(), neighbourhoodName));
            }
        }
    }
}
